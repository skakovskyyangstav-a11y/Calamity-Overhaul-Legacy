using CalamityOverhaulLegacy.Common ;
using Microsoft.Xna.Framework.Graphics ;
using Terraria.UI.Chat ;
using CalamityOverhaulLegacy.Common.Compatibility ;
using CalamityOverhaulLegacy.Common.LegacyTooltips ;
using CalamityOverhaulLegacy.Content.Tiles ;
using Terraria ;
using Terraria.DataStructures ;
using Terraria.ID ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Content.Items.Materials
{
    internal class InfiniteIngot : ModItem, ILegacyCategorizedItem
    {
        public override string Texture => "CalamityOverhaulLegacy/Content/Items/Materials/InfiniteIngot" ;
        public LegacyItemCategory Category => LegacyItemCategory.Material | LegacyItemCategory.Placeable ;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 64 ;
            ItemID.Sets.SortingPriorityMaterials[Type] = 114 ;
            ItemID.Sets.AnimatesAsSoul[Type] = true ;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(6, 16)) ;
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 25 ;
            Item.maxStack = 99 ;
            Item.material = true ;
            Item.rare = LegacyRarityHelper.HotPink ;
            Item.value = Item.sellPrice(gold: 99999) ;
            Item.useAnimation = Item.useTime = 15 ;
            Item.useStyle = ItemUseStyleID.Swing ;
            Item.consumable = true ;
            Item.useTurn = true ;
            Item.autoReuse = true ;
            Item.createTile = ModContent.TileType<InfiniteIngotTile>() ;
        }

        public static void DrawColorText(SpriteBatch sb, DrawableTooltipLine line)
        {
            Effect effect = EffectLoader.Crystal.Value ;

            if (effect is null) {
                return ;
            }

            Matrix projection = Matrix.CreateOrthographicOffCenter(
                0,
                Main.screenWidth,
                Main.screenHeight,
                0,
                -1,
                1
            ) ;

            Texture2D noiseTex = CWRAsset.SplitTrail.Value ;

            effect.Parameters["transformMatrix"].SetValue(projection) ;
            effect.Parameters["basePos"].SetValue(new Vector2(line.X, line.Y)) ;
            effect.Parameters["scale"].SetValue(new Vector2(1.2f / Main.GameZoomTarget)) ;
            effect.Parameters["uTime"].SetValue((float)Main.timeForVisualEffects * 0.02f) ;
            effect.Parameters["lightRange"].SetValue(0.15f) ;
            effect.Parameters["lightLimit"].SetValue(0.45f) ;
            effect.Parameters["addC"].SetValue(0.75f) ;
            effect.Parameters["highlightC"].SetValue(Color.White.ToVector4()) ;
            effect.Parameters["brightC"].SetValue(Main.DiscoColor.ToVector4()) ;
            effect.Parameters["darkC"].SetValue(Main.DiscoColor.ToVector4()) ;

            sb.End() ;
            sb.Begin(
                SpriteSortMode.Immediate,
                BlendState.AlphaBlend,
                SamplerState.PointWrap,
                DepthStencilState.Default,
                RasterizerState.CullNone,
                effect,
                Main.UIScaleMatrix
            ) ;

            Main.graphics.GraphicsDevice.Textures[1] = noiseTex ;

            ChatManager.DrawColorCodedStringWithShadow(
                Main.spriteBatch,
                line.Font,
                line.Text,
                new Vector2(line.X, line.Y),
                Color.White,
                line.Rotation,
                line.Origin,
                line.BaseScale,
                line.MaxWidth,
                line.Spread
            ) ;

            sb.End() ;
            sb.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointWrap,
                DepthStencilState.Default,
                RasterizerState.CullNone,
                null,
                Main.UIScaleMatrix
            ) ;
        }

    }
}
