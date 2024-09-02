namespace TestProject1
{
    using PathFinder2D.DataStructures;
    using PathFinder2D.Managers;
    using PathFinder2D.PathFindingAlgorithms;
    using PathFinder2D.Services;
    using PathFinder2D.UI;
    using System.Windows.Controls;
    using NUnit.Framework;
    using System.IO;
    using System.Threading;

    [TestFixture]
    [Apartment(ApartmentState.STA)]  // Ensures that all tests in this class run on an STA thread
    public class AlgorithmSpeedComparisonTest
    {
        private JPS? jps;
        private Astar? aStar;
        private Dijkstra? dijkstra;
        private Graph graphLondon;
        private Graph graphMaze;
        private Graph graphBlackLotus;
        private PathVisualizerWPF? pathVisualizer;
        private FileLoader? fileLoader;
        private string londonMap;
        private string mazeMap;
        private string blackLotusMap;
        private string testMap;
        private string expectedMapContent = string.Empty;
        private string speedComparisonDirectoryPath;
        private string londonSpeedComparisonFilePath;
        private string mazeSpeedComparisonFilePath;
        private string blackLotusSpeedComparisonFilePath;
        private int rounds;

        [SetUp]
        public void Setup()
        {
            this.fileLoader = new FileLoader();
            this.fileLoader.SetMapsDirectoryPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData", "Maps"));
            this.speedComparisonDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "Results");
            Directory.CreateDirectory(this.speedComparisonDirectoryPath);
            this.londonSpeedComparisonFilePath = Path.Combine(this.speedComparisonDirectoryPath, "JPSvsDijkstraAndAstar-SpeedComparison-London.csv");
            this.mazeSpeedComparisonFilePath = Path.Combine(this.speedComparisonDirectoryPath, "JPSvsDijkstraAndAstar-SpeedComparison-Maze.csv");
            this.blackLotusSpeedComparisonFilePath = Path.Combine(this.speedComparisonDirectoryPath, "JPSvsDijkstraAndAstar-SpeedComparison-BlackLotus.csv");

            // Load maps
            this.londonMap = this.fileLoader.LoadMapByName("London_1024x1024.map");
            this.mazeMap = this.fileLoader.LoadMapByName("Maze_512x512.map");
            this.blackLotusMap = this.fileLoader.LoadMapByName("BlackLotus.map");
            this.testMap = this.fileLoader.LoadMap("4");
            this.graphLondon = GraphBuilder.CreateGraphFromString(this.londonMap);
            this.graphMaze = GraphBuilder.CreateGraphFromString(this.mazeMap);
            this.graphBlackLotus = GraphBuilder.CreateGraphFromString(this.blackLotusMap);

            // Rounds
            this.rounds = 30;

            // Expected content of the test map
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
            Assert.That(this.testMap, Is.EqualTo(this.expectedMapContent));
        }

        [Test]
        public void ShortestPathInLondonMapIsRightLength()
        {
            this.pathVisualizer = new PathVisualizerWPF(new Canvas(), this.graphLondon);
            var coordinates = this.graphLondon.Coordinates();

            double expectedLength = 1548.3;
            this.jps = new JPS(this.graphLondon, this.pathVisualizer);

            this.pathVisualizer.ClearVisitedNodes();
            this.graphLondon.ResetNodes();

            var path = this.jps.FindShortestPath(coordinates[0], coordinates[coordinates.Count - 1]);

            Assert.That(this.jps.GetShortestPathLength(), Is.EqualTo(expectedLength));
        }

        [Test]
        public void IterateLondonMapMultipleTimes()
        {
            Random random = new Random();
            this.pathVisualizer = new PathVisualizerWPF(new Canvas(), this.graphLondon);

            var coordinates = this.graphLondon.Coordinates();
            int jpsFaster = 0;
            int jpsFaster2 = 0;
            int dijkstraFaster = 0;
            int aStarFaster = 0;

            // Initializing StreamWriter
            using StreamWriter speedComparisonWriter = new StreamWriter(this.londonSpeedComparisonFilePath, false);
            speedComparisonWriter.WriteLine($"JPS time, Dijkstra time, A* time, JPS visited nodes, Dijkstra visited nodes, A* visited nodes, Dijkstra path found, A* path found, JPS path found, Dijkstra shortest path length, A* shortest path length, JPS shortest path length");

            for (int i = 0; i < this.rounds; i++)
            {
                this.dijkstra = new Dijkstra(this.graphLondon, this.pathVisualizer);
                this.aStar = new Astar(this.graphLondon, this.pathVisualizer);
                this.jps = new JPS(this.graphLondon, this.pathVisualizer);

                this.dijkstra.TurnOnTesting();
                this.aStar.TurnOnTesting();
                this.jps.TurnOnTesting();

                int start = random.Next(0, coordinates.Count);
                int end = random.Next(0, coordinates.Count);

                this.graphLondon.ResetNodes();
                this.dijkstra.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphLondon.ResetNodes();
                this.aStar.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphLondon.ResetNodes();
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

                speedComparisonWriter.WriteLine($"{this.jps.GetStopwatchTime()},{this.dijkstra.GetStopwatchTime()},{this.aStar.GetStopwatchTime()},{this.jps.GetVisitedNodes()},{this.dijkstra.GetVisitedNodes()},{this.aStar.GetVisitedNodes()},{this.dijkstra.IsPathFound()},{this.aStar.IsPathFound()},{this.jps.IsPathFound()},{this.dijkstra.GetShortestPathCost()},{this.aStar.GetShortestPathCost()},{this.jps.GetShortestPathLength()}");
            }

            Console.WriteLine("London map result:");
            Console.WriteLine($"JPS faster: {jpsFaster}, Dijkstra faster: {dijkstraFaster}");
            Console.WriteLine($"JPS faster: {jpsFaster2}, A* faster: {aStarFaster}");

            Assert.Multiple(() =>
            {
                Assert.That(jpsFaster, Is.GreaterThan(dijkstraFaster));
                Assert.That(jpsFaster2, Is.GreaterThan(aStarFaster));
            });
        }

        [Test]
        public void IterateLondonMapPathSameLength()
        {
            Random random = new Random();
            this.pathVisualizer = new PathVisualizerWPF(new Canvas(), this.graphLondon);

            var coordinates = this.graphLondon.Coordinates();

            for (int i = 0; i < this.rounds; i++)
            {
                this.dijkstra = new Dijkstra(this.graphLondon, this.pathVisualizer);
                this.aStar = new Astar(this.graphLondon, this.pathVisualizer);
                this.jps = new JPS(this.graphLondon, this.pathVisualizer);

                this.dijkstra.TurnOnTesting();
                this.aStar.TurnOnTesting();
                this.jps.TurnOnTesting();

                int start = random.Next(0, coordinates.Count);
                int end = random.Next(0, coordinates.Count);

                this.graphLondon.ResetNodes();
                this.dijkstra.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphLondon.ResetNodes();
                this.aStar.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphLondon.ResetNodes();
                this.jps.FindShortestPath(coordinates[start], coordinates[end]);

                Assert.Multiple(() =>
                {
                    Assert.That(this.jps.GetShortestPathLength(), Is.EqualTo(this.dijkstra.GetShortestPathCost()));
                    Assert.That(this.jps.GetShortestPathLength(), Is.EqualTo(this.aStar.GetShortestPathCost()));
                });
            }
        }

        [Test]
        public void MazeMapPathSameLength()
        {
            this.pathVisualizer = new PathVisualizerWPF(new Canvas(), this.graphMaze);
            this.dijkstra = new Dijkstra(this.graphMaze, this.pathVisualizer);
            this.aStar = new Astar(this.graphMaze, this.pathVisualizer);
            this.jps = new JPS(this.graphMaze, this.pathVisualizer);

            this.dijkstra.TurnOnTesting();
            this.aStar.TurnOnTesting();
            this.jps.TurnOnTesting();

            Node start = this.graphMaze.Nodes[1][1];
            Node end = this.graphMaze.Nodes[509][509];
            double expectedLength = 4276.5;

            this.graphMaze.ResetNodes();
            this.dijkstra.FindShortestPath(start, end);

            this.graphMaze.ResetNodes();
            this.aStar.FindShortestPath(start, end);

            this.graphMaze.ResetNodes();
            this.jps.FindShortestPath(start, end);

            Assert.Multiple(() =>
            {
                Assert.That(this.dijkstra.GetShortestPathCost(), Is.EqualTo(expectedLength));
                Assert.That(this.aStar.GetShortestPathCost(), Is.EqualTo(expectedLength));
                Assert.That(this.jps.GetShortestPathLength(), Is.EqualTo(expectedLength));
            });
        }

        [Test]
        public void IterateMazeMapPathSameLength()
        {
            Random random = new Random();
            this.pathVisualizer = new PathVisualizerWPF(new Canvas(), this.graphMaze);

            var coordinates = this.graphMaze.Coordinates();

            for (int i = 0; i < this.rounds; i++)
            {
                this.dijkstra = new Dijkstra(this.graphMaze, this.pathVisualizer);
                this.aStar = new Astar(this.graphMaze, this.pathVisualizer);
                this.jps = new JPS(this.graphMaze, this.pathVisualizer);

                this.dijkstra.TurnOnTesting();
                this.aStar.TurnOnTesting();
                this.jps.TurnOnTesting();

                int start = random.Next(0, coordinates.Count);
                int end = random.Next(0, coordinates.Count);

                this.graphMaze.ResetNodes();
                this.dijkstra.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphMaze.ResetNodes();
                this.aStar.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphMaze.ResetNodes();
                this.jps.FindShortestPath(coordinates[start], coordinates[end]);

                Assert.Multiple(() =>
                {
                    Assert.That(this.jps.GetShortestPathLength(), Is.EqualTo(this.dijkstra.GetShortestPathCost()));
                    Assert.That(this.jps.GetShortestPathLength(), Is.EqualTo(this.aStar.GetShortestPathCost()));
                });
            }
        }

        [Test]
        public void IterateMazeMapMultipleTimes()
        {
            Random random = new Random();
            this.pathVisualizer = new PathVisualizerWPF(new Canvas(), this.graphMaze);

            var coordinates = this.graphMaze.Coordinates();
            int jpsFaster = 0;
            int jpsFaster2 = 0;
            int dijkstraFaster = 0;
            int aStarFaster = 0;

            // Initializing StreamWriter
            using StreamWriter speedComparisonWriter = new StreamWriter(this.mazeSpeedComparisonFilePath, false);
            speedComparisonWriter.WriteLine($"JPS time, Dijkstra time, A* time, JPS visited nodes, Dijkstra visited nodes, A* visited nodes, Dijkstra path found, A* path found, JPS path found, Dijkstra shortest path length, A* shortest path length, JPS shortest path length");

            for (int i = 0; i < this.rounds; i++)
            {
                this.dijkstra = new Dijkstra(this.graphMaze, this.pathVisualizer);
                this.aStar = new Astar(this.graphMaze, this.pathVisualizer);
                this.jps = new JPS(this.graphMaze, this.pathVisualizer);

                this.dijkstra.TurnOnTesting();
                this.aStar.TurnOnTesting();
                this.jps.TurnOnTesting();

                int start = random.Next(0, coordinates.Count);
                int end = random.Next(0, coordinates.Count);

                this.graphMaze.ResetNodes();
                this.dijkstra.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphMaze.ResetNodes();
                this.aStar.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphMaze.ResetNodes();
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

                speedComparisonWriter.WriteLine($"{this.jps.GetStopwatchTime()},{this.dijkstra.GetStopwatchTime()},{this.aStar.GetStopwatchTime()},{this.jps.GetVisitedNodes()},{this.dijkstra.GetVisitedNodes()},{this.aStar.GetVisitedNodes()},{this.dijkstra.IsPathFound()},{this.aStar.IsPathFound()},{this.jps.IsPathFound()},{this.dijkstra.GetShortestPathCost()},{this.aStar.GetShortestPathCost()},{this.jps.GetShortestPathLength()}");
            }

            Console.WriteLine("Maze map result:");
            Console.WriteLine($"JPS faster: {jpsFaster}, Dijkstra faster: {dijkstraFaster}");
            Console.WriteLine($"JPS faster: {jpsFaster2}, A* faster: {aStarFaster}");

            Assert.Multiple(() =>
            {
                Assert.That(jpsFaster, Is.GreaterThan(dijkstraFaster));
                Assert.That(jpsFaster2, Is.GreaterThan(aStarFaster));
            });
        }

        [Test]
        public void IterateBlackLotusMapPathSameLength()
        {
            Random random = new Random();
            this.pathVisualizer = new PathVisualizerWPF(new Canvas(), this.graphBlackLotus);

            var coordinates = this.graphBlackLotus.Coordinates();

            for (int i = 0; i < this.rounds; i++)
            {
                this.dijkstra = new Dijkstra(this.graphBlackLotus, this.pathVisualizer);
                this.aStar = new Astar(this.graphBlackLotus, this.pathVisualizer);
                this.jps = new JPS(this.graphBlackLotus, this.pathVisualizer);

                this.dijkstra.TurnOnTesting();
                this.aStar.TurnOnTesting();
                this.jps.TurnOnTesting();

                int start = random.Next(0, coordinates.Count);
                int end = random.Next(0, coordinates.Count);

                this.graphBlackLotus.ResetNodes();
                this.dijkstra.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphBlackLotus.ResetNodes();
                this.aStar.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphBlackLotus.ResetNodes();
                this.jps.FindShortestPath(coordinates[start], coordinates[end]);

                Assert.Multiple(() =>
                {
                    Assert.That(this.jps.GetShortestPathLength(), Is.EqualTo(this.dijkstra.GetShortestPathCost()));
                    Assert.That(this.jps.GetShortestPathLength(), Is.EqualTo(this.aStar.GetShortestPathCost()));
                });
            }
        }

        [Test]
        public void IterateBlackLotusMapMultipleTimes()
        {
            Random random = new Random();
            this.pathVisualizer = new PathVisualizerWPF(new Canvas(), this.graphBlackLotus);

            var coordinates = this.graphBlackLotus.Coordinates();
            int jpsFaster = 0;
            int jpsFaster2 = 0;
            int dijkstraFaster = 0;
            int aStarFaster = 0;

            // Initializing StreamWriter
            using StreamWriter speedComparisonWriter = new StreamWriter(this.blackLotusSpeedComparisonFilePath, false);
            speedComparisonWriter.WriteLine($"JPS time, Dijkstra time, A* time, JPS visited nodes, Dijkstra visited nodes, A* visited nodes, Dijkstra path found, A* path found, JPS path found, Dijkstra shortest path length, A* shortest path length, JPS shortest path length");

            for (int i = 0; i < this.rounds; i++)
            {
                this.dijkstra = new Dijkstra(this.graphBlackLotus, this.pathVisualizer);
                this.aStar = new Astar(this.graphBlackLotus, this.pathVisualizer);
                this.jps = new JPS(this.graphBlackLotus, this.pathVisualizer);

                this.dijkstra.TurnOnTesting();
                this.aStar.TurnOnTesting();
                this.jps.TurnOnTesting();

                int start = random.Next(0, coordinates.Count);
                int end = random.Next(0, coordinates.Count);

                this.graphBlackLotus.ResetNodes();
                this.dijkstra.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphBlackLotus.ResetNodes();
                this.aStar.FindShortestPath(coordinates[start], coordinates[end]);

                this.graphBlackLotus.ResetNodes();
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

                speedComparisonWriter.WriteLine($"{this.jps.GetStopwatchTime()},{this.dijkstra.GetStopwatchTime()},{this.aStar.GetStopwatchTime()},{this.jps.GetVisitedNodes()},{this.dijkstra.GetVisitedNodes()},{this.aStar.GetVisitedNodes()},{this.dijkstra.IsPathFound()},{this.aStar.IsPathFound()},{this.jps.IsPathFound()},{this.dijkstra.GetShortestPathCost()},{this.aStar.GetShortestPathCost()},{this.jps.GetShortestPathLength()}");
            }

            Console.WriteLine("BlackLotus map result:");
            Console.WriteLine($"JPS faster: {jpsFaster}, Dijkstra faster: {dijkstraFaster}");
            Console.WriteLine($"JPS faster: {jpsFaster2}, A* faster: {aStarFaster}");

            Assert.Multiple(() =>
            {
                Assert.That(jpsFaster, Is.GreaterThan(dijkstraFaster));
                Assert.That(jpsFaster2, Is.GreaterThan(aStarFaster));
            });
        }
    }
}
