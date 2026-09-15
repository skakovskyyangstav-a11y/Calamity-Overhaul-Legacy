using System.Linq ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Common
{
    internal static class LegacyKeybindText
    {
        internal static string WeaponSkillToken()
        {
            ModKeybind keybind = global::CalamityOverhaulLegacy.CalamityOverhaulLegacy.WeaponSkillKeybind ;

            if (keybind == null) {
                return "[未绑定按键][武器技能]" ;
            }

            string[] keys = keybind.GetAssignedKeys().ToArray() ;

            if (keys.Length == 0) {
                return "[未绑定按键][武器技能]" ;
            }

            return $"[{string.Join("/", keys)}]" ;
        }
    }
}
