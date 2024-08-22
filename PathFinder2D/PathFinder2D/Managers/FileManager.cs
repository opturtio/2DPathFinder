namespace PathFinder2D.Managers
{
    using PathFinder2D.Services;

    /// <summary>
    /// Manages file operations related to files.
    /// </summary>
    public class FileManager
    {
        private readonly FileLoader fileLoader;
        private readonly FileModifier fileModifier;

        private List<string> fileNames;
        private List<Tuple<string, string>> cleanedFileNames;

        public FileManager()
        {
            this.fileLoader = new FileLoader();
            this.fileModifier = new FileModifier();
        }

        /// <summary>
        /// Loads map file names from a directory and cleans them for display.
        /// </summary>
        public void LoadAndCleanMapFileNames()
        {
            this.fileNames = this.fileLoader.LoadMapFileNames();
            this.cleanedFileNames = this.fileModifier.ModifyMapNames(this.fileNames);
        }

        /// <summary>
        /// Gets the list of cleaned map file names.
        /// </summary>
        /// <returns>A list of tuples, where each tuple contains two strings representing the name and size of a map file.</returns>
        public List<Tuple<string, string>> GetCleanedFileNames()
        {
            return this.cleanedFileNames;
        }

        /// <summary>
        /// Loads the content of a map file based on a given index number.
        /// </summary>
        /// <param name="indexNum">The index number of the map file to be loaded.</param>
        /// <returns>The content of the map file as a string.</returns>
        public string LoadMap(string indexNum)
        {
            return this.fileLoader.LoadMap(indexNum);
        }
    }
}
