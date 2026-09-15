using CalamityOverhaul.Content.QuestLogs.Core ;
using CalamityOverhaulLegacy.Content.Items.Materials ;
using CalamityOverhaulLegacy.Content.Items.Placeable ;
using CalamityOverhaulLegacy.Content.Items.Ranged.HeavenfallLongbows ;
using CalamityOverhaulLegacy.Content.Items.Tools ;
using Microsoft.Xna.Framework ;
using Terraria ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Compatibility.QuestLogs
{
    internal abstract class LegacyObtainQuest<TItem> : QuestNode where TItem : ModItem
    {
        protected abstract Vector2 RelativePosition { get ; }
        protected abstract string ParentQuestID { get ; }
        protected virtual QuestType LegacyQuestType => QuestType.Main ;
        protected virtual QuestDifficulty LegacyDifficulty => QuestDifficulty.Master ;

        public override void SetStaticDefaults()
        {
            SetItemIcon(ModContent.ItemType<TItem>()) ;
            Position = RelativePosition ;
            ParentIDs.Add(ParentQuestID) ;
            QuestType = LegacyQuestType ;
            Difficulty = LegacyDifficulty ;
            AddObtainObjective() ;
        }

        public override void UpdateByPlayer()
        {
            if (Objectives.Count == 0) {
                return ;
            }

            int type = ModContent.ItemType<TItem>() ;
            int count = 0 ;
            Player player = Main.LocalPlayer ;

            for (int i = 0 ; i < player.inventory.Length ; i++) {
                if (player.inventory[i].type == type) {
                    count += player.inventory[i].stack ;
                }
            }

            Objectives[0].CurrentProgress = count > 0 ? 1 : 0 ;
            if (Objectives[0].IsCompleted && !IsCompleted) {
                IsCompleted = true ;
            }
        }
    }

    internal class LegacyDarkMatterCompressorQuest : LegacyObtainQuest<DarkMatterCompressorItem>
    {
        protected override Vector2 RelativePosition => new(0f, -150f) ;
        protected override string ParentQuestID => "BossRushQuest" ;

        public override void PostSetup()
        {
            if (GetQuest("BossRushQuest") != null) {
                return ;
            }

            ParentIDs.Clear() ;

            if (GetQuest("RockQuest") != null) {
                ParentIDs.Add("RockQuest") ;
            }
            else if (GetQuest("TerminusQuest") != null) {
                ParentIDs.Add("TerminusQuest") ;
            }
        }
    }

    internal class LegacyTransmutationOfMatterQuest : LegacyObtainQuest<TransmutationOfMatterItem>
    {
        protected override Vector2 RelativePosition => new(0f, -150f) ;
        protected override string ParentQuestID => nameof(LegacyDarkMatterCompressorQuest) ;

        public override void PostSetup()
        {
            QuestNode neutron = GetQuest("NeutronStarIngotQuest") ;
            if (neutron != null) {
                neutron.ParentIDs.Clear() ;
                neutron.ParentIDs.Add(ID) ;
                neutron.Position = new Vector2(-230f, 0f) ;
            }

            QuestNode rockII = GetQuest("RockQuestII") ;
            if (rockII != null) {
                rockII.ParentIDs.Clear() ;
                rockII.ParentIDs.Add(ID) ;
                rockII.Position = new Vector2(230f, 0f) ;
            }
        }
    }

    internal class LegacyInfinityCatalystQuest : LegacyObtainQuest<InfinityCatalyst>
    {
        protected override Vector2 RelativePosition => new(0f, -150f) ;
        protected override string ParentQuestID => nameof(LegacyTransmutationOfMatterQuest) ;
    }

    internal class LegacyInfiniteIngotQuest : LegacyObtainQuest<InfiniteIngot>
    {
        protected override Vector2 RelativePosition => new(0f, -150f) ;
        protected override string ParentQuestID => nameof(LegacyInfinityCatalystQuest) ;
    }

    internal class LegacyHeavenfallLongbowQuest : LegacyObtainQuest<HeavenfallLongbow>
    {
        protected override Vector2 RelativePosition => new(-220f, -170f) ;
        protected override string ParentQuestID => nameof(LegacyInfiniteIngotQuest) ;
    }

    internal class LegacyInfiniteToiletQuest : LegacyObtainQuest<InfiniteToiletItem>
    {
        protected override Vector2 RelativePosition => new(0f, -170f) ;
        protected override string ParentQuestID => nameof(LegacyInfiniteIngotQuest) ;
        protected override QuestType LegacyQuestType => QuestType.Side ;
        protected override QuestDifficulty LegacyDifficulty => QuestDifficulty.Easy ;
    }

    internal class LegacyInfinitePickQuest : LegacyObtainQuest<InfinitePick>
    {
        protected override Vector2 RelativePosition => new(220f, -170f) ;
        protected override string ParentQuestID => nameof(LegacyInfiniteIngotQuest) ;
    }
}
