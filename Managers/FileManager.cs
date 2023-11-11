using System.Collections.Generic;
using PathFinder.Services;

namespace PathFinder.Managers
{
    /// <summary>
    /// Manages file operations related to files.
    /// </summary>
    public class FileManager
    {
        private FileLoader _fileLoader;
        private FileModifier _fileModifier;

        private List<string> _fileNames;
        private List<Tuple<string, string>> _cleanedFileNames;

        public FileManager()
        {
            _fileLoader = new FileLoader();
            _fileModifier = new FileModifier();
        }

        /// <summary>
        /// Loads map file names from a directory and cleans them for display.
        /// </summary>
        public void LoadAndCleanMapFileNames()
        {
            _fileNames = _fileLoader.LoadMapFileNames();
            _cleanedFileNames = _fileModifier.ModifyMapNames(_fileNames);
        }

        /// <summary>
        /// Gets the list of cleaned map file names.
        /// </summary>
        /// <returns>A list of tuples, where each tuple contains two strings representing the name and size of a map file.</returns>
        public List<Tuple<string, string>> GetCleanedFileNames()
        {
            return _cleanedFileNames;
        }

        /// <summary>
        /// Loads the content of a map file based on a given index number.
        /// </summary>
        /// <param name="indexNum">The index number of the map file to be loaded.</param>
        /// <returns>The content of the map file as a string.</returns>
        public string LoadMap(string indexNum)
        {
            return _fileLoader.LoadMap(indexNum);
        }
    }
}
