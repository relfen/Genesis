using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;

namespace Genesis.Model
{
    internal static class CharacterAttributeLoader
    {
        // TODO:  Change this to use dependency injection so we can inject a data from the web or from a local file (or both!).  This is ok for now.
        internal static List<CharacterAttributeCollection> LoadFromFile(string file, string sectionPath)
        {
            var characterAttributes = new List<CharacterAttributeCollection>();

            if (!File.Exists(file))
                throw new FileNotFoundException("Failed to find file: " + file);

            try
            {
                var doc = GetXmlFromFile(file);

                var sections = doc.SelectNodes(sectionPath);
                if(null == sections)
                    throw new XmlException("Invalid Xml in file: " + file);

                characterAttributes.AddRange(from XmlNode section in sections 
                                             select CharacterAttributeCollection.FromXml(section));

                return characterAttributes;

            }
            catch (Exception ex)
            {
                throw new Exception("Failed to load Attribute list", ex);
            }
        }

        private static XmlDocument GetXmlFromFile(string file)
        {
            var doc = new XmlDocument();
            
            doc.Load(file);

            return doc;
        }
    }
}
