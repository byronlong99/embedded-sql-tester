using System.Text;
using TSQL.Clauses;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors
{
    internal class GeneralClauseConverter : ClauseConverterBase
    {
        public GeneralClauseConverter(TSQLClause clause) : base(clause)
        {
        }

        protected override TokenResult ProcessToken(int position)
        {
            var token = Tokens[position].Text;

            var result = new TokenResult {Text = token, NewPosition = position};

            if (token.ToLower() == "dateadd")
                result = HandleDateAdd(position);
            else if (token.ToLower() == "varchar")
                result = new TokenResult {Text = "text", NewPosition = position};

            return result;
        }

        private TokenResult HandleDateAdd(int position)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append("DATETIME(");
            stringBuilder.Append(Tokens[position + 6].Text);
            stringBuilder.Append(", ");
            stringBuilder.Append(Tokens[position + 4].Text);
            stringBuilder.Append(" ||' ");
            stringBuilder.Append($"{Tokens[position + 2].Text}s");
            stringBuilder.Append("')");
            position += 7;
            return new TokenResult {Text = stringBuilder.ToString(), NewPosition = position};
        }
    }
}