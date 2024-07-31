namespace PathFinder.Tests
{
    using System;
    using NUnit.Framework;
    using PathFinder.Algorithms;
    using PathFinder.DataStructures;
    using PathFinder.Managers;
    using PathFinder.Services;

    public class DataStructureTest
    {
        // Datastructures
        private Graph graph;

        private CommandManager commandManager;
        private FileManager fileManager;
        private FileLoader fileLoader;
        private BinaryHeap<Node> binaryHeap;
        private StringReader consoleInput;
        private StringWriter consoleOutput;
        private string expectedMapContent = string.Empty;
        private string testMap;

        [SetUp]
        public void Setup()
        {
            this.commandManager = new CommandManager();
            this.fileLoader = new FileLoader();
            this.fileManager = new FileManager();
            this.binaryHeap = new BinaryHeap<Node>();

            this.fileLoader.SetMapsDirectoryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData", "Maps"));

            // Map numbers: 1. London, 2. Maze, 3. TestMap40x40
            this.testMap = this.fileLoader.LoadMap("3");
            this.graph = GraphBuilder.CreateGraphFromString(this.testMap);
        }

        /*
        [Test]
        public void BinaryHeapHundredNumbersSameTest()
        {
            for (int i = 0; i < 100; i++)
            {
                this.binaryHeap.Insert
            }
        }
        */
    }
}