using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using System.Reflection;

namespace TiraLab
{
    public class OutputManager
    {
        private ResourceManager _resourceManager;

        public OutputManager()
        {
            // Instantiates the ResourceManager
            _resourceManager = new ResourceManager("TiraLab.Resources.AllStrings", Assembly.GetExecutingAssembly());
        }

        public void PrintText(string stringName)
        {
            string outputText = _resourceManager.GetString(stringName);
            Console.WriteLine(outputText);
        }
    }
}
