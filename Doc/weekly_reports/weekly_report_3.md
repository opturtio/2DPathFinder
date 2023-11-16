# Weekly report 3

### 16.11.2023
I still feel sick but better now. Mostly out of power. Decided to start to make the tests for the existing project. I decided to use NUnit testing
environment for the testing because I read it is widely used. I had problems to setup test environment for the project. Main reason was that I did
not understand that I need to add the tests to the same PathFinder solution. This took some time to figure out. In the end after many attempts I realized how everything is done. I changed once again the file structure. I created PathFinder.Tests folder where is the tests and PathFinder folder
for the main program files. I added PathFinder.Tests to same solution with the PathFinder. Now everything is set up for testing.

I am using NUnit for unit testing. I created script for coverage report. User just need to double click generate-coverage-report.bat file or run it in cmd. It will automatically delete old coverage file if one exist. It will create new coverage file and create html file to see the coverage. Today most of the time went to make one test which I was not able to do in the end. Tomorrow I will just make easier tests and start to create the 
graph.