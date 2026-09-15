# Phase 12 — Direct 0.9025 weapon port

Authoritative source:
- freshly uploaded `CalamityOverhaulOld.tmod`
- CalamityOverhaul 0.9025
- tModLoader 2026.4.3.0

## What was reset
The custom Phase 11 Heavenfall/Infinite Pick projectile implementations were removed.

The following old modules were ported back as separate files matching the original layout:
- HeavenfallLongbow.cs
- HeavenfallLongbowHeldProj.cs
- InfiniteArrow.cs
- HeavenRainbowImpact.cs
- InfiniteRune.cs
- ParadiseArrow.cs
- VientianePunishment.cs
- InfinitePick.cs (including InfinitePickProj, InfiniteEnmgs, SpanDMBall)

## Directly restored values / behavior
Heavenfall:
- InfiniteArrow MaxPos = 50
- Trail width = 130
- old GradientTrail.fx pipeline
- old PRT_HeavenfallStar scales/lifetimes
- HeavenRainbowImpact MaxUpdates = 5 and old PRT stream
- ParadiseArrow old homing, PRT_HeavenStar, 3 pulse rings, Extra_98 drawing
- full-charge infinity = 500 PRT_Light at scale 1.5
- Vientiane = old ThunderTrail / RandomThunder / TrailWig 0..32
- Vientiane execution time = 120
- center infinity = 500 @ 1.5, symbol scale 2
- bow infinity = 100 @ 0.5, symbol scale 0.5
- execution radius = 300
- ring radius = 320

Infinite Pick:
- raw right-click edge detection while held
- projectile speed = 32
- pick form MaxUpdates = 13
- pick erasure hitbox becomes 64x64
- 8 PRT stars per update
- hammer erasure = 500x500
- hammer burst = 188 PRT stars
- tile drops use GetTileDrop + KillTile(noItem:true)
- old rainbow particle colors / Main.DiscoColor lighting
- old 3 InfiniteEnmgs hit follow-ups
- old 36-star hit burst

Dark Matter Ball return:
- restored original SpanDMBall timing instead of the direct-inventory shortcut
- first 60 ticks: rotate / scale up / alpha grows
- after tick 60: ChasingBehavior(owner, 13) + Center Lerp(0.04)
- pickup on contact with owner

This explains why the original return is difficult to notice:
the original custom PreDraw uses alpha/255, so the ball starts visually at zero opacity and only becomes fully visible around the moment the rapid chase phase begins.

## Only compatibility translations
Private/deleted CWO helpers are translated, not redesigned:
- CWRKeySystem -> Legacy WeaponSkillKeybind
- EndlessDamageClass -> LegacyEndlessDamageClass direct port
- CWR texture constants -> local exact extracted assets
- CWRAsset / EffectLoader -> local exact assets / GradientTrail.fx
- NpcGroupHelper -> LegacyNpcGroupHelper
- old chest/tile helpers -> LegacyWeaponPortCompat
- old private item-state extension -> normal ModItem SaveData/NetSend

## QoL kept separate
The inventory charge bar is retained in:
`Common/QoL/HeavenfallChargeBarGlobalItem.cs`

It is intentionally outside `HeavenfallLongbow.cs` so the core weapon implementation stays traceable to 0.9025.
