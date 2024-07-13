# Weekly Report 4


### 5.2.2024
I created two new brances because I started to create pruning for JPS. This have been so difficult task. This project feels never ending and I cannot find solution. There is not really good material for JPS where I could see the whole algorithm. I can just find pieces and all the implementation differ. Today I used six hours to find the solution. I implemented some sort of pruning. I also deleted many hours of work. Later I realized I should have just created new branch out of it. It had some valuable code. Well I still try to finish the algorithm before end of this course.

### 9.2.2024
I was studying more JPS and tried to make pruning work in JPS-pruning-added branch.

### 10.2.2024
Created user guide and installation instructions. It took some time because I had to document all the steps. I was also trying to figure out what is wrong with the JPS. I know the implementation in main is not right but it is best yet so I leave it there. This structure is not right:                 
``` C#           
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
```
One problem is that I need more time to focus on this project, but it is taking too much time from my studies. Still, it is the most exciting project I have worked on. I think next week I will need help!