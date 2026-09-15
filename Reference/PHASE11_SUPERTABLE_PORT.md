# Phase 11 — 2026.4 Supertable restoration

This phase restores the old 0.9025 9x9 Supertable as an actual crafting system.

Restored:
- exact 646x458 MainValue UI asset and all Supertable buttons/arrows/book assets;
- 9x9 / 81 material slots;
- exact-position recipe matching;
- preview recipe placement;
- recipe result slot and multi-craft by minimum material stack;
- recipe book next/previous navigation;
- right-side recipe list;
- quick-place, quick-take and wrong-slot highlighting;
- draggable panel and old open/close animation speeds;
- all 24 arrays from the supplied 0.9025 SupertableRecipeData;
- native ModTileEntity-backed 81-slot storage for each placed Transmutation of Matter station;
- save/load and multiplayer item-array synchronization;
- station auto-close at the original 120-pixel interaction distance;
- stored-item drop when the station is destroyed.

Compatibility adaptation only:
- the old InnoVault TileProcessor storage was replaced by a native ModTileEntity because the old CWO TileProcessor types are not safely subclassable from an external mod;
- known removed CalamityOverhaul item full names resolve to CalamityOverhaulLegacy equivalents;
- renamed Calamity life-fruit ingredients are aliased to their current names;
- the old Magic Storage linkage is not re-enabled in this phase because it was implemented through CWO-internal MSRef; core station UI is independent of it.

The temporary Terraria Recipe.Create recipes for Supertable outputs have been removed, so the 9x9 layout is once again authoritative.
