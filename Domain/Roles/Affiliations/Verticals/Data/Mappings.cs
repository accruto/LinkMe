namespace LinkMe.Domain.Roles.Affiliations.Verticals.Data
{
    internal static class Mappings
    {
        public static Vertical Map(this VerticalEntity entity)
        {
            return new Vertical
            {
                Id = entity.id,
                Name = entity.name,
                IsDeleted = !entity.enabled,
                Url = entity.url,
                Host = entity.host,
                SecondaryHost = entity.secondaryHost,
                TertiaryHost = entity.tertiaryHost,
                CountryId = entity.countryId,
                RequiresExternalLogin = entity.requiresExternalLogin,
                ExternalLoginUrl = entity.externalLoginUrl,
                ExternalCookieDomain = entity.externalCookieDomain,
                EmailDisplayName = entity.emailDisplayName,
                ReturnEmailAddress = entity.returnEmailAddress,
                MemberServicesEmailAddress = entity.memberServicesEmailAddress,
                EmployerServicesEmailAddress = entity.employerServicesEmailAddress
           };
        }

        public static VerticalEntity Map(this Vertical vertical)
        {
            var entity = new VerticalEntity {id = vertical.Id};
            vertical.MapTo(entity);
            return entity;
        }

        public static void MapTo(this Vertical vertical, VerticalEntity entity)
        {
            entity.name = vertical.Name;
            entity.url = vertical.Url;
            entity.enabled = !vertical.IsDeleted;
            entity.host = vertical.Host;
            entity.secondaryHost = vertical.SecondaryHost;
            entity.tertiaryHost = vertical.TertiaryHost;
            entity.countryId = vertical.CountryId;
            entity.requiresExternalLogin = vertical.RequiresExternalLogin;
            entity.externalLoginUrl = vertical.ExternalLoginUrl;
            entity.externalCookieDomain = vertical.ExternalCookieDomain;
            entity.emailDisplayName = vertical.EmailDisplayName;
            entity.returnEmailAddress = vertical.ReturnEmailAddress;
            entity.memberServicesEmailAddress = vertical.MemberServicesEmailAddress;
            entity.employerServicesEmailAddress = vertical.EmployerServicesEmailAddress;
        }
    }
}
