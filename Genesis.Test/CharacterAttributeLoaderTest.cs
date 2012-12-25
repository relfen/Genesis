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
        // Race Attribute Constants
        private const string RaceName                 = "Elf";
        private const int    RacePoints               = 45;
        private const int    RaceRollover             = 5;
        private const string RaceAttributeName        = "Infravision";
        private const int    RaceAttributeCost        = 5;
        private const string RaceAttributeDescription = "Allows the character to see in the dark 60'.";

        // Class Attribute Constants
        private const string ClassName                 = "Fighter";
        private const int    ClassPoints               = 15;
        private const int    ClassRollover             = 15;
        private const string ClassAttributeName        = "Weapon Specialization";
        private const int    ClassAttributeCost        = 5;
        private const string ClassAttributeDescription = "Allows the character to see specialize in a weapon. Normal cost must still be paid for specialization.";

        #region Setup/Helpers
        
        private string CreateMockRaceXml()
        {
            return new XDocument(
                new XElement("Races",
                    new XElement("Race",
                        new XElement("Name", RaceName),
                        new XElement("Points", RacePoints),
                        new XElement("Rollover", RaceRollover),
                        new XElement("Attributes",
                            new XElement("Attribute",
                                new XElement("Name", RaceAttributeName),
                                new XElement("Cost", RaceAttributeCost),
                                new XElement("Description", RaceAttributeDescription)
                                ))))).ToString();

        }

        private string CreateMockClassXml()
        {
            return new XDocument(
                new XElement("Classes",
                    new XElement("Class",
                        new XElement("Name", ClassName),
                        new XElement("Points", ClassPoints),
                        new XElement("Rollover", ClassRollover),
                        new XElement("Attributes",
                            new XElement("Attribute",
                                new XElement("Name", ClassAttributeName),
                                new XElement("Cost", ClassAttributeCost),
                                new XElement("Description", ClassAttributeDescription)
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
        public void SuccessfullyLoadClassCollection()
        {
            var loader  = new XmlLoader(CreateMockClassXml());
            var classes = loader.GetCharacterAttributeCollection(XmlXPathConfiguration.ClassRootLocation);

            Assert.IsNotNull(classes);
            Assert.IsTrue(classes.Count == 1);
            Assert.IsTrue(classes[0].SectionName == ClassName);
            Assert.IsTrue(classes[0].Points == ClassPoints);
            Assert.IsNotNull(classes[0].CharacterAttributes);

            var charAttribute = classes[0].CharacterAttributes[0];
            Assert.IsTrue(charAttribute.Name == ClassAttributeName);
            Assert.IsTrue(charAttribute.Cost == ClassAttributeCost);
            Assert.IsTrue(charAttribute.Description == ClassAttributeDescription);
        }

        [Test]
        [ExpectedException()]
        public void ThrowExceptionOnNullDocument()
        {
            var loader = new XmlLoader(null);
        }

        [Test]
        [ExpectedException()]
        public void ThrowExceptionOnInvalidXmlString()
        {
            var loader = new XmlLoader("<xml");
        }

        #endregion Tests
    }
}
