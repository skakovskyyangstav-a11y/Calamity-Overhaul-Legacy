using CalamityOverhaulLegacy.Content ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Input ;
using System ;
using System.Collections.Generic ;
using System.Linq ;
using Terraria ;
using Terraria.ID ;
using Terraria.Localization ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy
{
    public static class CWRUtils
    {
        public const float atoR = MathHelper.Pi / 180f ;

        public static CWRPlayer CWR(this Player player)
        {
            return player.GetModPlayer<CWRPlayer>() ;
        }

        public static CWRItem CWR(this Item item)
        {
            return item.GetGlobalItem<CWRItem>() ;
        }

        public static void Initialize(this Item item)
        {
            if (item.CWR().ai == null) {
                item.CWR().ai = new float[] { 0f, 0f, 0f } ;
            }
        }

        public static void SetItemLegendContentTops(
            ref List<TooltipLine> tooltips,
            string itemKey
        )
        {
            TooltipLine legendtops = tooltips.FirstOrDefault(
                x => x.Text.Contains("[legend]") && x.Mod == "Terraria"
            ) ;

            if (legendtops == null) {
                return ;
            }

            KeyboardState state = Keyboard.GetState() ;

            if (state.IsKeyDown(Keys.LeftShift)
                || state.IsKeyDown(Keys.RightShift)) {
                legendtops.Text = Language.GetTextValue(
                    $"Mods.CalamityOverhaulLegacy.Items.{itemKey}.Legend"
                ) ;

                legendtops.OverrideColor = Color.Lerp(
                    Color.BlueViolet,
                    Color.White,
                    0.5f + (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.5f
                ) ;
            }
            else {
                legendtops.Text = Language.GetTextValue(
                    "Mods.CalamityOverhaulLegacy.Items.CWRItem.ItemLegendOnMouseLang"
                ) ;

                legendtops.OverrideColor = Color.Lerp(
                    Color.BlueViolet,
                    Color.Gold,
                    0.5f + (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.5f
                ) ;
            }
        }
    }
}
