using System.Collections.Generic ;
using Terraria ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Common.LegacyRecipes
{
    internal class LegacySupertableRecipeOverrideSystem : ModSystem
    {
        private static readonly string[] OldSupertableCwoOutputs = new string[] {
            "CalamityOverhaul/NeutronStarIngot",
            "CalamityOverhaul/DawnshatterAzure",
            "CalamityOverhaul/SpearOfLonginus",
            "CalamityOverhaul/DragonsWord",
            "CalamityOverhaul/AnnihilatingUniverse",
            "CalamityOverhaul/NeutronGlaive",
            "CalamityOverhaul/NeutronGun",
            "CalamityOverhaul/NeutronBow",
            "CalamityOverhaul/NeutronWand",
            "CalamityOverhaul/NeutronScythe",
            "CalamityOverhaul/EyeOfSingularity",
            "CalamityOverhaul/EmblemOfDread",
            "CalamityOverhaul/ArcaneThroneOfEternity",
            "CalamityOverhaul/CreativeUEPipeline"
        } ;

        public override void PostAddRecipes()
        {
            HashSet<int> protectedOutputs = new() ;

            foreach (string fullName in OldSupertableCwoOutputs) {
                if (ModContent.TryFind<ModItem>(fullName, out ModItem item)) {
                    protectedOutputs.Add(item.Type) ;
                }
            }

            if (protectedOutputs.Count == 0) {
                return ;
            }

            int disabledCount = 0 ;

            foreach (Recipe recipe in Main.recipe) {
                if (recipe == null
                    || recipe.createItem == null
                    || recipe.createItem.IsAir
                    || !protectedOutputs.Contains(recipe.createItem.type)
                    || recipe.Mod?.Name != "CalamityOverhaul") {
                    continue ;
                }

                recipe.DisableRecipe() ;
                disabledCount++ ;
            }

            Mod.Logger.Info(
                $"Legacy Supertable override: disabled {disabledCount} normal recipes " +
                $"for {protectedOutputs.Count} restored 0.9025 Supertable outputs."
            ) ;
        }
    }
}
