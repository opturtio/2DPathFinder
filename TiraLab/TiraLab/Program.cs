using System;
using TiraLab.Managers;
using UserInterface = TiraLab.UI.UI;

namespace TiraLab
{
    class Program
    {
        static void Main(string[] args)
        {
            UserInterface uiModule = new UserInterface();
            InputManager inputManager = new InputManager();
            while (true)
            {
                uiModule.WelcomeText();
                uiModule.MainText();
                inputManager.ReadInput();
            }
        }
    }
}
