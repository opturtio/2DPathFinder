using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;

namespace TiraLab.Managers
{
    public class OutputManager
    {
        private ResourceManager _resourceManager;

        // Constructor
        public OutputManager()
        {
            // Instantiates the ResourceManager
            _resourceManager = new ResourceManager("TiraLab.Resources.AllStrings", Assembly.GetExecutingAssembly());
        }

        // Prints the given string from AllStrings-ResX file using stringName as the key
        public void PrintText(string stringName)
        {
            string outputText = _resourceManager.GetString(stringName);
            Console.WriteLine(outputText);
        }
    }
}
