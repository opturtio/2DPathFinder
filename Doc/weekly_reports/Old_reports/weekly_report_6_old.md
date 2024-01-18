# Weekly report 6

### Saturday 09.12.2023
I have been studying how the JPS algorithm works and how to implement it. I studied it 28.11, 5.12 and 6.12. I watched some videos and read different sources. I've been busy with my two other courses and have felt very tired. Now, I am trying to finish this project. It took a lot of effort to write the peer reviews, but both were educating. I realized the CoverageReport folder is added to GitHub, so the Languages bar showed I mainly use HTML and CSS. I wanted to fix this first thing today. For the rest of the day, I decided to update the documentation. Tomorrow, I will start implementing the JPS algorithm and figure out how to write tests. Tuesday is the final return of the math course. Then I have another course, and I am not many weeks late in it because I was sick earlier. I think I can finish all of them.

I created design documentation. I will update it later, but at least now, the basics of it are done. I will create a user guide, but it is not my main priority.

### Sunday 10.12.2023
I created a visualization class to see how the algorithm works (Dijkstra, in this case). Next, I want the program to print the shortest path. Then, I will implement JPS. I used this video to implement the visualization: https://www.helsinki.fi/fi/unitube/video/ffe9e56a-f1d3-4ba7-a9d5-bd663d4079f7
It was useful. I remembered it from the past. I had some big problems implementing it. I get index out of range and could not figure out why but the I realized I do not clean the carriage return when the map is created only when the graph is created. I try to trim it when the currentMap string is created but it did not work so I had to Trim it in Visualize method.

### Monday 11.12.2023
I added StringBuilder to the PathVisualizer class for more efficient string modification. I also fixed some warnings. Now, a new class, ShortestPathBuilder, takes the end node as an input and uses the node class to examine the Parent of the current node. It adds the current node to the list and then processes the parent node as the current node and so on. This way, it creates a list for the shortest path. I think it was mentioned in the test lecture that the list must be reverted, so I did that. In addition, I need to fix the Graph.GetNeighborsWithCosts() so that the diagonal movements do not jump over the corners as told in the lecture. This is also one important thing to figure out. 

I added the method VisualizeShortestPath to the PathVisualizer class to visualize the shortest path. I also added a debugger option so the user can see how the algorithm works. In the end, the user will see the visualization of the shortest path, and the map will be printed to the console. I fixed some program structures, but now it is becoming more complicated, and I am beginning to get dependency problems. I tried to make only a few dependencies, but it is just best to do them at some point. Tomorrow, I need to return the final math assignments, and then I will mainly concentrate on this course. Still, a lot to do, but I can make it. Before Friday, I hope JPS has been implemented. I have read about it a lot, and now it starts to be pretty clear how it works. I even read the article of the creators of JPS.

### Tuesday 12.12.2023
I added possibility to show the current map in the main menu as option 3. I created a better Visualization because the image was flickering, and I realized I was creating a new instance of the StringBuilder every time, which was inefficient. To fix this, I started reusing a single StringBuilder instance and clearing its content with the Clear method for each update. This significantly improved the performance and reduced the flickering.

### Wednesday 13.12.2023
Long day! I created PathCoordinatesValidator, which validates the inputs of the user. It checks that the given coordinates are inside the matrix and if the given coordinate is an obstacle. It took the whole day to get it to work. I had many problems getting the given coordinates to work and the path to travel in the right direction. Trying to realize which way the x and y should be created a significant challenge. In the evening, I solved it in the end after I took a walk to think of something else. 

So, now the user can decide which start and end coordinates should be explored, and the shortest path is printed to the console. It is also easy to add hardcoded values for the start and the end coordinates to ease the debugging. I decided to prepare all this before implementing JPS because I will need all the debugging possibilities when implementing it.

### Saturday 16.12.2023
Created A* algorithm so implementing JPS will be easier. It took a lot of time to do it.

### Sunday 17.12.2023
I started to fix path visualizer because I started to have feeling it is showing A* wrong. In the end I realized I am having huge bug. I am using same graph for both algorithms so all the nodes are already visited when the second algoritm uses it. First, I started to fix the problem too diffucult way by creating both algorithms their own graph and got some problems with depencies but then fix the problem just by one look reseting all the nodes in the graph except their location. Easy fix in the end. The I realized A* does not work right so it took long to optimize it. I needed to create new property visited for node so that the A* does not check again the same nodes. I also set up better testing environment.

### Monday 18.12.2023
I fixed graph the way that jumping over corners is not possible anymore. Also, added timing and result screen for algorithm comparison.