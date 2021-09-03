using TSQL.Clauses;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors
{
    internal class UpdateClauseConverter : ClauseProcessor
    {
        public UpdateClauseConverter(TSQLClause clauseParameter) : base(clauseParameter)
        {
        }

        protected override TokenResult ProcessToken(int position)
        {
            var token = Tokens[position].Text;

            token = token.Replace('.', '_');
            
            token = token.Replace("[", string.Empty).Replace("]", string.Empty);

            return new TokenResult { Text = token, NewPosition = position };
        }
    }
}