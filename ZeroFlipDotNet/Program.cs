using System;

namespace ZeroFlipDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(5, 5);

            Console.WriteLine($"Board created with {board.Tiles.Length} tiles ({board.Rows} rows, {board.Cols} cols)");

            for (int row = 0; row < board.Rows; row++)
            {
                for (int col = 0; col < board.Cols; col++)
                {
                    Console.Write(board.Tiles[row, col].Value + " ");
                }
                Console.WriteLine();
            }
        }
    }
}
