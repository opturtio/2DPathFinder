using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PathFinder.Services;
using PathFinder.Managers;

namespace PathFinder.Managers
{
    public class FileManager
    {
        private FileLoader _fileLoader;
        private FileModifier _fileModifier;
        private OutputManager _outputManager;
        private List<string> _fileNames;
        private List<Tuple<string, string>> _cleanedFileNames;
        public FileManager()
        {
            _fileLoader = new FileLoader();
            _fileModifier = new FileModifier();
            _outputManager = new OutputManager();
            _fileNames = new List<string>();
            _cleanedFileNames = new List<Tuple<string,string>>();
        }
        public void Initialize()
        {
            _fileNames = _fileLoader.LoadMapFileNames();
            _cleanedFileNames = _fileModifier.ModifyMapNames(_fileNames);
            _outputManager.PrintMapNames(_cleanedFileNames);
        }

    }
}
