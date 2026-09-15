using CalamityOverhaulLegacy.Content.Items.Tools ;
using CalamityOverhaulLegacy.Content.TileEntities ;
using CalamityOverhaulLegacy.Content.UIs.SupertableUIs.Inventory ;
using CalamityOverhaulLegacy.Content.UIs.SupertableUIs.UIContent ;
using InnoVault.UIHandles ;
using Microsoft.Xna.Framework ;
using Microsoft.Xna.Framework.Graphics ;
using Microsoft.Xna.Framework.Input ;
using System ;
using System.Collections.Generic ;
using System.Linq ;
using System.Reflection ;
using Terraria ;
using Terraria.Audio ;
using Terraria.DataStructures ;
using Terraria.GameContent ;
using Terraria.ID ;
using Terraria.Localization ;
using Terraria.ModLoader ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs
{
    public class SupertableUI : UIHandle, ILocalizedModType
    {
        public string LocalizationCategory => "UI" ;
        public static SupertableUI Instance => UIHandleLoader.GetUIHandleOfType<SupertableUI>() ;
        public static readonly List<RecipeData> AllRecipes = new() ;
        public static readonly List<string> SkippedRecipes = new() ;

        public static LocalizedText RecipeViewLabel { get ; private set ; }
        public static LocalizedText RecipeEmptyLabel { get ; private set ; }
        public static LocalizedText PlacementMonitorOff { get ; private set ; }
        public static LocalizedText PlacementMonitorOn { get ; private set ; }
        public static LocalizedText QuickPlaceMaterials { get ; private set ; }
        public static LocalizedText QuickTakeMaterials { get ; private set ; }

        private SupertableController controller ;
        private RecipeSidebarManager sidebar ;
        private RecipeNavigator navigator ;
        private DragController drag ;
        private QuickActionsManager quick ;
        private int boundEntityId = -1 ;
        private GridCoordinate hovered = new(-1, -1) ;
        private Rectangle grid ;
        private Rectangle result ;
        private int autoSave ;

        public int BoundEntityId => boundEntityId ;
        internal RecipeSidebarManager SidebarManager => sidebar ;
        internal RecipeNavigator RecipeNavigator => navigator ;
        public bool HoverInPutItemCellPage { get ; private set ; }
        public bool OnInputSlot { get ; private set ; }
        public Vector2 TopLeft => DrawPosition + SupertableConstants.MAIN_UI_OFFSET ;

        private static Texture2D MainValue =>
            ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/SupertableUIs/MainValue").Value ;

        private static Texture2D InputArrow =>
            ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/SupertableUIs/InputArrow").Value ;

        private static Texture2D InputArrow2 =>
            ModContent.Request<Texture2D>("CalamityOverhaulLegacy/Assets/UIs/SupertableUIs/InputArrow2").Value ;

        public override Texture2D Texture => MainValue ;
        public override bool CloseOnEscape => true ;

        public Item[] Items
        {
            get
            {
                Ensure() ;
                return controller.SlotManager.Slots ;
            }
            set
            {
                Ensure() ;
                controller.SlotManager.Slots = value ;
            }
        }

        public override void SetStaticDefaults()
        {
            RecipeViewLabel = this.GetLocalization(nameof(RecipeViewLabel), () => "查看配方") ;
            RecipeEmptyLabel = this.GetLocalization(nameof(RecipeEmptyLabel), () => "无") ;
            PlacementMonitorOff = this.GetLocalization(nameof(PlacementMonitorOff), () => "关闭摆放监视") ;
            PlacementMonitorOn = this.GetLocalization(nameof(PlacementMonitorOn), () => "开启摆放监视") ;
            QuickPlaceMaterials = this.GetLocalization(nameof(QuickPlaceMaterials), () => "快捷放置材料") ;
            QuickTakeMaterials = this.GetLocalization(nameof(QuickTakeMaterials), () => "快捷拿取材料") ;
        }

        private void Ensure()
        {
            if (controller != null) {
                return ;
            }

            controller = new SupertableController() ;
            sidebar = new RecipeSidebarManager(this) ;
            navigator = new RecipeNavigator(this, controller) ;
            drag = new DragController(this) ;
            quick = new QuickActionsManager(this, controller) ;

            LoadRecipes() ;
            controller.InitializeRecipes(AllRecipes) ;
            sidebar.InitializeRecipeElements() ;
            navigator.LoadAllRecipes() ;

            if (DrawPosition == Vector2.Zero) {
                DrawPosition = new Vector2(
                    Math.Max(20, (Main.screenWidth - MainValue.Width) / 2),
                    Math.Max(20, (Main.screenHeight - MainValue.Height) / 2)
                ) ;
            }
        }

        private static void LoadRecipes()
        {
            if (AllRecipes.Count > 0) {
                return ;
            }

            SkippedRecipes.Clear() ;

            Type type = typeof(SupertableRecipeData) ;

            foreach (FieldInfo field in type.GetFields(BindingFlags.Public | BindingFlags.Static)) {
                if (field.FieldType != typeof(string[])) {
                    continue ;
                }

                string[] values = (string[])field.GetValue(null) ;

                if (!LegacyItemResolver.ValidateRecipe(values, out string missing)) {
                    if (values != null
                        && values.Length == 82
                        && values[^1] != SupertableConstants.NULL_ITEM_KEY) {
                        SkippedRecipes.Add($"{field.Name}: {missing}") ;
                    }
                    continue ;
                }

                RecipeData recipe = new RecipeData {
                    Values = values,
                    Target = LegacyItemResolver.Resolve(values[^1])
                } ;

                recipe.BuildMaterialTypesCache() ;
                AllRecipes.Add(recipe) ;
            }

            ModContent.GetInstance<global::CalamityOverhaulLegacy.CalamityOverhaulLegacy>()
                .Logger.Info(
                    $"Legacy Supertable recipes: {AllRecipes.Count}, skipped: {SkippedRecipes.Count}"
                ) ;
        }

        internal static void Open(LegacyTransmutationEntity entity)
        {
            if (Main.dedServ || entity == null) {
                return ;
            }

            SupertableUI ui = Instance ;

            if (ui == null) {
                ModContent.GetInstance<global::CalamityOverhaulLegacy.CalamityOverhaulLegacy>()
                    .Logger.Warn("Legacy Supertable UIHandle instance is unavailable.") ;
                return ;
            }

            ui.Ensure() ;

            if (ui.IsOpen && ui.boundEntityId == entity.ID) {
                ui.Close() ;
                return ;
            }

            if (ui.IsOpen && ui.boundEntityId >= 0 && ui.boundEntityId != entity.ID) {
                ui.SaveBoundItems(true) ;
            }

            ui.boundEntityId = entity.ID ;
            ui.controller.SlotManager.Slots = entity.Items
                .Select(item => item?.Clone() ?? new Item())
                .ToArray() ;

            ui.controller.UpdateRecipeMatching(false) ;

            if (!Main.playerInventory) {
                Main.playerInventory = true ;
            }

            Main.recBigList = false ;
            ui.Open() ;

            SoundEngine.PlaySound(
                new SoundStyle("CalamityOverhaulLegacy/Assets/Sounds/ButtonZero") with { Pitch = 0.3f }
            ) ;
        }

        protected override void OnClose()
        {
            Ensure() ;
            controller.AnimationController.RequestDelayedClose(0) ;
            SaveBoundItems(true) ;
            boundEntityId = -1 ;
        }

        internal void ReceiveEntityItems(Item[] items)
        {
            Ensure() ;

            if (items == null) {
                return ;
            }

            controller.SlotManager.Slots = items
                .Select(item => item?.Clone() ?? new Item())
                .ToArray() ;

            controller.UpdateRecipeMatching(false) ;
        }

        public override void OnEnterWorld()
        {
            Ensure() ;

            if (IsOpen) {
                Close() ;
            }

            SnapOpenProgress() ;
            boundEntityId = -1 ;
            controller.AnimationController.ForceClose() ;
        }

        public override void Update()
        {
            Ensure() ;

            controller.UpdateAnimations(
                IsOpen,
                hovered.IsValid() ? hovered.ToIndex() : -1
            ) ;

            if (!IsOpen && controller.AnimationController.OpenProgress <= 0f) {
                return ;
            }

            UpdateRects() ;
            drag.Update() ;

            if (drag.IsDragging) {
                Main.LocalPlayer.mouseInterface = true ;
            }

            sidebar.Update() ;
            navigator.Update() ;
            quick.Update() ;

            UpdateRects() ;
            HandleInput() ;

            if (IsOpen && boundEntityId >= 0) {
                autoSave++ ;

                if (autoSave >= 300) {
                    autoSave = 0 ;
                    SaveBoundItems(true) ;
                }

                if (TileEntity.ByID.TryGetValue(boundEntityId, out TileEntity tileEntity)
                    && tileEntity is LegacyTransmutationEntity entity) {
                    if (Vector2.Distance(
                        Main.LocalPlayer.Center,
                        entity.Position.ToWorldCoordinates(40, 24)
                    ) >= 120f || Main.LocalPlayer.dead) {
                        Close() ;
                        SoundEngine.PlaySound(SoundID.MenuClose with { Pitch = -0.2f }) ;
                    }
                }
                else {
                    Close() ;
                }
            }
        }

        private void UpdateRects()
        {
            grid = new Rectangle(
                (int)TopLeft.X,
                (int)TopLeft.Y,
                48 * 9,
                46 * 9
            ) ;

            result = new Rectangle(
                (int)(DrawPosition.X + 555),
                (int)(DrawPosition.Y + 215),
                92,
                92
            ) ;

            UIHitBox = new Rectangle(
                (int)DrawPosition.X,
                (int)DrawPosition.Y,
                grid.Width + 200,
                grid.Height + 44
            ) ;

            hoverInMainPage = UIHitBox.Intersects(MouseHitBox) ;
            HoverInPutItemCellPage = grid.Intersects(MouseHitBox) ;
            OnInputSlot = result.Intersects(MouseHitBox) ;

            hovered = HoverInPutItemCellPage
                ? GridCoordinate.FromScreenPosition(MousePosition, TopLeft)
                : new GridCoordinate(-1, -1) ;
        }

        private void HandleInput()
        {
            if (!hoverInMainPage
                && !Main.LocalPlayer.mouseInterface
                && keyLeftPressState == KeyPressState.Pressed) {
                SoundEngine.PlaySound(
                    new SoundStyle("CalamityOverhaulLegacy/Assets/Sounds/ButtonZero") with { Pitch = -0.2f }
                ) ;
                Close() ;
                return ;
            }

            if (OnInputSlot) {
                Main.LocalPlayer.mouseInterface = true ;

                if ((keyLeftPressState == KeyPressState.Pressed
                    || keyLeftPressState == KeyPressState.Held)
                    && controller.TryTakeResult(ref Main.mouseItem)) {
                    SoundEngine.PlaySound(SoundID.Research) ;
                    SaveBoundItems(true) ;
                }

                return ;
            }

            if (!HoverInPutItemCellPage || !hovered.IsValid()) {
                return ;
            }

            Main.LocalPlayer.mouseInterface = true ;

            int index = hovered.ToIndex() ;
            Item slot = controller.SlotManager.GetSlot(index) ;

            KeyboardState keyboard = Keyboard.GetState() ;
            bool shift = keyboard.IsKeyDown(Keys.LeftShift)
                || keyboard.IsKeyDown(Keys.RightShift) ;

            if (shift && keyLeftPressState == KeyPressState.Pressed) {
                ItemInteractionHandler.QuickTransferToInventory(
                    slot,
                    Main.LocalPlayer
                ) ;
                controller.UpdateRecipeMatching() ;
                return ;
            }

            if (keyLeftPressState == KeyPressState.Pressed) {
                ItemInteractionHandler.HandleLeftClick(
                    ref slot,
                    ref Main.mouseItem
                ) ;
                controller.SlotManager.SetSlot(index, slot) ;
                controller.UpdateRecipeMatching() ;
            }

            if (keyRightPressState == KeyPressState.Pressed) {
                ItemInteractionHandler.HandleRightClick(
                    ref slot,
                    ref Main.mouseItem
                ) ;
                controller.SlotManager.SetSlot(index, slot) ;
                controller.UpdateRecipeMatching() ;
            }

            if (keyRightPressState == KeyPressState.Held) {
                ItemInteractionHandler.HandleDragPlace(
                    ref slot,
                    ref Main.mouseItem
                ) ;
                controller.SlotManager.SetSlot(index, slot) ;
                controller.UpdateRecipeMatching() ;
            }

            if (shift && Main.mouseItem.IsAir) {
                ItemInteractionHandler.GatherSameItems(
                    controller.SlotManager.Slots,
                    index
                ) ;
                controller.UpdateRecipeMatching() ;
            }
        }

        internal static void SyncToNetworkIfNeeded()
        {
            Instance?.SaveBoundItems(true) ;
        }

        private void SaveBoundItems(bool sync)
        {
            if (boundEntityId < 0 || controller == null) {
                return ;
            }

            if (TileEntity.ByID.TryGetValue(boundEntityId, out TileEntity tileEntity)
                && tileEntity is LegacyTransmutationEntity entity) {
                entity.SetItems(controller.SlotManager.Slots) ;

                if (sync) {
                    entity.SendClientSync() ;
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Ensure() ;

            float alpha = controller.AnimationController.OpenProgress ;

            if (alpha <= 0f) {
                return ;
            }

            sidebar.Draw(spriteBatch, alpha) ;
            spriteBatch.Draw(MainValue, DrawPosition, Color.White * alpha) ;

            for (int i = 0 ; i < 81 ; i++) {
                Item preview = controller.SlotManager.GetPreviewSlot(i) ;

                if (!preview.IsAir) {
                    DrawItemIcon(
                        spriteBatch,
                        preview,
                        ArcCellPos(i),
                        alpha * 0.25f,
                        1f + controller.AnimationController.GetSlotHoverProgress(i) * 0.2f
                    ) ;
                }
            }

            for (int i = 0 ; i < 81 ; i++) {
                Item item = controller.SlotManager.GetSlot(i) ;

                if (!item.IsAir) {
                    DrawItemIcon(
                        spriteBatch,
                        item,
                        ArcCellPos(i),
                        alpha,
                        1f + controller.AnimationController.GetSlotHoverProgress(i) * 0.2f
                    ) ;
                }
            }

            if (controller.ResultManager.HasResult) {
                DrawItemIcon(
                    spriteBatch,
                    controller.ResultManager.ResultItem,
                    new Vector2(result.X, result.Y),
                    alpha,
                    1.5f
                ) ;
            }

            spriteBatch.Draw(
                controller.ResultManager.HasResult ? InputArrow : InputArrow2,
                DrawPosition + new Vector2(460, 225),
                Color.White * alpha
            ) ;

            navigator.Draw(spriteBatch, alpha) ;
            quick.Draw(spriteBatch, alpha) ;
            DrawHover() ;
        }

        private void DrawHover()
        {
            if (hovered.IsValid() && HoverInPutItemCellPage) {
                Item item = controller.SlotManager.GetSlot(hovered.ToIndex()) ;

                if (item.IsAir) {
                    item = controller.SlotManager.GetPreviewSlot(hovered.ToIndex()) ;
                }

                if (!item.IsAir) {
                    Main.HoverItem = item.Clone() ;
                    Main.hoverItemName = item.Name ;
                }
            }

            if (OnInputSlot && controller.ResultManager.HasResult) {
                Main.HoverItem = controller.ResultManager.ResultItem.Clone() ;
                Main.hoverItemName = Main.HoverItem.Name ;
            }
        }

        public static void DrawItemIcon(
            SpriteBatch spriteBatch,
            Item item,
            Vector2 position,
            float alpha,
            float scale
        )
        {
            if (item == null || item.IsAir) {
                return ;
            }

            Main.instance.LoadItem(item.type) ;

            Texture2D texture = TextureAssets.Item[item.type].Value ;
            Rectangle frame = Main.itemAnimations[item.type] != null
                ? Main.itemAnimations[item.type].GetFrame(texture)
                : texture.Frame() ;

            float max = Math.Max(frame.Width, frame.Height) ;
            float drawScale = (max > 36f ? 36f / max : 1f) * scale ;
            Vector2 center = position + new Vector2(24, 23) ;

            spriteBatch.Draw(
                texture,
                center,
                frame,
                Color.White * alpha,
                0f,
                frame.Size() / 2,
                drawScale,
                SpriteEffects.None,
                0f
            ) ;

            if (item.type == ModContent.ItemType<DarkMatterBall>()) {
                Texture2D full = ModContent.Request<Texture2D>(
                    "CalamityOverhaulLegacy/Content/Items/Tools/Full"
                ).Value ;

                spriteBatch.Draw(
                    full,
                    center,
                    null,
                    Color.White
                        * (0.35f + 0.35f * (float)Math.Abs(Math.Sin(Main.GameUpdateCount * 0.01f)))
                        * alpha,
                    0f,
                    full.Size() / 2,
                    drawScale,
                    SpriteEffects.None,
                    0f
                ) ;
            }

            if (item.stack > 1) {
                Utils.DrawBorderStringFourWay(
                    spriteBatch,
                    FontAssets.ItemStack.Value,
                    item.stack.ToString(),
                    position.X,
                    position.Y + 25,
                    Color.White * alpha,
                    Color.Black * alpha,
                    new Vector2(0.3f),
                    scale
                ) ;
            }
        }

        public Vector2 ArcCellPos(int index)
        {
            return GridCoordinate
                .FromIndex(index)
                .ToScreenPosition(TopLeft) ;
        }
    }
}
