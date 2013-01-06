using System.Collections.Generic;

namespace Genesis.Model.Attribute
{
    /// <summary>
    /// All the groups and values that make up the available skills and powers for a character
    /// </summary>
    internal class CharacterElements
    {
        internal List<Race>                     Races                  { get; set; }
        internal List<CharacterClass>           Classes                { get; set; }
        internal NonWeaponProficiencyCollection NonWeaponProficiencies { get; set; }
        internal WeaponProficiencyCollection    WeaponProficiencies    { get; set; }
        internal AdvantageCollection            Advantages             { get; set; }
        internal DisadvantageCollection         Disadvantages          { get; set; } 
    }
}