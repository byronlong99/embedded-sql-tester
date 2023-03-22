using System.Collections.Generic;
using System.Text;
using TSQL.Clauses;
using TSQL.Tokens;

namespace EmbeddedSQLTester.SQLitePlatformConversion.StatementConverters.ClauseProcessors
{
    internal abstract class ClauseConverterBase : ConverterBase
    {
        private readonly TSQLClause _clause;
        private StringBuilder _stringBuilder;
        protected List<TSQLToken> Tokens;

        protected ClauseConverterBase(TSQLClause clauseParameter)
        {
            _clause = clauseParameter;
        }

        public override string Convert()
        {
            _stringBuilder = new StringBuilder();

            if (_clause != null)
            {
                Tokens = _clause.Tokens;
                ConvertHelper();
            }

            return _stringBuilder.ToString();
        }

        private void ConvertHelper()
        {
            var lastEndPosition = 0;

            for (int i = 0; i < Tokens.Count; i++)
            {
                var result = ProcessToken(i);

                i = result.NewPosition;
                
                if (Tokens[i].BeginPosition - lastEndPosition > 1 && !string.IsNullOrEmpty(result.Text))
                    _stringBuilder.Append(" ");
                
                _stringBuilder.Append(result.Text);

                lastEndPosition = Tokens[i].EndPosition;
            }
        }

        protected abstract TokenResult ProcessToken(int position);
    }
}