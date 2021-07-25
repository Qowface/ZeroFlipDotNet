using System;

namespace ZeroFlipDotNet
{
    class Program
    {
        private static Board _board;
        
        static void Main(string[] args)
        {
            _board = new Board(5, 5);

            // TODO: Intro before game starts

            while (Play())
            {
            }

            // TODO: Show results (won, lost, quit)
        }

        public static bool Play()
        {
            Console.WriteLine();
            PrintBoard();
            Console.WriteLine();
            Console.WriteLine();

            // Get player input
            Console.WriteLine("Which tile would you like to flip? Enter as: Row,Col");
            string input = Console.ReadLine();

            // Quit game if "quit" is entered
            if (input.ToLower().Equals("quit"))
            {
                return false;
            }

            // Split input at the ',' and make sure there are exactly two arguments
            string[] inputs = input.Split(',');
            if (inputs.Length != 2)
            {
                Console.WriteLine("Please enter exactly two coordinates");
                return true;
            }

            // Parse the row and col
            if (int.TryParse(inputs[0], out int row) && int.TryParse(inputs[1], out int col))
            {
                // Make sure we're entering coordinates actually on the board
                if (row < 1 || row > _board.Rows || col < 1 || col > _board.Cols)
                {
                    Console.WriteLine("Please enter coordinates on the board");
                    return true;
                }

                bool flipped = _board.FlipTile(row - 1, col - 1);

                // Indicate if tile has already been flipped
                if (!flipped)
                {
                    Console.WriteLine("This tile is already flipped");
                    return true;
                }

                Console.WriteLine($"Flipped tile at {row},{col}");

                // TODO: Check if the board is won or lost to determine return value

                return true;
            }
            else
            {
                Console.WriteLine("Please enter valid whole numbers");
                return true;
            }
        }

        public static void PrintBoard()
        {
            for (int row = 0; row < _board.Rows; row++)
            {
                if (row == 0)
                {
                    for (int col = 0; col < _board.Cols; col++)
                    {
                        if (col == 0) Console.Write("      ");
                        
                        Console.Write($"[{col+1}]   ");
                    }

                    Console.WriteLine();
                    Console.WriteLine("    -------------------------------"); // TODO: Make dynamic based on number of columns
                }

                for (int col = 0; col < _board.Cols; col++)
                {
                    Tile tile = _board.Tiles[row, col];

                    if (col == 0) Console.Write($"[{row+1}] |  ");
                    
                    Console.Write((tile.Flipped ? tile.Value : "?") + "  |  ");

                    if (col == _board.Cols - 1) Console.Write(_board.RowCounters[row].Total + " 0x" + _board.RowCounters[row].ZeroCount);
                }

                Console.WriteLine();
                Console.WriteLine("    -------------------------------");

                if (row == _board.Rows - 1)
                {
                    for (int col = 0; col < _board.Cols; col++)
                    {
                        if (col == 0) Console.Write("       ");

                        Console.Write(_board.ColCounters[col].Total + "     "); // TODO: Fix spacing based on number of digits
                    }

                    Console.WriteLine();

                    for (int col = 0; col < _board.Cols; col++)
                    {
                        if (col == 0) Console.Write("      ");

                        Console.Write("0x" + _board.ColCounters[col].ZeroCount + "   ");
                    }
                }
            }
        }
    }
}
