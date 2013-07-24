using System;
using System.Collections;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

namespace LinkMe.Framework.SqlServer
{
    public class UserDefinedFunctions
    {
        [SqlFunction(Name = "SplitGuids", FillRowMethodName = "FillGuidRow", TableDefinition = "value UNIQUEIDENTIFIER")]
        public static IEnumerable SplitGuids(SqlString delimiter, SqlString input)
        {
            foreach (var guid in input.Value.Split(new [] {delimiter.Value[0]}, StringSplitOptions.RemoveEmptyEntries))
                yield return guid;
        }

        [SqlFunction(Name = "SplitInts", FillRowMethodName = "FillIntRow", TableDefinition = "value INT")]
        public static IEnumerable SplitInts(SqlString delimiter, SqlString input)
        {
            foreach (var i in input.Value.Split(new[] { delimiter.Value[0] }, StringSplitOptions.RemoveEmptyEntries))
                yield return i;
        }

        public static void FillGuidRow(object row, out SqlGuid value)
        {
            value = new SqlGuid((string)row);
        }

        public static void FillIntRow(object row, out SqlInt32 value)
        {
            value = SqlInt32.Parse((string)row);
        }
    }
}
