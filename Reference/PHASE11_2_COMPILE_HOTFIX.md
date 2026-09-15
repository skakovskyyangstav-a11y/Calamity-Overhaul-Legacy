# Phase 11.2 compile hotfix

Fixed the SupertableUI compile-error cascade reported by the user.

## SupertableUI.cs
- Added `using Terraria.DataStructures ;`.
  `TileEntity` is declared in `Terraria.DataStructures`, so the missing import caused:
  - CS0103 / CS0246 for TileEntity,
  - cascading CS0165 errors for pattern variables `e` and `entity`.
- Changed:
  `public static void Open(LegacyTransmutationEntity entity)`
  to:
  `internal static void Open(LegacyTransmutationEntity entity)`
  because `LegacyTransmutationEntity` is internal and the public method caused CS0051 inconsistent accessibility.

## HeavenfallLongbowHeldProj.cs
Merged the user's confirmed correction:
`Vector2 origin = new Vector2(source.Width, source.Height) * 0.5f ;`

A project-wide namespace scan was also run for:
TileEntity, Point16, DrawAnimationVertical, SoundEngine, Recipe and Asset<T>.
