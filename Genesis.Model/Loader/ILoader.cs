using System.Collections.Generic;

namespace Genesis.Model.Loader
{
    internal interface ILoader
    {
        List<CharacterAttributeCollection> GetCharacterAttributeCollection(string configLocation);
    }
}
