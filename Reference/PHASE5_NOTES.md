# Phase 5 — recheck + weapon restoration

## Rechecked / fixed
- 暗物质压缩机动画帧跨度从 54 修正为 56（旧贴图 73x224，4 帧）。
- 暗物质球恢复旧版右键交互顺序：手持其它物品右键存入；空手右键一次性取出全部。
- 暗物质球恢复 maxStack=99，并加入堆叠/拆分时的数据保护。
- Guestlogbook 五节点保留；天堂陨落长弓与寰宇贪婪镐现在有真实物品 ID，节点可完成。

## Added
- 寰宇贪婪镐：旧 Pickaxe/Hammer 贴图、9999 镐/锤力、Q（可改键）切换、右键范围破坏。
- 天堂陨落长弓：旧贴图、左键无尽之箭、右键天堂箭雨、200 终焉充能、无尽符文、13 枚万象审判。

## Exact legacy recipe counts
- Infinite Pick: Infinite Ingot x18 + Neutron Star Ingot x12 + Crystyl Crusher x1.
- Heavenfall Longbow: Infinite Ingot x19 + Neutron Star Ingot x7 + Drataliornus x1 + Eternity x1 + Heavenly Gale x1.

## Intentional rewrite
旧版两件装备大量依赖 CWR 自定义 DamageClass、BaseHeldProj、PRT、VaultUtils 和 CWRKeySystem。Phase 5 保留核心玩法、原贴图、主要数值与旧配方，但弹幕/热键改为当前 tModLoader 原生实现。
