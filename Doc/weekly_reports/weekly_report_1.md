# Weekly Report 1


### Wednesday 17.1.2024
I have been trying to solve the JPS problem for many weeks now. I am finding minor bugs every time. I was fixing the structure many times. I tried the older code I had made but realized it was not the right approach. I realized at the end of the Jump method I had changed the return to null at some point when it was supposed to be return this.Jump(nextNode, direction, end). I can debug the code pretty well now and see what is happening.

### Thursday 18.01.2024
I created to visualization map a start point presenting letter 'S' as start and an end point presenting letter 'G' as goal. There was a big bug in the Heuristic method of the JPS class. It calculated the distance between the current node and the jump point node when it was supposed to calculate the distance between the end node and the jump point node. There is still a problem with the coordinates, and for that reason, the algorithm does not work. I need more debugging to find out what the problem is. I updated requirement specification. I partly used chatGPT to update my old requirement specification. Later at evening I fixed the GetDirections so that the it gets the directions more logically. I am pretty sure now the problem is in Jump method diagonal, vertical or horizontal movements. I think the problem is recursion. I need to check it well later.