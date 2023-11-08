using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiraLab.Managers
{
    public class CommandManager
    {
        // processes users input
        public void ProcessInput(string input)
        {
            switch (input)
            {
                case "1":
                    Console.WriteLine("Pääsit tänne!");
                    break;
                case "2":
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
