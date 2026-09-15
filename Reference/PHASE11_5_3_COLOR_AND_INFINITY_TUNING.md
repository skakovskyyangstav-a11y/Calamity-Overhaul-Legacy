# Phase 11.5.3

Only two visual parameters were changed from the user-approved Phase 11.5.2 shape.

## Vientiane beam color
The lens/light-spear geometry is unchanged.

Each beam color now combines:
- its indexed rainbow position among the 13 bows;
- the sampled color of that bow texture.

The result is saturation-boosted before rendering.

The execution beam now uses:
- a stronger colored halo;
- a stronger colored main body;
- much less whitening in the main body;
- a thinner and weaker white internal spine.

This keeps the bright central character while making all 13 rays visibly colored.

## Infinity initial thickness
The 500-point infinity geometry and lifetime/fade behavior are unchanged.

Only the initial PRT_Light scale was reduced:
- full-charge infinity: 1.5 -> 1.15
- Vientiane center infinity: 1.5 -> 1.15

Per-bow Vientiane infinity remains unchanged at 0.5.
