using System.Collections.Generic;
using EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors;
using TSQL.Statements;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters
{
    internal class SelectStatementConverter
    {
        public List<ConverterBase> GetClauseProcessorList(TSQLSelectStatement statement)
        {
            return new List<ConverterBase>
            {
                new GeneralClauseConverter(statement.Select),
                new FromClauseConverter(statement.From),
                new WhereClauseConverter(statement.Where),
                new GeneralClauseConverter(statement.GroupBy),
                new OrderByClauseConverter(statement.OrderBy),
                new LimitStatementConverter(statement.Select)
            };
        }
    }
}