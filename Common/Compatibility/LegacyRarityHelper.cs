using Terraria.ID ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Common.Compatibility
{
    internal static class LegacyRarityHelper
    {
        public static int HotPink
        {
            get
            {
                if (ModContent.TryFind<ModRarity>("CalamityMod/HotPink", out ModRarity rarity)) {
                    return rarity.Type ;
                }
                return ItemRarityID.Purple ;
            }
        }

        public static int DarkOrange
        {
            get
            {
                if (ModContent.TryFind<ModRarity>("CalamityMod/DarkOrange", out ModRarity rarity)) {
                    return rarity.Type ;
                }
                return ItemRarityID.Red ;
            }
        }
    }
}
