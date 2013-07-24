using LinkMe.Apps.Agents.Security.Commands;

namespace LinkMe.Apps.Agents.Users.Sessions.Data
{
    internal static class Mappings
    {
        private enum ActivityType
        {
            Login,
            Logout,
            SessionEnd
        }

        public static UserLoginEntity Map(this UserLogin login)
        {
            var entity = login.Map(ActivityType.Login);
            entity.authenticationStatus = (int?) login.AuthenticationStatus;
            return entity;
        }

        public static UserLoginEntity Map(this UserLogout logout)
        {
            return logout.Map(ActivityType.Logout);
        }

        public static UserLoginEntity Map(this UserSessionEnd sessionEnd)
        {
            return sessionEnd.Map(ActivityType.SessionEnd);
        }

        public static UserSessionActivity Map(this UserLoginEntity entity)
        {
            switch ((ActivityType)entity.activityType)
            {
                case ActivityType.Login:
                    var login = entity.Map<UserLogin>();
                    login.AuthenticationStatus = (AuthenticationStatus) entity.authenticationStatus;
                    return login;

                case ActivityType.Logout:
                    return entity.Map<UserLogout>();

                default:
                    return entity.Map<UserSessionEnd>();
            }
        }

        private static TActivity Map<TActivity>(this UserLoginEntity entity)
            where TActivity : UserSessionActivity, new()
        {
            return new TActivity
            {
                Id = entity.id,
                UserId = entity.userId,
                Time = entity.time,
                IpAddress = entity.ipAddress,
                SessionId = entity.sessionId
            };
        }

        private static UserLoginEntity Map(this UserSessionActivity activity, ActivityType activityType)
        {
            return new UserLoginEntity
            {
                activityType = (int)activityType,
                id = activity.Id,
                userId = activity.UserId,
                time = activity.Time,
                ipAddress = activity.IpAddress,
                sessionId = activity.SessionId,
            };
        }
    }
}
