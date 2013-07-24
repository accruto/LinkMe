using System.Collections.Generic;
using System.Data;
using System.Linq;
using LinkMe.Domain;
using LinkMe.Domain.Data;
using LinkMe.Query.Reports.Data;

namespace LinkMe.Query.Reports.Roles.Communications.Data
{
    public class CommunicationsRepository
        : ReportsRepository<CommunicationsDataContext>, ICommunicationsRepository
    {
        public CommunicationsRepository(IDataContextFactory dataContextFactory)
            : base(dataContextFactory)
        {
        }

        IDictionary<string, CommunicationReport> ICommunicationsRepository.GetCommunicationReport(DayRange day)
        {
            using (var dc = CreateDataContext(true))
            {
                var startTicks = day.Start.Value.Ticks;
                var endTicks = day.End.Value.Ticks;

                return (from d in (from d in (from c in dc.TrackingCommunicationEntities where !Equals(c.definition, null) select c.definition).Distinct() select d)
                        select new
                        {
                            Definition = d,
                            Sent = (from c in dc.TrackingCommunicationEntities
                                    where c.definition == d
                                    && c.sent >= startTicks && c.sent < endTicks
                                    select c).Count(),
                            Opened = (from c in dc.TrackingCommunicationEntities
                                      where c.definition == d
                                      && c.sent >= startTicks && c.sent < endTicks
                                      &&
                                      (
                                        (from o in dc.TrackingCommunicationOpenedEntities where o.id == c.id select o).Any()
                                        ||
                                        (from l in dc.TrackingCommunicationLinkEntities
                                         join lc in dc.TrackingCommunicationLinkClickedEntities on l.id equals lc.id
                                         where l.communicationId == c.id
                                         select l).Any()
                                      )
                                      select c).Count(),
                            Clicked = (from c in dc.TrackingCommunicationEntities
                                       join l in dc.TrackingCommunicationLinkEntities on c.id equals l.communicationId
                                       join lc in dc.TrackingCommunicationLinkClickedEntities on l.id equals lc.id
                                       where c.definition == d
                                       && c.sent >= startTicks && c.sent < endTicks
                                       select c).Count(),
                        }).ToDictionary(x => x.Definition, x => new CommunicationReport { Sent = x.Sent, Opened = x.Opened, LinksClicked = x.Clicked });
            }
        }

        protected override CommunicationsDataContext CreateDataContext(IDbConnection connection)
        {
            return new CommunicationsDataContext(connection);
        }
    }
}