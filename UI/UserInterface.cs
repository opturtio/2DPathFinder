using System;
using PathFinder.Managers;

namespace PathFinder.UI
{
    /// <summary>
    /// Manages the user interface, including displaying text and reading user input.
    /// </summary>
    public class UserInterface
    {
        private OutputManager _outputManager;
        private CommandManager _commandManager;

        public UserInterface()
        {
            _outputManager = new OutputManager();
            _commandManager = new CommandManager();
        }

        /// <summary>
        /// Starts the main loop of the user interface. Displays main menu options and reads input.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                WelcomeText();
                MainText();
                ReadMainMenuInput();
            }
        }

        /// <summary>
        /// Displays a welcome message to the user.
        /// </summary>
        public void WelcomeText()
        {
            _outputManager.PrintText("WelcomeText");
        }

        /// <summary>
        /// Displays the main menu options to the user.
        /// </summary>
        public void MainText()
        {
            _outputManager.PrintText("MainText");
        }

        /// <summary>
        /// Reads user main menu input from the console and processes it.
        /// </summary>
        private void ReadMainMenuInput()
        {
            string userInput = Console.ReadLine();
            if (userInput != null)
            {
                _commandManager.ProcessMainMenuInput(userInput);
            }
        }
    }
}