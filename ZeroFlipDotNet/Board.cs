using System;
using System.Linq;

namespace ZeroFlipDotNet
{
    class Board
    {
        private static Random _random = new Random();

        public int Rows { get; private set; }
        public int Cols { get; private set; }

        public Tile[,] Tiles { get; private set; }
        public Counter[] RowCounters { get; private set; }
        public Counter[] ColCounters { get; private set; }

        public Board(int rows, int cols)
        {
            Rows = rows;
            Cols = cols;
            Tiles = new Tile[rows, cols];
            RowCounters = new Counter[rows];
            ColCounters = new Counter[cols];

            Create();
        }

        public void Create()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Cols; col++)
                {
                    Tiles[row, col] = new Tile(_random.Next(4)); // TODO: Distribute tiles in a better way
                }
            }

            for (int i = 0; i < Rows; i++)
            {
                RowCounters[i] = new Counter(GetRow(i));
            }

            for (int i = 0; i < Cols; i++)
            {
                ColCounters[i] = new Counter(GetCol(i));
            }
        }

        public Tile[] GetRow(int row)
        {
            return Enumerable.Range(0, Cols).Select(x => Tiles[row, x]).ToArray();
        }

        public Tile[] GetCol(int col)
        {
            return Enumerable.Range(0, Rows).Select(x => Tiles[x, col]).ToArray();
        }

        public bool FlipTile(int row, int col)
        {
            Tile tile = Tiles[row, col];

            // Can't flip a tile that's already flipped
            if (tile.Flipped)
            {
                return false;
            }

            tile.Flipped = true;

            return true;
        }
    }
}
