# Phase 13.0.1 compile hotfix

No frozen weapon implementation was modified.

Fixed four missing old-runtime dependencies:

1. Restored `Common/IAdditiveDrawable.cs` from 0.9025 source.
2. Restored root `string.GetSound(...)` extension with the old semantics.
3. Restored `CWRSound.None` and exact `Assets/Sounds/None.ogg`.
4. Added `EffectLoader.GradientTrail`, pointing at the exact extracted
   `Assets/Effects/GradientTrail.fx`.

Affected compile errors:
- VientianePunishment.cs: string.GetSound
- HeavenfallLongbow.cs: string.GetSound
- InfiniteArrow.cs: EffectLoader.GradientTrail
- ProjectileLayerRender.cs: IAdditiveDrawable

The frozen 0.9025 files themselves were not edited.
