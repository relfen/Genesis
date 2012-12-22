using System.IO;
using Genesis.Model;
using NUnit.Framework;

namespace Genesis.Test
{
    [TestFixture]
    public class CharacterAttributeLoaderTest
    {

        private const string RaceModelFile = @"D:\Code\Genesis\Genesis.Test\Races.xml";
        //private const string ClassModelFile = "Classes.xml";

        [Test]
        public void SuccessfullyLoadRacesFromFile()
        {
            var races = CharacterAttributeLoader.LoadFromFile(RaceModelFile, "//Race");

            Assert.IsNotNull(races);
            Assert.IsTrue(races.Count > 0);
            Assert.IsTrue(races[0].Points == 45);
            Assert.IsTrue(races[0].SectionName == "Elf");
            Assert.IsNotNull(races[0].CharacterAttributes);

            var charAttribute = races[0].CharacterAttributes[0];
            Assert.IsTrue(charAttribute.Name == "Infravision");
            Assert.IsTrue(charAttribute.Cost == 5);
            Assert.IsTrue(charAttribute.Description == "Allows the character to see in the dark 60'.");
        }

        [Test]
        [ExpectedException(typeof(FileNotFoundException))]
        public void FailToLoadRacesFromInvalidFileName()
        {
            CharacterAttributeLoader.LoadFromFile(null, null);
        }
    }
}
