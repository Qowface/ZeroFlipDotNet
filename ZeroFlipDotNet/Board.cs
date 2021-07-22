using System;

namespace ZeroFlipDotNet
{
    class Board
    {
        private static Random _random = new Random();

        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public Tile[,] Tiles { get; private set; }

        public Board(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;    
            Tiles = new Tile[rows, cols];

            Create();
        }

        public void Create()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Tiles[row, col] = new Tile(_random.Next(4));
                    Tiles[row, col].Flipped = true; // Flip all tiles so we can see the values for now
                }
            }
        }
    }
}
