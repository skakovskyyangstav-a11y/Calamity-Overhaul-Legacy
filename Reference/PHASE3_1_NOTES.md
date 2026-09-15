# Phase 3.1

## Fixed

- 虚粒子、衰变物质、消渐物质、幽灵物质、无尽催化剂不再拥有 HoldUp 使用动作。
- 保留它们的物品栏帧动画，但左键不再触发放大手持动作。
- 无尽锭不再由 Legacy 自己重复插入“可放置”；只保留 Terraria 原生“可放置”提示。
- 暗物质球改为 maxStack = 1，避免带内部数据的球互相堆叠损坏数据。
- 暗物质球不再在 CanRightClick() 中修改数据，存取行为移到 RightClick()。

## Playable recipe bridge

在终焉物质聚合仪恢复完成之前，前三种旧 SuperTable 配方临时挂到月亮合成站：
- 虚粒子
- 衰变物质
- 消渐物质

材料数量严格来自 0.9025 的 9x9 配方数据。

幽灵物质依赖 Calamity 的 Dark Plasma，留到下一阶段与 Calamity 物品 ID 兼容层一起注册。

## Next

- DarkMatterCompressorItem + Tile
- TransmutationOfMatterItem + Tile
- InfiniteToiletItem + Tile
- 把临时月亮合成站配方迁回终焉物质聚合仪
- Guestlogbook 五节点兼容补丁
