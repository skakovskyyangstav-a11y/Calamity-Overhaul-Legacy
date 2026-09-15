using Microsoft.Xna.Framework ;

namespace CalamityOverhaulLegacy.Common
{
    internal static class LegacyVisualColor
    {
        internal static readonly Color[] RainbowColors = new Color[] {
            Color.Red,
            Color.Orange,
            Color.Yellow,
            Color.Green,
            Color.Blue,
            Color.Indigo,
            Color.Violet
        } ;

        internal static Color MultiStep(float progress, Color[] colors)
        {
            if (colors == null || colors.Length == 0) {
                return Color.White ;
            }

            progress -= (float)System.Math.Floor(progress) ;
            if (progress < 0f) {
                progress += 1f ;
            }

            float scaled = progress * colors.Length ;
            int index = (int)scaled % colors.Length ;
            int next = (index + 1) % colors.Length ;
            return Color.Lerp(colors[index], colors[next], scaled - (float)System.Math.Floor(scaled)) ;
        }

        internal static Color Rainbow(float progress) => MultiStep(progress, RainbowColors) ;
    }
}
