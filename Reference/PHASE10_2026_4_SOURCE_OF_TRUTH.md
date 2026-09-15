# Phase 10 — 2026.4 source-of-truth reset

Authoritative baseline:
- CalamityOverhaul 0.9025 supplied by the user
- tModLoader 2026.4.3.0

From this phase onward:
- Legacy behavior and visuals are not redesigned.
- Old source wins over screenshots, memory, or prior hand-tuned requirements.
- Only inaccessible/deleted dependencies are adapted.

Corrections:
- Removed the custom item-slot Heavenfall charge bar. The supplied 0.9025 HeavenfallLongbow source does not contain a PostDrawInInventory charge bar.
- Removed fake Q fallback. If Weapon Skill is unbound, the tooltip now says it is unbound.
- Restored [KEY]-token replacement semantics.
- Reverted Infinite Pick visual colors to the supplied 0.9025 source, which directly uses HeavenfallLongbow.rainbowColors.
- Restored primitive render layer weight to 1.2.
- Vientiane friendliness restored toward the supplied source.
