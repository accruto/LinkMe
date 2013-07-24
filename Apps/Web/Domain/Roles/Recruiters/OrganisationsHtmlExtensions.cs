using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;
using LinkMe.Domain.Contacts;
using LinkMe.Domain.Roles.Recruiters;

namespace LinkMe.Web.Domain.Roles.Recruiters
{
    public static class OrganisationsExtensions
    {
        public static string OrganisationHierarchy(this HtmlHelper htmlHelper, OrganisationHierarchy organisationHierarchy, Organisation currentOrganisation, Func<Organisation, MvcHtmlString> getLink)
        {
            var ulTagBuilder = new TagBuilder("ul") { InnerHtml = htmlHelper.GetOrganisationHierarchyHtml(organisationHierarchy, currentOrganisation, getLink) };
            return ulTagBuilder.ToString(TagRenderMode.Normal);
        }

        private static string GetOrganisationHierarchyHtml(this HtmlHelper htmlHelper, OrganisationHierarchy organisationHierarchy, Organisation currentOrganisation, Func<Organisation, MvcHtmlString> getLink)
        {
            var sb = new StringBuilder();

            // Add the list item for this organisaton.

            var liTagBuilder = new TagBuilder("li")
            {
                InnerHtml = htmlHelper.GetOrganisationHtml(organisationHierarchy.Organisation, currentOrganisation, getLink)
            };
            sb.AppendLine(liTagBuilder.ToString(TagRenderMode.Normal));

            // Add any children.

            sb.Append(htmlHelper.GetChildOrganisationHierarchiesHtml(organisationHierarchy.ChildOrganisationHierarchies, currentOrganisation, getLink));
            return sb.ToString();
        }

        private static string GetOrganisationHtml(this HtmlHelper htmlHelper, Organisation organisation, IOrganisation currentOrganisation, Func<Organisation, MvcHtmlString> getLink)
        {
            if (organisation.Id == currentOrganisation.Id)
                return htmlHelper.GetOrganisationHtml(organisation);

            // Generate a link.

            return getLink(organisation).ToString();
        }

        private static string GetOrganisationHtml(this HtmlHelper htmlHelper, IOrganisation organisation)
        {
            var tagBuilder = new TagBuilder("strong") { InnerHtml = htmlHelper.Encode(organisation.Name) };
            return tagBuilder.ToString(TagRenderMode.Normal);
        }

        private static string GetChildOrganisationHierarchiesHtml(this HtmlHelper htmlHelper, ICollection<OrganisationHierarchy> childOrganisationHierarchies, Organisation currentOrganisation, Func<Organisation, MvcHtmlString> getLink)
        {
            if (childOrganisationHierarchies.Count == 0)
                return string.Empty;

            var ulTagBuilder = new TagBuilder("ul");

            var sb = new StringBuilder();
            foreach (var childOrganisationHierarchy in childOrganisationHierarchies)
                sb.Append(htmlHelper.GetOrganisationHierarchyHtml(childOrganisationHierarchy, currentOrganisation, getLink));

            ulTagBuilder.InnerHtml = sb.ToString();
            return ulTagBuilder.ToString(TagRenderMode.Normal);
        }
    }
}