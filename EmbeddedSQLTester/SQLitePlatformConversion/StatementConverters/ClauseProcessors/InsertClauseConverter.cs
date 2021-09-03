using System.Text;
using TSQL.Clauses;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors
{
    internal class InsertClauseConverter : ClauseConverterBase
    {
        public InsertClauseConverter(TSQLClause clause) : base(clause)
        {
        }

        protected override TokenResult ProcessToken(int position)
        {
            var token = Tokens[position].Text;

            token = token.Replace("[", string.Empty).Replace("]", string.Empty);
            token = token.Replace('.', '_');

            var result = new TokenResult {Text = token, NewPosition = position};

            return result;
        }
    }
}