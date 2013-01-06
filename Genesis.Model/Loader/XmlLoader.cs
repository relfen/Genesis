using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

        public CharacterElements GetCharacterAttributes()
        {
            return new CharacterElements
            { 
                Races                  = GetRaces(),
                Classes                = GetCharacterClasses(),
                NonWeaponProficiencies = GetNonWeaponProficiencyCollection(),
                WeaponProficiencies    = GetWeaponProficiencyCollection(),
                Advantages             = GetAdvantages(),
                Disadvantages          = GetDisadvantages(),
            };
        }

        /// <summary>
        /// TODO: Implement
        /// </summary>
        /// <returns></returns>
        private DisadvantageCollection GetDisadvantages()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO: Implement
        /// </summary>
        /// <returns></returns>
        private AdvantageCollection GetAdvantages()
        {
            throw new NotImplementedException();
        }

        private List<Race> GetRaces()
        {
            var races = new List<Race>();

            try
            {
                var sections = _doc.SelectNodes(XmlXPathConfiguration.RaceRootLocation);
                if(null == sections)
                    throw new XmlException("Invalid or missing Xml - Race section not found.");

                races.AddRange(from XmlNode node in sections
                                select new Race()
                                {
                                    Points     = GetIntFromXml(node, "Points"), 
                                    Rollover   = GetIntFromXml(node, "Rollover"), 
                                    GroupName  = GetStringFromXml(node, "GroupName"),
                                    Abilities  = GetRaceAbilities(node),
                                });

                return races;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load races list", ex);
            }
        }

        private List<RaceAbility> GetRaceAbilities(XmlNode node)
        {
            var attributeNodes = node.SelectNodes(XmlXPathConfiguration.RaceAbilitiesPath);
            if (null == attributeNodes)
                throw new XmlException("Missing race abilities section.  Please validate the source xml files.");

            return (from XmlNode attributeNode in attributeNodes
                    select new RaceAbility()
                    {
                        Name        = GetStringFromXml(attributeNode, "Name"),
                        Description = GetStringFromXml(attributeNode, "Description"),
                        Cost        = GetIntFromXml(attributeNode, "Cost"),
                    }).ToList();
        }

        private List<CharacterClass> GetCharacterClasses()
        {
            var classes = new List<CharacterClass>();

            try
            {
                var sections = _doc.SelectNodes(XmlXPathConfiguration.ClassRootLocation);
                if(null == sections)
                    throw new XmlException("Invalid or missing Xml - Class section not found");

                classes.AddRange(from XmlNode node in sections
                                select new CharacterClass
                                {
                                    Points     = GetIntFromXml(node, "Points"), 
                                    Rollover   = GetIntFromXml(node, "Rollover"), 
                                    GroupName  = GetStringFromXml(node, "GroupName"), 
                                    Name       = GetStringFromXml(node, "Name"), 
                                    Abilities  = GetClassAbilities(node),
                                });

                return classes;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load Attribute list", ex);
            }
        }

        private List<ClassAbility> GetClassAbilities(XmlNode node)
        {
            var attributeNodes = node.SelectNodes(XmlXPathConfiguration.ClassAbilitiesPath);
            if (null == attributeNodes)
                throw new XmlException("Missing class abilities section.  Please validate the source xml files.");

            return (from XmlNode attributeNode in attributeNodes
                    select new ClassAbility()
                    {
                        Name        = GetStringFromXml(attributeNode, "Name"),
                        Description = GetStringFromXml(attributeNode, "Description"),
                        Cost        = GetIntFromXml(attributeNode, "Cost"),
                    }).ToList();
        }

        private NonWeaponProficiencyCollection GetNonWeaponProficiencyCollection()
        {
            var nwpList = new NonWeaponProficiencyCollection();

            try
            {
                var nwpSection    = _doc.SelectNodes(XmlXPathConfiguration.NonWeaponProficiencyRootLocation);
                var groupNameNode = _doc.SelectSingleNode(XmlXPathConfiguration.NonWeaponProficiencyGroupNameLocation);
                var pointsNode    = _doc.SelectSingleNode(XmlXPathConfiguration.NonWeaponProficiencyPointsLocation);
                var rolloverNode  = _doc.SelectSingleNode(XmlXPathConfiguration.NonWeaponProficiencyRolloverLocation);

                if(null == nwpSection)
                    throw new XmlException("Invalid or missing Xml");

                nwpList.NonWeaponProficiencies = GetNonWeaponProficiencies(nwpSection);
                nwpList.GroupName              = GetStringFromXml(groupNameNode);
                nwpList.Points                 = GetIntFromXml(pointsNode);
                nwpList.Rollover               = GetIntFromXml(rolloverNode);

                return nwpList;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load Attribute list", ex);
            }
        }

        private List<NonWeaponProficiency> GetNonWeaponProficiencies(IEnumerable nodes)
        {
            if (null == nodes)
                throw new XmlException("Missing non-weapon proficiency section.  Please validate the source xml files.");

            return (from XmlNode attributeNode in nodes
                    select new NonWeaponProficiency
                    {
                        Name        = GetStringFromXml(attributeNode, "Name"),
                        Description = GetStringFromXml(attributeNode, "Description"),
                        Cost        = GetIntFromXml(attributeNode, "Cost"),
                        BaseScore   = GetIntFromXml(attributeNode, "BaseScore")
                    }).ToList();
        }

        private WeaponProficiencyCollection GetWeaponProficiencyCollection()
        {
            var wpList = new WeaponProficiencyCollection();

            try
            {
                var wpSection     = _doc.SelectNodes(XmlXPathConfiguration.WeaponProficiencyRootLocation);
                var groupNameNode = _doc.SelectSingleNode(XmlXPathConfiguration.WeaponProficiencyGroupNameLocation);
                var pointsNode    = _doc.SelectSingleNode(XmlXPathConfiguration.WeaponProficiencyPointsLocation);
                var rolloverNode  = _doc.SelectSingleNode(XmlXPathConfiguration.WeaponProficiencyRolloverLocation);

                if(null == wpSection)
                    throw new XmlException("Invalid or missing Xml - Weapon Proficiency Section not found.");

                wpList.WeaponProficiencies = GetWeaponProficiencies(wpSection);
                wpList.GroupName           = GetStringFromXml(groupNameNode);
                wpList.Points              = GetIntFromXml(pointsNode);
                wpList.Rollover            = GetIntFromXml(rolloverNode);

                return wpList;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load Attribute list", ex);
            }
        }


        private List<WeaponProficiency> GetWeaponProficiencies(IEnumerable nodes)
        {
            if (null == nodes)
                throw new XmlException("Missing attributes section.  Please validate the source xml files.");

            return (from XmlNode attributeNode in nodes
                    select new WeaponProficiency
                        {
                        Name        = GetStringFromXml(attributeNode, "Name"),
                        Description = GetStringFromXml(attributeNode, "Description"),
                        Cost        = GetIntFromXml(attributeNode, "Cost"),
                    }).ToList();
        }

        private int GetIntFromXml(XmlNode node)
        {
            throw new NotImplementedException();
        }

        private int GetIntFromXml(XmlNode node, string element)
        {
            int value;
            if (null == node[element] || !Int32.TryParse(node[element].InnerText, out value))
                throw new XmlException(String.Format("Missing or invalid value for element in XML.  Please validate the {0} attribute", element));

            return value;
        }

        private string GetStringFromXml(XmlNode node)
        {
            throw new NotImplementedException();
        }

        private string GetStringFromXml(XmlNode node, string element)
        {
            if (null == node[element] || String.IsNullOrWhiteSpace(node[element].InnerText))
                throw new XmlException(String.Format("Missing element in XML.  Please validate the {0} attribute", element));

            return node[element].InnerText;
        }

    }
}
