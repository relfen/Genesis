using System.Collections.Generic;
using Genesis.Model.Attribute;

namespace Genesis.Model
{
    internal class CharacterAttributes
    {
        internal List<AttributeCollection> RaceAttributes                 { get; set; }
        internal List<AttributeCollection> ClassAttributes                { get; set; }
        internal List<AttributeCollection> NonWeaponProficiencyAttributes { get; set; }
        internal List<AttributeCollection> WeaponProficiencyAttributes    { get; set; }
    }
}
