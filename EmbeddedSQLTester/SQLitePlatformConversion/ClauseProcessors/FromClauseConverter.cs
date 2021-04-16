using TSQL.Clauses;

namespace EmbeddedSQLTester.SQLitePlatformConversion.ClauseProcessors
{
    internal class FromClauseConverter : ClauseProcessor
    {
        private bool _insideOnClause;

        public FromClauseConverter(TSQLClause clause) : base(clause)
        {
            _insideOnClause = false;
        }

        protected override TokenResult ProcessToken(int position)
        {
            var token = Tokens[position].Text;

            switch (token.ToLower())
            {
                case "on":
                    _insideOnClause = true;
                    break;
                case "join":
                    _insideOnClause = false;
                    break;
            }

            if (!_insideOnClause)
                token = token.Replace('.', '_');

            token = token.Replace("[", string.Empty).Replace("]", string.Empty);

            var result = new TokenResult {Text = token, NewPosition = position};
            
            var handleNoLock = HandleNoLock(position);
            
            if (handleNoLock != null)
                result = handleNoLock;

            return result;
        }

        private TokenResult HandleNoLock(int position)
        {
            if (Tokens[position].Text.ToLower() == "with" && Tokens[position + 1].Text.ToLower() == "("
                                                          && Tokens[position + 2].Text.ToLower() == "nolock" && Tokens[position + 3].Text.ToLower() == ")")
                return new TokenResult {Text = string.Empty, NewPosition = position + 3};

            return null;
        }
    }
}