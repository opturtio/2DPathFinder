namespace PathFinder.Tests
{
    using System;
    using NUnit.Framework;
    using PathFinder.Algorithms;
    using PathFinder.DataStructures;
    using PathFinder.Managers;
    using PathFinder.Services;

    public class Tests
    {
        // Datastructures
        private Graph graph;

        private CommandManager commandManager;
        private FileManager fileManager;
        private FileLoader fileLoader;
        private StringReader consoleInput;
        private StringWriter consoleOutput;
        private string expectedMapContent = string.Empty;
        private string map;

        [SetUp]
        public void Setup()
        {
            this.commandManager = new CommandManager();
            this.fileLoader = new FileLoader();
            this.fileManager = new FileManager();
            this.fileLoader.SetMapsDirectoryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData", "Maps"));
            // Map numbers: 1. London, 2. Maze, 3. TestMap40x40
            this.map = this.fileLoader.LoadMap("2");
            this.graph = GraphBuilder.CreateGraphFromString(this.map);

            this.expectedMapContent =
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
        public void File_Can_Be_Loaded_Test()
        {
            // Act
            string fileContent = this.fileLoader.LoadMap("3");
            Console.WriteLine(fileContent);
            // Assert
            Assert.AreEqual(this.expectedMapContent, fileContent);
        }
    }
}


        /*
        [Test]
        public void ProcessMainMenuInput_OptionTwo_Returns_Map_As_String_Test()
        {
            // Arrange
            var mainMenuInput = "2"; 
            var mapSelectionInput = "1";
            var simulatedInput = mapSelectionInput;

            this.consoleInput = new StringReader(simulatedInput);
            Console.SetIn(this.consoleInput);

            this.consoleOutput = new StringWriter();
            Console.SetOut(this.consoleOutput);

            this.commandManager = new CommandManager();

            // Act
            this.commandManager.ProcessMainMenuInput(mainMenuInput);

            string actualOutput = this.consoleOutput.ToString();

            // Assert
            Assert.AreEqual(this.expectedMapContent, actualOutput);
        }

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