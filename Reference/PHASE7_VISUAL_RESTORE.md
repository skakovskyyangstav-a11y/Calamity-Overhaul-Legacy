# Phase 7 — 0.9025 visual restoration pass

## Why Phase 6 looked wrong
The old Heavenfall Longbow did NOT use the inventory item graphic as a repeated projectile effect.
Its visual stack was:
1. One channelled held-bow projectile using a dedicated 5-frame 250x1320 sprite sheet.
2. Infinite Arrow: broad rainbow/star trail.
3. Heaven Rainbow Impact: invisible 1600px collision line, with moving rainbow star particles rather than a solid rectangle.
4. Paradise Arrow: two layered `Extra_98` star streaks.
5. Full-charge Infinite Rune: a large rainbow lemniscate/infinity-sign particle field.
6. Vientiane Punishment: 13 different Calamity bows arranged around the cursor target, followed by lightning/star trails converging on the center.

Phase 7 restores that visual architecture using exact 0.9025 textures, but uses native tModLoader drawing instead of the removed CWR shader/PRT/Trail systems.

## Heavenfall Longbow changes
- Restored channelled held projectile.
- Exact old `HeavenfallLongbowProj` and glow sheets are bundled.
- Charge now visibly strengthens the held-bow rainbow aura.
- Left click fires every ~11 ticks and grants +5 charge.
- Right click rains 5 Paradise Arrows every ~16 ticks and grants +3 charge.
- Full charge still uses the visible HUD and now also produces the large old-style rainbow infinity symbol.
- Ultimate uses 13 actual Calamity bow item textures where available and arranges them around the cursor before converging star-lightning trails.

## Infinite Pick changes
- Removed the simple straight beam graphic.
- Pick mode now looks like the old moving rainbow-star erasure path.
- Hammer mode creates a dense radial star field around the 500x500 deletion area.
- Collected drops are packed into one Dark Matter Ball.
- The Dark Matter Ball is now visibly created at the erased area, grows/rotates, then flies back to the player before entering inventory.
- Enemy hit follow-up projectiles use the same rainbow star visual family.

## Bundled exact old assets
- HeavenfallLongbowProj
- HeavenfallLongbowProjGlow
- StarTexture_White
- Extra_98
- SoftGlow
- ThunderTrail
