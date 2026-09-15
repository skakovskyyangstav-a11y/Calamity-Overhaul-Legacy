using CalamityOverhaulLegacy.Content.TileEntities ;
using CalamityOverhaulLegacy.Common ;
using InnoVault ;
using System.Collections.Generic ;
using System.IO ;
using Terraria ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy
{
    internal enum LegacyPacketType : byte { SupertableItems = 1 }

    public class CalamityOverhaulLegacy : Mod
    {
        public static ModKeybind WeaponSkillKeybind { get ; private set ; }

        internal static List<ICWRLoader> ILoaders { get ; private set ; } = new() ;

        public override void Load()
        {
            // Match the original 0.9025 keybind identity.
            WeaponSkillKeybind = KeybindLoader.RegisterKeybind(
                this,
                "WeponSkill_Q",
                "Q"
            ) ;

            ILoaders = VaultUtils.GetDerivedInstances<ICWRLoader>() ;

            foreach (ICWRLoader loader in ILoaders) {
                loader.LoadData() ;
            }
        }

        public override void PostSetupContent()
        {
            // Validate compiled Effects at mod-load time rather than on the first shot.
            if (!Main.dedServ) {
                _ = EffectLoader.GradientTrail.Value ;
                _ = EffectLoader.Crystal.Value ;
            }

            CWRLoad.Setup() ;
            CWRID.PreloadAll() ;

            foreach (ICWRLoader loader in ILoaders) {
                loader.SetupData() ;

                if (!Main.dedServ) {
                    loader.LoadAsset() ;
                }
            }
        }

        public override void Unload()
        {
            for (int i = ILoaders.Count - 1 ; i >= 0 ; i--) {
                ILoaders[i].UnLoadData() ;
            }

            ILoaders.Clear() ;
            WeaponSkillKeybind = null ;
            CWRID.Unload() ;
            CWRLoad.Unload() ;
        }

        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            LegacyPacketType type = (LegacyPacketType)reader.ReadByte() ;

            if (type == LegacyPacketType.SupertableItems) {
                LegacyTransmutationEntity.ReceiveSync(reader, whoAmI) ;
            }
        }
    }
}
