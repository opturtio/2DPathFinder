using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace PathFinder.Services
{
    public class FileModifier
    {
        public List<Tuple<string, string>> ModifyMapNames(List<string> mapNames)
        {
            List<Tuple<string, string>> cleanMapNames = new List<Tuple<string, string>>();

            foreach (var mapName in mapNames)
            {
                var parts = mapName.Split(new char[] { '_', '.' });

                if (parts.Length >= 2)
                {
                    cleanMapNames.Add(Tuple.Create(parts[0], parts[1]));
                }
                else
                {
                    Console.WriteLine("Map name does not have two parts");
                }
            }
            return cleanMapNames;
        }
    }
}

