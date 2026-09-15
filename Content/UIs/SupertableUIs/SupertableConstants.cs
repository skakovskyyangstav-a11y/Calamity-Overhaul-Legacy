using Microsoft.Xna.Framework ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs
{
    public static class SupertableConstants
    {
        public const int CELL_WIDTH = 48 ;
        public const int CELL_HEIGHT = 46 ;
        public const int GRID_COLUMNS = 9 ;
        public const int GRID_ROWS = 9 ;
        public const int TOTAL_SLOTS = 81 ;
        public const int RECIPE_LENGTH = 82 ;
        public const float ANIMATION_SPEED_OPEN = 0.2f ;
        public const float ANIMATION_SPEED_CLOSE = 0.14f ;
        public const float HOVER_ANIMATION_SPEED = 0.1f ;
        public const float SOUND_PITCH_HIGH = 0.6f ;
        public const float SOUND_PITCH_LOW = -0.5f ;
        public const float SOUND_PITCH_CLOSE = -0.2f ;
        public static readonly Vector2 MAIN_UI_OFFSET = new(16, 30) ;
        public static readonly Vector2 INPUT_SLOT_OFFSET = new(555, 215) ;
        public static readonly Vector2 RECIPE_UI_OFFSET = new(545, 80) ;
        public static readonly Vector2 ORGANIZER_OFFSET = new(574, 330) ;
        public static readonly Vector2 ORGANIZER_LEFT_OFFSET = new(540, 330) ;
        public static readonly Vector2 HIGHLIGHTER_OFFSET = new(460, 420) ;
        public const int INPUT_SLOT_SIZE = 92 ;
        public const string NULL_ITEM_KEY = "Null/Null" ;
    }
}
