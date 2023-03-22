using System.Collections.Generic;
using EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors;
using EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ExpressionProcessors;
using TSQL.Statements;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters
{
    internal class InsertStatementConverter
    {
        public List<ConverterBase> GetClauseProcessorList(TSQLInsertStatement statement)
        {
            return new List<ConverterBase>
            {
                new InsertClauseConverter(statement.Insert),
                new ValuesExpressionConverter(statement.Values)
            };
        }
    }
}