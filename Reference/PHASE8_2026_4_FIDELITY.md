# Phase 8 — 2026.4 fidelity pass

This phase intentionally stops adding reinterpretations. The goal is to mirror the 0.9025 / tModLoader 2026.4.3 behavior first.

## Infinite Pick
- Keeps the 2026.4 Pickaxe/Hammer textures already extracted from the old .tmod.
- Keeps the Phase 7 mechanics:
  - pick form: moving remote-erasure projectile,
  - hammer form: 500x500 erasure area,
  - drops compressed into one Dark Matter Ball that returns to the player.
- Changes the visible effect family from rainbow to dark purple/black only.
- The dark palette is based on the old package's DarklightGreatsword gradient family.

## Heavenfall Longbow charge
- Charge is moved back from ModPlayer to the Heavenfall Longbow item instance itself, matching old 0.9025 storage semantics.
- The bottom-screen HUD is removed.
- A thin charge bar is drawn directly under the weapon icon in inventory/hotbar.
- No numeric "200" maximum is displayed.
- Actual charge remains integer (+5 left fire / +3 right volley); only the visible bar is smoothly interpolated.

## Full charge
- At full charge, the old-style infinity visual appears once.
- An invisible InfiniteRune performs the original one-time large-area 99999 damage pulse.

## Q / Vientiane Punishment
- The 13-bow formation remains purely visual before execution.
- The bows follow the mouse target for the first 120 ticks.
- At tick 120, the lead Vientiane projectile calls `HeavenfallLongbow.Obliterate(targetPosition)`.
- Obliterate is an instant-kill operation, not repeated frame damage.
- The current tModLoader `NPC.StrikeInstantKill()` API is used after clearing invulnerability flags.
- Worm / multipart groups are killed together using realLife anchors plus the old 0.9025 segment-group list where resolvable.
- After execution, the 13 bows continue drawing bright ThunderTrail beams toward the execution center, matching the old Vientiane visual structure.
