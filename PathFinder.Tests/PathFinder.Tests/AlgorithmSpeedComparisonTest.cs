namespace PathFinder.Tests
{
    using System;
    using NUnit.Framework;
    using PathFinder.Algorithms;
    using PathFinder.DataStructures;
    using PathFinder.Managers;
    using PathFinder.Services;

    public class AlgorithmSpeedComparisonTest
    {
        private Graph graph;
        private FileLoader fileLoader;
        private string londonMap;

        [SetUp]
        public void Setup()
        {
            this.fileLoader = new FileLoader();
            this.fileLoader.SetMapsDirectoryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData", "Maps"));
            this.londonMap = this.fileLoader.LoadMap("2");
            this.graph = GraphBuilder.CreateGraphFromString(this.londonMap);
            var coordinates = this.graph.Coordinates();
        }





    }
}
