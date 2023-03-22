using System.Collections.Generic;
using System.Text;
using EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors;
using TSQL.Elements;
using TSQL.Expressions;
using TSQL.Tokens;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ExpressionProcessors
{
    internal abstract class ExpressionConverterBase : ConverterBase
    {
        private readonly TSQLValues _expression;
        private StringBuilder _stringBuilder;
        protected List<TSQLToken> Tokens;
        
        protected ExpressionConverterBase(TSQLValues expression)
        {
            _expression = expression;
        }
        
        public override string Convert()
        {
            _stringBuilder = new StringBuilder();

            if (_expression != null)
            {
                Tokens = _expression.Tokens;
                ConvertHelper();
            }

            return _stringBuilder.ToString();
        }
        
        private void ConvertHelper()
        {
            var lastEndPosition = 0;

            for (int i = 0; i < Tokens.Count; i++)
            {
                if (Tokens[i].BeginPosition - lastEndPosition > 1)
                    _stringBuilder.Append(" ");

                var result = ProcessToken(i);

                i = result.NewPosition;
                
                _stringBuilder.Append(result.Text);

                lastEndPosition = Tokens[i].EndPosition;
            }
        }
        
        protected abstract TokenResult ProcessToken(int position);
    }
}