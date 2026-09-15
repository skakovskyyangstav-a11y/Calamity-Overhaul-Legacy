using System.Collections.Generic ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Common.Compatibility
{
    internal static class LegacyExternalContent
    {
        public static bool TryItem(string fullName, out int type)
        {
            type = 0 ;
            if (ModContent.TryFind<ModItem>(fullName, out ModItem item)) {
                type = item.Type ;
                return true ;
            }
            return false ;
        }

        public static bool TryItemAny(out int type, params string[] fullNames)
        {
            foreach (string fullName in fullNames) {
                if (TryItem(fullName, out type)) {
                    return true ;
                }
            }
            type = 0 ;
            return false ;
        }

        public static bool TryTile(string fullName, out int type)
        {
            type = -1 ;
            if (ModContent.TryFind<ModTile>(fullName, out ModTile tile)) {
                type = tile.Type ;
                return true ;
            }
            return false ;
        }

        public static int[] ExistingTiles(params string[] names)
        {
            List<int> result = new() ;
            foreach (string name in names) {
                if (TryTile(name, out int type)) {
                    result.Add(type) ;
                }
            }
            return result.ToArray() ;
        }
    }
}
