using CalamityOverhaulLegacy.Content.Tiles ;
using CalamityOverhaulLegacy.Content.UIs.SupertableUIs ;
using System.Collections.Generic ;
using System.Reflection ;
using Terraria ;
using Terraria.Localization ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Common.LegacyRecipes
{
    internal class LegacySupertableRecipeBrowserSystem : ModSystem
    {
        public override void AddRecipes()
        {
            LocalizedText browserOnly = Language.GetText(
                "Mods.CalamityOverhaulLegacy.Common.SupertableBrowserOnly"
            ) ;

            FieldInfo[] fields = typeof(SupertableRecipeData).GetFields(
                BindingFlags.Public
                | BindingFlags.Static
            ) ;

            int registered = 0 ;

            foreach (FieldInfo field in fields) {
                if (field.FieldType != typeof(string[])
                    || field.GetValue(null) is not string[] values
                    || !LegacyItemResolver.ValidateRecipe(values, out _)) {
                    continue ;
                }

                int resultType = LegacyItemResolver.Resolve(values[^1]) ;

                if (resultType <= 0) {
                    continue ;
                }

                Dictionary<int, int> ingredients = new() ;

                for (int slot = 0 ; slot < SupertableConstants.TOTAL_SLOTS ; slot++) {
                    int type = LegacyItemResolver.Resolve(values[slot]) ;

                    if (type <= 0) {
                        continue ;
                    }

                    ingredients.TryGetValue(type, out int oldCount) ;
                    ingredients[type] = oldCount + 1 ;
                }

                Recipe recipe = Recipe.Create(resultType) ;

                foreach (KeyValuePair<int, int> pair in ingredients) {
                    recipe.AddIngredient(pair.Key, pair.Value) ;
                }

                recipe.AddTile(
                    ModContent.TileType<TransmutationOfMatter>()
                ) ;

                // Indexing mirror only. The real position-sensitive craft
                // remains exclusive to the restored 9x9 Supertable UI.
                recipe.AddCondition(
                    browserOnly,
                    () => false
                ) ;

                recipe.DisableDecraft() ;
                recipe.Register() ;
                registered++ ;
            }

            Mod.Logger.Info(
                $"Registered {registered} read-only Supertable recipe mirrors for recipe browsers."
            ) ;
        }
    }
}
