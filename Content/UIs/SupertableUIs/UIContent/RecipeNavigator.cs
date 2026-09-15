using InnoVault.UIHandles ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using System.Collections.Generic ;
using Terraria ;
using Terraria.Audio ;
using Terraria.GameContent ;
using Terraria.ID ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs.UIContent
{
    internal class RecipeNavigator
    {
        private readonly SupertableUI ui; private readonly SupertableController controller; private readonly List<RecipeData> recipes=new();
        private Rectangle main,right,left; private bool hm,hr,hl; public int CurrentIndex{get;private set;}
        private static Texture2D Book=>ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/SupertableUIs/RecPBook").Value;
        private static Texture2D Arrow=>ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/SupertableUIs/BlueArrow").Value;
        private static Texture2D Arrow2=>ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/SupertableUIs/BlueArrow2").Value;
        public RecipeNavigator(SupertableUI ui,SupertableController controller){this.ui=ui;this.controller=controller;}
        public void LoadAllRecipes(){recipes.Clear();recipes.AddRange(SupertableUI.AllRecipes);CurrentIndex=recipes.Count>0?0:-1;if(CurrentIndex>=0)LoadPreview();}
        public void Update(){Vector2 p=ui.DrawPosition+SupertableConstants.RECIPE_UI_OFFSET;main=new((int)p.X,(int)p.Y,Book.Width,Book.Height);right=new((int)p.X+62,(int)p.Y+20,25,25);left=new((int)p.X-30,(int)p.Y+20,25,25);Rectangle m=ui.MouseHitBox;hm=main.Intersects(m);hr=right.Intersects(m);hl=left.Intersects(m);if(hm||hr||hl)DragController.SetGlobalDontDragTime(2);if(ui.keyLeftPressState==KeyPressState.Pressed){if(hr){CurrentIndex++;Step();}else if(hl){CurrentIndex--;Step();}}}
        private void Step(){if(recipes.Count==0)return;if(CurrentIndex<0)CurrentIndex=recipes.Count-1;if(CurrentIndex>=recipes.Count)CurrentIndex=0;SoundEngine.PlaySound(SoundID.Chat with{Pitch=hr?0.6f:-0.5f});LoadPreview();SyncSidebar();}
        private void LoadPreview(){if(CurrentIndex<0||CurrentIndex>=recipes.Count)return;RecipeData r=recipes[CurrentIndex];for(int i=0;i<81;i++)controller.SlotManager.SetPreviewSlot(i,new Item(r.MaterialTypesCache[i]));}
        public void SetRecipeByData(RecipeData recipe){int i=recipes.IndexOf(recipe);if(i>=0){CurrentIndex=i;LoadPreview();}}
        private void SyncSidebar(){if(CurrentIndex>=0&&CurrentIndex<ui.SidebarManager.RecipeElements.Count){ui.SidebarManager.SelectedRecipe=ui.SidebarManager.RecipeElements[CurrentIndex];ui.SidebarManager.ScrollToRecipe(CurrentIndex);}}
        public void Draw(SpriteBatch sb,float alpha){if(CurrentIndex<0)return;Vector2 p=ui.DrawPosition+SupertableConstants.RECIPE_UI_OFFSET;sb.Draw(Book,p,Color.White*alpha);sb.Draw(hr?Arrow:Arrow2,p+new Vector2(62,20),Color.White*alpha);sb.Draw(hl?Arrow:Arrow2,p+new Vector2(-30,20),null,Color.White*alpha,0,Vector2.Zero,1,SpriteEffects.FlipHorizontally,0);RecipeData r=recipes[CurrentIndex];Item target=new(r.Target);SupertableUI.DrawItemIcon(sb,target,p+new Vector2(5,5),0.6f*alpha,1.5f);string idx=$"{CurrentIndex+1} -:- {recipes.Count}";Vector2 sz=FontAssets.MouseText.Value.MeasureString(idx);Utils.DrawBorderStringFourWay(sb,FontAssets.MouseText.Value,idx,p.X-sz.X/2+Book.Width/2,p.Y+65,Color.White*alpha,Color.Black*alpha,new Vector2(0.3f),0.8f);string text=$"{SupertableUI.RecipeViewLabel.Value}:{target.HoverName}";sz=FontAssets.MouseText.Value.MeasureString(text);Utils.DrawBorderStringFourWay(sb,FontAssets.MouseText.Value,text,p.X-sz.X/2+Book.Width/2,p.Y-25,Color.White*alpha,Color.Black*alpha,new Vector2(0.3f),0.8f);if(hm){Main.HoverItem=target.Clone();Main.hoverItemName=target.Name;}}
    }
}
