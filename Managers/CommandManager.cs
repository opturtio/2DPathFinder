using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder.Managers
{
    public class CommandManager
    {
        private FileManager _fileManager;
        public CommandManager()
        {
            _fileManager = new FileManager();
        }
        // processes users input
        public void ProcessMainMenuInput(string input)
        {
            switch (input)
            {
                case "1":
                    Console.WriteLine("Pääsit tänne!");
                    break;
                case "2":
                    _fileManager.Initialize();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
