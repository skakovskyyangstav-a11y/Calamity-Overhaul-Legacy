using CalamityOverhaulLegacy.Common.LegacyTooltips ;
using Terraria ;
using Terraria.DataStructures ;
using Terraria.ID ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Content.Items.Materials
{
    internal class DecaySubstance : ModItem, ILegacyCategorizedItem
    {
        public override string Texture => "CalamityOverhaulLegacy/Content/Items/Materials/DecaySubstance" ;
        public LegacyItemCategory Category => LegacyItemCategory.Material ;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 64 ;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 5)) ;
            ItemID.Sets.AnimatesAsSoul[Type] = true ;
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 25 ;
            Item.material = true ;
            Item.maxStack = 9999 ;
            Item.rare = ItemRarityID.Lime ;
            Item.value = Item.sellPrice(gold: 13) ;
}
    }
}
