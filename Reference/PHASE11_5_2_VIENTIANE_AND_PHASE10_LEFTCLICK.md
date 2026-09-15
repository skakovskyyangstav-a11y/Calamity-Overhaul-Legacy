# Phase 11.5.2

## Normal left-click attack
User requested an exact rollback to Phase 10.

The following classes are copied directly from the Phase 10 package:
- InfiniteArrow
- HeavenRainbowImpact

No Phase 11.5 smooth-beam changes are retained for normal left click.

ParadiseArrow (right click) remains on the newer reduced-bloom implementation.

## Vientiane Judgment
Phase 11.5 used a linear-width strip:
bow end = thin
execution center = maximum width

With 13 beams this produced a huge filled polygon/star at the center.

Phase 11.5.2 changes the execution-ray cross section to a lens/light-spear profile:
- narrow point at the bow;
- expands through the middle;
- maximum thickness around the inner-middle region;
- pinches back down to a narrow point at the execution center.

Three additive layers are used:
- soft colored halo;
- pastel main body;
- white internal spine.

The beam color still comes from the corresponding bow texture and gradually whitens toward the center.

Full-charge and Vientiane infinity-particle effects are unchanged from the restored Phase 10 strength.
