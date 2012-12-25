using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Genesis.Model.Attribute;

namespace Genesis.Model.Loader
{
    internal class XmlLoader : ILoader
    {
        private readonly XmlDocument _doc = new XmlDocument();

        public XmlLoader(string data)
        {
            if(String.IsNullOrWhiteSpace(data))
                throw new NullReferenceException();

            _doc.LoadXml(data);
        }

        public List<CharacterAttributeCollection> GetCharacterAttributeCollection(string configLocation)
        {
            var characterAttributes = new List<CharacterAttributeCollection>();

            try
            {
                var sections = _doc.SelectNodes(configLocation);
                if(null == sections)
                    throw new XmlException("Invalid or missing Xml");

                characterAttributes.AddRange(from XmlNode section in sections 
                                             select FromXml(section));

                return characterAttributes;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load Attribute list", ex);
            }
        }

        private CharacterAttributeCollection FromXml(XmlNode node)
        {
            if (null == node)
                throw new NullReferenceException("Xml section is missing.  Please validate the source xml files.");

            return new CharacterAttributeCollection
            {
                Points              = GetIntFromXml(node, "Points"),
                Rollover            = GetStringFromXml(node, "Rollover"),
                SectionName         = GetStringFromXml(node, "Name"),
                CharacterAttributes = GetAttributes(node),
            };
        }

        private List<CharacterAttribute> GetAttributes(XmlNode node)
        {
            var attributeNodes = node.SelectNodes("//Attribute");
            if (null == attributeNodes)
                throw new XmlException("Missing attributes section.  Please validate the source xml files.");

            return (from XmlNode attributeNode in attributeNodes
                    select new CharacterAttribute
                    {
                        Name        = GetStringFromXml(attributeNode, "Name"),
                        Description = GetStringFromXml(attributeNode, "Description"),
                        Cost        = GetIntFromXml(attributeNode, "Cost"),
                    }).ToList();

        }

        private int GetIntFromXml(XmlNode node, string element)
        {
            int value;
            if (null == node[element] || !Int32.TryParse(node[element].InnerText, out value))
                throw new XmlException(String.Format("Missing or invalid value for element in XML.  Please validate the {0} attribute", element));

            return value;
        }

        private string GetStringFromXml(XmlNode node, string element)
        {
            if (null == node[element] || String.IsNullOrWhiteSpace(node[element].InnerText))
                throw new XmlException(String.Format("Missing element in XML.  Please validate the {0} attribute", element));

            return node[element].InnerText;
        }
    }
}
