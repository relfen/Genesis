using System.Collections.Generic;

namespace Genesis.Model.Attribute
{
    internal class AttributeCollection
    {
        internal List<BaseAttribute> Attributes { get; set; }
        internal int                 Points     { get; set; }
        internal int                 Rollover   { get; set; }
        internal string              Name       { get; set; }
        internal string              Group      { get; set; }
    }
}
