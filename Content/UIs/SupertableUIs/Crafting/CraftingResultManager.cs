using System ;
using Terraria ;
using Terraria.ID ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs.Crafting
{
    public class CraftingResultManager
    {
        private Item result=new Item() ; private RecipeData current ;
        public Item ResultItem=>result; public bool HasResult=>!result.IsAir; public RecipeData CurrentRecipe=>current;
        public void SetResult(RecipeData recipe,int amount){if(recipe==null||recipe.Target==ItemID.None){ClearResult();return;}current=recipe;result=new Item(recipe.Target);result.stack=Math.Min(amount,result.maxStack);}
        public void ClearResult(){result=new Item();current=null;}
        public bool TryTakeResult(ref Item mouse,out int amount)
        {
            amount=0;if(!HasResult)return false;
            if(mouse.IsAir){mouse=result.Clone();amount=result.stack;ClearResult();return true;}
            if(mouse.type==result.type&&mouse.prefix==result.prefix&&mouse.stack<mouse.maxStack){int move=Math.Min(result.stack,mouse.maxStack-mouse.stack);mouse.stack+=move;amount=move;ClearResult();return true;}return false;
        }
    }
}
