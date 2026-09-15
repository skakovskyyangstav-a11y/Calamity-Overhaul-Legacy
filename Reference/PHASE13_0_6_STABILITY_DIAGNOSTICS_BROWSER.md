# Phase 13.0.6

This build intentionally stops visual retuning.

## OldEndEntityDraw diagnostics
ProjectileLayerRender now catches each primitive/additive drawable separately.
A failing type is logged only once with a full exception and stack trace:

`[LegacyPrimitiveFailure] <full type name>`
or
`[LegacyAdditiveFailure] <full type name>`

The outer InnoVault OldEndEntityDraw stage should no longer spam the HUD every frame.
The exact failing method can be fixed next without guessing.

## Visual source assets
The 0.9025 rawimg RGBA bytes are losslessly converted to PNG for the ModSources build.
Rawimg copies are removed from the source tree.

No Heavenfall / Vientiane / InfinitePick particle counts, widths, scales,
colors, timings or AI values are retuned in this build.

## Endless Damage localization
The localization is merged into the main language files:
- zh-Hans: 无尽伤害
- en-US: Endless Damage

## Recipe Browser mirrors
Every valid 9x9 Supertable recipe gets a standard tModLoader Recipe mirror
containing the aggregated ingredient counts and real output.

These mirrors:
- are visible to recipe-indexing mods;
- have an always-false condition;
- cannot be crafted through normal crafting;
- do not replace the spatial 9x9 recipe.

Actual crafting remains exclusive to the Transmutation of Matter UI.
