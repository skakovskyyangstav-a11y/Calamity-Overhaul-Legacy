using CalamityOverhaulLegacy.Common.Compatibility ;
using CalamityOverhaulLegacy.Common.LegacyTooltips ;
using CalamityOverhaulLegacy.Content.Tiles ;
using Terraria ;
using Terraria.DataStructures ;
using Terraria.ID ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Content.Items.Placeable
{
    internal class TransmutationOfMatterItem : ModItem, ILegacyCategorizedItem
    {
        public override string Texture => "CalamityOverhaulLegacy/Content/Items/Placeable/TransmutationOfMatterItem" ;
        public LegacyItemCategory Category => LegacyItemCategory.Placeable ;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.AnimatesAsSoul[Type] = true ;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 11)) ;
        }

        public override void SetDefaults()
        {
            Item.width = 26 ;
            Item.height = 26 ;
            Item.maxStack = 1 ;
            Item.useTurn = true ;
            Item.autoReuse = true ;
            Item.useAnimation = Item.useTime = 15 ;
            Item.useStyle = ItemUseStyleID.Swing ;
            Item.consumable = true ;
            Item.createTile = ModContent.TileType<TransmutationOfMatter>() ;
            Item.rare = LegacyRarityHelper.DarkOrange ;
            Item.value = Item.buyPrice(gold: 16) ;
        }

        public override void AddRecipes()
        {
            if (LegacyExternalContent.TryItem("CalamityMod/ShadowspecBar", out int shadowspecBar)
                && LegacyExternalContent.TryItem("CalamityMod/DraedonsForge", out int draedonsForge)) {
                CreateRecipe()
                    .AddIngredient(shadowspecBar, 5)
                    .AddIngredient(draedonsForge)
                    .AddIngredient<DarkMatterCompressorItem>()
                    .Register() ;
            }
            else {
                CreateRecipe()
                    .AddIngredient(ItemID.LunarBar, 15)
                    .AddTile(TileID.LunarCraftingStation)
                    .Register() ;
            }
        }
    }
}
