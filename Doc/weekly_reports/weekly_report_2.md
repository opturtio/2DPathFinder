# Weekly Report 2


### Thursday 25.11.2024
I started to create tests for algorithm speed comparison. It took some time to set things up. I wanted to do the tests with asserts and struggled with the problems it caused. Then, I realized that JPS is not always the fastest. So now I need to find out why this is. I created a test that ran the London map 1000 times. JPS is faster, over 700 times of the thousand maps. One problem could be that my computer is too fast and might sometimes calculate faster than the other.
Another problem could be the place where the nodes are placed. One problem could be that the shortest road is built at the end of the algorithm by ShortestPathBuilder.ShortestPath(end). I need to create a timer inside the algorithms to get all the extra features disabled for the comparison.

### Friday 26.11.2024
I created boolean JumpPoint to node class so that I can mark to maps which points are the jump points. I inserted the Stopwatch inside the algorithms so that the timing happens in side the pathfinding algorithms and it does not count in the shortest path building.