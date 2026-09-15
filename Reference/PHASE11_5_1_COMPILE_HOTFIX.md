# Phase 11.5.1 compile hotfix

Fixed CS0103 in HeavenfallProjectiles.cs.

Phase 11.5 replaced VientianePunishment's old ThunderTrail renderer with
LegacySmoothBeamRenderer.DrawColoredBeam(), but two obsolete helper methods
from Phase 11.4 remained:
- GetTrailWidth(float completionRatio)
- GetTrailColor(float completionRatio)

GetTrailWidth still referenced the removed `trailWidth` field at line ~750.

Phase 11.5.1 removes both obsolete helper methods and all residual
`trailWidth` references.

No Phase 11.5 visual behavior was retuned:
- smooth InfiniteArrow beam retained;
- smooth HeavenRainbowImpact ray retained;
- smooth ParadiseArrow trail retained;
- smooth tapered Vientiane beams retained;
- 27-unit center-width target retained;
- restored Supertable-only recipe overrides retained.
