using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiraLab
{
    public class UI
    {
        private OutputManager _outputManager;

        public UI()
        {
            // Instantiates the OutputManager
            _outputManager = new OutputManager();
        }

        public void WelcomeText()
        {
            _outputManager.PrintText("Welcome");
        }

        public void MainText()
        {
            _outputManager.PrintText("MainText");
        }
    }
}
