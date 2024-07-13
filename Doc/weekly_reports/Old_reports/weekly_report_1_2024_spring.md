# Weekly Report 1


### Wednesday 17.1.2024
I have been trying to solve the JPS problem for many weeks now. I am finding minor bugs every time. I was fixing the structure many times. I tried the older code I had made but realized it was not the right approach. I realized at the end of the Jump method I had changed the return to null at some point when it was supposed to be return this.Jump(nextNode, direction, end). I can debug the code pretty well now and see what is happening.

### Thursday 18.01.2024
I created to visualization map a start point presenting letter 'S' as start and an end point presenting letter 'G' as goal. There was a big bug in the Heuristic method of the JPS class. It calculated the distance between the current node and the jump point node when it was supposed to calculate the distance between the end node and the jump point node. There is still a problem with the coordinates, and for that reason, the algorithm does not work. I need more debugging to find out what the problem is. I updated requirement specification. I partly used chatGPT to update my old requirement specification. Later at evening I fixed the GetDirections so that the it gets the directions more logically. I am pretty sure now the problem is in Jump method diagonal, vertical or horizontal movements. I think the problem is recursion. I need to check it well later.

### Friday 19.01.2024
I started working on improving the JPS algorithm. First, I removed the 'HasForcedNeighbors' method and combined its functionality with the 'Jump' method, which made the code easier to understand. As I continued debugging, I made significant progress in identifying issues by using breakpoints and the 'GetNodeInfo' method. I narrowed down the problem to checking diagonal movements in the Jump method. Eventually, I pinpointed the issue to a part where the recursion wasn't functioning correctly:

if (this.Jump(nextNode, (direction.x, 0), start, end) != null || this.Jump(nextNode, (0, direction.y), start, end) != null)
{
    return nextNode;
}

I discovered that the line 'return nextNode' was returning the starting point of the search for the jump point rather than the jump point itself. 
So, I separated the horizontal and vertical jump checks into two distinct steps, making it clearer and more straightforward. This way, the algorithm now saves the checked nodes in newNodeX and newNodeY. Before, it was just returning nextNode, but with this change, it correctly identifies and returns the specific jump points:

Node? newNodeX = this.Jump(nextNode, (direction.x, 0), start, end);
Node? newNodeY = this.Jump(nextNode, (0, direction.y), start, end);

if (newNodeX != null)
{
    return newNodeX;
}

if (newNodeY != null)
{
    return newNodeY;
}

Finally, it seems the JPS is functioning properly. Next, I need to begin testing it in various scenarios. Next weeks I will be testing the program.