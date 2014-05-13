﻿namespace FacePuncher.Geometry
{
    /// <summary>
    /// Structure representing a location in Cartesian coordinates.
    /// </summary>
    struct Position
    {
        public const Position Zero = new Position(0, 0);
        public const Position UnitX = new Position(1, 0);
        public const Position UnitY = new Position(0, 1);

        /// <summary>
        /// Horizontal component of the position.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Vertical component of the position.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Constructs a position with the specified coordinates.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Position operator +(Position a, Position b)
        {
            return new Position(a.X + b.X, a.Y + b.Y);
        }

        public static Position operator -(Position a, Position b)
        {
            return new Position(a.X - b.X, a.Y - b.Y);
        }

        public static Position operator *(Position pos, int mul)
        {
            return new Position(pos.X * mul, pos.Y * mul);
        }

        public static Position operator *(int mul, Position pos)
        {
            return new Position(pos.X * mul, pos.Y * mul);
        }
    }
}