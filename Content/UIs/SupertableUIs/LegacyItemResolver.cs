using System ;
using System.Collections.Generic ;
using Terraria.ID ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs
{
    internal static class LegacyItemResolver
    {
        private static readonly Dictionary<string, string[]> Aliases = new(StringComparer.OrdinalIgnoreCase) {
            ["CalamityOverhaul/DecayParticles"] = new[] { "CalamityOverhaulLegacy/DecayParticles" },
            ["CalamityOverhaul/DecaySubstance"] = new[] { "CalamityOverhaulLegacy/DecaySubstance" },
            ["CalamityOverhaul/DissipationSubstance"] = new[] { "CalamityOverhaulLegacy/DissipationSubstance" },
            ["CalamityOverhaul/SpectralMatter"] = new[] { "CalamityOverhaulLegacy/SpectralMatter" },
            ["CalamityOverhaul/DarkMatterBall"] = new[] { "CalamityOverhaulLegacy/DarkMatterBall" },
            ["CalamityOverhaul/InfinityCatalyst"] = new[] { "CalamityOverhaulLegacy/InfinityCatalyst" },
            ["CalamityOverhaul/InfiniteIngot"] = new[] { "CalamityOverhaulLegacy/InfiniteIngot" },
            ["CalamityOverhaul/InfinitePick"] = new[] { "CalamityOverhaulLegacy/InfinitePick" },
            ["CalamityOverhaul/HeavenfallLongbow"] = new[] { "CalamityOverhaulLegacy/HeavenfallLongbow" },
            ["CalamityMod/Elderberry"] = new[] { "CalamityMod/TaintedCloudberry", "CalamityMod/Elderberry" },
            ["CalamityMod/BloodOrange"] = new[] { "CalamityMod/SanguineTangerine", "CalamityMod/BloodOrange" },
            ["CalamityMod/Dragonfruit"] = new[] { "CalamityMod/SacredStrawberry", "CalamityMod/Dragonfruit" }
        } ;

        internal static int Resolve(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName) || fullName == "0" || fullName == SupertableConstants.NULL_ITEM_KEY) return ItemID.None ;
            if (int.TryParse(fullName, out int vanilla)) return vanilla ;
            if (Aliases.TryGetValue(fullName, out string[] aliases)) {
                foreach (string alias in aliases) if (ModContent.TryFind<ModItem>(alias, out ModItem mapped)) return mapped.Type ;
            }
            if (ModContent.TryFind<ModItem>(fullName, out ModItem direct)) return direct.Type ;
            return ItemID.None ;
        }

        internal static bool ValidateRecipe(string[] values, out string missing)
        {
            missing = null ;
            if (values == null || values.Length != SupertableConstants.RECIPE_LENGTH) { missing = "length" ; return false ; }
            if (values[^1] == SupertableConstants.NULL_ITEM_KEY) { missing = "null recipe" ; return false ; }
            if (Resolve(values[^1]) == ItemID.None) { missing = values[^1] ; return false ; }
            for (int i = 0 ; i < SupertableConstants.TOTAL_SLOTS ; i++) {
                if (values[i] == "0" || values[i] == SupertableConstants.NULL_ITEM_KEY) continue ;
                if (Resolve(values[i]) == ItemID.None) { missing = values[i] ; return false ; }
            }
            return true ;
        }
    }
}
