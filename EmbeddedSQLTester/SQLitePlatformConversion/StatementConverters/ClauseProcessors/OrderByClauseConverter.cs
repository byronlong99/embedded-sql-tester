using System.Text;
using TSQL.Clauses;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors
{
    internal class OrderByClauseConverter : ClauseConverterBase
    {
        public OrderByClauseConverter(TSQLClause clauseParameter) : base(clauseParameter)
        {
        }

        protected override TokenResult ProcessToken(int position)
        {
            var token = Tokens[position].Text;

            var result = new TokenResult {Text = token, NewPosition = position};

            if (token.ToLower() == "offset")
                result = HandleOffset(position);

            return result;
        }

        private TokenResult HandleOffset(int position)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("LIMIT (");
            stringBuilder.Append(Tokens[position + 11].Text);
            stringBuilder.Append(") OFFSET ");
            stringBuilder.Append(Tokens[position + 2].Text);
            stringBuilder.Append(" -1");            
            position += 13;
            return new TokenResult {Text = stringBuilder.ToString(), NewPosition = position};
        }
    }
}