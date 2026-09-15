using CalamityOverhaulLegacy.Content.UIs.SupertableUIs ;
using CalamityOverhaulLegacy.Content.TileEntities ;
using CalamityOverhaulLegacy.Common.Compatibility ;
using CalamityOverhaulLegacy.Content.Items.Placeable ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using Terraria ;
using Terraria.Audio ;
using Terraria.DataStructures ;
using Terraria.Enums ;
using Terraria.GameContent.ObjectInteractions ;
using Terraria.ID ;
using Terraria.Localization ;
using Terraria.ModLoader ;
using Terraria.ObjectData ;
using System.Collections.Generic ;

namespace CalamityOverhaulLegacy.Content.Tiles
{
    internal class TransmutationOfMatter : ModTile
    {
        public override string Texture => "CalamityOverhaulLegacy/Content/Tiles/TransmutationOfMatter" ;
        public const int Width = 5 ;
        public const int Height = 3 ;
        private const int FrameHeight = 54 ;

        public override void SetStaticDefaults()
        {
            Main.tileLighted[Type] = true ;
            Main.tileFrameImportant[Type] = true ;
            Main.tileNoAttach[Type] = true ;
            Main.tileLavaDeath[Type] = false ;
            Main.tileWaterDeath[Type] = false ;
            AnimationFrameHeight = FrameHeight ;
            AddMapEntry(new Color(67, 72, 81), ModContent.GetInstance<TransmutationOfMatterItem>().DisplayName) ;

            List<int> adj = new() {
                TileID.WorkBenches, TileID.Chairs, TileID.Tables, TileID.Anvils, TileID.MythrilAnvil,
                TileID.Furnaces, TileID.Hellforge, TileID.AdamantiteForge, TileID.TinkerersWorkbench,
                TileID.LunarCraftingStation, TileID.DemonAltar, ModContent.TileType<DarkMatterCompressor>()
            } ;
            foreach (int tile in LegacyExternalContent.ExistingTiles(
                "CalamityMod/CosmicAnvil", "CalamityMod/SCalAltarLarge", "CalamityMod/AncientAltar",
                "CalamityMod/AshenAltar", "CalamityMod/BotanicPlanter", "CalamityMod/EutrophicShelf",
                "CalamityMod/MonolithAmalgam", "CalamityMod/VoidCondenser", "CalamityMod/WulfrumLabstation",
                "CalamityMod/StaticRefiner", "CalamityMod/ProfanedCrucible", "CalamityMod/PlagueInfuser",
                "CalamityMod/DraedonsForge")) {
                adj.Add(tile) ;
            }
            AdjTiles = adj.ToArray() ;

            TileObjectData.newTile.CopyFrom(TileObjectData.Style3x3) ;
            TileObjectData.newTile.Width = Width ;
            TileObjectData.newTile.Height = Height ;
            TileObjectData.newTile.Origin = new Point16(1, 1) ;
            TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop | AnchorType.SolidSide, Width, 0) ;
            TileObjectData.newTile.CoordinateHeights = new int[] { 16, 16, 16 } ;
            TileObjectData.newTile.LavaDeath = false ;
            TileObjectData.newTile.HookPostPlaceMyPlayer = new PlacementHook(ModContent.GetInstance<LegacyTransmutationEntity>().Hook_AfterPlacement, -1, 0, false) ;
            TileObjectData.addTile(Type) ;
            RegisterItemDrop(ModContent.ItemType<TransmutationOfMatterItem>()) ;
        }

        public override void AnimateTile(ref int frame, ref int frameCounter)
        {
            frameCounter++ ;
            if (frameCounter >= 5) {
                frameCounter = 0 ;
                frame = (frame + 1) % 11 ;
            }
        }

        public override bool CanExplode(int i, int j) => false ;
        public override bool HasSmartInteract(int i, int j, SmartInteractScanSettings settings) => true ;

        public override void MouseOver(int i, int j)
        {
            Player player = Main.LocalPlayer ;
            player.noThrow = 2 ;
            player.cursorItemIconEnabled = true ;
            player.cursorItemIconID = ModContent.ItemType<TransmutationOfMatterItem>() ;
        }

        public override bool RightClick(int i, int j)
        {
            Point16 topLeft = GetTopLeft(i, j) ;

            if (!LegacyTransmutationEntity.TryGet(topLeft, out LegacyTransmutationEntity entity)) {
                if (Main.netMode == NetmodeID.MultiplayerClient) {
                    NetMessage.SendData(
                        MessageID.TileEntityPlacement,
                        -1,
                        -1,
                        null,
                        topLeft.X,
                        topLeft.Y,
                        ModContent.GetInstance<LegacyTransmutationEntity>().Type
                    ) ;

                    SoundEngine.PlaySound(SoundID.MenuTick) ;
                    return true ;
                }

                int entityId = ModContent.GetInstance<LegacyTransmutationEntity>()
                    .Place(topLeft.X, topLeft.Y) ;

                if (entityId >= 0
                    && TileEntity.ByID.TryGetValue(entityId, out TileEntity tileEntity)
                    && tileEntity is LegacyTransmutationEntity createdEntity) {
                    entity = createdEntity ;
                }
            }

            if (entity != null) {
                SupertableUI.Open(entity) ;
                return true ;
            }

            ModContent.GetInstance<global::CalamityOverhaulLegacy.CalamityOverhaulLegacy>()
                .Logger.Warn($"Failed to bind LegacyTransmutationEntity at {topLeft.X},{topLeft.Y}.") ;

            SoundEngine.PlaySound(SoundID.MenuClose) ;
            return true ;
        }

        internal static Point16 GetTopLeft(int i, int j)
        {
            Tile tile = Framing.GetTileSafely(i, j) ;
            int localX = (tile.TileFrameX / 18) % Width ;
            int localY = (tile.TileFrameY / 18) % Height ;
            return new Point16(i - localX, j - localY) ;
        }

        public override void KillMultiTile(int i, int j, int frameX, int frameY)
        {
            int localX = (frameX / 18) % Width ;
            int localY = (frameY / 18) % Height ;
            Point16 topLeft = new Point16(i - localX, j - localY) ;
            if (LegacyTransmutationEntity.TryGet(topLeft, out LegacyTransmutationEntity entity)) {
                entity.DropAll() ;
                ModContent.GetInstance<LegacyTransmutationEntity>().Kill(topLeft.X, topLeft.Y) ;
            }
        }

        public override bool CreateDust(int i, int j, ref int type)
        {
            Dust.NewDust(new Vector2(i, j) * 16f, 16, 16, DustID.Electric) ;
            return false ;
        }

        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
            Tile tile = Main.tile[i, j] ;
            Texture2D glow = ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Content/Tiles/TransmutationOfMatterGlow").Value ;
            Vector2 offset = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange) ;
            Vector2 pos = new(i * 16 - Main.screenPosition.X, j * 16 - Main.screenPosition.Y) ;
            float pulse = 0.45f + 0.45f * (float)System.Math.Abs(System.Math.Sin(Main.GameUpdateCount * 0.03f)) ;
            Rectangle source = new(tile.TileFrameX, tile.TileFrameY % FrameHeight, 16, 16) ;
            spriteBatch.Draw(glow, pos + offset, source, Color.White * pulse) ;
        }
    }
}
