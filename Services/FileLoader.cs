using System;
using System.Collections.Generic;
using System.IO;

namespace PathFinder.Services
{
    public class FileLoader
    {
        private readonly string _mapsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", "Maps");
        private string map = "";

        public List<string> LoadMapFileNames()
        {
            var mapFileNames = new List<string>();

            if (!Directory.Exists(_mapsDirectory))
            {
                Console.WriteLine("Maps directory not found!");
                return mapFileNames;
            }

            foreach (var filePath in Directory.GetFiles(_mapsDirectory))
            {
                string fileName = Path.GetFileName(filePath);
                mapFileNames.Add(fileName);
            }
            return mapFileNames;
        }

        public string LoadMap(string indexNum)
        {
            int indexNumber = int.Parse(indexNum)-1;
            var mapFileNames = LoadMapFileNames();
            if (indexNumber >= 0 && indexNumber <= mapFileNames.Count)
            {
                string selectedMapFilePath = Path.Combine(_mapsDirectory, mapFileNames[indexNumber]);
                map = File.ReadAllText(selectedMapFilePath);
            }
            return map;
        }
    }
}
