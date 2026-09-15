using CalamityOverhaulLegacy.Content.Items.Ranged.HeavenfallLongbows ;
using InnoVault ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using Terraria ;
using Terraria.GameContent ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Common.QoL
{
    internal class HeavenfallChargeBarGlobalItem : GlobalItem
    {
        public override bool IsLoadingEnabled(Mod mod) => false ;

        public override bool InstancePerEntity => true ;

        private float displayedCharge ;

        public override bool AppliesToEntity(Item entity, bool lateInstantiation)
        {
            return entity.ModItem is HeavenfallLongbow ;
        }

        public override void PostDrawInInventory(
            Item item,
            SpriteBatch spriteBatch,
            Vector2 position,
            Rectangle frame,
            Color drawColor,
            Color itemColor,
            Vector2 origin,
            float scale
        )
        {
            if (item.ModItem is not HeavenfallLongbow bow) {
                return ;
            }

            displayedCharge = MathHelper.Lerp(
                displayedCharge,
                bow.ChargeValue,
                0.10f
            ) ;

            if (System.Math.Abs(displayedCharge - bow.ChargeValue) < 0.02f) {
                displayedCharge = bow.ChargeValue ;
            }

            float progress = MathHelper.Clamp(
                displayedCharge / 200f,
                0f,
                1f
            ) ;

            const int width = 38 ;
            const int height = 4 ;

            int x = (int)position.X - width / 2 ;
            int y = (int)position.Y + 20 ;

            Color background = new Color(5, 5, 10, 225) ;
            DrawRounded(spriteBatch, new Rectangle(x - 1, y - 1, width + 2, height + 2), background) ;

            int fill = (int)(width * progress) ;

            if (fill <= 0) {
                return ;
            }

            Color color = VaultUtils.MultiStepColorLerp(
                (Main.GlobalTimeWrappedHourly * 0.18f) % 1f,
                HeavenfallLongbow.rainbowColors
            ) ;

            DrawRounded(
                spriteBatch,
                new Rectangle(x, y, fill, height),
                color
            ) ;
        }

        private static void DrawRounded(SpriteBatch spriteBatch, Rectangle rectangle, Color color)
        {
            if (rectangle.Width <= 0 || rectangle.Height <= 0) {
                return ;
            }

            Texture2D pixel = TextureAssets.MagicPixel.Value ;

            if (rectangle.Width <= 2 || rectangle.Height <= 2) {
                spriteBatch.Draw(pixel, rectangle, color) ;
                return ;
            }

            spriteBatch.Draw(
                pixel,
                new Rectangle(rectangle.X + 1, rectangle.Y, rectangle.Width - 2, rectangle.Height),
                color
            ) ;

            spriteBatch.Draw(
                pixel,
                new Rectangle(rectangle.X, rectangle.Y + 1, rectangle.Width, rectangle.Height - 2),
                color
            ) ;
        }
    }
}
