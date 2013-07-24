using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using LinkMe.Domain.Contacts;
using LinkMe.Framework.Utility.Sql;
using LinkMe.Framework.Utility.Unity;
using LinkMe.Utility;
using LinkMe.Utility.Utilities;

namespace LinkMe.Web.Service
{
    public class GetProfessionalTitles : AutoSuggestWebServiceHandler
    {
        private readonly IDbConnectionFactory _connectionFactory = Container.Current.Resolve<IDbConnectionFactory>();

        public const string ProfessionalTitleParameter = "txtProfessionalTitle";

        private const int maxRowsLimit = 10;
        private const int commandTimeout = 5;

        protected override string MaxResultsParam
        {
            get { return "limitBy"; }
        }

        protected override IList<string> GetSuggestionList(HttpContext context, int maxResults)
        {
            int rowsLimit = Math.Min(maxResults, maxRowsLimit);

            string userQuery = context.Request.QueryString[ProfessionalTitleParameter];
            if (string.IsNullOrEmpty(userQuery))
                throw new ServiceEndUserException("A string to search for must be specified.");

            userQuery = StringUtils.EscapeSqlLike(userQuery, false);
            List<string> res;
            using (IDbConnection connection = _connectionFactory.CreateConnection())
            {
                connection.Open();
                res = GetSearchResults(connection, rowsLimit, userQuery + "%");
                if(res.Count== 0) {
                    // No data - try another query.
                    res = GetSearchResults(connection, rowsLimit, "%" + userQuery + "%");
                }
            }

            return res;
        }

        private static List<string> GetSearchResults(IDbConnection connection, int rowsLimit, string searchTerm)
        {
            List<string> results = new List<string>();

            string commandText = "select distinct top " + rowsLimit + " searchTerm from dbo.EquivalentTerms " +
                "where searchTerm like @searchPattern order by 1 desc";

            using (IDbCommand command = DatabaseHelper.CreateTextCommand(connection, commandText,
                commandTimeout, null))
            {
                DatabaseHelper.AddParameter(command, "@searchPattern", DbType.AnsiString, searchTerm);
                using (IDataReader rd = command.ExecuteReader())
                    while (rd.Read())
                        results.Add(rd.GetString(0));
            }

            return results;
        }

        protected override UserType[] AuthorizedUserTypes
        {
            get { return null; }
        }
    }
}
