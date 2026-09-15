# Phase 11.3 compile hotfix

Fixed the two compile errors reported after Phase 11.2:

## RecipeSidebarManager.cs
Added:
`using Terraria.ID ;`

This resolves the missing `SoundID` symbol.

## LegacyTransmutationEntity.cs
Added:
`using Microsoft.Xna.Framework ;`

This resolves the missing `Rectangle` type.

A project-wide namespace sweep was also run for:
- Terraria.ID symbols (`SoundID`, `ItemID`, `TileID`, `NPCID`, `ProjectileID`, `DustID`, `WallID`, `MessageID`, `NetmodeID`, rarities/use styles)
- Microsoft.Xna.Framework types (`Rectangle`, `Vector2`, `Vector3`, `Color`, `MathHelper`, `Point`)
- Terraria.DataStructures (`Point16`, `TileEntity`, `DrawAnimationVertical`)
- `SoundEngine`
- `Asset<T>`
- `Recipe`

The user's confirmed HeavenfallLongbowHeldProj origin fix from Phase 11.2 remains unchanged.
