# Weekly report 3

### Thursday 16.11.2023
I still feel sick but better now. Mostly, I feel powerless. I started to make the tests for the existing project. I chose to use the NUnit testing
environment because I read it is widely used. I had problems setting up a test environment for the project. The main reason was that I did
not understand that I needed to add the tests to the same PathFinder solution. This took some time to figure out. In the end, after many attempts, I realized how everything is done. I changed once again the file structure. I created PathFinder.Tests folder where is the tests and PathFinder folder
for the main program files. I added PathFinder.Tests to same solution with the PathFinder. Now, everything is set up for testing.

I am using NUnit for unit testing. I created a script for the coverage report. The user just needs to double click generate-coverage-report.bat file or run it in cmd. It will automatically delete an old coverage file if one exists. It will create a new coverage file and an HTML file to see the coverage. Today, I spent most of the time making one test, which I could not do in the end. Tomorrow, I will make more accessible tests and start to create the 
graph.

### Friday 17.11.2023
I was creating tests, but there is a problem. The tests cannot find the file location on maps for some reason. I was struggling with this problem the whole day and yesterday. I cannot find the solution. I even modified PathFinder.Tests.csproj file and added code to it that copies the content of TestDate/Maps/* to runtime folders. Not finding the solution was frustrating, but I learned much from all of this. In the end, after six hours, I gave up and decided to start to make the algorithms. I created a Node class and began investigating how to change the string currentMap to the graph.

### Saturday 18.11.2023
I have a high fever so I cannot do anything. If I feel better tomorrow, I will continue investigating how to create the graph.