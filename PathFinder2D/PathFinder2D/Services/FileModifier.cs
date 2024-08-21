namespace PathFinder.Services
{
    /// <summary>
    /// Provides functionality to modify files.
    /// </summary>
    public class FileModifier
    {
        /// <summary>
        /// Takes a list of map file names as input. Each file name is split into two parts representing the name and size of the map.
        /// </summary>
        /// <param name="mapNames">The list of map file names to be modified.</param>
        /// <returns>The method returns a list of tuples, where each tuple contains the name and size of the map.</returns>
        public List<Tuple<string, string>> ModifyMapNames(List<string> mapNames)
        {
            List<Tuple<string, string>> cleanMapNames = new List<Tuple<string, string>>();

            foreach (var mapName in mapNames)
            {
                var parts = mapName.Split(new char[] { '_', '.' });

                if (parts.Length >= 2)
                {
                    cleanMapNames.Add(Tuple.Create(parts[0], parts[1]));
                }
                else
                {
                    Console.WriteLine("Map name does not have two parts");
                }
            }
            return cleanMapNames;
        }
    }
}

