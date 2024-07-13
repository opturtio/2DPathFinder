# Weekly report 1

### Wednesday 10.7.2024
I started to check what the problem was with the JPS and got familiar with the project again. I ended up fixing pruning.

### Thursday 11.7.2024
I fixed the jump method. I need to fix more pruning. 

### Friday 12.7.2024
I was able to get the JPS to work. It finds the shortest path in small maps, but it is slow. I decided to create a heap, and it got fast straight away. The problem was PriorityQueue. Now, JPS works fast on all the maps. It does not work correct on the big open maps like Berlin and London. It does not find the shortest path. One problem could be heuristic or recursion in the jump method.

### Saturday 13.7.2024
I created a test using maze map. JPS works in every case. It is hard to find out what the problem is with the open maps. At least I have a lot of time to determine what is wrong.