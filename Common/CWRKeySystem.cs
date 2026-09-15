using Terraria.Localization ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Common
{
    internal class CWRKeySystem : ModSystem, ILocalizedModType
    {
        public string LocalizationCategory => "Keybinds" ;

        public static LocalizedText Notbound { get ; private set ; }

        public static ModKeybind WeponSkill_Q =>
            global::CalamityOverhaulLegacy.CalamityOverhaulLegacy.WeaponSkillKeybind ;

        public override void SetStaticDefaults()
        {
            Notbound = this.GetLocalization(
                nameof(Notbound),
                () => "[未绑定按键]"
            ) ;
        }
    }
}
