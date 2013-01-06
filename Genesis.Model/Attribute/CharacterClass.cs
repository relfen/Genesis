using System.Collections.Generic;

namespace Genesis.Model.Attribute
{
    /// <summary>
    /// 
    /// </summary>
    internal class CharacterClass : CommonGroupElements
    {
        internal List<ClassAbility> Abilities { get; set; }
        internal string             Name      { get; set; }
    }
}