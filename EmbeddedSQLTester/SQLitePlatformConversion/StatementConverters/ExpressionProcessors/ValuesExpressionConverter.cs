using EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors;
using TSQL.Expressions;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ExpressionProcessors
{
    internal class ValuesExpressionConverter : ExpressionConverterBase
    {
        public ValuesExpressionConverter(TSQLExpression clause) : base(clause)
        {
        }

        protected override TokenResult ProcessToken(int position)
        {
            var token = Tokens[position].Text;

            return new TokenResult { Text = token, NewPosition = position };
        }
    }
}