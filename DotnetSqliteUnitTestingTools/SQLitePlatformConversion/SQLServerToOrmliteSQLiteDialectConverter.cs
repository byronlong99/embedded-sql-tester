using System.Collections.Generic;
using System.Text;
using DotnetSqliteUnittestingTools.SQLitePlatformConversion.ClauseProcessors;
using TSQL;
using TSQL.Statements;

namespace DotnetSqliteUnittestingTools.SQLitePlatformConversion
{
    public class SQLServerToOrmliteSQLiteDialectConverter
    {
        public static bool ConvertToOrmliteSQLiteDialect { get; set; }

        private List<ClauseProcessor> _clauseConverters;
        
        public string ConvertToOrmliteSQLiteSQL(string sqlInput)
        {            
            var statements = TSQLStatementReader.ParseStatements(sqlInput);
            var statement = (TSQLSelectStatement) statements[0];

            _clauseConverters = new List<ClauseProcessor>
            {
                new GeneralClauseConverter(statement.Select),
                new FromClauseConverter(statement.From),
                new WhereClauseConverter(statement.Where),
                new GeneralClauseConverter(statement.GroupBy),
                new OrderByClauseConverter(statement.OrderBy)
            };

            var sb = new StringBuilder();

            foreach (var clauseConverter in _clauseConverters)
                sb.Append(clauseConverter.Convert());

            return sb.ToString();           
        }       
    }
}