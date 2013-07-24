namespace LinkMe.Domain.Roles.Affiliations.Partners.Data
{
    internal static class Mappings
    {
        public static ServicePartnerEntity Map(this Partner partner)
        {
            return new ServicePartnerEntity
            {
                id = partner.Id,
                name = partner.Name
            };
        }

        public static Partner Map(this ServicePartnerEntity entity)
        {
            return new Partner
            {
                Id = entity.id,
                Name = entity.name
            };
        }
    }
}
