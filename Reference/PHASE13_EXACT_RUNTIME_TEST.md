# Phase 13 — Exact Runtime Test

Source of truth:
- freshly reuploaded CalamityOverhaulOld.tmod
- CalamityOverhaul 0.9025
- tModLoader 2026.4.3.0
- original dependency metadata: InnoVault 1.9.97

This build removes the Phase 12 hand-written weapon behavior and compiles the
original 0.9025 weapon files after namespace isolation.

Restored original source files:
- HeavenfallLongbow.cs
- HeavenfallLongbowHeldProj.cs
- InfiniteArrow.cs
- HeavenRainbowImpact.cs
- InfiniteRune.cs
- ParadiseArrow.cs
- VientianePunishment.cs
- InfinitePick.cs
- DarkMatterBall.cs
- EndlessDamageClass.cs
- PRT_HeavenfallStar.cs
- PRT_HeavenStar.cs
- PRT_Light.cs
- PRT_StarPulseRing.cs
- IPrimitiveDrawable.cs
- ProjectileLayerRender.cs
- NpcGroupHelper.cs

The original logical asset paths are also restored under
`CalamityOverhaulLegacy/Assets/...`, including GradientTrail.fx, Crystal.fx,
ThunderTrail, all masks used by Heavenfall, DarkMatterBall UI assets, and sounds.

Exact-runtime test rule:
- no charge-bar QoL;
- no custom smooth-beam renderer;
- no manually retuned particle intensity;
- no modified Dark Matter Ball chase speed.

Only compatibility layers are allowed outside the frozen files:
CWRConstant/CWRAsset path relocation, CWRID lookup, CWRItem state storage,
CWRKeySystem routing, CWRLoad lookup tables, and ICWRLoader lifecycle.

One intentional source-level compatibility patch remains in
HeavenfallLongbowHeldProj.cs:
`Vector2 origin = new Vector2(source.Width, source.Height) * 0.5f ;`
as previously verified by the user for the current tModLoader build.
