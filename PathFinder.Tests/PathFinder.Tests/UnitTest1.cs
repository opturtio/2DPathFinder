using PathFinder.Managers;
using PathFinder.Services;
using System.IO;

namespace PathFinder.Tests
{
    public class Tests
    {
        private CommandManager _commandManager;
        private FileLoader _fileLoader;
        private StringReader _consoleInput;
        private StringWriter _consoleOutput;
        private string _expectedMapContent = "";

        [SetUp]
        public void Setup()
        {
            _commandManager = new CommandManager();
            _fileLoader = new FileLoader();
            _fileLoader.SetMapsDirectoryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData", "Maps"));
            _expectedMapContent =
                ".......................................\r\n" +
                "..................................@@@@@\r\n" +
                ".................................@@@@@@\r\n" +
                ".......................................\r\n" +
                "................................@@@@@@.\r\n" +
                "..............................@@@@@@@..\r\n" +
                ".............................@@@@@@@@..\r\n" +
                "..............................@@@@@@@..\r\n" +
                "...............................@@@@@@@.\r\n" +
                "...............................@@@@@@@.\r\n" +
                "...............................@@@@....\r\n" +
                "............................@@@@@@@@...\r\n" +
                "................@@@@........@@@@@@@@...\r\n" +
                "@............@@@@@@@.........@@@@@@@...\r\n" +
                "@............@@@@@@@.........@@@@@@@@@.\r\n" +
                "@@...........@@@@@@@@........@@@@@@@@@.\r\n" +
                "@@............@@@@@@@..........@@@@@@@.\r\n" +
                "@@............@@@@@@@..........@@@@@@@@\r\n" +
                "@@@@.........@@@@@@@............@@@@@@@\r\n" +
                "@@@@........@@@@@@@@............@@@@@@@\r\n" +
                "@@@@........@@@@@@@@............@@@@@@@\r\n" +
                "@@@@@.......@@@@@@@@@...........@@@@@@@\r\n" +
                "@@@@@........@@@@@@@@...........@@@@@@@\r\n" +
                "@@@@@........@@@@@@@@...........@@@@@@@\r\n" +
                "@@@@@........@@@@@@@@............@@@@@@\r\n" +
                "@@@@@........@@@@@@@@............@@@@@@\r\n" +
                "@@@@@@........@@@@@@@...........@@@@@@@\r\n" +
                "@@@@@@........@@@@@@@@........@@@@@@@..\r\n" +
                "@@@@@@........@@@@@@@@........@@@@@@@..\r\n" +
                "@@@@@@........@@@@@@@@.......@@@@@@@...\r\n" +
                "@@@@@@@........@@@@@@@......@@@@@@@....\r\n" +
                "@@@@@@@........@@@@@@@.....@@@@@@@.....\r\n" +
                "@@@@@@@........@@@@@@@@...@@@@@........\r\n" +
                "@@@@@@@........@@@@@@@@..........@@@...\r\n" +
                "@@@@@@@........@@@@@@@@......@@@@@@@...\r\n" +
                "@@@@@@@.........@@@@@@@......@@@@@@@@..\r\n" +
                "@@@@@@@@........@@@@@@@......@@@@@@@...\r\n" +
                ".@@@@@@@........@@@@@@@...@@@@@@@@@@@..\r\n" +
                ".@@@@@@@........@@@@@@@.@@@@@@@@@@@@...\r\n" +
                ".@@@@@@@........@@@@@@@....@@@@@@@@@...";
        }

        /*
        [Test]
        public void ProcessMainMenuInput_OptionTwo_Returns_Map_As_String_Test()
        {
            // Arrange
            var mainMenuInput = "2"; // The initial input to select option 2
            var mapSelectionInput = "1"; // The input for map selection
            var simulatedInput = mapSelectionInput; // Only the map selection input is needed for the console

            _consoleInput = new StringReader(simulatedInput);
            Console.SetIn(_consoleInput);

            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);

            _commandManager = new CommandManager();

            // Act
            // Call ProcessMainMenuInput with the initial choice ("2")
            _commandManager.ProcessMainMenuInput(mainMenuInput);
            // After processing "2", the command manager will read the map selection ("1") from the console

            string actualOutput = _consoleOutput.ToString();

            // Assert
            // The actualOutput should contain the content of the map file selected (in this case, map index "1")
            Assert.AreEqual(_expectedMapContent, actualOutput);
        }
        */




        [Test]
        public void File_Can_Be_Loaded_Test()
        {
            // Act
            string fileContent = _fileLoader.LoadMap("1");

            // Assert
            Assert.AreEqual(_expectedMapContent, fileContent);
        }
    }
}