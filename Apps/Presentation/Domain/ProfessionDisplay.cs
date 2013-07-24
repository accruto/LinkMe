using System.Collections.Generic;
using LinkMe.Domain;
using System.Linq;
using System;

namespace LinkMe.Apps.Presentation.Domain
{
    public static class ProfessionDisplay
    {
        private static readonly IDictionary<Profession, string> Texts = new Dictionary<Profession, string>
        {
            {Profession.Accounting, "Accounting/Auditing"},
            {Profession.Artistic, "Artistic/Creative"},
            {Profession.BusinessDevelopment, "Business development"},
            {Profession.CustomerService, "Customer service"},
            {Profession.Editing, "Editing/Writing"},
            {Profession.GeneralBusiness, "General Business"},
            {Profession.HealthCare, "Health care"},
            {Profession.HumanResources, "Human resources"},
            {Profession.InformationTechnology, "Information technology"},
            {Profession.ProductManagement, "Product management"},
            {Profession.ProjectManagement, "Project management"},
            {Profession.PublicRelations, "Public relations"},
            {Profession.QualityAssurance, "Quality assurance"},
            {Profession.Strategy, "Strategy/Planning"},
            {Profession.SupplyChain, "Supply chain"},
        };

        public static Profession[] Values = Texts.Keys.ToArray();

        public static string GetDisplayText(this Profession? profession)
        {
            if (profession == null)
                return null;
            string text;
            return Texts.TryGetValue(profession.Value, out text) ? text : Enum.GetName(typeof(Profession), profession.Value);
        }
    }
}
