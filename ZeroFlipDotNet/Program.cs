﻿using System;

namespace ZeroFlipDotNet
{
    class Program
    {
        private static Board _board;
        
        static void Main(string[] args)
        {
            _board = new Board(5, 5);

            Console.WriteLine($"Board created with {_board.Tiles.Length} tiles ({_board.Rows} rows, {_board.Cols} cols)");

            PrintBoard();
        }

        public static void PrintBoard()
        {
            for (int row = 0; row < _board.Rows; row++)
            {
                if (row == 0)
                {
                    Console.WriteLine("-------------------------------");
                }

                for (int col = 0; col < _board.Cols; col++)
                {
                    Tile tile = _board.Tiles[row, col];

                    if (col == 0) Console.Write("|  ");
                    
                    Console.Write((tile.Flipped ? tile.Value : "?") + "  |  ");
                }

                Console.WriteLine();
                Console.WriteLine("-------------------------------");
            }
        }
    }
}
