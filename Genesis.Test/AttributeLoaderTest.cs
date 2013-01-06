using System.IO;
using System.Xml;
using System.Xml.Linq;
using Genesis.Model.Loader;
using NUnit.Framework;

namespace Genesis.Test
{
    [TestFixture]
    public class AttributeLoaderTest
    {
        // Race Attribute Constants
        private const string RaceGroup                = "Elf";
        private const string RaceName                 = "Wood Elf";
        private const int    RacePoints               = 45;
        private const int    RaceRollover             = 5;
        private const string RaceAttributeName        = "Infravision";
        private const int    RaceAttributeCost        = 5;
        private const string RaceAttributeDescription = "Allows the character to see in the dark 60'.";

        // Class Attribute Constants
        private const string ClassGroup                = "Warrior";
        private const string ClassName                 = "Fighter";
        private const int    ClassPoints               = 15;
        private const int    ClassRollover             = 15;
        private const string ClassAttributeName        = "Weapon Specialization";
        private const int    ClassAttributeCost        = 5;
        private const string ClassAttributeDescription = "Allows the character to see specialize in a weapon. Normal cost must still be paid for specialization.";

        // Non-Weapon Proficiency Attribute Constants
        private const string NWPClassGroup             = "Priest";
        private const int    NWPPoints                 = 5;
        private const int    NWPRollover               = 5;
        private const string NWPAttributeName          = "Knowledge Religion";
        private const int    NWPAttributeCost          = 3;
        private const int    NWPAttributeBaseScore     = 8;
        private const string NWPAttributeDescription   = "The character knows stuff about religion.";

        #region Setup/Helpers
        
        private string CreateMockRaceXml()
        {
            return new XDocument(
                new XElement("Races",
                    new XElement("Race",
                        new XElement("Group", RaceGroup),
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
                        new XElement("Group", ClassGroup),
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

        private string CreateMockNonWeaponProficiencyXml()
        {
            return new XDocument(
                new XElement("NonWeaponProficiencyGroups",
                    new XElement("NonWeaponProficiencyGroup",
                        new XElement("Group", NWPClassGroup),
                        new XElement("Points", NWPPoints),
                        new XElement("Rollover",NWPRollover),
                        new XElement("NonWeaponProficiencies",
                            new XElement("NonWeaponProficiency",
                                new XElement("Name", NWPAttributeName),
                                new XElement("Cost", NWPAttributeCost),
                                new XElement("BaseScore", NWPAttributeBaseScore),
                                new XElement("Description", NWPAttributeDescription)
                                ))))).ToString();
        }

        #endregion Setup/Helpers

        #region Tests

        [Test]
        public void SuccessfullyLoadRaceCollection()
        {
            var loader = new XmlLoader(CreateMockRaceXml());
            var races  = loader.GetAttributeCollection(XmlXPathConfiguration.RaceRootLocation);

            Assert.IsNotNull(races);
            Assert.IsTrue(races.Count == 1);
            Assert.IsTrue(races[0].Group    == RaceGroup);
            Assert.IsTrue(races[0].Name     == RaceName);
            Assert.IsTrue(races[0].Points   == RacePoints);
            Assert.IsTrue(races[0].Rollover == RaceRollover);
            Assert.IsNotNull(races[0].Attributes);

            var charAttribute = races[0].Attributes[0];
            Assert.IsTrue(charAttribute.Name        == RaceAttributeName);
            Assert.IsTrue(charAttribute.Cost        == RaceAttributeCost);
            Assert.IsTrue(charAttribute.Description == RaceAttributeDescription);
        }

        [Test]
        public void SuccessfullyLoadClassCollection()
        {
            var loader  = new XmlLoader(CreateMockClassXml());
            var classes = loader.GetAttributeCollection(XmlXPathConfiguration.ClassRootLocation);

            Assert.IsNotNull(classes);
            Assert.IsTrue(classes.Count == 1);
            Assert.IsTrue(classes[0].Group    == ClassGroup);
            Assert.IsTrue(classes[0].Name     == ClassName);
            Assert.IsTrue(classes[0].Points   == ClassPoints);
            Assert.IsTrue(classes[0].Rollover == ClassRollover);
            Assert.IsNotNull(classes[0].Attributes);

            var charAttribute = classes[0].Attributes[0];
            Assert.IsTrue(charAttribute.Name        == ClassAttributeName);
            Assert.IsTrue(charAttribute.Cost        == ClassAttributeCost);
            Assert.IsTrue(charAttribute.Description == ClassAttributeDescription);
        }

        [Test]
        public void SuccessfullyLoadNWPCollection()
        {
            var loader  = new XmlLoader(CreateMockClassXml());
            var nwpList = loader.GetAttributeCollection(XmlXPathConfiguration.NonWeaponProficiencyRootLocation);

            Assert.IsNotNull(nwpList);
            Assert.IsTrue(nwpList.Count == 1);
            Assert.IsTrue(nwpList[0].Group    == NWPClassGroup);
            Assert.IsTrue(nwpList[0].Points   == NWPPoints);
            Assert.IsTrue(nwpList[0].Rollover == NWPRollover);
            Assert.IsNotNull(nwpList[0].Attributes);

            var charAttribute = nwpList[0].Attributes[0];
            Assert.IsTrue(charAttribute.Name        == NWPAttributeName);
            Assert.IsTrue(charAttribute.Cost        == NWPAttributeCost);
            Assert.IsTrue(charAttribute.BaseScore   == NWPAttributeBaseScore);
            Assert.IsTrue(charAttribute.Description == ClassAttributeDescription);
        }

        [Test]
        [ExpectedException]
        public void ThrowExceptionOnNullDocument()
        {
            var loader = new XmlLoader(null);
        }

        [Test]
        [ExpectedException]
        public void ThrowExceptionOnInvalidXmlString()
        {
            var loader = new XmlLoader("<xml");
        }

        #endregion Tests
    }
}
