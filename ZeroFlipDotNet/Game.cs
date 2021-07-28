using System;

namespace ZeroFlipDotNet
{
    enum GameState
    {
        Starting,
        Playing,
        Ending
    }
    
    class Game
    {
        private Board _board;
        public GameState State { get; private set; }
        public bool Active { get; private set; }

        public Game()
        {
            _board = new Board(5, 5);
            State = GameState.Starting;
            Active = true;
        }

        public void Update()
        {
            switch (State)
            {
                case GameState.Starting:
                    StartScreen();
                    break;
                case GameState.Playing:
                    Play();
                    break;
                case GameState.Ending:
                    EndScreen();
                    break;
            }
        }

        private void StartScreen()
        {
            Console.WriteLine("Welcome to ZeroFlip!");
            Console.WriteLine("Game starting...");

            // TODO: Proper intro with instructions, maybe a menu

            State = GameState.Playing;
        }

        private void Play()
        {
            PrintBoard();

            // Get player input
            Console.WriteLine("Which tile would you like to flip? Enter as: Row,Col");
            string input = Console.ReadLine();

            // Quit game if "quit" is entered
            if (input.ToLower().Equals("quit"))
            {
                Active = false;
                return;
            }

            // Split input at the ',' and make sure there are exactly two arguments
            string[] inputs = input.Split(',');
            if (inputs.Length != 2)
            {
                Console.WriteLine("Please enter exactly two coordinates");
                return;
            }

            // Parse the row and col
            if (int.TryParse(inputs[0], out int row) && int.TryParse(inputs[1], out int col))
            {
                // Make sure we're entering coordinates actually on the board
                if (row < 1 || row > _board.Rows || col < 1 || col > _board.Cols)
                {
                    Console.WriteLine("Please enter coordinates on the board");
                    return;
                }

                bool flipped = _board.FlipTile(row - 1, col - 1);

                // Indicate if tile has already been flipped
                if (!flipped)
                {
                    Console.WriteLine("This tile is already flipped");
                    return;
                }

                Console.WriteLine($"Flipped tile at {row},{col}");

                // Return false to end game if the board is won or lost
                if (_board.Won || _board.Lost)
                {
                    State = GameState.Ending;
                }
            }
            else
            {
                Console.WriteLine("Please enter valid whole numbers");
            }
        }

        private void EndScreen()
        {
            _board.FlipAllTiles();
            PrintBoard();

            if (_board.Won)
            {
                Console.WriteLine("You win!");
            }
            else if (_board.Lost)
            {
                Console.WriteLine("You lose!");
            }

            // TODO: Offer option to play again or quit

            Active = false;
        }

        private void PrintBoard()
        {
            Console.WriteLine();

            for (int row = 0; row < _board.Rows; row++)
            {
                if (row == 0)
                {
                    for (int col = 0; col < _board.Cols; col++)
                    {
                        if (col == 0) Console.Write("      ");

                        Console.Write($"[{col + 1}]   ");
                    }

                    Console.WriteLine();
                    Console.WriteLine("    -------------------------------"); // TODO: Make dynamic based on number of columns
                }

                for (int col = 0; col < _board.Cols; col++)
                {
                    Tile tile = _board.Tiles[row, col];

                    if (col == 0) Console.Write($"[{row + 1}] |  ");

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

            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
