using Microsoft.Xna.Framework.Graphics ;
using ReLogic.Content ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Common
{
    public static class EffectLoader
    {
        public static Asset<Effect> GradientTrail =>
            ModContent.Request<Effect>(
                "CalamityOverhaulLegacy/Assets/Effects/GradientTrail",
                AssetRequestMode.ImmediateLoad
            ) ;

        public static Asset<Effect> Crystal =>
            ModContent.Request<Effect>(
                "CalamityOverhaulLegacy/Assets/Effects/Crystal",
                AssetRequestMode.ImmediateLoad
            ) ;
    }
}
