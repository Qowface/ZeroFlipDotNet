using System;

namespace ZeroFlipDotNet
{
    class Program
    {
        private static Game _game;
        
        static void Main(string[] args)
        {
            _game = new Game();

            while (_game.Active)
            {
                _game.Update();
            }
        }
    }
}
