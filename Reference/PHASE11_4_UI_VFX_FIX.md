# Phase 11.4 — Supertable opening + Heavenfall visual correction

## Supertable / terminal UI
The current InnoVault UIHandle API uses an explicit Open()/Close() lifecycle.
Phase 11 bypassed that lifecycle with a private requestedOpen flag.

Phase 11.4:
- uses UIHandle.Open()/Close();
- enables CloseOnEscape;
- initializes the RecipeEngine after loading recipes;
- opens Terraria inventory together with the legacy terminal UI;
- removes `player.mouseInterface = true` from ModTile.MouseOver;
- lazily creates a missing LegacyTransmutationEntity for old-world stations placed before Phase 11;
- multiplayer clients request TileEntity placement when the entity is missing.

## Full-charge infinity
Restored to Phase 10 strength:
- 500 PRT_Light points;
- 1.5 starting scale;
- global PRT_Light / PRT_HeavenfallStar dimming removed.
The infinity starts thick/bright and naturally shrinks/fades through the original PRT lifetime.

## Vientiane execution
- center infinity restored to 500 particles;
- each bow infinity restored to 100 particles;
- ThunderTrail grows to a 22-unit center width;
- spatial taper is bow -> center: thin/dim at the bow, progressively thicker/brighter toward the execution point;
- after Time=300 the beams fade over the final 20 ticks.

## Basic Heavenfall attack
The screen-filling white pillar was traced to the modern InnoVault interpretation of the old primitive strip plus oversized HeavenRainbowImpact stars.

Phase 11.4:
- InfiniteArrow uses 64 tail->head samples;
- strip width tapers smoothly instead of staying constant;
- additive color stays strong;
- star details are smaller/finer to remove obvious grain;
- HeavenRainbowImpact keeps every-update detail, but with smaller stars so it reads as a smooth rainbow trace rather than a white column.

## Charge bar
- retained as an approved QoL optimization;
- reduced to 38x4;
- rounded 1px corners;
- filled area is one color at a time;
- that one color smoothly cycles through the seven rainbow colors.

## Preserved user fix
`HeavenfallLongbowHeldProj.cs` keeps:
`Vector2 origin = new Vector2(source.Width, source.Height) * 0.5f ;`
