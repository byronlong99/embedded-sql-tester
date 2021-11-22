using System.Collections.Generic;
using EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors;
using TSQL.Statements;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters
{
    internal class DeleteStatementConverter
    {
        public List<ConverterBase> GetClauseProcessorList(TSQLDeleteStatement statement)
        {
            return new List<ConverterBase>
            {
                new GeneralClauseConverter(statement.Delete),
                new FromClauseConverter(statement.From),
                new WhereClauseConverter(statement.Where),
            };
        }
    }
}