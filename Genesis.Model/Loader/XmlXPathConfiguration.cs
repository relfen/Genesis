namespace Genesis.Model.Loader
{
    internal static class XmlXPathConfiguration
    {
        // Race
        internal static readonly string RaceRootLocation  = "//Race";
        internal static readonly string RaceAbilitiesPath = "//Abilities";

        // Class
        internal static readonly string ClassRootLocation  = "//Class";
        internal static readonly string ClassAbilitiesPath = "//Abilities";

        // Non-Weapon
        internal static readonly string NonWeaponProficiencyRootLocation      = "//NWP";
        internal static readonly string NonWeaponProficiencyGroupNameLocation = NonWeaponProficiencyRootLocation + "/GroupName";
        internal static readonly string NonWeaponProficiencyPointsLocation    = NonWeaponProficiencyRootLocation + "/Points";
        internal static readonly string NonWeaponProficiencyRolloverLocation  = NonWeaponProficiencyRootLocation + "/Rollover";

        // Weapon
        internal static readonly string WeaponProficiencyRootLocation      = "//WP";
        internal static readonly string WeaponProficiencyGroupNameLocation = WeaponProficiencyRootLocation + "/GroupName";
        internal static readonly string WeaponProficiencyPointsLocation    = WeaponProficiencyRootLocation + "/Points";
        internal static readonly string WeaponProficiencyRolloverLocation  = WeaponProficiencyRootLocation + "/Rollover";

        // Advantages

        // Disadvantages






    }
}
