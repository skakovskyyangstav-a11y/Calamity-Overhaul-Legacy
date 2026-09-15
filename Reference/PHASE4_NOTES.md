# Phase 4

## Added
- 暗物质压缩机：旧贴图、4x3 Tile、动画/发光、旧配方兼容
- 终焉物质聚合仪：旧贴图、5x3 Tile、11帧动画/发光、作为标准合成站
- 无限马桶：旧贴图、2x3 Tile、可坐、仅天顶世界配方
- Guestlogbook 五节点补丁（挂在 RockQuestII 上方）

## Recipe migration
Phase 3.1 的临时月亮合成站配方已迁移到终焉物质聚合仪。
已注册：虚粒子、衰变物质、消渐物质、幽灵物质、暗物质球（两条旧路线）、无尽锭。
无尽催化剂按旧版 QFH/QFD 动态倍率逻辑恢复，并在暗物质压缩机制作。

## Dark Matter Ball
恢复旧版交互语义：手持物品右键存入；空手右键一次性取出全部内容物。
内部数据继续使用 ModItem SaveData/NetSend，不依赖旧 InnoVault UI。

## Quest patch
任务节点顺序：无限马桶 / 无尽催化剂 / 天堂陨落长弓 / 寰宇贪婪镐 / 终焉物质聚合仪。
长弓和镐的本体尚未恢复，节点先使用旧图标并保持不可完成；下一阶段加入物品后自动识别。

## Current-version compatibility
- Calamity 2.x fruit names supported: SanguineTangerine / TaintedCloudberry / SacredStrawberry, with old-name fallbacks.
- HotPink and DarkOrange rarities resolve from CalamityMod, matching the original CWRID behavior.
- Quest branch falls back to NeutronStarIngotQuest/RockQuest when RockQuestII is unavailable.
