namespace PathFinder
{
    using PathFinder.UI;

    /// <summary>
    /// The main entry point for the Pathfinder application.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Initializes and starts the user interface.
        /// </summary>
        /// <param name="args">Not in use!</param>
        public static void Main(string[] args)
        {
            var uiModule = new UserInterface();
            uiModule.Run();
        }
    }
}