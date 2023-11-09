using System;
using PathFinder.Managers;
using UserInterface = PathFinder.UI.UI;

namespace PathFinder
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
