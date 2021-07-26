using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace ZeroFlipDotNet
{
    class Level
    {
        private static Random _random = new Random();
        private static string _filePath = @"data\levels.json";

        public int LevelNumber { get; set; }
        public List<int[]> TileSets { get; set; }

        public int[] GetRandomTileSet(bool shuffled)
        {
            int[] tileSet = TileSets[_random.Next(0, TileSets.Count)];
            if (shuffled)
            {
                tileSet = tileSet.OrderBy(r => _random.Next()).ToArray();
            }
            return tileSet;
        }

        public static List<Level> LoadLevels()
        {
            string json = File.ReadAllText(_filePath);
            List<Level> levels = JsonSerializer.Deserialize<List<Level>>(json);
            return levels;
        }
    }
}
