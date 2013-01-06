namespace Genesis.Model.Attribute
{
    /// <summary>
    /// This class represents elements which apply to each S&P section or group (race/class/nwp/wp)
    /// </summary>
    internal class CommonGroupElements
    {
        internal int    Points    { get; set; }
        internal int    Rollover  { get; set; }
        internal string GroupName { get; set; }
    }
}