using InnoVault.UIHandles ;
using Microsoft.Xna.Framework ;
using Terraria ;
using Terraria.ID ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs.UIContent
{
    internal class DragController
    {
        private readonly SupertableUI ui ; private static int noDrag ; private Vector2 offset ; private bool dragging ;
        public bool IsDragging=>dragging;
        public DragController(SupertableUI ui){this.ui=ui;}
        public void Update()
        {
            if(noDrag>0)noDrag--; bool hover=ui.hoverInMainPage;
            if(Main.mouseItem.type>ItemID.None&&ui.HoverInPutItemCellPage){noDrag=2;dragging=false;return;}
            if(noDrag<=0&&hover&&ui.keyLeftPressState==KeyPressState.Pressed&&!dragging){dragging=true;offset=ui.MousePosition-ui.DrawPosition;}
            if(dragging){if(ui.keyLeftPressState==KeyPressState.Released)dragging=false;else ui.DrawPosition=Clamp(ui.MousePosition-offset);}
        }
        private Vector2 Clamp(Vector2 pos)=>new(MathHelper.Clamp(pos.X,0,Main.screenWidth-ui.Texture.Width),MathHelper.Clamp(pos.Y,0,Main.screenHeight-ui.Texture.Height));
        public void SetDontDragTime(int f)=>noDrag=f; public static void SetGlobalDontDragTime(int f)=>noDrag=f;
    }
}
