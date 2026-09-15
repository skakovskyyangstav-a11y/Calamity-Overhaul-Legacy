using CalamityOverhaulLegacy.Content.Items.Materials ;
using CalamityOverhaulLegacy.Content.Items.Tools ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using Terraria ;
using Terraria.ID ;
using Terraria.ModLoader ;
using Terraria.ObjectData ;

namespace CalamityOverhaulLegacy.Content.Tiles
{
    internal class InfiniteIngotTile : ModTile
    {
        public override string Texture => "CalamityOverhaulLegacy/Content/Tiles/InfiniteIngotTile" ;

        public override void SetStaticDefaults()
        {
            Main.tileShine[Type] = 1100 ;
            Main.tileSolid[Type] = true ;
            Main.tileSolidTop[Type] = true ;
            Main.tileFrameImportant[Type] = true ;
            MinPick = 9999 ;
            TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1) ;
            TileObjectData.newTile.StyleHorizontal = true ;
            TileObjectData.newTile.LavaDeath = false ;
            TileObjectData.addTile(Type) ;
            AddMapEntry(new Color(121, 89, 9), ModContent.GetInstance<InfiniteIngot>().DisplayName) ;
        }


        public override bool CanExplode(int i, int j) => false ;

        public override bool CanKillTile(int i, int j, ref bool blockDamaged)
        {
            // Server must accept the client's authorized tile kill packet.
            if (Main.netMode == NetmodeID.Server) {
                return true ;
            }

            Player player = Main.LocalPlayer ;

            return player != null
                && player.active
                && player.HeldItem.ModItem is InfinitePick ;
        }

        public override bool CanReplace(int i, int j, int tileTypeBeingPlaced)
        {
            if (Main.netMode == NetmodeID.Server) {
                return false ;
            }

            Player player = Main.LocalPlayer ;

            return player != null
                && player.active
                && player.HeldItem.ModItem is InfinitePick ;
        }

        public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j] ;
            Texture2D texture = ModContent.Request<Texture2D>(Texture).Value ;
            Vector2 offset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange) ;
            Vector2 drawPosition = new Vector2(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) + offset ;
            Color drawColor = InnoVault.VaultUtils.MultiStepColorLerp(
                Main.GameUpdateCount % 60 / 60f,
                Content.Items.Ranged.HeavenfallLongbows.HeavenfallLongbow.rainbowColors
            ) ;

            if (!tile.IsHalfBlock && tile.Slope == 0) {
                spriteBatch.Draw(texture, drawPosition, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), drawColor) ;
            }
            else if (tile.IsHalfBlock) {
                spriteBatch.Draw(texture, drawPosition + Vector2.UnitY * 8f, new Rectangle(tile.TileFrameX, tile.TileFrameY, 16, 16), drawColor) ;
            }
            return false ;
        }
    }
}
