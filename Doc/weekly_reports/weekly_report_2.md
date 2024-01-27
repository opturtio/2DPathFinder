# Weekly Report 2


### Thursday 25.11.2024
I started to create tests for algorithm speed comparison. It took some time to set things up. I wanted to do the tests with asserts and struggled with the problems it caused. Then, I realized that JPS is not always the fastest. So now I need to find out why this is. I created a test that ran the London map 1000 times. JPS is faster, over 700 times of the thousand maps.
Another problem could be the place where the nodes are placed. One problem could be that the shortest road is built at the end of the algorithm by ShortestPathBuilder.ShortestPath(end). I need to create a timer inside the algorithms to get all the extra features disabled for the comparison.

### Friday 26.11.2024
I created boolean JumpPoint to node class so that I can mark to maps which points are the jump points. I inserted the Stopwatch inside the algorithms so that the timing happens inside the pathfinding algorithms and it does not count in the shortest path building. After I modified tests and run them, JPS was everytime, out of thousand test runs in London map, faster than dijkstra. After that, I added also A* and it was also faster than A* in all cases. I added also maze map to tests and JPS is always faster. I run the tests 100 times in both maps several times and JPS is always faster. I am starting to be sure that JPS is working correctly. I can start to write testing documentary. 

### Saturday 27.11.2024
Now, it is possible to see the time comparisons in a CSV file after running the tests in the path: ..\PathFinder.Tests\PathFinder.Tests\Results\
I thought everything was working, BUT I realized the stopwatch was always starting again when I ran the tests. I solved this problem by initializing the algorithms in the for-loop. After this, I realized that most of the time, the Dijkstra algorithm ends just in a few ticks, so something is badly wrong. Well I am happy it is dijkstra, not JPS. Still, JPS is faster than A* every time. Much faster, but I think this is because of the way I implemented A*. I think I will also fix A*. It was nice to see how tests can show so much, even when you think everything is all right. I was sure everything was working right. Well, next weeks I will run more tests and try to fix all the problems.