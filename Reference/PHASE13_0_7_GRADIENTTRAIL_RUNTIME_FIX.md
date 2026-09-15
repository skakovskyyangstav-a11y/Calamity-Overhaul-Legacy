# Phase 13.0.7 — GradientTrail runtime asset fix

The uploaded client.log isolated the failure to:
`InfiniteArrow.DrawPrimitives()` line 101.

Runtime error:
`AssetLoadException: Asset could not be found: Assets\Effects\GradientTrail`

Root cause:
Phase 13.0.4-13.0.6 kept `GradientTrail.fx` in ModSources and assumed tModLoader
would compile it into a runtime Effect asset. It does not. `ModContent.Request<Effect>`
needs a compiled/requestable Effect asset.

Fix:
- restore exact 0.9025 `GradientTrail.fxc` bytes;
- remove active `GradientTrail.fx` from the build tree;
- retain its HLSL source only as a reference `.txt`;
- make GradientTrail and Crystal ImmediateLoad assets;
- preflight both Effects in PostSetupContent.

GradientTrail.fxc SHA-256:
437be2d59d3dd1b1cbd3ee0f828a5eadd6e228372bdeed7f33362e5c6c5577e7

No Heavenfall / Vientiane / InfinitePick / DarkMatterBall visual or AI parameter
was changed in this build.
