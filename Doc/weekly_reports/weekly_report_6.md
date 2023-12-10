# Weekly report 6

### Saturday 09.12.2023
I have been studying how the JPS algorithm works and how to implement it. I studied it 28.11, 5.12 and 6.12. I watched some videos and read different sources. I've been busy with my two other courses and have felt very tired. Now, I am trying to finish this project. It took a lot of effort to write the peer reviews, but both were educating. I realized the CoverageReport folder is added to GitHub, so the Languages bar showed I mainly use HTML and CSS. I wanted to fix this first thing today. For the rest of the day, I decided to update the documentation. Tomorrow, I will start implementing the JPS algorithm and figure out how to write tests. Tuesday is the final return of the math course. Then I have another course, and I am not many weeks late in it because I was sick earlier. I think I can finish all of them.

I created design documentation. I will update it later, but at least now, the basics of it are done. I will create a user guide, but it is not my main priority.

### Sunday 10.12.2023
I created a visualization class to see how the algorithm works (Dijkstra, in this case). Next, I want the program to print the shortest path. Then, I will implement JPS. I used this video to implement the visualization: https://www.helsinki.fi/fi/unitube/video/ffe9e56a-f1d3-4ba7-a9d5-bd663d4079f7
It was useful. I remembered it from the past. I had some big problems implementing it. I get index out of range and could not figure out why but the I realized I do not clean the carriage return when the map is created only when the graph is created. I try to trim it when the currentMap string is created but it did not work so I had to Trim it in Visualize method.