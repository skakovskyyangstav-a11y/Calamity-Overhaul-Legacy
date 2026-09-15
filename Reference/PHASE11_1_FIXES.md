# Phase 11.1 — fidelity/performance hotfix

## Heavenfall Longbow
- Restored a visible item-slot charge bar as a user-approved quality-of-life optimization.
- Bar has no numeric maximum.
- Filled section uses one uniform color at a time; that single color smoothly cycles through the seven legacy rainbow colors.
- Charge fill itself eases smoothly toward the integer ChargeValue.

## VFX compatibility
The old 0.9025 particle/trail values were authored for an older InnoVault renderer.
On current InnoVault they render substantially brighter and are much more expensive.

The geometry, timing and effect families are preserved, but modern-renderer compensation is applied:
- PRT_Light additive RGB intensity reduced.
- HeavenfallStar RGB intensity reduced.
- InfiniteArrow Trail width reduced from the legacy numeric 130 to a modern-renderer equivalent 22.
- InfiniteArrow Trail color opacity reduced.
- HeavenRainbowImpact particle generation throttled by 50%.
- Full-charge infinity symbol samples 180 points instead of 500.
- Vientiane center symbol samples 180 instead of 500.
- Each Vientiane bow symbol samples 40 instead of 100.
- ThunderTrail maximum width reduced to a modern-renderer equivalent 8.

These changes target the same visible shape while preventing the current renderer from turning the screen into a white block.

## Infinite Ingot
- MinPick = 9999.
- Explosions cannot destroy Infinite Ingot tiles.
- Local tile mining/block replacement requires the held item to be Infinite Pick.
- The server accepts the synchronized authorized kill; Infinite Pick remote erasure remains functional.

## Infinite Pick / Dark Matter Ball
- Removed the expensive per-tile world Item snapshot/interception path.
- Restored the old lightweight `Tile.GetTileDrop(x,y) + WorldGen.KillTile(... noItem:true)` flow.
- This is the key performance fix for right-click erasure.
- Dark Matter Ball is now delivered directly to the player after the erasure projectile finishes, instead of spawning a slow return-flight ball.
