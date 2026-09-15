using System.Collections.Generic ;
using Terraria.ID ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy
{
    internal static class CWRID
    {
        private static readonly Dictionary<string, int> ItemCache = new() ;
        private static readonly Dictionary<string, int> NPCCache = new() ;

        public static void PreloadAll()
        {
            _ = Item_Alluvion ;
            _ = Item_ArterialAssault ;
            _ = Item_AstrealDefeat ;
            _ = Item_Barinade ;
            _ = Item_Barinautical ;
            _ = Item_BlossomFlux ;
            _ = Item_BrimstoneFury ;
            _ = Item_ClockworkBow ;
            _ = Item_Contagion ;
            _ = Item_CorrodedCaustibow ;
            _ = Item_ContinentalGreatbow ;
            _ = Item_DaemonsFlame ;
            _ = Item_DarkechoGreatbow ;
            _ = Item_Deathwind ;
            _ = Item_Drataliornus ;
            _ = Item_FlarewingBow ;
            _ = Item_Galeforce ;
            _ = Item_Goobow ;
            _ = Item_HeavenlyGale ;
            _ = Item_HoarfrostBow ;
            _ = Item_LunarianBow ;
            _ = Item_Malevolence ;
            _ = Item_MarksmanBow ;
            _ = Item_Monsoon ;
            _ = Item_NettlevineGreatbow ;
            _ = Item_Phangasm ;
            _ = Item_PlanetaryAnnihilation ;
            _ = Item_Shellshooter ;
            _ = Item_TelluricGlare ;
            _ = Item_TheBallista ;
            _ = Item_TheMaelstrom ;
            _ = Item_Ultima ;
            _ = Item_Toxibow ;
            _ = Item_VernalBolter ;
            _ = NPC_SepulcherHead ;
            _ = NPC_SepulcherBody ;
            _ = NPC_SepulcherTail ;
            _ = NPC_StormWeaverHead ;
            _ = NPC_StormWeaverBody ;
            _ = NPC_StormWeaverTail ;
            _ = NPC_PrimordialWyrmHead ;
            _ = NPC_PrimordialWyrmBody ;
            _ = NPC_PrimordialWyrmTail ;
            _ = NPC_PerforatorHeadLarge ;
            _ = NPC_PerforatorBodyLarge ;
            _ = NPC_PerforatorTailLarge ;
            _ = NPC_PerforatorHeadMedium ;
            _ = NPC_PerforatorBodyMedium ;
            _ = NPC_PerforatorTailMedium ;
            _ = NPC_Apollo ;
            _ = NPC_Artemis ;
            _ = NPC_AresBody ;
            _ = NPC_ThanatosHead ;
            _ = NPC_ThanatosBody1 ;
            _ = NPC_ThanatosBody2 ;
            _ = NPC_ThanatosTail ;
            _ = NPC_AresLaserCannon ;
            _ = NPC_AresPlasmaFlamethrower ;
            _ = NPC_AresTeslaCannon ;
            _ = NPC_AresGaussNuke ;
            _ = NPC_DevourerofGodsHead ;
            _ = NPC_DevourerofGodsBody ;
            _ = NPC_DevourerofGodsTail ;
            _ = NPC_DesertScourgeHead ;
            _ = NPC_DesertScourgeBody ;
            _ = NPC_DesertScourgeTail ;
            _ = NPC_DesertNuisanceHead ;
            _ = NPC_DesertNuisanceBody ;
            _ = NPC_DesertNuisanceTail ;
            _ = NPC_AstrumDeusHead ;
            _ = NPC_AstrumDeusBody ;
            _ = NPC_AstrumDeusTail ;
            _ = NPC_AquaticScourgeHead ;
            _ = NPC_AquaticScourgeBody ;
            _ = NPC_AquaticScourgeTail ;
            _ = NPC_EidolonWyrmHead ;
            _ = NPC_EidolonWyrmBody ;
            _ = NPC_EidolonWyrmBodyAlt ;
            _ = NPC_EidolonWyrmTail ;
            _ = NPC_RavagerBody ;
            _ = NPC_RavagerClawLeft ;
            _ = NPC_RavagerClawRight ;
            _ = NPC_RavagerHead ;
            _ = NPC_RavagerLegLeft ;
            _ = NPC_RavagerLegRight ;
            _ = NPC_PerforatorHeadSmall ;
            _ = NPC_PerforatorBodySmall ;
            _ = NPC_PerforatorTailSmall ;
            _ = NPC_DesertNuisanceBodyYoung ;
        }

        public static void Unload()
        {
            ItemCache.Clear() ;
            NPCCache.Clear() ;
        }

        public static bool IsValid(int id) => id > ItemID.None ;

        private static int ResolveItem(string name)
        {
            if (ItemCache.TryGetValue(name, out int cached)) {
                return cached ;
            }

            int type = 0 ;

            if (ModContent.TryFind("CalamityMod", name, out ModItem item)) {
                type = item.Type ;
            }

            ItemCache[name] = type ;
            return type ;
        }

        private static int ResolveNPC(string name)
        {
            if (NPCCache.TryGetValue(name, out int cached)) {
                return cached ;
            }

            int type = 0 ;

            if (ModContent.TryFind("CalamityMod", name, out ModNPC npc)) {
                type = npc.Type ;
            }

            NPCCache[name] = type ;
            return type ;
        }

        public static int Item_Alluvion => ResolveItem("Alluvion") ;
        public static int Item_ArterialAssault => ResolveItem("ArterialAssault") ;
        public static int Item_AstrealDefeat => ResolveItem("AstrealDefeat") ;
        public static int Item_Barinade => ResolveItem("Barinade") ;
        public static int Item_Barinautical => ResolveItem("Barinautical") ;
        public static int Item_BlossomFlux => ResolveItem("BlossomFlux") ;
        public static int Item_BrimstoneFury => ResolveItem("BrimstoneFury") ;
        public static int Item_ClockworkBow => ResolveItem("ClockworkBow") ;
        public static int Item_Contagion => ResolveItem("Contagion") ;
        public static int Item_CorrodedCaustibow => ResolveItem("CorrodedCaustibow") ;
        public static int Item_ContinentalGreatbow => ResolveItem("ContinentalGreatbow") ;
        public static int Item_DaemonsFlame => ResolveItem("DaemonsFlame") ;
        public static int Item_DarkechoGreatbow => ResolveItem("DarkechoGreatbow") ;
        public static int Item_Deathwind => ResolveItem("Deathwind") ;
        public static int Item_Drataliornus => ResolveItem("Drataliornus") ;
        public static int Item_FlarewingBow => ResolveItem("FlarewingBow") ;
        public static int Item_Galeforce => ResolveItem("Galeforce") ;
        public static int Item_Goobow => ResolveItem("Goobow") ;
        public static int Item_HeavenlyGale => ResolveItem("HeavenlyGale") ;
        public static int Item_HoarfrostBow => ResolveItem("HoarfrostBow") ;
        public static int Item_LunarianBow => ResolveItem("LunarianBow") ;
        public static int Item_Malevolence => ResolveItem("Malevolence") ;
        public static int Item_MarksmanBow => ResolveItem("MarksmanBow") ;
        public static int Item_Monsoon => ResolveItem("Monsoon") ;
        public static int Item_NettlevineGreatbow => ResolveItem("NettlevineGreatbow") ;
        public static int Item_Phangasm => ResolveItem("Phangasm") ;
        public static int Item_PlanetaryAnnihilation => ResolveItem("PlanetaryAnnihilation") ;
        public static int Item_Shellshooter => ResolveItem("Shellshooter") ;
        public static int Item_TelluricGlare => ResolveItem("TelluricGlare") ;
        public static int Item_TheBallista => ResolveItem("TheBallista") ;
        public static int Item_TheMaelstrom => ResolveItem("TheMaelstrom") ;
        public static int Item_Ultima => ResolveItem("Ultima") ;
        public static int Item_Toxibow => ResolveItem("Toxibow") ;
        public static int Item_VernalBolter => ResolveItem("VernalBolter") ;
        public static int NPC_SepulcherHead => ResolveNPC("SepulcherHead") ;
        public static int NPC_SepulcherBody => ResolveNPC("SepulcherBody") ;
        public static int NPC_SepulcherTail => ResolveNPC("SepulcherTail") ;
        public static int NPC_StormWeaverHead => ResolveNPC("StormWeaverHead") ;
        public static int NPC_StormWeaverBody => ResolveNPC("StormWeaverBody") ;
        public static int NPC_StormWeaverTail => ResolveNPC("StormWeaverTail") ;
        public static int NPC_PrimordialWyrmHead => ResolveNPC("PrimordialWyrmHead") ;
        public static int NPC_PrimordialWyrmBody => ResolveNPC("PrimordialWyrmBody") ;
        public static int NPC_PrimordialWyrmTail => ResolveNPC("PrimordialWyrmTail") ;
        public static int NPC_PerforatorHeadLarge => ResolveNPC("PerforatorHeadLarge") ;
        public static int NPC_PerforatorBodyLarge => ResolveNPC("PerforatorBodyLarge") ;
        public static int NPC_PerforatorTailLarge => ResolveNPC("PerforatorTailLarge") ;
        public static int NPC_PerforatorHeadMedium => ResolveNPC("PerforatorHeadMedium") ;
        public static int NPC_PerforatorBodyMedium => ResolveNPC("PerforatorBodyMedium") ;
        public static int NPC_PerforatorTailMedium => ResolveNPC("PerforatorTailMedium") ;
        public static int NPC_Apollo => ResolveNPC("Apollo") ;
        public static int NPC_Artemis => ResolveNPC("Artemis") ;
        public static int NPC_AresBody => ResolveNPC("AresBody") ;
        public static int NPC_ThanatosHead => ResolveNPC("ThanatosHead") ;
        public static int NPC_ThanatosBody1 => ResolveNPC("ThanatosBody1") ;
        public static int NPC_ThanatosBody2 => ResolveNPC("ThanatosBody2") ;
        public static int NPC_ThanatosTail => ResolveNPC("ThanatosTail") ;
        public static int NPC_AresLaserCannon => ResolveNPC("AresLaserCannon") ;
        public static int NPC_AresPlasmaFlamethrower => ResolveNPC("AresPlasmaFlamethrower") ;
        public static int NPC_AresTeslaCannon => ResolveNPC("AresTeslaCannon") ;
        public static int NPC_AresGaussNuke => ResolveNPC("AresGaussNuke") ;
        public static int NPC_DevourerofGodsHead => ResolveNPC("DevourerofGodsHead") ;
        public static int NPC_DevourerofGodsBody => ResolveNPC("DevourerofGodsBody") ;
        public static int NPC_DevourerofGodsTail => ResolveNPC("DevourerofGodsTail") ;
        public static int NPC_DesertScourgeHead => ResolveNPC("DesertScourgeHead") ;
        public static int NPC_DesertScourgeBody => ResolveNPC("DesertScourgeBody") ;
        public static int NPC_DesertScourgeTail => ResolveNPC("DesertScourgeTail") ;
        public static int NPC_DesertNuisanceHead => ResolveNPC("DesertNuisanceHead") ;
        public static int NPC_DesertNuisanceBody => ResolveNPC("DesertNuisanceBody") ;
        public static int NPC_DesertNuisanceTail => ResolveNPC("DesertNuisanceTail") ;
        public static int NPC_AstrumDeusHead => ResolveNPC("AstrumDeusHead") ;
        public static int NPC_AstrumDeusBody => ResolveNPC("AstrumDeusBody") ;
        public static int NPC_AstrumDeusTail => ResolveNPC("AstrumDeusTail") ;
        public static int NPC_AquaticScourgeHead => ResolveNPC("AquaticScourgeHead") ;
        public static int NPC_AquaticScourgeBody => ResolveNPC("AquaticScourgeBody") ;
        public static int NPC_AquaticScourgeTail => ResolveNPC("AquaticScourgeTail") ;
        public static int NPC_EidolonWyrmHead => ResolveNPC("EidolonWyrmHead") ;
        public static int NPC_EidolonWyrmBody => ResolveNPC("EidolonWyrmBody") ;
        public static int NPC_EidolonWyrmBodyAlt => ResolveNPC("EidolonWyrmBodyAlt") ;
        public static int NPC_EidolonWyrmTail => ResolveNPC("EidolonWyrmTail") ;
        public static int NPC_RavagerBody => ResolveNPC("RavagerBody") ;
        public static int NPC_RavagerClawLeft => ResolveNPC("RavagerClawLeft") ;
        public static int NPC_RavagerClawRight => ResolveNPC("RavagerClawRight") ;
        public static int NPC_RavagerHead => ResolveNPC("RavagerHead") ;
        public static int NPC_RavagerLegLeft => ResolveNPC("RavagerLegLeft") ;
        public static int NPC_RavagerLegRight => ResolveNPC("RavagerLegRight") ;
        public static int NPC_PerforatorHeadSmall => ResolveNPC("PerforatorHeadSmall") ;
        public static int NPC_PerforatorBodySmall => ResolveNPC("PerforatorBodySmall") ;
        public static int NPC_PerforatorTailSmall => ResolveNPC("PerforatorTailSmall") ;
        public static int NPC_DesertNuisanceBodyYoung => ResolveNPC("DesertNuisanceBodyYoung") ;
    }
}
