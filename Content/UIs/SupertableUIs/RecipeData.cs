using System ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs
{
    public class RecipeData
    {
        public int Target { get ; set ; }
        public string[] Values { get ; set ; }
        public int[] MaterialTypesCache ;
        private int? cachedHash ;
        public void BuildMaterialTypesCache()
        {
            MaterialTypesCache = new int[SupertableConstants.TOTAL_SLOTS] ;
            for (int i = 0 ; i < MaterialTypesCache.Length ; i++) MaterialTypesCache[i] = LegacyItemResolver.Resolve(Values[i]) ;
        }
        public override bool Equals(object obj)
        {
            if (obj is not RecipeData other || Target != other.Target || Values == null || other.Values == null || Values.Length != other.Values.Length) return false ;
            for (int i = 0 ; i < Values.Length ; i++) if (!string.Equals(Values[i], other.Values[i], StringComparison.Ordinal)) return false ;
            return true ;
        }
        public override int GetHashCode()
        {
            if (cachedHash.HasValue) return cachedHash.Value ;
            int hash = Target ;
            for (int i = 0 ; i < Math.Min(5, Values?.Length ?? 0) ; i++) hash = hash * 31 + (Values[i]?.GetHashCode() ?? 0) ;
            cachedHash = hash ; return hash ;
        }
    }
}
