using System;
using PathFinder.Managers;

namespace PathFinder.Managers
{
    public class CommandManager
    {
        private FileManager _fileManager;
        private OutputManager _outputManager;

        public CommandManager()
        {
            _fileManager = new FileManager();
            _outputManager = new OutputManager();
        }

        public void ProcessMainMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    Console.WriteLine("Pääsit tänne!");
                    break;
                case "2":
                    _fileManager.LoadAndCleanMapFileNames();
                    var cleanedFileNames = _fileManager.GetCleanedFileNames();
                    _outputManager.PrintMapNames(cleanedFileNames);
                    input = Console.ReadLine();
                    ProcessMapMenuInput(input);
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
        }

        public void ProcessMapMenuInput(string input)
        {
            Console.WriteLine(input);
        }
    }
}