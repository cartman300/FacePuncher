﻿using System.Collections.Generic;

namespace FacePuncher.Geometry
{
    class Room : IEnumerable<Tile>
    {
        public Rectangle Rect { get; private set; }

        public int Left { get { return Rect.Left; } }
        public int Top { get { return Rect.Top; } }
        public int Right { get { return Rect.Right; } }
        public int Bottom { get { return Rect.Bottom; } }

        public int Width { get { return Rect.Width; } }
        public int Height { get { return Rect.Height; } }
        
        private Tile[,] _tiles;

        public Room(Rectangle rect)
        {
            Rect = rect;

            _tiles = new Tile[Width, Height];

            for (int y = 0; y < Height; ++y) {
                for (int x = 0; x < Width; ++x) {
                    _tiles[x, y] = new Tile(this, x, y);
                }
            }
        }

        public IEnumerator<Tile> GetEnumerator()
        {
            for (int y = 0; y < Height; ++y) {
                for (int x = 0; x < Width; ++x) {
                    yield return _tiles[x, y];
                }
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
