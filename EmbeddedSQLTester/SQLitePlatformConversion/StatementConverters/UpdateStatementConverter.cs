using System.Collections.Generic;
using EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors;
using TSQL.Statements;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters
{
    internal class UpdateStatementConverter
    {
        public List<ConverterBase> GetClauseProcessorList(TSQLUpdateStatement statement)
        {
            return new List<ConverterBase>
            {
                new UpdateClauseConverter(statement.Update),
                new GeneralClauseConverter(statement.Set),
                new FromClauseConverter(statement.From),
                new WhereClauseConverter(statement.Where),
            };
        }
    }
}