using System.Collections.Generic ;
using System.Linq ;
using Terraria.ID ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs
{
    public readonly struct RecipeMatchResult
    {
        public readonly bool IsMatch ; public readonly RecipeData MatchedRecipe ;
        private RecipeMatchResult(bool match, RecipeData recipe) { IsMatch = match ; MatchedRecipe = recipe ; }
        public static RecipeMatchResult Success(RecipeData recipe) => new(true, recipe) ;
        public static RecipeMatchResult NoMatch() => new(false, null) ;
    }

    public class RecipeMatchingEngine
    {
        private readonly List<RecipeData> recipes = new() ;
        public IReadOnlyList<RecipeData> AllRecipes => recipes ;
        public void Clear() => recipes.Clear() ;
        public void AddRecipes(IEnumerable<RecipeData> source) { if (source != null) recipes.AddRange(source.Where(r => r != null)) ; }
        public RecipeMatchResult MatchRecipe(int[] materialTypes)
        {
            if (materialTypes == null || materialTypes.Length != SupertableConstants.TOTAL_SLOTS) return RecipeMatchResult.NoMatch() ;
            foreach (RecipeData recipe in recipes) {
                if (recipe.MaterialTypesCache == null || recipe.MaterialTypesCache.Length != SupertableConstants.TOTAL_SLOTS) recipe.BuildMaterialTypesCache() ;
                bool match = true ;
                for (int i = 0 ; i < SupertableConstants.TOTAL_SLOTS ; i++) if (materialTypes[i] != recipe.MaterialTypesCache[i]) { match = false ; break ; }
                if (match) return RecipeMatchResult.Success(recipe) ;
            }
            return RecipeMatchResult.NoMatch() ;
        }
    }
}
