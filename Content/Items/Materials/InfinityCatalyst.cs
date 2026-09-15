using CalamityOverhaulLegacy.Common.Compatibility ;
using CalamityOverhaulLegacy.Common.LegacyTooltips ;
using Terraria ;
using Terraria.DataStructures ;
using Terraria.ID ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Content.Items.Materials
{
    internal class InfinityCatalyst : ModItem, ILegacyCategorizedItem
    {
        public override string Texture => "CalamityOverhaulLegacy/Content/Items/Materials/InfinityCatalyst" ;
        public LegacyItemCategory Category => LegacyItemCategory.Material ;

        public override void SetStaticDefaults()
        {
            Item.ResearchUnlockCount = 64 ;
            ItemID.Sets.AnimatesAsSoul[Type] = true ;
            Main.RegisterItemAnimation(Type, new DrawAnimationVertical(10, 6)) ;
        }

        public override void SetDefaults()
        {
            Item.width = Item.height = 32 ;
            Item.material = true ;
            Item.maxStack = 99 ;
            Item.rare = LegacyRarityHelper.HotPink ;
            Item.value = Item.sellPrice(gold: 99999) ;
}

        private static int Scaled(int amount)
        {
            float bonus = 1f + System.Math.Max(0f, ModLoader.Mods.Length / 10f < 0.5f ? 0f : ModLoader.Mods.Length / 10f) ;
            string[] heavyMods = new string[] { "LightAndDarknessMod", "DDmod", "MaxStackExtra", "Wild", "Coralite", "AncientsAwakened" } ;
            string[] veryHeavyMods = new string[] { "NoxusBoss", "FargowiltasSouls", "MagicBuilder", "CalamityPostMLBoots" } ;
            foreach (string name in heavyMods) if (ModLoader.HasMod(name)) bonus += 0.1f ;
            foreach (string name in veryHeavyMods) if (ModLoader.HasMod(name)) bonus += 0.25f ;
            return (int)(amount * bonus) ;
        }

        public override void AddRecipes()
        {
            if (!LegacyExternalContent.TryItem("CalamityMod/Rock", out int rock)
                || !LegacyExternalContent.TryItem("CalamityMod/MiracleFruit", out int miracleFruit)
                || !LegacyExternalContent.TryItem("CalamityMod/ExoPrism", out int exoPrism)
                || !LegacyExternalContent.TryItem("CalamityMod/AshesofAnnihilation", out int ashesOfAnnihilation)
                || !LegacyExternalContent.TryItem("CalamityMod/DarkPlasma", out int darkPlasma)
                || !LegacyExternalContent.TryItem("CalamityMod/TwistingNether", out int twistingNether)
                || !LegacyExternalContent.TryItem("CalamityMod/ArmoredShell", out int armoredShell)
                || !LegacyExternalContent.TryItem("CalamityMod/AshesofCalamity", out int ashesOfCalamity)
                || !LegacyExternalContent.TryItem("CalamityMod/DivineGeode", out int divineGeode)
                || !LegacyExternalContent.TryItem("CalamityMod/DubiousPlating", out int dubiousPlating)
                || !LegacyExternalContent.TryItem("CalamityMod/LifeAlloy", out int lifeAlloy)
                || !LegacyExternalContent.TryItem("CalamityMod/Necroplasm", out int necroplasm)
                || !LegacyExternalContent.TryItem("CalamityMod/RuinousSoul", out int ruinousSoul)) {
                return ;
            }

            CreateRecipe()
                .AddIngredient(rock)
                .AddIngredient(miracleFruit, Scaled(5))
                .AddIngredient(exoPrism, Scaled(5))
                .AddIngredient(ashesOfAnnihilation, Scaled(5))
                .AddIngredient(ItemID.FragmentSolar, Scaled(5))
                .AddIngredient(darkPlasma, Scaled(10))
                .AddIngredient(twistingNether, Scaled(10))
                .AddIngredient(armoredShell, Scaled(10))
                .AddIngredient(ItemID.FragmentVortex, Scaled(15))
                .AddIngredient(ashesOfCalamity, Scaled(20))
                .AddIngredient(ItemID.Gel, Scaled(50))
                .AddIngredient(ItemID.HellstoneBar, Scaled(50))
                .AddIngredient(ItemID.SoulofNight, Scaled(50))
                .AddIngredient(divineGeode, Scaled(50))
                .AddIngredient(dubiousPlating, Scaled(50))
                .AddIngredient(ItemID.Obsidian, Scaled(50))
                .AddIngredient(ItemID.HallowedBar, Scaled(50))
                .AddIngredient(ItemID.LunarBar, Scaled(50))
                .AddIngredient(lifeAlloy, Scaled(50))
                .AddIngredient(ItemID.LifeCrystal, Scaled(50))
                .AddIngredient(ItemID.FallenStar, Scaled(50))
                .AddIngredient(ItemID.Ectoplasm, Scaled(50))
                .AddIngredient(necroplasm, Scaled(50))
                .AddIngredient(ruinousSoul, Scaled(50))
                .AddIngredient(ItemID.SoulofLight, Scaled(50))
                .AddTile<Content.Tiles.DarkMatterCompressor>()
                .Register() ;
        }

    }
}
