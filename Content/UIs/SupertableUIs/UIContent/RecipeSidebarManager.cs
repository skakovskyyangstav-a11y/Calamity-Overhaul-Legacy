using InnoVault ;
using InnoVault.UIHandles ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using Microsoft.Xna.Framework.Input ;
using System ;
using System.Collections.Generic ;
using Terraria ;
using Terraria.Audio ;
using Terraria.ID ;
using Terraria.GameInput ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs.UIContent
{
    internal class RecipeSidebarManager
    {
        private readonly SupertableUI ui;public List<RecipeTargetElement> RecipeElements{get;}=new();public RecipeTargetElement SelectedRecipe;private float scroll;private int height=448;private Rectangle hit;private MouseState old;
        private static Texture2D Jar=>ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/JAR").Value;
        public RecipeSidebarManager(SupertableUI ui){this.ui=ui;}
        public void InitializeRecipeElements(){RecipeElements.Clear();foreach(RecipeData r in SupertableUI.AllRecipes)RecipeElements.Add(new RecipeTargetElement{RecipeData=r});}
        public void Update(){Vector2 p=ui.DrawPosition+new Vector2(ui.UIHitBox.Width+18,8);hit=new((int)p.X-4,(int)p.Y,72,height);if(hit.Intersects(ui.MouseHitBox)){Main.LocalPlayer.mouseInterface=true;MouseState now=Mouse.GetState();scroll-=now.ScrollWheelValue-old.ScrollWheelValue;scroll=MathHelper.Clamp(scroll,0,Math.Max(0,RecipeElements.Count*64-height));scroll=((int)scroll/64)*64;old=now;PlayerInput.LockVanillaMouseScroll("CalamityOverhaulLegacy/SupertableSidebar");}for(int i=0;i<RecipeElements.Count;i++){RecipeElements[i].DrawPosition=p+new Vector2(4,i*64-scroll);RecipeElements[i].Update(ui,this);}}
        public bool Visible(RecipeTargetElement e)=>hit.Intersects(e.Hitbox);
        public void ScrollToRecipe(int index){scroll=MathHelper.Clamp(index*64-3*64,0,Math.Max(0,RecipeElements.Count*64-height));scroll=((int)scroll/64)*64;}
        public void Draw(SpriteBatch sb,float alpha){Vector2 p=ui.DrawPosition+new Vector2(ui.UIHitBox.Width+18,8);VaultUtils.DrawBorderedRectangle(sb,Jar,4,p,70,height,Color.AliceBlue*0.8f*alpha,Color.Transparent,1);VaultUtils.DrawBorderedRectangle(sb,Jar,4,p,70,height,Color.Transparent,Color.Azure*alpha,1);foreach(RecipeTargetElement e in RecipeElements)if(Visible(e))e.Draw(sb,alpha);}
    }
    internal class RecipeTargetElement
    {
        public RecipeData RecipeData;public Vector2 DrawPosition;private float scale=1f;private Color bg=Color.Azure*0.2f;private Rectangle hit;public Rectangle Hitbox=>hit;
        public void Update(SupertableUI ui,RecipeSidebarManager sidebar){hit=new((int)DrawPosition.X,(int)DrawPosition.Y,64,64);bool hover=sidebar.Visible(this)&&hit.Intersects(ui.MouseHitBox);float ts=1f;Color tc=Color.Azure*0.2f;if(hover){Main.LocalPlayer.mouseInterface=true;if(ui.keyLeftPressState==KeyPressState.Pressed){sidebar.SelectedRecipe=this;SoundEngine.PlaySound(SoundID.Grab with{Pitch=0.6f,Volume=0.8f});ui.RecipeNavigator.SetRecipeByData(RecipeData);}Item item=new(RecipeData.Target);Main.HoverItem=item;Main.hoverItemName=item.Name;ts=1.2f;tc=Color.LightGoldenrodYellow;}if(sidebar.SelectedRecipe==this){ts=1.2f;tc=Color.Gold;}scale=MathHelper.Lerp(scale,ts,0.1f);bg=Color.Lerp(bg,tc,0.1f);}
        public void Draw(SpriteBatch sb,float alpha){Texture2D jar=ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/JAR").Value;VaultUtils.DrawBorderedRectangle(sb,jar,4,DrawPosition,64,64,Color.AliceBlue*0.8f*alpha,bg*alpha,scale);SupertableUI.DrawItemIcon(sb,new Item(RecipeData.Target),DrawPosition+new Vector2(8,8),alpha,1.25f*scale);}
    }
}
