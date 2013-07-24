using System;
using System.Collections.Generic;
using System.Linq;
using LinkMe.Domain.Channels;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Credits;
using LinkMe.Domain.Roles.Contenders;
using LinkMe.Domain.Users.Employers.Credits.Commands;
using LinkMe.Domain.Users.Members.Views;
using LinkMe.Framework.Utility;
using LinkMe.Framework.Utility.Preparation;
using LinkMe.Framework.Utility.Validation;

namespace LinkMe.Domain.Users.Employers.Views.Commands
{
    public class EmployerMemberViewsCommand
        : IEmployerMemberViewsCommand
    {
        private readonly IEmployerViewsRepository _repository;
        private readonly IEmployerCreditsCommand _employerCreditsCommand;
        private readonly int _dailyLimit;
        private readonly int _weeklyLimit;
        private readonly int _bulkDailyLimit;
        private readonly int _bulkWeeklyLimit;
        private readonly int _bulkUnlockDailyLimit;
        private readonly int _bulkUnlockWeeklyLimit;

        public EmployerMemberViewsCommand(IEmployerViewsRepository repository, IEmployerCreditsCommand employerCreditsCommand, int dailyLimit, int weeklyLimit, int bulkDailyLimit, int bulkWeeklyLimit, int bulkUnlockDailyLimit, int bulkUnlockWeeklyLimit)
        {
            _repository = repository;
            _employerCreditsCommand = employerCreditsCommand;
            _dailyLimit = dailyLimit;
            _weeklyLimit = weeklyLimit;
            _bulkDailyLimit = bulkDailyLimit;
            _bulkWeeklyLimit = bulkWeeklyLimit;
            _bulkUnlockDailyLimit = bulkUnlockDailyLimit;
            _bulkUnlockWeeklyLimit = bulkUnlockWeeklyLimit;
        }

        void IEmployerMemberViewsCommand.ViewMember(ChannelApp app, IEmployer employer, IMember member)
        {
            CreateMemberViewing(app, employer, member, null);
        }

        void IEmployerMemberViewsCommand.ViewMember(ChannelApp app, IEmployer employer, IMember member, InternalApplication application)
        {
            CreateMemberViewing(app, employer, member, application);
        }

        MemberAccess IEmployerMemberViewsCommand.AccessMember(ChannelApp app, IEmployer employer, ProfessionalView view, MemberAccessReason reason)
        {
            return AccessMember(app, employer, view, reason);
        }

        void IEmployerMemberViewsCommand.CheckCanAccessMember(ChannelApp app, IEmployer employer, ProfessionalView view, MemberAccessReason reason)
        {
            CheckCanAccessMember(employer, view, reason);
        }

        IList<MemberAccess> IEmployerMemberViewsCommand.AccessMembers(ChannelApp app, IEmployer employer, ProfessionalViews views, MemberAccessReason reason)
        {
            return AccessMembers(app, employer, views, reason);
        }

        void IEmployerMemberViewsCommand.CheckCanAccessMembers(ChannelApp app, IEmployer employer, ProfessionalViews views, MemberAccessReason reason)
        {
            CheckCanAccessMembers(employer, views, reason);
        }

        private void CreateMemberViewing(ChannelApp app, IHasId<Guid> employer, IHasId<Guid> member, Application application)
        {
            if (member == null)
                return;

            var viewing = new MemberViewing
            {
                EmployerId = employer == null ? (Guid?)null : employer.Id,
                MemberId = member.Id,
                JobAdId = application == null ? (Guid?)null : application.PositionId,
                ChannelId = app.ChannelId,
                AppId = app.Id,
            };

            viewing.Prepare();
            viewing.Validate();
            _repository.CreateMemberViewing(viewing);
        }

        private IList<MemberAccess> AccessMembers(ChannelApp app, IEmployer employer, ProfessionalViews views, MemberAccessReason reason)
        {
            switch (views.Count())
            {
                case 0:
                    return new List<MemberAccess>();

                case 1:
                    return new List<MemberAccess> { AccessMember(app, employer, views.First(), reason) };

                default:
                    return AccessBulkMembers(app, employer, views, reason);
            }
        }

        private void CheckCanAccessMembers(IEmployer employer, ProfessionalViews views, MemberAccessReason reason)
        {
            switch (views.Count())
            {
                case 0:
                    break;

                case 1:
                    CheckCanAccessMember(employer, views.First(), reason);
                    break;

                default:
                    CheckCanAccessBulkMembers(employer, views, reason);
                    break;
            }
        }

        private MemberAccess AccessMember(ChannelApp app, IEmployer employer, ProfessionalView view, MemberAccessReason reason)
        {
            // An anonymous employer means insufficient credits.

            if (employer == null)
                throw new InsufficientCreditsException { Available = 0, Required = 1 };

            // Make sure the details are visible.

            CheckVisibility(view, reason);

            // Check against limits.

            CheckAccessLimit(employer.Id, view.Id, reason);

            // Exercise the credit.

            var exercisedCreditId = _employerCreditsCommand.ExerciseContactCredit(employer, view);

            // Record the access.

            return CreateMemberAccess(app, employer.Id, view.Id, reason, exercisedCreditId);
        }

        private void CheckCanAccessMember(IEmployer employer, ProfessionalView view, MemberAccessReason reason)
        {
            // An anonymous employer means insufficient credits.

            if (employer == null)
                throw new InsufficientCreditsException { Available = 0, Required = 1 };

            // Make sure the details are visible.

            CheckVisibility(view, reason);

            // Check against limits.

            CheckAccessLimit(employer.Id, view.Id, reason);

            // Check the credit.

            _employerCreditsCommand.CheckCanExerciseContactCredit(employer, view);
        }

        private IList<MemberAccess> AccessBulkMembers(ChannelApp app, IEmployer employer, ProfessionalViews views, MemberAccessReason reason)
        {
            if (employer == null)
                throw new InsufficientCreditsException { Available = 0, Required = views.Count() };

            // Make sure the details are visible.

            CheckVisibility(views, reason);

            // Check against limits.

            CheckAccessLimit(employer.Id, from v in views select v.Id, reason);

            // Exercise the credits.

            var exercisedCreditIds = _employerCreditsCommand.ExerciseContactCredits(employer, views);

            // Record the accesses.

            return CreateMemberAccesses(app, employer.Id, from v in views select v.Id, reason, exercisedCreditIds);
        }

        private void CheckCanAccessBulkMembers(IEmployer employer, ProfessionalViews views, MemberAccessReason reason)
        {
            if (employer == null)
                throw new InsufficientCreditsException { Available = 0, Required = views.Count() };

            // Make sure the details are visible.

            CheckVisibility(views, reason);

            // Check against limits.

            CheckAccessLimit(employer.Id, from v in views select v.Id, reason);

            // Check the credits.

            _employerCreditsCommand.CheckCanExerciseContactCredits(employer, views);
        }

        private static void CheckVisibility(IEnumerable<ProfessionalView> views, MemberAccessReason reason)
        {
            foreach (var view in views)
                CheckVisibility(view, reason);
        }

        private static void CheckVisibility(ProfessionalView view, MemberAccessReason reason)
        {
            // This check is here is to ensure that the member does in fact have something the employer wants to see
            // and that it is visible, even after exercising a credit.

            switch (reason)
            {
                case MemberAccessReason.PhoneNumberViewed:

                    // Must have a phone number.

                    if (!view.HasPhoneNumbers
                        || !view.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume)
                        || !view.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.PhoneNumbers))
                        throw new NotVisibleException();
                    break;

                default:

                    // Must have a visible resume.  Really should be checking for view.Resume != null but that means
                    // loading a lot of information from the database for a rare case.  It is basically assumed
                    // that if it gets to here the member has a resume.

                    if (!view.VisibilitySettings.Professional.EmploymentVisibility.IsFlagSet(ProfessionalVisibility.Resume))
                        throw new NotVisibleException();
                    break;
            }
        }

        private void CheckAccessLimit(Guid employerId, Guid memberId, MemberAccessReason reason)
        {
            switch (reason)
            {
                case MemberAccessReason.ResumeDownloaded:
                case MemberAccessReason.ResumeSent:
                    CheckAccessLimit(employerId, memberId, _dailyLimit, _weeklyLimit, MemberAccessReason.ResumeDownloaded, MemberAccessReason.ResumeSent);
                    break;

                case MemberAccessReason.Unlock:
                case MemberAccessReason.PhoneNumberViewed:
                    CheckAccessLimit(employerId, memberId, _dailyLimit, _weeklyLimit, MemberAccessReason.Unlock, MemberAccessReason.PhoneNumberViewed);
                    break;

                default:
                    CheckAccessLimit(employerId, memberId, _dailyLimit, _weeklyLimit, reason);
                    break;
            }
        }

        private void CheckAccessLimit(Guid employerId, IEnumerable<Guid> memberIds, MemberAccessReason reason)
        {
            switch (reason)
            {
                case MemberAccessReason.ResumeDownloaded:
                case MemberAccessReason.ResumeSent:
                    CheckAccessLimit(employerId, memberIds, _bulkDailyLimit, _bulkWeeklyLimit, MemberAccessReason.ResumeDownloaded, MemberAccessReason.ResumeSent);
                    break;

                case MemberAccessReason.Unlock:
                case MemberAccessReason.PhoneNumberViewed:
                    CheckAccessLimit(employerId, memberIds, _bulkUnlockDailyLimit, _bulkUnlockWeeklyLimit, MemberAccessReason.Unlock, MemberAccessReason.PhoneNumberViewed);
                    break;

                default:
                    CheckAccessLimit(employerId, memberIds, _bulkDailyLimit, _bulkWeeklyLimit, reason);
                    break;
            }
        }

        private void CheckAccessLimit(Guid employerId, Guid memberId, int dailyLimit, int weeklyLimit, params MemberAccessReason[] reasons)
        {
            var counts = _repository.GetMemberAccessCounts(employerId, memberId, reasons);

            // Check whether the current count + the new member puts them over the limit.

            if (counts.Item1 + 1 > dailyLimit || counts.Item2 + 1 > weeklyLimit)
                throw new TooManyAccessesException();
        }

        private void CheckAccessLimit(Guid employerId, IEnumerable<Guid> memberIds, int dailyLimit, int weeklyLimit, params MemberAccessReason[] reasons)
        {
            var counts = _repository.GetMemberAccessCounts(employerId, memberIds, reasons);

            // Check whether the current count + the new members puts them over the limit.

            var count = memberIds.Count();
            if (counts.Item1 + count > dailyLimit || counts.Item2 + count > weeklyLimit)
                throw new TooManyAccessesException();
        }

        private MemberAccess CreateMemberAccess(ChannelApp app, Guid employerId, Guid memberId, MemberAccessReason reason, Guid? exercisedCreditId)
        {
            var access = new MemberAccess
            {
                Reason = reason,
                EmployerId = employerId,
                MemberId = memberId,
                ExercisedCreditId = exercisedCreditId,
                ChannelId = app.ChannelId,
                AppId = app.Id,
            };

            access.Prepare();
            access.Validate();
            _repository.CreateMemberAccess(access);
            return access;
        }

        private IList<MemberAccess> CreateMemberAccesses(ChannelApp app, Guid employerId, IEnumerable<Guid> memberIds, MemberAccessReason reason, IDictionary<Guid, Guid?> exercisedCreditIds)
        {
            var accesses = new List<MemberAccess>();
            foreach (var memberId in memberIds)
            {
                var access = new MemberAccess
                {
                    Reason = reason,
                    EmployerId = employerId,
                    MemberId = memberId,
                    ExercisedCreditId = exercisedCreditIds[memberId],
                    ChannelId = app.ChannelId,
                    AppId = app.Id,
                };
                access.Prepare();
                access.Validate();
                accesses.Add(access);
            }

            _repository.CreateMemberAccesses(accesses);
            return accesses;
        }
    }
}