using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Managers;

namespace PathFinder.Managers
{
    public class InputManager
    {
        CommandManager _commandManager;
        public InputManager()
        {
            _commandManager = new CommandManager();
        }
        public void ReadInput()
        {
            string userInput = Console.ReadLine();
            if (userInput == null)
            {
                return;
            }
            _commandManager.ProcessMainMenuInput(userInput);
        }

    }
}
