using Lansky.SqlTokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SqlTokenizer.Test
{
    [TestClass]
    public class SimpleQueries
    {
        [TestMethod]
        public void NullStringThrows()
        {
            try
            {
                var tokenizer = new Tokenizer();
                var results = tokenizer.Tokenize(null).ToList();
            }
            catch (System.ArgumentNullException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void EmptyStringDoesNotReturnAnyToken()
        {
            var result = new Tokenizer().Tokenize("");
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void WhitespaceOnlyStringDoesNotReturnAnyToken()
        {
            var result = new Tokenizer().Tokenize("  \t ");
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void SimpleSelectGetsTokenized()
        {
            var result = new Tokenizer().Tokenize("SELECT * FROM [dbo].[Some Table] WHERE [SomeColumn]=@stringParameter").ToList();

            Assert.AreEqual(9, result.Count);
            Assert.AreEqual("Keyword: SELECT", result[0].ToString());
            Assert.AreEqual("Keyword: FROM", result[1].ToString());
            Assert.AreEqual("Identifier: [dbo]", result[2].ToString());
            Assert.AreEqual("Dot: .", result[3].ToString());
            Assert.AreEqual("Identifier: [Some Table]", result[4].ToString());
            Assert.AreEqual("Keyword: WHERE", result[5].ToString());
            Assert.AreEqual("Identifier: [SomeColumn]", result[6].ToString());
            Assert.AreEqual("Operator: =", result[7].ToString());
            Assert.AreEqual("Parameter: @stringParameter", result[8].ToString());
        }
    }
}
