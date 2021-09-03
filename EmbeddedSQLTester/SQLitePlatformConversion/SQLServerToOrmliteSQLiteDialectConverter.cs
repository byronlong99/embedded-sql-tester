﻿using System.Collections.Generic;
using System.Text;
using EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters;
using EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors;
using TSQL;
using TSQL.Statements;

namespace EmbeddedSQLTester.SQLitePlatformConversion
{
    public class SQLServerToOrmliteSQLiteDialectConverter
    {
        public static bool ConvertToOrmliteSQLiteDialect { get; set; }
        private List<ConverterBase> _clauseConverters;
        
        public string ConvertToOrmliteSQLiteSQL(string sqlInput)
        {            
            var statements = TSQLStatementReader.ParseStatements(sqlInput);

            var statement = statements[0];

            if (statement.GetType() == typeof(TSQLSelectStatement))
            {
                var selectStatement = (TSQLSelectStatement)statement;
                var converter = new SelectStatementConverter();
                _clauseConverters = converter.GetClauseProcessorList(selectStatement);
            }
            else if (statement.GetType() == typeof(TSQLUpdateStatement))
            {
                var updateStatement = (TSQLUpdateStatement)statement;
                var converter = new UpdateStatementConverter();
                _clauseConverters = converter.GetClauseProcessorList(updateStatement);
            }
            else if (statement.GetType() == typeof(TSQLInsertStatement))
            {
                var updateStatement = (TSQLInsertStatement)statement;
                var converter = new InsertStatementConverter();
                _clauseConverters = converter.GetClauseProcessorList(updateStatement);
            }

            var sb = new StringBuilder();

            foreach (var clauseConverter in _clauseConverters)
                sb.Append(clauseConverter.Convert());

            return sb.ToString();           
        }       
    }
}