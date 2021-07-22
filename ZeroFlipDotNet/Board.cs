namespace ZeroFlipDotNet
{
    class Board
    {   
        public int Width { get; private set; }
        public int Height { get; private set; }
        public Tile[,] Tiles { get; private set; }

        public Board(int width, int height)
        {
            Width = width;
            Height = height;    
            Tiles = new Tile[height, width];
        }
    }
}
