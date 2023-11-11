using System;
using System.Collections.Generic;
using System.IO;

namespace PathFinder.Services
{
    public class FileLoader
    {
        private readonly string _mapsDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Resources", "Maps");

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


    }
}
