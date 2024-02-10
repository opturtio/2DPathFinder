namespace PathFinder.Managers
{
    using System.Reflection;
    using System.Resources;
    using PathFinder.DataStructures;

    /// <summary>
    /// Manages the output of the text to the console.
    /// </summary>
    public class OutputManager
    {
        private ResourceManager resourceManager;

        public OutputManager()
        {
            // Instantiates the ResourceManager
            this.resourceManager = new ResourceManager("PathFinder.Resources.AllStrings", Assembly.GetExecutingAssembly());
        }

        /// <summary>
        /// Prints the given string from AllStrings-ResX file using stringName as the key.
        /// </summary>
        /// <param name="stringName">The key used to find and print the string from the resource file.</param>
        public void PrintText(string stringName)
        {
            string outputText = this.resourceManager.GetString(stringName);
            Console.WriteLine(outputText);
        }

        /// <summary>
        /// Prints a list of map names and their sizes to the console.
        /// </summary>
        /// <param name="mapNames">A list of tuples, each containing two strings representing the name and size of a map file.</param>
        public void PrintMapNames(List<Tuple<string, string>> mapNames)
        {
            Console.WriteLine("Choose map:");
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine(String.Format("| {0,-3} | {1,-18} | {2,-10} |", "ID", "Name", "Size"));
            for (int i = 0; i < mapNames.Count; i++)
            {
                Console.WriteLine(String.Format("| {0,-3} | {1,-18} | {2,-10} |", $"{i + 1}.", $"{mapNames[i].Item1}", $"{mapNames[i].Item2}"));
            }

            Console.WriteLine("-----------------------------------------");
            Console.Write("Choose map by ID: ");
        }

        /// <summary>
        /// Prints detailed information for each node in the graph.
        /// This method iterates through all the nodes in the graph and prints the information returned by GetNodeInfo(),
        /// which includes the node's position, whether it's an obstacle, its cost, and its parent.
        /// </summary>
        /// <param name="graph">The graph whose nodes' information is to be printed.</param>
        public void PrintNodeInfo(Graph graph)
        {
            for (int i = 0; i < graph.Nodes.Count; i++)
            {
                for (int j = 0; j < graph.Nodes[i].Count; j++)
                {
                    Console.WriteLine(graph.Nodes[i][j].GetNodeInfo());
                }
            }
        }
    }
}
