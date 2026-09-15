using Microsoft.Xna.Framework.Graphics ;
using ReLogic.Content ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy
{
    internal static class CWRAsset
    {
        public static Asset<Texture2D> Placeholder_Transparent => ModContent.Request<Texture2D>(CWRConstant.Placeholder) ;
        public static Asset<Texture2D> Placeholder_White => ModContent.Request<Texture2D>(CWRConstant.Placeholder2) ;
        public static Asset<Texture2D> Placeholder_ERROR => ModContent.Request<Texture2D>(CWRConstant.Placeholder3) ;
        public static Asset<Texture2D> UI_JAR => ModContent.Request<Texture2D>(CWRConstant.UI + "JAR") ;
        public static Asset<Texture2D> Airflow => ModContent.Request<Texture2D>(CWRConstant.Masking + "Airflow") ;
        public static Asset<Texture2D> Extra_193 => ModContent.Request<Texture2D>(CWRConstant.Masking + "Extra_193") ;
        public static Asset<Texture2D> StarTexture_White => ModContent.Request<Texture2D>(CWRConstant.Masking + "StarTexture_White") ;
        public static Asset<Texture2D> StarTexture => ModContent.Request<Texture2D>(CWRConstant.Masking + "StarTexture") ;
        public static Asset<Texture2D> ThunderTrail => ModContent.Request<Texture2D>(CWRConstant.Masking + "ThunderTrail") ;
        public static Asset<Texture2D> Extra_98 => ModContent.Request<Texture2D>(CWRConstant.Masking + "Extra_98") ;
        public static Asset<Texture2D> SplitTrail => ModContent.Request<Texture2D>(CWRConstant.Masking + "SplitTrail") ;
        public static Asset<Texture2D> DarklightGreatsword_Bar => ModContent.Request<Texture2D>(CWRConstant.ColorBar + "DarklightGreatsword_Bar") ;
    }
}
