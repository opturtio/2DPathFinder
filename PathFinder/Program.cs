using PathFinder.UI;

namespace PathFinder
{
    /// <summary>
    /// The main entry point for the Pathfinder application.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Initializes and starts the user interface.
        /// </summary>
        /// <param name="args">Not in use!</param>
        static void Main(string[] args)
        {
            var uiModule = new UserInterface();
            uiModule.Run();
        }
    }
}