using Terraria ;
using Terraria.ID ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs
{
    public static class RecipeUtilities
    {
        public static string GetItemFullName(int itemType)
        {
            if (itemType == ItemID.None) return SupertableConstants.NULL_ITEM_KEY ;
            Item item = new(itemType) ;
            return item.ModItem == null ? itemType.ToString() : item.ModItem.FullName ;
        }
        public static string[] ConvertTypesToFullNames(int[] types)
        {
            if (types == null) return null ;
            string[] names = new string[types.Length] ;
            for (int i = 0 ; i < types.Length ; i++) names[i] = GetItemFullName(types[i]) ;
            return names ;
        }
    }
}
