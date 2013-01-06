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

        public CharacterAttributes GetCharacterAttributes()
        {
            return new CharacterAttributes()
            {
                RaceAttributes                 = GetRaceAttributeCollection(),
                ClassAttributes                = GetClassAttributeCollection(),
                NonWeaponProficiencyAttributes = GetNonWeaponProficiencyAttributeCollection(),
                WeaponProficiencyAttributes    = GetWeaponProficiencyAttributeCollection(),
            };
        }

        private List<AttributeCollection> GetRaceAttributeCollection()
        {
            var characterAttributes = new List<AttributeCollection>();

            try
            {
                var sections = _doc.SelectNodes(XmlXPathConfiguration.RaceRootLocation);
                if(null == sections)
                    throw new XmlException("Invalid or missing Xml");

                characterAttributes.AddRange(from XmlNode node in sections
                                             select new AttributeCollection
                                             {
                                                Points     = GetIntFromXml(node, "Points"), 
                                                Rollover   = GetIntFromXml(node, "Rollover"), 
                                                Group      = GetStringFromXml(node, "Group"), 
                                                Name       = GetStringFromXml(node, "Name"), 
                                                Attributes = GetBaseAttributes(node),
                                             });

                return characterAttributes;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load Attribute list", ex);
            }
        }

        private List<AttributeCollection> GetClassAttributeCollection()
        {
            var characterAttributes = new List<AttributeCollection>();

            try
            {
                var sections = _doc.SelectNodes(XmlXPathConfiguration.ClassRootLocation);
                if(null == sections)
                    throw new XmlException("Invalid or missing Xml");

                characterAttributes.AddRange(from XmlNode node in sections
                                             select new AttributeCollection
                                             {
                                                Points     = GetIntFromXml(node, "Points"), 
                                                Rollover   = GetIntFromXml(node, "Rollover"), 
                                                Group      = GetStringFromXml(node, "Group"), 
                                                Name       = GetStringFromXml(node, "Name"), 
                                                Attributes = GetBaseAttributes(node),
                                             });

                return characterAttributes;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load Attribute list", ex);
            }
        }

        private List<AttributeCollection> GetNonWeaponProficiencyAttributeCollection()
        {
            var characterAttributes = new List<AttributeCollection>();

            try
            {
                var sections = _doc.SelectNodes(XmlXPathConfiguration.NonWeaponProficiencyRootLocation);
                if(null == sections)
                    throw new XmlException("Invalid or missing Xml");

                characterAttributes.AddRange(from XmlNode node in sections
                                             select new AttributeCollection
                                             {
                                                Points     = GetIntFromXml(node, "Points"), 
                                                Rollover   = GetIntFromXml(node, "Rollover"), 
                                                Group      = GetStringFromXml(node, "Group"), 
                                                Name       = GetStringFromXml(node, "Name"), 
                                                Attributes = GetNonWeaponProficiencyAttributes(node),
                                             });

                return characterAttributes;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load Attribute list", ex);
            }
        }

        private List<AttributeCollection> GetWeaponProficiencyAttributeCollection()
        {
            var characterAttributes = new List<AttributeCollection>();

            try
            {
                var sections = _doc.SelectNodes(XmlXPathConfiguration.NonWeaponProficiencyRootLocation);
                if (null == sections)
                    throw new XmlException("Invalid or missing Xml");

                characterAttributes.AddRange(from XmlNode node in sections
                                             select new AttributeCollection
                                             {
                                                 Points = GetIntFromXml(node, "Points"),
                                                 Rollover = GetIntFromXml(node, "Rollover"),
                                                 Group = GetStringFromXml(node, "Group"),
                                                 Name = GetStringFromXml(node, "Name"),
                                                 Attributes = GetNonWeaponProficiencyAttributes(node),
                                             });

                return characterAttributes;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load Attribute list", ex);
            }
        }

        private List<BaseAttribute> GetBaseAttributes(XmlNode node)
        {
            var attributeNodes = node.SelectNodes("//Attribute");
            if (null == attributeNodes)
                throw new XmlException("Missing attributes section.  Please validate the source xml files.");

            return (from XmlNode attributeNode in attributeNodes
                    select new BaseAttribute
                    {
                        Name        = GetStringFromXml(attributeNode, "Name"),
                        Description = GetStringFromXml(attributeNode, "Description"),
                        Cost        = GetIntFromXml(attributeNode, "Cost"),
                    }).ToList();
        }

        private List<NonWeaponProficiencyAttribute> GetNonWeaponProficiencyAttributes(XmlNode node)
        {
            var attributeNodes = node.SelectNodes("//Attribute");
            if (null == attributeNodes)
                throw new XmlException("Missing attributes section.  Please validate the source xml files.");

            return (from XmlNode attributeNode in attributeNodes
                    select new NonWeaponProficiencyAttribute()
                    {
                        Name        = GetStringFromXml(attributeNode, "Name"),
                        Description = GetStringFromXml(attributeNode, "Description"),
                        Cost        = GetIntFromXml(attributeNode, "Cost"),
                        BaseScore   = GetIntFromXml(attributeNode, "BaseScore")
                    }).ToList();
        }

        private List<WeaponProficiencyAttribute> GetWeaponProficiencyAttributes(XmlNode node)
        {
            var attributeNodes = node.SelectNodes("//Attribute");
            if (null == attributeNodes)
                throw new XmlException("Missing attributes section.  Please validate the source xml files.");

            return (from XmlNode attributeNode in attributeNodes
                    select new WeaponProficiencyAttribute()
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
