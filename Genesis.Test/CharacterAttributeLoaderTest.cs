using System.IO;
using System.Xml;
using System.Xml.Linq;
using Genesis.Model.Loader;
using NUnit.Framework;

namespace Genesis.Test
{
    [TestFixture]
    public class CharacterAttributeLoaderTest
    {
        private const string RaceName                 = "Elf";
        private const int    RacePoints               = 45;
        private const string RaceAttributeName        = "Infravision";
        private const int    RaceAttributeCost        = 5;
        private const string RaceAttributeDescription = "Allows the character to see in the dark 60'.";

        #region Setup/Helpers
        
        private string CreateMockRaceXml()
        {
            return new XDocument(
                new XElement("Races",
                    new XElement("Race",
                        new XElement("Name", RaceName),
                        new XElement("Points", RacePoints),
                        new XElement("Attributes",
                            new XElement("Attribute",
                                new XElement("Name", RaceAttributeName),
                                new XElement("Cost", RaceAttributeCost),
                                new XElement("Description", RaceAttributeDescription)
                                ))))).ToString();

        }

        #endregion Setup/Helpers

        #region Tests

        [Test]
        public void SuccessfullyLoadRaceCollection()
        {
            var loader = new XmlLoader(CreateMockRaceXml());
            var races  = loader.GetCharacterAttributeCollection(XmlXPathConfiguration.RaceRootLocation);

            Assert.IsNotNull(races);
            Assert.IsTrue(races.Count == 1);
            Assert.IsTrue(races[0].SectionName == RaceName);
            Assert.IsTrue(races[0].Points == RacePoints);
            Assert.IsNotNull(races[0].CharacterAttributes);

            var charAttribute = races[0].CharacterAttributes[0];
            Assert.IsTrue(charAttribute.Name == RaceAttributeName);
            Assert.IsTrue(charAttribute.Cost == RaceAttributeCost);
            Assert.IsTrue(charAttribute.Description == RaceAttributeDescription);
        }

        [Test]
        [ExpectedException()]
        public void ThrowExceptionOnNullDocument()
        {
            var loader = new XmlLoader(null);
        }

        #endregion Tests
    }
}
