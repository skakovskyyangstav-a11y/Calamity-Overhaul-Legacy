using CalamityOverhaulLegacy.Content.UIs.SupertableUIs.Inventory ;
using InnoVault.UIHandles ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using Terraria ;
using Terraria.Audio ;
using Terraria.GameContent ;
using Terraria.ID ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs.UIContent
{
    internal class QuickActionsManager
    {
        private readonly SupertableUI ui;private readonly SupertableController controller;private Rectangle place,take,eye;private bool highlight;
        private static Texture2D One=>ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/SupertableUIs/OneClick").Value;
        private static Texture2D Two=>ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/SupertableUIs/TwoClick").Value;
        private static Texture2D Eye=>ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/SupertableUIs/Eye").Value;
        private static Texture2D Call=>ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/SupertableUIs/CallFull").Value;
        public QuickActionsManager(SupertableUI ui,SupertableController c){this.ui=ui;controller=c;}
        public void Update(){Vector2 p=ui.DrawPosition;place=new((int)(p.X+574),(int)(p.Y+330),34,34);take=new((int)(p.X+540),(int)(p.Y+330),34,34);eye=new((int)(p.X+460),(int)(p.Y+420),38,26);if(place.Intersects(ui.MouseHitBox)&&ui.keyLeftPressState==KeyPressState.Pressed){if(ItemInteractionHandler.TryQuickPlaceRecipe(controller.SlotManager.Slots,controller.SlotManager.PreviewSlots,ref Main.mouseItem,Main.LocalPlayer))controller.UpdateRecipeMatching();DragController.SetGlobalDontDragTime(2);}if(take.Intersects(ui.MouseHitBox)&&ui.keyLeftPressState==KeyPressState.Pressed){foreach(var pair in controller.SlotManager.GetNonEmptySlots()){Item item=pair.item.Clone();Main.LocalPlayer.QuickSpawnItem(Main.LocalPlayer.GetSource_Misc("LegacySupertableTakeAll"),item,item.stack);controller.SlotManager.ClearSlot(pair.index);}SoundEngine.PlaySound(SoundID.Grab);controller.UpdateRecipeMatching();DragController.SetGlobalDontDragTime(2);}if(eye.Intersects(ui.MouseHitBox)&&ui.keyLeftPressState==KeyPressState.Pressed){highlight=!highlight;SoundEngine.PlaySound(SoundID.Unlock with{Pitch=highlight?0.5f:-0.5f});}}
        public void Draw(SpriteBatch sb,float alpha){Vector2 p=ui.DrawPosition;sb.Draw(One,p+new Vector2(574,330),Color.White*alpha);sb.Draw(Two,p+new Vector2(540,330),Color.White*alpha);int fh=Eye.Height/2;sb.Draw(Eye,p+new Vector2(460,420),new Rectangle(0,highlight?fh:0,Eye.Width,fh),Color.White*alpha);if(highlight){for(int i=0;i<81;i++){Item a=controller.SlotManager.GetSlot(i),b=controller.SlotManager.GetPreviewSlot(i);if(!a.IsAir&&!b.IsAir&&a.type!=b.type)sb.Draw(Call,ui.ArcCellPos(i)+new Vector2(-1,0),Color.White*0.6f*alpha);}}if(place.Intersects(ui.MouseHitBox))Utils.DrawBorderString(sb,SupertableUI.QuickPlaceMaterials.Value,new Vector2(place.X-30,place.Y+32),Color.White*alpha,0.8f);if(take.Intersects(ui.MouseHitBox))Utils.DrawBorderString(sb,SupertableUI.QuickTakeMaterials.Value,new Vector2(take.X-30,take.Y+32),Color.White*alpha,0.8f);if(eye.Intersects(ui.MouseHitBox))Utils.DrawBorderString(sb,(highlight?SupertableUI.PlacementMonitorOff:SupertableUI.PlacementMonitorOn).Value,new Vector2(eye.X-30,eye.Y+30),Color.White*alpha,0.8f);}
    }
}
