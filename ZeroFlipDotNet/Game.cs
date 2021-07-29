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
            Setup();
        }

        public void Setup()
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
            Console.WriteLine("In ZeroFlip, the board consists of a grid of tiles with values 0 through 3. Tiles start out flipped");
            Console.WriteLine("face down. On each turn, flip over a tile! If a 0 is flipped over, the game is lost. If all of the");
            Console.WriteLine("2 and 3 tiles are flipped over, the game is won. Each row and column on the board is labeled with the");
            Console.WriteLine("total value of the tiles in that row/colum, as well as a count of how many 0's are in that row/column.");
            Console.WriteLine("You can type \"quit\" at any time to quit.");

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
                Console.WriteLine("You flipped over all of the 2 and 3 tiles! You win!");
            }
            else if (_board.Lost)
            {
                Console.WriteLine("You flipped over a 0 tile! You lost this round!");
            }

            Console.WriteLine("Would you like to play again? Y/N");
            string input = Console.ReadLine();
            if (input.ToLower().Equals("y"))
            {
                Console.WriteLine("Restarting...");
                Setup();
                return;
            }

            Console.WriteLine("Thanks for playing! Goodbye!");
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
