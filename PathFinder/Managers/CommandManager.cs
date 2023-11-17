using System;
using PathFinder.Managers;

namespace PathFinder.Managers
{
    /// <summary>
    /// Manages the processing of user input commands.
    /// </summary>
    public class CommandManager
    {
        private FileManager _fileManager;
        private OutputManager _outputManager;

        private string currentMap;

        public CommandManager()
        {
            _fileManager = new FileManager();
            _outputManager = new OutputManager();
        }

        /// <summary>
        /// Processes the user's input received from the main menu.
        /// </summary>
        /// <param name="input">The user input as a string.</param>
        public void ProcessMainMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    Console.WriteLine("Pääsit tänne!");
                    break;
                case "2":
                    ProcessMainMenuOptionTwo();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
        }

        /// <summary>
        /// Processes main menu option two
        /// </summary>
        public void ProcessMainMenuOptionTwo()
        {
            _fileManager.LoadAndCleanMapFileNames();
            var cleanedFileNames = _fileManager.GetCleanedFileNames();
            _outputManager.PrintMapNames(cleanedFileNames);
            var mapInput = Console.ReadLine();
            if (mapInput != null) { ProcessMapMenuInput(mapInput); }
        }

        /// <summary>
        /// Processes the user's input received from the map menu.
        /// </summary>
        /// <param name="input">The user input as a string, representing the index number of a map.</param>
        public void ProcessMapMenuInput(string input)
        {
            currentMap = _fileManager.LoadMap(input);
            Console.WriteLine($"Debug, print map:\n {currentMap}");
        }

        /// <summary>
        /// Retrieves the current map.
        /// </summary>
        /// <returns>The current map as a string.</returns>
        public string GetCurrentMap()
        {
            return currentMap;
        }
    }
}