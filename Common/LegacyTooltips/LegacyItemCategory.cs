using System ;

namespace CalamityOverhaulLegacy.Common.LegacyTooltips
{
    [Flags]
    internal enum LegacyItemCategory
    {
        None = 0 ,
        Material = 1 ,
        Placeable = 2
    }

    internal interface ILegacyCategorizedItem
    {
        LegacyItemCategory Category { get ; }
    }

    // Current Terraria/tModLoader automatically displays:
    // - "材料" when Item.material = true
    // - "可放置" when Item.createTile is set
    // No GlobalItem tooltip insertion is needed here.
}
