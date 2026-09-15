using System.Collections.Generic ;
using Terraria ;
using Terraria.ID ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Common
{
    internal static class LegacyNpcGroupHelper
    {
        private static List<HashSet<int>> segmentGroups ;

        internal static int GetAnchorIndex(NPC npc)
        {
            if (npc == null || !npc.active) {
                return -1 ;
            }

            int realLife = npc.realLife ;
            if (realLife >= 0 && realLife < Main.maxNPCs && Main.npc[realLife].active) {
                return realLife ;
            }

            return npc.whoAmI ;
        }

        internal static void CollectGroup(NPC root, List<NPC> output)
        {
            output.Clear() ;

            if (root == null || !root.active) {
                return ;
            }

            EnsureGroups() ;

            int anchor = GetAnchorIndex(root) ;
            HashSet<int> segmentGroup = FindSegmentGroup(root.type) ;

            for (int i = 0 ; i < Main.maxNPCs ; i++) {
                NPC npc = Main.npc[i] ;
                if (!npc.active) {
                    continue ;
                }

                if (GetAnchorIndex(npc) == anchor || (segmentGroup != null && segmentGroup.Contains(npc.type))) {
                    output.Add(npc) ;
                }
            }
        }

        private static HashSet<int> FindSegmentGroup(int type)
        {
            for (int i = 0 ; i < segmentGroups.Count ; i++) {
                if (segmentGroups[i].Contains(type)) {
                    return segmentGroups[i] ;
                }
            }

            return null ;
        }

        private static void EnsureGroups()
        {
            if (segmentGroups != null) {
                return ;
            }

            segmentGroups = new List<HashSet<int>>() {
                new HashSet<int>() {
                    NPCID.MoonLordFreeEye,
                    NPCID.MoonLordCore,
                    NPCID.MoonLordHand,
                    NPCID.MoonLordHead,
                    NPCID.MoonLordLeechBlob
                },
                new HashSet<int>() {
                    NPCID.EaterofWorldsHead,
                    NPCID.EaterofWorldsBody,
                    NPCID.EaterofWorldsTail
                },
                new HashSet<int>() {
                    NPCID.TheDestroyer,
                    NPCID.TheDestroyerBody,
                    NPCID.TheDestroyerTail
                }
            } ;

            if (!ModLoader.TryGetMod("CalamityMod", out Mod calamity)) {
                return ;
            }

            string[][] names = new string[][] {
                new string[] { "SepulcherHead", "SepulcherBody", "SepulcherTail" },
                new string[] { "StormWeaverHead", "StormWeaverBody", "StormWeaverTail" },
                new string[] { "PrimordialWyrmHead", "PrimordialWyrmBody", "PrimordialWyrmTail" },
                new string[] { "PerforatorHeadLarge", "PerforatorBodyLarge", "PerforatorTailLarge" },
                new string[] { "PerforatorHeadMedium", "PerforatorBodyMedium", "PerforatorTailMedium" },
                new string[] { "Apollo", "Artemis", "AresBody", "ThanatosHead", "ThanatosBody1", "ThanatosBody2", "ThanatosTail" },
                new string[] { "DevourerofGodsHead", "DevourerofGodsBody", "DevourerofGodsTail" },
                new string[] { "DesertScourgeHead", "DesertScourgeBody", "DesertScourgeTail", "DesertNuisanceHead", "DesertNuisanceBody", "DesertNuisanceTail" },
                new string[] { "AstrumDeusHead", "AstrumDeusBody", "AstrumDeusTail" },
                new string[] { "AquaticScourgeHead", "AquaticScourgeBody", "AquaticScourgeTail" },
                new string[] { "EidolonWyrmHead", "EidolonWyrmBody", "EidolonWyrmBodyAlt", "EidolonWyrmTail" }
            } ;

            foreach (string[] groupNames in names) {
                HashSet<int> group = new() ;

                foreach (string name in groupNames) {
                    if (calamity.TryFind(name, out ModNPC modNpc)) {
                        group.Add(modNpc.Type) ;
                    }
                }

                if (group.Count > 0) {
                    segmentGroups.Add(group) ;
                }
            }
        }
    }
}
