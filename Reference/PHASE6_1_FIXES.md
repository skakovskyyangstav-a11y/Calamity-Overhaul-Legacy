# Phase 6.1

## Compile fixes
- Added `Terraria.DataStructures` to InfinitePickBeam.cs for Point16.
- Added `Terraria.DataStructures` to InfinitePick.cs for DrawAnimationVertical.
- Replaced unavailable `WorldGen.KillTile_GetItemDrops` with a current-compatible drop interception path:
  normal Terraria/mod tile drops are created, immediately captured, removed from the world, and stored in the single Dark Matter Ball.

## Tooltip
- Removed the custom extra “材料” line.
- Terraria now supplies the single native “材料” and “可放置” labels.

## Heavenfall Longbow
- Added a visible 0/200 charge bar while held.
- At 200/200 it displays the bound weapon-skill key and READY state.
- Full charge + weapon-skill key spawns 13 Vientiane Punishment projectiles, resets charge, and gives obvious sound/particle feedback.
