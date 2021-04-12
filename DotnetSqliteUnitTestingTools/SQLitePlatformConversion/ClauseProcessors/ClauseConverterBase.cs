using System.Collections.Generic;
using System.Text;
using TSQL.Clauses;
using TSQL.Tokens;

namespace DotnetSqliteUnittestingTools.SQLitePlatformConversion.ClauseProcessors
{
    internal abstract class ClauseProcessor
    {
        private readonly TSQLClause _clause;
        private StringBuilder _stringBuilder;
        protected List<TSQLToken> Tokens;

        protected ClauseProcessor(TSQLClause clauseParameter)
        {
            _clause = clauseParameter;
        }

        public string Convert()
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