using CalamityOverhaulLegacy.Common.Compatibility ;
using CalamityOverhaulLegacy.Common.LegacyTooltips ;
using CalamityOverhaulLegacy.Content.Tiles ;
using Terraria ;
using Terraria.DataStructures ;
using Terraria.ID ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Content.Items.Placeable
{
    internal class DarkMatterCompressorItem : ModItem, ILegacyCategorizedItem
    {
        public override string Texture => "CalamityOverhaulLegacy/Content/Items/Placeable/DarkMatterCompressorItem" ;
        public LegacyItemCategory Category => LegacyItemCategory.Placeable ;

        public override void SetStaticDefaults()
        {
            ItemID.Sets.AnimatesAsSoul[Type] = true ;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(5, 4)) ;
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
            Item.createTile = ModContent.TileType<DarkMatterCompressor>() ;
            Item.rare = ItemRarityID.Cyan ;
            Item.value = Item.buyPrice(gold: 16) ;
        }

        public override void AddRecipes()
        {
            if (LegacyExternalContent.TryItem("CalamityMod/DarkPlasma", out int darkPlasma)
                && LegacyExternalContent.TryItem("CalamityMod/StaticRefiner", out int staticRefiner)
                && LegacyExternalContent.TryItem("CalamityMod/ProfanedCrucible", out int profanedCrucible)
                && LegacyExternalContent.TryItem("CalamityMod/PlagueInfuser", out int plagueInfuser)
                && LegacyExternalContent.TryItem("CalamityMod/MonolithAmalgam", out int monolithAmalgam)
                && LegacyExternalContent.TryItem("CalamityMod/VoidCondenser", out int voidCondenser)) {
                CreateRecipe()
                    .AddIngredient(darkPlasma, 5)
                    .AddIngredient(staticRefiner)
                    .AddIngredient(profanedCrucible)
                    .AddIngredient(plagueInfuser)
                    .AddIngredient(monolithAmalgam)
                    .AddIngredient(voidCondenser)
                    .Register() ;
            }
            else {
                CreateRecipe()
                    .AddIngredient(ItemID.LunarBar, 5)
                    .AddTile(TileID.LunarCraftingStation)
                    .Register() ;
            }
        }
    }
}
