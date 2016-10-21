using Lansky.SqlTokenizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace SqlTokenizer.Test
{
    [TestClass]
    public class Comments
    {
        [TestMethod]
        public void SingleLineCommentIsRecognized()
        {
            var results = new Tokenizer().Tokenize(@"
-- single line comment

SELECT *
FROM Table
WHERE Column='value value' AND Column2=45").ToList();

            Assert.IsTrue(results.Any(result => result.ToString() == "Comment: -- single line comment"));
        }

        [TestMethod]
        public void SingleMySqlCommentIsRecognized()
        {
            var results = new Tokenizer().Tokenize(@"
# single line comment

SELECT *
FROM Table
WHERE Column='value value' AND Column2=45").ToList();

            Assert.IsTrue(results.Any(result => result.ToString() == "Comment: # single line comment"));
        }

        [TestMethod]
        public void MultipleOneLineCommentsAreRecognized()
        {
            var results = new Tokenizer().Tokenize(@"
-- multi
-- line

SELECT *
FROM Table -- comment
WHERE Column='value value' AND Column2=45").ToList();

            Assert.IsTrue(results.Any(result => result.ToString() == "Comment: -- multi"));
            Assert.IsTrue(results.Any(result => result.ToString() == "Comment: -- line"));
            Assert.IsTrue(results.Any(result => result.ToString() == "Comment: -- comment"));
        }

        [TestMethod]
        public void MultiLineCommentIsRecognized()
        {
            var results = new Tokenizer().Tokenize(@"
/* multi
line
comment */

SELECT *
FROM Table
WHERE Column='value value' AND Column2=45").ToList();

            Assert.IsTrue(results.Any(result => result.ToString() == "Comment: /* multi\nline\ncomment */"));
        }
    }
}
