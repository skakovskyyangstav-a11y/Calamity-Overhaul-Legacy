using CalamityOverhaulLegacy.Common.LegacyTooltips ;
using CalamityOverhaulLegacy.Content.Items.Materials ;
using CalamityOverhaulLegacy.Content.Tiles ;
using Terraria ;
using Terraria.ID ;
using Terraria.Localization ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Content.Items.Placeable
{
    internal class InfiniteToiletItem : ModItem, ILegacyCategorizedItem
    {
        public LegacyItemCategory Category => LegacyItemCategory.Placeable ;
        public static LocalizedText OnlyZenithCondition { get ; private set ; }

        public override string Texture => "CalamityOverhaulLegacy/Content/Items/Placeable/InfiniteToiletItem" ;

        public override void SetStaticDefaults()
        {
            OnlyZenithCondition = this.GetLocalization(nameof(OnlyZenithCondition), () => "仅在天顶世界") ;
        }

        public override void SetDefaults()
        {
            Item.width = 28 ;
            Item.height = 20 ;
            Item.maxStack = 9999 ;
            Item.useTurn = true ;
            Item.autoReuse = true ;
            Item.useAnimation = 15 ;
            Item.useTime = 10 ;
            Item.useStyle = ItemUseStyleID.Swing ;
            Item.consumable = true ;
            Item.createTile = ModContent.TileType<InfiniteToiletTile>() ;
        }

        public override void AddRecipes()
        {
            Condition condition = new(OnlyZenithCondition.Value, () => Main.zenithWorld) ;
            CreateRecipe()
                .AddIngredient<InfiniteIngot>(29)
                .AddIngredient<InfinityCatalyst>(9)
                .AddTile<TransmutationOfMatter>()
                .AddCondition(condition)
                .Register() ;
        }
    }
}
