# Phase 9 — direct 2026.4 module port

Goal: stop approximating the old effects and restore the old modules themselves.

## Directly restored architecture
- InnoVault PRT pipeline is used again.
- Exact-style local copies of:
  - PRT_HeavenfallStar
  - PRT_HeavenStar
  - PRT_Light
  - PRT_StarPulseRing
- InnoVault.Trails.Trail is used for InfiniteArrow.
- InnoVault.Trails.ThunderTrail is used for VientianePunishment.
- The original GradientTrail shader from the supplied 0.9025 tmod is bundled.
- Exact old masking/gradient resources are bundled:
  Airflow, StarTexture, StarTexture_White, Extra_193, Extra_98,
  Photosphere, BlankStar, DiffusionCircle4/6, ThunderTrail,
  DarklightGreatsword_Bar.

## Heavenfall Longbow
- Left click and right click cadence follows the old HeldProj module.
- InfiniteArrow restores the original huge 130-width / 50-point shader trail,
  which is the missing "light pollution" arrow-shadow effect.
- HeavenRainbowImpact restores the original dense PRT star stream and 1600px line collision.
- ParadiseArrow restores the old Extra_98 layered star visual plus HeavenStar particles and pulse rings.
- Full charge restores the old 500-particle PRT_Light infinity symbol.
- Q Vientiane restores 13 real bow textures, mouse-following setup phase,
  120-tick execution threshold, 300px Obliterate instant-kill area,
  per-bow PRT infinity bursts, and InnoVault ThunderTrail convergence.

## Charge bar
- Still attached directly under the item/hotbar weapon icon.
- Increased to 40x6 pixels.
- No numeric maximum is displayed.
- Fill is a continuously scrolling seven-color gradient.
- The internal integer counter is preserved; visual fill eases toward it.

## Infinite Pick
- Right-click is no longer a Terraria tool-use/AltFunction action.
- Like the old module, HoldItem directly detects the right mouse transition,
  so the cursor may point at empty air.
- Pick mode spawns eight dark-purple/black HeavenfallStar particles every MaxUpdate,
  giving the continuous erasure-ray appearance while the projectile moves.
- Hammer mode spawns 188 dark-purple/black sparks over the old 500x500 area.
- Drop compression and the returning Dark Matter Ball remain enabled.
