# Phase 11.5

## Heavenfall ordinary attack
- InfiniteArrow no longer draws a visible PRT bead chain.
- It is rendered as a continuous additive rainbow strip: colored halo + saturated body + thin white core.
- HeavenRainbowImpact uses the same smooth renderer for its long 1600px strike ray.
- This is specifically intended to match the smooth straight light seen in the 2026.4 author video rather than a dotted/pearl line.

## Heavenfall right-click
- ParadiseArrow no longer emits a PRT_HeavenStar every AI update.
- The 5-arrow volley now uses a short smooth history strip.
- Hit bloom is limited to one small StarPulseRing.
- This targets the overlapping white-mass/light-pollution problem without making the actual bolts dim.

## Vientiane Judgment
- Jagged RandomThunder/ThunderTrail drawing has been removed.
- The 13 lines are continuous curved strips.
- Each line is thin/dimmer at its bow and progressively thicker/brighter toward the execution center.
- Center width grows to 27 units, with a broad colored halo and white core.
- Existing full-charge/center infinity particle effects are left at the Phase 10/11.4 restored strength.

## Old Supertable-only recipes
Current Calamity Overhaul normal recipes are disabled in PostAddRecipes for every still-existing CWO result that belonged to the old 0.9025 Supertable:
NeutronStarIngot, DawnshatterAzure, SpearOfLonginus, DragonsWord,
AnnihilatingUniverse, NeutronGlaive, NeutronGun, NeutronBow,
NeutronWand, NeutronScythe, EyeOfSingularity, EmblemOfDread,
ArcaneThroneOfEternity and CreativeUEPipeline.

The restored 9x9 Supertable recipes remain available, so these results are no longer craftable at the modern endgame/Draedon station.
