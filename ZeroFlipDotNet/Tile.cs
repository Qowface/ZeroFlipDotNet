﻿namespace ZeroFlipDotNet
{
    class Tile
    {
        public int Value { get; set; }
        public bool Flipped { get; set; }

        public Tile(int value)
        {
            Value = value;
            Flipped = false;
        }
    }
}
