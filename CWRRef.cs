using CalamityOverhaulLegacy.Common ;
using Terraria.Audio ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy
{
    internal static class CWRRef
    {
        public static SoundStyle GetSound(
            this string path,
            SoundStyle backupSound = default
        )
        {
            if (ModContent.HasAsset(path)) {
                return new SoundStyle(path) ;
            }

            if (backupSound == default) {
                backupSound = CWRSound.None ;
            }

            return backupSound ;
        }
    }
}
