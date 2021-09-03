using System.Collections.Generic;
using EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors;
using TSQL.Statements;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters
{
    internal class UpdateStatementConverter
    {
        public List<ClauseProcessor> GetClauseProcessorList(TSQLUpdateStatement statement)
        {
            return new List<ClauseProcessor>
            {
                new UpdateClauseConverter(statement.Update),
                new GeneralClauseConverter(statement.Set),
                new FromClauseConverter(statement.From),
                new WhereClauseConverter(statement.Where),
            };
        }
    }
}