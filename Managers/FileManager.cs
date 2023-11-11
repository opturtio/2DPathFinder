using System.Collections.Generic;
using PathFinder.Services;

namespace PathFinder.Managers
{
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

        public void LoadAndCleanMapFileNames()
        {
            _fileNames = _fileLoader.LoadMapFileNames();
            _cleanedFileNames = _fileModifier.ModifyMapNames(_fileNames);
        }

        public List<Tuple<string, string>> GetCleanedFileNames()
        {
            return _cleanedFileNames;
        }
    }
}
