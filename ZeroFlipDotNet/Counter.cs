namespace ZeroFlipDotNet
{
    class Counter
    {
        private Tile[] _tiles;
        public int Total { get; private set; }
        public int ZeroCount { get; private set; }

        public Counter(Tile[] tiles)
        {
            _tiles = tiles;
        }
    }
}
