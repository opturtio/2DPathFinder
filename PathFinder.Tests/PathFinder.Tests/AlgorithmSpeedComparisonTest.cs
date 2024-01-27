namespace PathFinder.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using NUnit.Framework;
    using PathFinder.Algorithms;
    using PathFinder.DataStructures;
    using PathFinder.Managers;
    using PathFinder.Services;

    public class AlgorithmSpeedComparisonTest
    {
        private JPS? jps;
        private Astar? aStar;
        private Dijkstra? dijkstra;
        private Graph graphLondon;
        private Graph graphMaze;
        private PathVisualizer? pathVisualizer;
        private FileLoader? fileLoader;
        private string londonMap;
        private string mazeMap;
        private string speedComparisonDirectoryPath;
        private string dijkstraVsJpsLondonFilePath;
        private string aStarVsJpsLondonFilePath;
        private string dijkstraVsJpsMazeFilePath;
        private string aStarVsJpsMazeFilePath;

        [SetUp]
        public void Setup()
        {
            this.fileLoader = new FileLoader();
            this.fileLoader.SetMapsDirectoryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData", "Maps"));
            this.speedComparisonDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Results");
            Directory.CreateDirectory(this.speedComparisonDirectoryPath);
            this.dijkstraVsJpsLondonFilePath = Path.Combine(this.speedComparisonDirectoryPath, "JPSvsDijkstra-SpeedComparison-London.csv");
            this.aStarVsJpsLondonFilePath = Path.Combine(this.speedComparisonDirectoryPath, "JPSvsAstar-SpeedComparison-London.csv");
            this.dijkstraVsJpsMazeFilePath = Path.Combine(this.speedComparisonDirectoryPath, "JPSvsDijkstra-SpeedComparison-Maze.csv");
            this.aStarVsJpsMazeFilePath = Path.Combine(this.speedComparisonDirectoryPath, "JPSvsAstar-SpeedComparison-Maze.csv");

            // Map numbers: 1. London, 2. Maze, 3. TestMap40x40
            this.londonMap = this.fileLoader.LoadMap("1");
            this.mazeMap = this.fileLoader.LoadMap("2");
            this.graphLondon = GraphBuilder.CreateGraphFromString(this.londonMap);
            this.graphMaze = GraphBuilder.CreateGraphFromString(this.mazeMap);
        }

        [Test]
        public void IterateLondonMapHundredTimes()
        {
            Random random = new Random();
            this.pathVisualizer = new PathVisualizer(this.graphLondon, this.londonMap);

            var coordinates = this.graphLondon.Coordinates();
            int jpsFaster = 0;
            int jpsFaster2 = 0;
            int dijkstraFaster = 0;
            int aStarFaster = 0;

            // Initializing StreamWriters
            using StreamWriter dijkstraVsJpsLondonWriter = new StreamWriter(this.dijkstraVsJpsLondonFilePath, true);
            using StreamWriter aStarVsJpsLondonWriter = new StreamWriter(this.aStarVsJpsLondonFilePath, true);

            for (int i = 0; i < 100; i++)
            {
                this.dijkstra = new Dijkstra(this.graphLondon, this.pathVisualizer);
                this.aStar = new Astar(this.graphLondon, this.pathVisualizer);
                this.jps = new JPS(this.graphLondon, this.pathVisualizer);

                int start = random.Next(0, coordinates.Count);
                int end = random.Next(0, coordinates.Count);

                this.dijkstra.FindShortestPath(coordinates[start], coordinates[end]);
                this.aStar.FindShortestPath(coordinates[start], coordinates[end]);
                this.jps.FindShortestPath(coordinates[start], coordinates[end]);

                if (this.jps.GetStopwatchTime() < this.dijkstra.GetStopwatchTime())
                {
                    jpsFaster++;
                }
                else
                {
                    dijkstraFaster++;
                }

                if (this.jps.GetStopwatchTime() < this.aStar.GetStopwatchTime())
                {
                    jpsFaster2++;
                }
                else
                {
                    aStarFaster++;
                }

                dijkstraVsJpsLondonWriter.WriteLine($"{this.jps.GetStopwatchTime()},{this.dijkstra.GetStopwatchTime()}");
                aStarVsJpsLondonWriter.WriteLine($"{this.jps.GetStopwatchTime()},{this.aStar.GetStopwatchTime()}");
            }

            Console.WriteLine("London map result:");
            Console.WriteLine($"JPS faster: {jpsFaster}, Dijksta faster: {dijkstraFaster}");
            Console.WriteLine($"JPS faster: {jpsFaster2}, A* faster: {aStarFaster}");

            Assert.Multiple(() =>
            {
                Assert.That(jpsFaster, Is.GreaterThan(dijkstraFaster));
                Assert.That(jpsFaster2, Is.GreaterThan(aStarFaster));
            });
        }

        [Test]
        public void IterateMazeMapHundredTimes()
        {
            Random random = new Random();
            this.pathVisualizer = new PathVisualizer(this.graphMaze, this.londonMap);

            var coordinates = this.graphMaze.Coordinates();
            int jpsFaster = 0;
            int jpsFaster2 = 0;
            int dijkstraFaster = 0;
            int aStarFaster = 0;

            // Initializing StreamWriters
            using StreamWriter dijkstraVsJpsMazeWriter = new StreamWriter(this.dijkstraVsJpsMazeFilePath, true);
            using StreamWriter aStarVsJpsMazeWriter = new StreamWriter(this.aStarVsJpsMazeFilePath, true);

            for (int i = 0; i < 100; i++)
            {
                this.dijkstra = new Dijkstra(this.graphMaze, this.pathVisualizer);
                this.aStar = new Astar(this.graphMaze, this.pathVisualizer);
                this.jps = new JPS(this.graphMaze, this.pathVisualizer);

                int start = random.Next(0, coordinates.Count);
                int end = random.Next(0, coordinates.Count);

                this.dijkstra.FindShortestPath(coordinates[start], coordinates[end]);

                this.aStar.FindShortestPath(coordinates[start], coordinates[end]);

                this.jps.FindShortestPath(coordinates[start], coordinates[end]);

                if (this.jps.GetStopwatchTime() < this.dijkstra.GetStopwatchTime())
                {
                    jpsFaster++;
                }
                else
                {
                    dijkstraFaster++;
                }

                if (this.jps.GetStopwatchTime() < this.aStar.GetStopwatchTime())
                {
                    jpsFaster2++;
                }
                else
                {
                    aStarFaster++;
                }

                dijkstraVsJpsMazeWriter.WriteLine($"{this.jps.GetStopwatchTime()},{this.dijkstra.GetStopwatchTime()}");
                aStarVsJpsMazeWriter.WriteLine($"{this.jps.GetStopwatchTime()},{this.aStar.GetStopwatchTime()}");
            }

            Console.WriteLine("Maze map result:");
            Console.WriteLine($"JPS faster: {jpsFaster}, Dijksta faster: {dijkstraFaster}");
            Console.WriteLine($"JPS faster: {jpsFaster2}, A* faster: {aStarFaster}");

            Assert.Multiple(() =>
            {
                Assert.That(jpsFaster, Is.GreaterThan(dijkstraFaster));
                Assert.That(jpsFaster2, Is.GreaterThan(aStarFaster));
            });
        }
    }
}
