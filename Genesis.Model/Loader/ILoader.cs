using System.Collections.Generic;
using Genesis.Model.Attribute;

namespace Genesis.Model.Loader
{
    internal interface ILoader
    {
        List<CharacterAttributeCollection> GetCharacterAttributeCollection(string configLocation);
    }
}
