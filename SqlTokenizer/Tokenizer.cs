using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lansky.SqlTokenizer
{
    public class Tokenizer
    {
        public IEnumerable<Token> Tokenize(string sql)
        {
            if (sql == null)
            {
                throw new ArgumentNullException(nameof(sql));
            }

            sql = sql.Replace("\r\n", "\n").Replace("\r", "\n"); // normalize to Linux endlines

            var types = new Dictionary<string, TokenType> {
                { @"\-\-[^\n]*", TokenType.Comment },
                { @"\#[^\n]*", TokenType.Comment },
                { @"\/\*[^\*]*\*\/", TokenType.Comment },
                { @"@[A-Za-z]+", TokenType.Parameter },
                { @"[A-Za-z]+", TokenType.Keyword },
                { @"\[[^\]]*\]", TokenType.Identifier},
                { @"\.", TokenType.Dot },
                { @"'[^']*'", TokenType.StringConstant },
                { @";", TokenType.Semicolon },
                { @"\+|\-|\=", TokenType.Operator },
                { @"\(|\)", TokenType.Parenthesis }
            };

            var regex = string.Join("|", types.Keys.Select(k => $"({k})"));
            var matches = Regex.Matches(sql, regex, RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace | RegexOptions.ExplicitCapture);

            foreach (Match match in matches)
            {
                var currentTokenType = (TokenType?)null;
                foreach (var type in types)
                {
                    if (!currentTokenType.HasValue && Regex.Match(match.Value, $"^{type.Key}$").Success)
                    {
                        currentTokenType = type.Value;
                    }
                }

                yield return new Token(match.Value, currentTokenType.Value);
            }
        }
    }
}
