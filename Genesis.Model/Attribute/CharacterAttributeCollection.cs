using System.Collections.Generic;

namespace Genesis.Model.Attribute
{
    internal class CharacterAttributeCollection
    {
        internal List<CharacterAttribute> CharacterAttributes { get; set; }
        internal int                      Points              { get; set; }
        internal string                   SectionName         { get; set; }
        internal string                   Rollover            { get; set; }

    }
}
