namespace PathFinder.Managers
{
    using PathFinder.DataStructures;

    /// <summary>
    /// Manages the processing of user input commands.
    /// </summary>
    public class CommandManager
    {
        private readonly FileManager fileManager;
        private readonly OutputManager outputManager;

        private string currentMap;

        // Constructor that initializes the necessary components
        public CommandManager()
        {
            this.fileManager = new FileManager();
            this.outputManager = new OutputManager();
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
                    this.ProcessMainMenuOptionOne();
                    break;
                case "2":
                    this.ProcessMainMenuOptionTwo();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
            }
        }

        /// <summary>
        /// Processes main menu option one.
        /// </summary>
        public void ProcessMainMenuOptionOne()
        {
            if (this.currentMap == null)
            {
                this.outputManager.PrintText("MapNotFound");
                return;
            }

            Graph graph = GraphBuilder.CreateGraphFromString(this.currentMap);
            AlgorithmComparisonManager algorithmComparisonManager = new AlgorithmComparisonManager(graph, this.currentMap);
            algorithmComparisonManager.Initialize();

            // For debug
            this.outputManager.PrintNodeInfo(graph);
            Console.WriteLine(this.currentMap);
        }

        /// <summary>
        /// Processes main menu option two.
        /// </summary>
        public void ProcessMainMenuOptionTwo()
        {
            this.fileManager.LoadAndCleanMapFileNames();
            var cleanedFileNames = this.fileManager.GetCleanedFileNames();
            this.outputManager.PrintMapNames(cleanedFileNames);
            var mapInput = Console.ReadLine();
            if (mapInput != null)
            {
                this.ProcessMapMenuInput(mapInput);
            }
        }

        /// <summary>
        /// Processes the user's input received from the map menu.
        /// </summary>
        /// <param name="input">The user input as a string, representing the index number of a map.</param>
        public void ProcessMapMenuInput(string input)
        {
            this.currentMap = this.fileManager.LoadMap(input);
        }

        /// <summary>
        /// Retrieves the current map.
        /// </summary>
        /// <returns>The current map as a string.</returns>
        public string GetCurrentMap()
        {
            return this.currentMap;
        }
    }
}