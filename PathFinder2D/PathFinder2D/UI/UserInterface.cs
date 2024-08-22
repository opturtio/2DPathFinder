namespace PathFinder2D.UI
{
    using PathFinder2D.Managers;

    /// <summary>
    /// Manages the user interface, including displaying text and reading user input.
    /// </summary>
    public class UserInterface
    {
        private OutputManager outputManager;
        private CommandManager commandManager;

        public UserInterface()
        {
            this.outputManager = new OutputManager();
            this.commandManager = new CommandManager();
        }

        /// <summary>
        /// Starts the main loop of the user interface. Displays main menu options and reads input.
        /// </summary>
        public void Run()
        {
            while (true)
            {
                this.WelcomeText();
                this.MainText();
                this.ReadMainMenuInput();
            }
        }

        /// <summary>
        /// Displays a welcome message to the user.
        /// </summary>
        public void WelcomeText()
        {
            this.outputManager.PrintText("WelcomeText");
        }

        /// <summary>
        /// Displays the main menu options to the user.
        /// </summary>
        public void MainText()
        {
            this.outputManager.PrintText("MainText");
        }

        /// <summary>
        /// Reads user main menu input from the console and processes it.
        /// </summary>
        private void ReadMainMenuInput()
        {
            string userInput = Console.ReadLine();
            if (userInput != null)
            {
                this.commandManager.ProcessMainMenuInput(userInput);
            }
        }
    }
}