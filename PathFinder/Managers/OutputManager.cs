using System;
using System.Collections.Generic;
using System.Reflection;
using System.Resources;

namespace PathFinder.Managers
{
    /// <summary>
    /// Manages the output of the text to the console.
    /// </summary>
    public class OutputManager
    {
        private ResourceManager _resourceManager;

        public OutputManager()
        {
            // Instantiates the ResourceManager
            _resourceManager = new ResourceManager("PathFinder.Resources.AllStrings", Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Prints the given string from AllStrings-ResX file using stringName as the key
        /// </summary>
        /// <param name="stringName">The key used to find and print the string from the resource file.</param>
        public void PrintText(string stringName)
        {
            string outputText = _resourceManager.GetString(stringName);
            Console.WriteLine(outputText);
        }

        /// <summary>
        /// Prints a list of map names and their sizes to the console.
        /// </summary>
        /// <param name="mapNames">A list of tuples, each containing two strings representing the name and size of a map file.</param>
        public void PrintMapNames(List<Tuple<string, string>> mapNames)
        {
            for (int i = 0; i < mapNames.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {mapNames[i].Item1} - {mapNames[i].Item2}");
            }
        }
    }
}
