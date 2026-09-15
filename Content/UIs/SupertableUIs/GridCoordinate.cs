using Microsoft.Xna.Framework ;
using System ;

namespace CalamityOverhaulLegacy.Content.UIs.SupertableUIs
{
    public readonly struct GridCoordinate : IEquatable<GridCoordinate>
    {
        public readonly int X ;
        public readonly int Y ;
        public GridCoordinate(int x, int y) { X = x ; Y = y ; }
        public static GridCoordinate FromScreenPosition(Vector2 screenPos, Vector2 gridTopLeft)
        {
            Vector2 relativePos = screenPos - gridTopLeft ;
            return new GridCoordinate((int)(relativePos.X / SupertableConstants.CELL_WIDTH), (int)(relativePos.Y / SupertableConstants.CELL_HEIGHT)) ;
        }
        public int ToIndex() => Y * SupertableConstants.GRID_COLUMNS + X ;
        public static GridCoordinate FromIndex(int index) => new(index % SupertableConstants.GRID_COLUMNS, index / SupertableConstants.GRID_COLUMNS) ;
        public Vector2 ToScreenPosition(Vector2 topLeft) => new Vector2(X * SupertableConstants.CELL_WIDTH, Y * SupertableConstants.CELL_HEIGHT) + topLeft ;
        public bool IsValid() => X >= 0 && X < 9 && Y >= 0 && Y < 9 ;
        public bool Equals(GridCoordinate other) => X == other.X && Y == other.Y ;
        public override bool Equals(object obj) => obj is GridCoordinate other && Equals(other) ;
        public override int GetHashCode() => HashCode.Combine(X, Y) ;
    }
}
