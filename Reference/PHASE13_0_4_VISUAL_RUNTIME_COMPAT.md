# Phase 13.0.4 visual/runtime compatibility

Frozen 0.9025 weapon sources are byte-identical to Phase 13.0.3.

## InfiniteArrow shader animation
Phase 13.0.3 forced the old precompiled GradientTrail.fxc.
It loads under tML 2026.7.3, but user testing showed the uTime/uTimeG animation
was effectively frozen.

13.0.4:
- removes GradientTrail.fxc;
- restores the exact 0.9025 GradientTrail.fx source;
- lets the current graphics backend compile that exact HLSL source.

Crystal remains the exact old Crystal.fxc because that fixed the asset-load failure.

## Vientiane lightning glow shell
VientianePunishment.cs is not edited.
A same-namespace ThunderTrail compatibility adapter is supplied.
It:
- keeps one exact InnoVault ThunderTrail as the core;
- creates a second ThunderTrail using the same texture and SAME RandomlyPositions;
- draws the second copy wider and low-opacity first;
- draws the exact core on top.

The result is a glow envelope hugging the old lightning, not an independently
randomized second lightning bolt.

## Infinite Pick hit particles
InfinitePick.cs retains the exact old literal:
`outerSparkScale = 3.2f + scaleBoost`.

A same-namespace PRTLoader adapter only compensates current-runtime apparent
scale when:
- T == PRT_HeavenfallStar
- Scale > 2

That condition selects the 36 outer OnHitNPC stars.
Scale=1 erasure sparks, inner 0.6 stars, and PRT_HeavenStar followups are untouched.

Current compensation factor: 0.30.

## Localization
Restores:
- zh-Hans: 无尽伤害
- en-US: Endless Damage
