# Phase 13.0.5 — exact visual runtime reset

This build removes the two custom visual adapters introduced in 13.0.4.

Removed:
- same-namespace Heavenfall `ThunderTrail` wrapper
- same-namespace InfinitePick `PRTLoader` scale adapter

VientianePunishment now resolves directly to InnoVault.Trails.ThunderTrail again.
The v1.9.97 ThunderTrail implementation already contains its own two render layers:
1. the main strip;
2. the narrower additive flow strip.

No additional glow shell is layered on top.

InfinitePick again passes its literal old source scales directly to InnoVault PRTLoader:
- outer OnHit star: 3.2 + scaleBoost
- inner OnHit star: 0.6 + scaleBoost
- erasure particles: unchanged

## GradientTrail crash
Current Effect compilation removes unused shader parameters.
The old GradientTrail.fx declares `uTimeG` and `udissolveS`, but the pixel shader
does not use either one.

Only these two old SetValue calls are changed to null-safe calls.
This is render-output neutral because the old shader never consumes those values.
The actually-used `uTime` remains unchanged, so flow animation remains active.

## Raw visual assets
All Heavenfall / Infinite Pick visual textures are now copied as the exact
`.rawimg` bytes from the uploaded 0.9025 tmod.
Converted PNG duplicates are removed.

This avoids an unnecessary rawimg -> PNG -> tModLoader conversion round trip.

Crystal continues to use the exact old `Crystal.fxc`, which already fixed its
runtime asset-load error.
