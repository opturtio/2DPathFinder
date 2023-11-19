using PathFinder.Managers;
using PathFinder.Services;
using System.IO;

namespace PathFinder.Tests
{
    public class Tests
    {
        private CommandManager _commandManager;
        private FileManager _fileManager;
        private FileLoader _fileLoader;
        private StringReader _consoleInput;
        private StringWriter _consoleOutput;
        private string _expectedMapContent = "";

        [SetUp]
        public void Setup()
        {
            _commandManager = new CommandManager();
            _fileLoader = new FileLoader();
            _fileManager = new FileManager();
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

        
        [Test]
        public void ProcessMainMenuInput_OptionTwo_Returns_Map_As_String_Test()
        {
            // Arrange
            var mainMenuInput = "2"; 
            var mapSelectionInput = "1";
            var simulatedInput = mapSelectionInput;

            _consoleInput = new StringReader(simulatedInput);
            Console.SetIn(_consoleInput);

            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);

            _commandManager = new CommandManager();

            // Act
            _commandManager.ProcessMainMenuInput(mainMenuInput);

            string actualOutput = _consoleOutput.ToString();

            // Assert
            Assert.AreEqual(_expectedMapContent, actualOutput);
        }
        

        [Test]
        public void File_Can_Be_Loaded_Test()
        {
            // Act
            string fileContent = _fileLoader.LoadMap("1");

            // Assert
            Assert.AreEqual(_expectedMapContent, fileContent);
        }

        /*
        [Test]
        public void ProcessMainMenuOptionTwo_BehavesAsExpected()
        {

            _fileLoader.SetMapsDirectoryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Maps"));

            // Arrange
            var simulatedInput = "1" + Environment.NewLine; // Simulate selecting the first map
            _consoleInput = new StringReader(simulatedInput);
            Console.SetIn(_consoleInput);

            _consoleOutput = new StringWriter();
            Console.SetOut(_consoleOutput);

            // Act
            _commandManager.ProcessMainMenuOptionTwo();
            // Assert
            // Check if the correct map content is loaded and output
            string actualOutput = _consoleOutput.ToString();
            Console.WriteLine(actualOutput);
            // Assuming that the output contains the map name or some identifier
            Assert.AreEqual(_expectedMapContent, actualOutput);
        }
        
        [Test]
        public void Check_Inputs_Test()
        {
            _commandManager.ProcessMainMenuInput("2");
            Console.WriteLine("Toimii");
            //_commandManager.ProcessMapMenuInput("1");
            string currentMap = _commandManager.GetCurrentMap();
            Console.WriteLine(currentMap);

            Assert.AreEqual(_expectedMapContent, currentMap);
        }

        
        [Test]
        public void Cleaned_Files_Names_Are_Correct_Test()
        {
            _fileLoader.SetMapsDirectoryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData", "Maps"));

            // Arrange
            var _correctFormMapNames = new List<Tuple<string, string>>
            {
                Tuple.Create("Test", "40x40")
            };

            // Act
            string fileContent = _fileLoader.LoadMap("1");
            _fileManager.LoadAndCleanMapFileNames();
            var cleanedMapFileNames = _fileManager.GetCleanedFileNames();

            // Assert
            Assert.AreEqual(_correctFormMapNames, cleanedMapFileNames);
        }
        */
    }
}