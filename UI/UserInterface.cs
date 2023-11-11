using System;
using PathFinder.Managers;

namespace PathFinder.UI
{
    public class UserInterface
    {
        private OutputManager _outputManager;
        private CommandManager _commandManager;

        public UserInterface()
        {
            _outputManager = new OutputManager();
            _commandManager = new CommandManager();
        }

        public void Run()
        {
            while (true)
            {
                WelcomeText();
                MainText();
                ReadInput();
            }
        }

        public void WelcomeText()
        {
            _outputManager.PrintText("WelcomeText");
        }

        public void MainText()
        {
            _outputManager.PrintText("MainText");
        }

        private void ReadInput()
        {
            string userInput = Console.ReadLine();
            if (userInput != null)
            {
                _commandManager.ProcessMainMenuInput(userInput);
            }
        }
    }
}