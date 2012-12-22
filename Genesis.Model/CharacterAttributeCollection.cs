using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Genesis.Model
{
    internal class CharacterAttributeCollection
    {
        internal List<CharacterAttribute> CharacterAttributes { get; set; }
        internal int                      Points              { get; set; }
        internal string                   SectionName         { get; set; }

        internal static CharacterAttributeCollection FromXml(XmlNode node)
        {
            if (null == node)
                throw new NullReferenceException("Xml section is missing.  Please validate the source xml files.");

            return new CharacterAttributeCollection
            {
                Points              = GetIntFromXml(node, "Points"),
                SectionName         = GetStringFromXml(node, "Name"),
                CharacterAttributes = GetAttributes(node),
            };
        }

        private static List<CharacterAttribute> GetAttributes(XmlNode node)
        {
            var attributeNodes = node.SelectNodes("//Attribute");
            if(null == attributeNodes)
                throw new XmlException("Missing attributes section.  Please validate the source xml files.");

            return (from XmlNode attributeNode in attributeNodes
                    select new CharacterAttribute()
                        {
                            Name        = GetStringFromXml(attributeNode, "Name"), 
                            Description = GetStringFromXml(attributeNode, "Description"), 
                            Cost        = GetIntFromXml(attributeNode, "Cost"),
                        }).ToList();

        }

        private static int GetIntFromXml(XmlNode node, string element)
        {
            int value;
            if (null == node[element] || !Int32.TryParse(node[element].InnerText, out value))
                throw new XmlException(String.Format("Missing or invalid value for element in XML.  Please validate the {0} attribute", element));

            return value;
        }

        private static string GetStringFromXml(XmlNode node, string element)
        {
            if (null == node[element] || String.IsNullOrWhiteSpace(node[element].InnerText))
                throw new XmlException(String.Format("Missing element in XML.  Please validate the {0} attribute", element));

            return node[element].InnerText;
        }
    }
}
