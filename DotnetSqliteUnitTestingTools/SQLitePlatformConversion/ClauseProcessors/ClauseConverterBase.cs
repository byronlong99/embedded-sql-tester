using System.Collections.Generic;
using System.Text;
using TSQL.Clauses;
using TSQL.Tokens;

namespace DotnetSqliteUnittestingTools.SQLitePlatformConversion.ClauseProcessors
{
    internal abstract class ClauseProcessor
    {
        protected readonly TSQLClause Clause;
        protected StringBuilder StringBuilder;
        protected List<TSQLToken> Tokens;

        protected ClauseProcessor(TSQLClause clauseParameter)
        {
            Clause = clauseParameter;
        }

        public string Convert()
        {
            StringBuilder = new StringBuilder();

            if (Clause != null)
            {
                Tokens = Clause.Tokens;
                ConvertHelper();
            }

            return StringBuilder.ToString();
        }

        private void ConvertHelper()
        {
            var lastEndPosition = 0;

            for (int i = 0; i < Tokens.Count; i++)
            {
                if (Tokens[i].BeginPosition - lastEndPosition > 1)
                    StringBuilder.Append(" ");

                var result = ProcessToken(i);

                i = result.NewPosition;
                
                StringBuilder.Append(result.Text);

                lastEndPosition = Tokens[i].EndPosition;
            }
        }

        protected abstract TokenResult ProcessToken(int position);


    }
}