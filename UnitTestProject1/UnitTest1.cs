using Microsoft.VisualStudio.TestTools.UnitTesting;
using UrlSlugBenchmarks;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private static char s_longDash = '\u2013';
        private readonly SlugProducerBase _slugProducer = new SlugProducerV2_2();

        [TestMethod]
        [TestCategory("ClassTest")]
        public void SlugProducer_GetUrlSlug_WhenDescriptionContainsNoIllegalCharacters_ReturnsAsIs()
        {
            // Arrange
            var expectedSlug = "abcdegfhijklmnopqrstuvwxyz0123456789";

            // Act
            var actualSlug = _slugProducer.GetUrlSlug(expectedSlug);

            // Assert
            Assert.AreEqual(expectedSlug, actualSlug, $"We were expecting the Url slug to be: {expectedSlug}, but found the actual Url slug to be {actualSlug}");
        }

        [TestMethod]
        [TestCategory("ClassTest")]
        public void SlugProducer_GetUrlSlug_WhenTitleContainsUppercaseCharacters_ReturnsLegalUrl()
        {
            // Arrange
            var title = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var expectedSlug = title.ToLower();

            // Act
            var actualSlug = _slugProducer.GetUrlSlug(title);

            // Assert
            Assert.AreEqual(expectedSlug, actualSlug, $"We were expecting the Url slug to be: {expectedSlug}, but found the actual Url slug to be {actualSlug}");
        }

        [TestMethod]
        [TestCategory("ClassTest")]
        public void SlugProducer_GetUrlSlug_WhenTitleContainsIllegalCharacters_ReplacesWithEmpty()
        {
            // Arrange
            var title = "@#a$@bc%d&^*^%&*EF$%^&GHI^&*&^J()_KLMNOPQRSTUVWXYZ0123456789&^*)";
            var expectedSlug = "abcdefghijklmnopqrstuvwxyz0123456789";

            // Act
            var actualSlug = _slugProducer.GetUrlSlug(title);

            // Assert
            Assert.AreEqual(expectedSlug, actualSlug, $"We were expecting the Url slug to be: {expectedSlug}, but found the actual Url slug to be {actualSlug}");
        }

        [TestMethod]
        [TestCategory("ClassTest")]
        public void SlugProducer_GetUrlSlug_WhenTitleContainsIllegalAndAllowedIllegalCharacters_ReplacesWithEmptyAndSingleDash()
        {
            // Arrange
            var title = " @#a$ @bc%.d&^*" + s_longDash + "^%&*EF$%^&GHI^&*&^J()_KLMNOPQRS TUVWXYZ0123456789&^*) ";
            var expectedSlug = "a-bc-d-efghijklmnopqrs-tuvwxyz0123456789";

            // Act
            var actualSlug = _slugProducer.GetUrlSlug(title);

            // Assert
            Assert.AreEqual(expectedSlug, actualSlug, $"We were expecting the Url slug to be: {expectedSlug}, but found the actual Url slug to be {actualSlug}");
        }

        [TestMethod]
        [TestCategory("ClassTest")]
        public void SlugProducer_GetUrlSlug_WhenTitleContainsAllowedIllegalCharacters_ReplacesWithSingleDash()
        {
            // Arrange
            var title = "A B.C-D" + s_longDash + "EFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var expectedSlug = "a-b-c-d-efghijklmnopqrstuvwxyz0123456789";

            // Act
            var actualSlug = _slugProducer.GetUrlSlug(title);

            // Assert
            Assert.AreEqual(expectedSlug, actualSlug, $"We were expecting the Url slug to be: {expectedSlug}, but found the actual Url slug to be {actualSlug}");
        }

        [TestMethod]
        [TestCategory("ClassTest")]
        public void SlugProducer_GetUrlSlug_WhenTitleStartsWithAllowedIllegalCharacters_ReplacesAllWithEmptyString()
        {
            // Arrange
            var title = " .-" + s_longDash + "ABCDEFGHIJKLMN" + s_longDash + "OPQRSTUVWXYZ0123456789";
            var expectedSlug = "abcdefghijklmn-opqrstuvwxyz0123456789";

            // Act
            var actualSlug = _slugProducer.GetUrlSlug(title);

            // Assert
            Assert.AreEqual(expectedSlug, actualSlug, $"We were expecting the Url slug to be: {expectedSlug}, but found the actual Url slug to be {actualSlug}");
        }

        [TestMethod]
        [TestCategory("ClassTest")]
        public void SlugProducer_GetUrlSlug_WhenTitleEndsWithAllowedIllegalCharacters_ReplacesAllWithEmptyString()
        {
            // Arrange
            var title = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 .-" + s_longDash;
            var expectedSlug = "abcdefghijklmnopqrstuvwxyz0123456789";

            // Act
            var actualSlug = _slugProducer.GetUrlSlug(title);

            // Assert
            Assert.AreEqual(expectedSlug, actualSlug, $"We were expecting the Url slug to be: {expectedSlug}, but found the actual Url slug to be {actualSlug}");
        }
    }
}
