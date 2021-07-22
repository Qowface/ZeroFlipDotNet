using System;

namespace ZeroFlipDotNet
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(5, 5);
            Console.WriteLine($"Board created with {board.Tiles.Length} tiles ({board.Height} rows, {board.Width} cols)");
        }
    }
}
