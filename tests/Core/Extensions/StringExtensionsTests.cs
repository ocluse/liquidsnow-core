using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ocluse.LiquidSnow.Core.Extensions.Tests
{
    [TestClass()]
    public class StringExtensionsTests
    {
        const string TEST_PASCAL = "WhereIsMyMother47Satire";
        const string TEST_KEBAB = "where-is-my-mother-47-satire";
        const string TEST_SNAKE = "where_is_my_mother_47_satire";
        const string TEST_UPPER = "WHERE IS MY MOTHER47 SATIRE";
        const string TEST_SENTENCE = "Where is my mother47 satire";
        const string TEST_TITLE = "Where Is My Mother47 Satire";

        [TestMethod()]
        public void PascalToKebabCaseTest()
        {

            string actual = TEST_PASCAL.PascalToKebabCase();

            Assert.AreEqual(TEST_KEBAB, actual);
        }

        [TestMethod()]
        public void ToSnakeCaseTest()
        {
            string actual = TEST_PASCAL.ToSnakeCase();

            Assert.AreEqual(TEST_SNAKE, actual);
        }

        [TestMethod()]
        public void AddSpacesToPascalCaseStringTest()
        {
            string actual = TEST_PASCAL.AddSpacesToPascalCaseString();
            Assert.AreEqual(TEST_TITLE, actual);
        }

        [TestMethod()]
        public void ToSentenceCaseTest()
        {
            string actual = TEST_UPPER.ToSentenceCase();
            Assert.AreEqual(TEST_SENTENCE, actual);
        }

        [TestMethod()]
        public void ToTitleCaseTest()
        {
            string actual = TEST_UPPER.ToTitleCase();
            Assert.AreEqual(TEST_TITLE, actual);
        }
    }
}