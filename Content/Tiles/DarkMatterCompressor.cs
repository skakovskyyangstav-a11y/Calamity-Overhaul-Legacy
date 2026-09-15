using CalamityOverhaulLegacy.Common.Compatibility ;
using CalamityOverhaulLegacy.Content.Items.Placeable ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using Terraria ;
using Terraria.DataStructures ;
using Terraria.Enums ;
using Terraria.GameContent.ObjectInteractions ;
using Terraria.ID ;
using Terraria.ModLoader ;
using Terraria.ObjectData ;

namespace CalamityOverhaulLegacy.Content.Tiles
{
    internal class DarkMatterCompressor : ModTile
    {
        public override string Texture => "CalamityOverhaulLegacy/Content/Tiles/DarkMatterCompressor" ;
        public const int Width = 4 ;
        public const int Height = 3 ;
        private const int FrameHeight = 56 ;

        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true ;
            Main.tileFrameImportant[Type] = true ;
            Main.tileNoAttach[Type] = true ;
            Main.tileLavaDeath[Type] = false ;
            Main.tileWaterDeath[Type] = false ;
            AnimationFrameHeight = FrameHeight ;
            AddMapEntry(new Color(67, 72, 81), ModContent.GetInstance<DarkMatterCompressorItem>().DisplayName) ;

            AdjTiles = LegacyExternalContent.ExistingTiles(
                "CalamityMod/StaticRefiner",
                "CalamityMod/ProfanedCrucible",
                "CalamityMod/PlagueInfuser",
                "CalamityMod/MonolithAmalgam",
                "CalamityMod/VoidCondenser"
            ) ;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x4) ;
            TileObjectData.newTile.Width = Width ;
            TileObjectData.newTile.Height = Height ;
            TileObjectData.newTile.Origin = new Point16(1, 1) ;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, Width, 0) ;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 } ;
            TileObjectData.newTile.LavaDeath = false ;
            TileObjectData.addTile(Type) ;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++ ;
            if (frameCounter >= 8) {
                frameCounter = 0 ;
                frame = (frame + 1) % 4 ;
            }
        }

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer ;
            player.noThrow = 2 ;
            player.mouseInterface = true ;
            player.cursorItemIconEnabled = true ;
            player.cursorItemIconID = ModContent.ItemType<DarkMatterCompressorItem>() ;
        }

        public override bool RightClick(int i, int j) => true ;
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true ;
        public override bool CanExplode(int i, int j) => false ;

        public override bool CreateDust(int i, int j, ref int type)
        {
            Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, DustID.Electric) ;
            return false ;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j] ;
            Texture2D glow = ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Content/Tiles/DarkMatterCompressorGlow").Value ;
            Vector2 offset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange) ;
            Vector2 pos = new(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) ;
            float pulse = 0.35f + 0.35f * (float)System.Math.Abs(System.Math.Sin(Main.GameUpdateCount * 0.04f)) ;
            Rectangle source = new(tile.TileFrameX, tile.TileFrameY % FrameHeight, 16, 16) ;
            spriteBatch.Draw(glow, pos + offset, source, Color.AliceBlue * pulse) ;
        }
    }
}
