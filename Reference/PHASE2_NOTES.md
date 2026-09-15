# Phase 2 notes

This phase builds on Phase 1 and adds synchronized category tooltip lines.

Restored in this phase:
- InfinityCatalyst / 无尽催化剂
- InfiniteIngot / 无尽锭
- InfiniteIngotTile / 无尽锭放置方块
- DarkMatterBall / 暗物质球

Category tooltip policy:
- Material items display `材料` / `Material` immediately below the item name.
- Items that can be placed display `可放置` / `Placeable` as an additional line.
- Infinite Ingot is both a material and placeable, so it displays both lines.

Compatibility notes:
- Old custom HotPink rarity is requested from the currently loaded Calamity Overhaul when available. If it is unavailable, Purple is used as a safe fallback.
- Dark Matter Ball keeps its item storage / retrieval behavior and multiplayer item serialization. The old InnoVault hover-content UI is intentionally replaced with a standard tooltip content list to avoid restoring the old UI dependency.
- Legacy SuperTable recipe grids remain preserved separately and are still not registered to an arbitrary crafting station.
