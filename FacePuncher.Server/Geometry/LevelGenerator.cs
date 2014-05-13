﻿using System;

using FacePuncher.Entities;

namespace FacePuncher.Geometry
{
    class LevelGenerator
    {
        public Level Generate(int seed)
        {
            var rand = new Random(seed == 0 ? (int) (DateTime.Now.Ticks & 0x7fffffff) : seed);

            var level = new Level();

            for (int i = 0; i < 4; ++i) {
                for (int j = 0; j < 4; ++j) {
                    var room = level.CreateRoom(new Rectangle(i * 8, j * 8, 8, 8));
                    
                    room.AddGeometry(new Rectangle(0, 0, room.Width, room.Height));
                    room.SubtractGeometry(new Rectangle(1, 1, room.Width - 2, room.Height - 2));

                    if (i > 0) room.SubtractGeometry(new Rectangle(0, 3, 1, 2));
                    if (j > 0) room.SubtractGeometry(new Rectangle(3, 0, 2, 1));
                    if (i < 3) room.SubtractGeometry(new Rectangle(7, 3, 1, 2));
                    if (j < 3) room.SubtractGeometry(new Rectangle(3, 7, 2, 1));

                    foreach (var tile in room) {
                        if (tile.State == TileState.Floor && rand.NextDouble() < 0.125) {
                            var dust = Entity.Create("dust");
                            dust.Place(tile);
                        }
                    }
                }
            }

            return level;
        }
    }
}
