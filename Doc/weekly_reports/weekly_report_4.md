# Weekly report 4

### Wednesday 31.7.2024

After a long examination, I realized the problem with why the big maps do not work, because the nodes are examined twice, so when the path is created, it will have the same parent with more than one node, creating an infinite loop. The path is found, and I believe it is correct, but there is no way to prove it. The algorithm still always works in mazes, so it works. I added this line to the JPS algorithm to prevent the line crossing
```
            if (currentNode.Parent != null && currentNode.Parent != parentNode)
            {
                return null;
            }
```
But in this way, the shortest path is not found. It finds some path short path. In visualization, you can see that this blocks other lines. Now I am not sure anymore, how the algorithm should work. Should it sometimes go through the same nodes or not? I feel frustrated when I believe this should be somehow prevented, but I don't know how, and I always end up with this same problem. When I added the presentation, the algorithm did not work correctly.

