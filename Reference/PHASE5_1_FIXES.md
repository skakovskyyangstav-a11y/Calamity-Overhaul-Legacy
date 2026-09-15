# Phase 5.1 compile hotfix

Fixed:
- Added `using Terraria ;` to `Common/LegacyRecipes/LegacyTransmutationRecipes.cs`, which resolves all `CS0103: Recipe does not exist in the current context` errors.
- Removed the compile-time `PlayerSittingHelper` dependency from `Content/Tiles/InfiniteToiletTile.cs` and replaced the smart-interact gate with a simple `true` return.

This is a compile hotfix on top of Phase 5.
