using CalamityOverhaulLegacy.Content.UIs.SupertableUIs.Animation ;
using CalamityOverhaulLegacy.Content.UIs.SupertableUIs.Crafting ;
using CalamityOverhaulLegacy.Content.UIs.SupertableUIs.Inventory ;
using System.Collections.Generic ;
using Terraria ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs
{
    public class SupertableController
    {
        public ItemSlotManager SlotManager { get ; } = new() ;
        public RecipeMatchingEngine RecipeEngine { get ; } = new() ;
        public CraftingResultManager ResultManager { get ; } = new() ;
        public UIAnimationController AnimationController { get ; } = new() ;
        public void InitializeRecipes(IEnumerable<RecipeData> recipes){RecipeEngine.Clear();RecipeEngine.AddRecipes(recipes);}
        public void UpdateRecipeMatching(bool sync=true)
        {
            RecipeMatchResult match=RecipeEngine.MatchRecipe(SlotManager.GetAllItemTypes());
            if(match.IsMatch){ResultManager.SetResult(match.MatchedRecipe,SlotManager.GetMinimumStackSize());SlotManager.SetPreviewFromTypes(match.MatchedRecipe.MaterialTypesCache);}else ResultManager.ClearResult();
            if(sync)SupertableUI.SyncToNetworkIfNeeded();
        }
        public bool TryTakeResult(ref Item mouse){if(ResultManager.TryTakeResult(ref mouse,out int amount)){SlotManager.ConsumeItems(amount);UpdateRecipeMatching();return true;}return false;}
        public void UpdateAnimations(bool active,int hover){AnimationController.UpdateOpenAnimation(active);AnimationController.UpdateSlotHoverAnimation(hover);}
    }
}
