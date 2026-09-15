# Phase 13.0.3 runtime fidelity hotfix

No frozen Heavenfall / Infinite Pick / Dark Matter Ball source was changed.

Changes:
- Restored the exact 0.9025 `GradientTrail.fxc`.
- Restored the exact 0.9025 `Crystal.fxc`.
- Removed `GradientTrail.fx` and `Crystal.fx` from the build tree so tModLoader
  2026.7.3 cannot recompile them with a newer shader compiler.
- Restored original CWRID semantics: external item/NPC IDs are resolved only
  from `CalamityMod`, exactly like 0.9025. The previous Legacy compatibility
  layer incorrectly tried current `CalamityOverhaul` first.
- Preserved exact raw VFX mask bytes alongside converted PNGs for the next
  runtime-loader audit.

Why this matters:
The 0.9025 weapon source does not define the final rendered appearance by
itself. The compiled Effect binary and the exact external item IDs used by
VientianePunishment are also part of that runtime state.
