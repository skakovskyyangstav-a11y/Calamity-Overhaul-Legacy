using System.Collections.Generic ;
using Terraria ;
using Terraria.ID ;

namespace CalamityOverhaulLegacy
{
    internal static class CWRLoad
    {
        public static Dictionary<ushort, int> WallToItem { get ; private set ; }

        public static List<List<int>> AllBossSegmentLists { get ; private set ; }

        public static void Setup()
        {
            SetupWallToItem() ;
            SetupBossSegmentLists() ;
        }

        public static void Unload()
        {
            WallToItem = null ;
            AllBossSegmentLists = null ;
        }

        private static void SetupWallToItem()
        {
            WallToItem = new Dictionary<ushort, int>() ;

            foreach (KeyValuePair<int, Item> pair in ContentSamples.ItemsByType) {
                Item item = pair.Value ;

                if (item != null
                    && item.createWall > WallID.None
                    && item.createWall <= ushort.MaxValue) {
                    ushort wall = (ushort)item.createWall ;

                    if (!WallToItem.ContainsKey(wall)) {
                        WallToItem[wall] = item.type ;
                    }
                }
            }
        }

        private static List<int> Valid(params int[] values)
        {
            List<int> result = new() ;

            foreach (int value in values) {
                if (value > 0 && !result.Contains(value)) {
                    result.Add(value) ;
                }
            }

            return result ;
        }

        private static void SetupBossSegmentLists()
        {
            List<int> sepulcher = Valid(CWRID.NPC_SepulcherHead, CWRID.NPC_SepulcherBody, CWRID.NPC_SepulcherTail) ;
            List<int> stormWeaver = Valid(CWRID.NPC_StormWeaverHead, CWRID.NPC_StormWeaverBody, CWRID.NPC_StormWeaverTail) ;
            List<int> primordialWyrm = Valid(CWRID.NPC_PrimordialWyrmHead, CWRID.NPC_PrimordialWyrmBody, CWRID.NPC_PrimordialWyrmTail) ;
            List<int> perforatorLarge = Valid(CWRID.NPC_PerforatorHeadLarge, CWRID.NPC_PerforatorBodyLarge, CWRID.NPC_PerforatorTailLarge) ;
            List<int> perforatorMedium = Valid(CWRID.NPC_PerforatorHeadMedium, CWRID.NPC_PerforatorBodyMedium, CWRID.NPC_PerforatorTailMedium) ;
            List<int> armoredDigger = new() ;
            List<int> exoMech = Valid(CWRID.NPC_Apollo, CWRID.NPC_Artemis, CWRID.NPC_AresBody, CWRID.NPC_ThanatosHead, CWRID.NPC_ThanatosBody1, CWRID.NPC_ThanatosBody2, CWRID.NPC_ThanatosTail) ;
            List<int> devourer = Valid(CWRID.NPC_DevourerofGodsHead, CWRID.NPC_DevourerofGodsBody, CWRID.NPC_DevourerofGodsTail) ;
            List<int> desert = Valid(CWRID.NPC_DesertScourgeHead, CWRID.NPC_DesertScourgeBody, CWRID.NPC_DesertScourgeTail, CWRID.NPC_DesertNuisanceHead, CWRID.NPC_DesertNuisanceBody, CWRID.NPC_DesertNuisanceTail) ;
            List<int> astrum = Valid(CWRID.NPC_AstrumDeusHead, CWRID.NPC_AstrumDeusBody, CWRID.NPC_AstrumDeusTail) ;
            List<int> aquatic = Valid(CWRID.NPC_AquaticScourgeHead, CWRID.NPC_AquaticScourgeBody, CWRID.NPC_AquaticScourgeTail) ;
            List<int> eidolon = Valid(CWRID.NPC_EidolonWyrmHead, CWRID.NPC_EidolonWyrmBody, CWRID.NPC_EidolonWyrmBodyAlt, CWRID.NPC_EidolonWyrmTail) ;
            List<int> moonLord = Valid(NPCID.MoonLordFreeEye, NPCID.MoonLordCore, NPCID.MoonLordHand, NPCID.MoonLordHead, NPCID.MoonLordLeechBlob) ;
            List<int> eater = Valid(NPCID.EaterofWorldsHead, NPCID.EaterofWorldsBody, NPCID.EaterofWorldsTail) ;
            List<int> destroyer = Valid(NPCID.TheDestroyer, NPCID.TheDestroyerBody, NPCID.TheDestroyerTail) ;

            AllBossSegmentLists = new List<List<int>>() {
                sepulcher,
                stormWeaver,
                primordialWyrm,
                perforatorLarge,
                perforatorMedium,
                armoredDigger,
                exoMech,
                devourer,
                desert,
                astrum,
                aquatic,
                eidolon,
                moonLord,
                eater,
                destroyer
            } ;
        }
    }
}
