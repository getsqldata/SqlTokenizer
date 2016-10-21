namespace Lansky.SqlTokenizer
{
    public struct Token
    {
        public string Content { get; private set; }

        public TokenType Type { get; private set; }

        public Token(string content, TokenType type)
        {
            Content = content;
            Type = type;
        }

        public override string ToString()
            => $"{Type.ToString()}: {Content}";
    }
}
