# Phase 12.0.1 compile hotfix

Fixed the three compile errors reported after the direct 0.9025 weapon port.

## InfiniteRune.cs
Added:
`using Microsoft.Xna.Framework ;`

This resolves the missing `Color` type.

## InfinitePick / IsOwnedByLocalPlayer
Removed the compatibility-layer extension:
`LegacyWeaponPortCompat.IsOwnedByLocalPlayer(this Projectile projectile)`

Reason:
InnoVault already exposes the same extension through `VaultUtils`.
Keeping both extensions made the two original 0.9025 calls in InfinitePick.cs ambiguous (CS0121).

The original calls themselves remain unchanged:
`Projectile.IsOwnedByLocalPlayer()`

They now resolve to InnoVault's original implementation, which is closer to the 0.9025 source behavior.

No weapon timings, VFX values, Dark Matter Ball behavior, Heavenfall logic, or Infinite Pick logic were changed.
