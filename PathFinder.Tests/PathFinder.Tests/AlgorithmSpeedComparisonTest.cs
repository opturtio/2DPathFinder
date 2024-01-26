namespace PathFinder.Tests
{
    using System;
    using System.Diagnostics;
    using NUnit.Framework;
    using PathFinder.Algorithms;
    using PathFinder.DataStructures;
    using PathFinder.Managers;
    using PathFinder.Services;

    public class AlgorithmSpeedComparisonTest
    {
        private JPS jps;
        private Astar aStar;
        private Dijkstra dijkstra;
        private Graph graph;
        private PathVisualizer pathVisualizer;
        private FileLoader fileLoader;
        private Stopwatch dijkstraStopwatch;
        private Stopwatch aStarStopwatch;
        private Stopwatch jpsStopwatch;
        private string mazeMap;

        [SetUp]
        public void Setup()
        {
            this.fileLoader = new FileLoader();
            this.fileLoader.SetMapsDirectoryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData", "Maps"));
            this.mazeMap = this.fileLoader.LoadMap("2");
            this.graph = GraphBuilder.CreateGraphFromString(this.mazeMap);
            this.pathVisualizer = new PathVisualizer(this.graph, this.mazeMap);
            this.dijkstra = new Dijkstra(this.graph, this.pathVisualizer);
            this.aStar = new Astar(this.graph, this.pathVisualizer);
            this.jps = new JPS(this.graph, this.pathVisualizer);
            this.dijkstraStopwatch = new Stopwatch();
            this.aStarStopwatch = new Stopwatch();
            this.jpsStopwatch = new Stopwatch();
        }

        [Test]
        public void IterateLondonMapHundredTimes()
        {
            Random random = new Random();
            var coordinates = this.graph.Coordinates();
            int jpsFaster = 0;
            int dijkstraFaster = 0;

            for (int i = 0; i < 1000; i++)
            {
                int start = random.Next(0, coordinates.Count);
                int end = random.Next(0, coordinates.Count);

                this.dijkstraStopwatch.Start();
                this.dijkstra.FindShortestPath(coordinates[start], coordinates[end]);
                this.dijkstraStopwatch.Stop();

                this.aStarStopwatch.Start();
                this.aStar.FindShortestPath(coordinates[start], coordinates[end]);
                this.aStarStopwatch.Stop();

                this.jpsStopwatch.Start();
                this.jps.FindShortestPath(coordinates[start], coordinates[end]);
                this.jpsStopwatch.Stop();

                if (this.jpsStopwatch.ElapsedTicks < this.dijkstraStopwatch.ElapsedTicks)
                {
                    jpsFaster++;
                }
                else
                {
                    dijkstraFaster++;
                }

                this.dijkstraStopwatch.Reset();
                this.aStarStopwatch.Reset();
                this.jpsStopwatch.Reset();
            }

            Console.WriteLine($"JPS faster: {jpsFaster}, Dijksta faster: {dijkstraFaster}");
        }
    }
}
