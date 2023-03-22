using EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors;
using TSQL.Clauses;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters
{
    internal class LimitStatementConverter : ClauseConverterBase
    {
        public LimitStatementConverter(TSQLClause clauseParameter) : base(clauseParameter)
        {
        }

        protected override TokenResult ProcessToken(int position)
        {
            var token = Tokens[position].Text;

            TokenResult result;
            
            if (token.ToLower() == "top")
            {
                result = new TokenResult {Text = $"LIMIT {Tokens[position+1].Text}", NewPosition = position+1};
            }
            else
            {
                result = new TokenResult {Text = string.Empty, NewPosition = position};   
            }

            return result;
        }
    }
}