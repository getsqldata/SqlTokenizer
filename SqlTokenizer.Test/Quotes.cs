using Lansky.SqlTokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SqlTokenizer.Test
{
    [TestClass]
    public class Quotes
    {
        [TestMethod]
        public void SingleQuotesAreRecognized()
        {
            var results = new Tokenizer().Tokenize("SELECT * FROM Table WHERE Column='value value' AND Column2=45").ToList();
            Assert.IsTrue(results.Any(result => result.Content == "'value value'"));
        }

        [TestMethod]
        [Ignore]
        public void SingleQuotesWithEscapedQuoteInsideAreRecognized()
        {
            var results = new Tokenizer().Tokenize("SELECT * FROM Table WHERE Column='value '' value' AND Column2=45").ToList();
            Assert.IsTrue(results.Any(result => result.Content == "'value '' value'"));
        }

        [TestMethod]
        public void MultipleQuotesInQueryAreSeparated()
        {
            var results = new Tokenizer().Tokenize("SELECT * FROM Table WHERE Column='value value' AND Column2='value value value'").ToList();
            Assert.IsTrue(results.Any(result => result.ToString() == "StringConstant: 'value value'"));
            Assert.IsTrue(results.Any(result => result.ToString() == "StringConstant: 'value value value'"));
        }
    }
}
