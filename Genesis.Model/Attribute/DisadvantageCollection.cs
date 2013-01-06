using System.Collections.Generic;

namespace Genesis.Model.Attribute
{
    internal class DisadvantageCollection
    {
        internal List<Disadvantage> Disadvantages     { get; set; }
        internal int                MaxBenefitAllowed { get; set; }
    }
}