using LinkMe.Domain.Contacts;

namespace LinkMe.Apps.Presentation.Domain.Roles.Representatives
{
    public static class RepresentativesExtensions
    {
        public static string GetRepresentativeDescriptionText(this PersonalContactDegree contactDegree)
        {
            switch (contactDegree)
            {
                case PersonalContactDegree.Representative:
                    return "My nominated representative";

                case PersonalContactDegree.Representee:
                    return "Represented by me";

                default:
                    return null;
            }
        }

        public static string GetRepresentativeDescriptionText(this PersonalContactDegree contactDegree, string name)
        {
            switch (contactDegree)
            {
                case PersonalContactDegree.Representative:
                    return name + " is my nominated representative";

                case PersonalContactDegree.Representee:
                    return name + " is represented by me";

                default:
                    return null;
            }
        }
    }
}
