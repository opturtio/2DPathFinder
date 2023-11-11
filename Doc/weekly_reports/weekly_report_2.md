# Weekly report 2

### Wednesday 8.11.2023
This is my first all-by-myself program with C#, so at first I decided to study a bit how to start the project. 
I read some documentations, stackoverflow, chatgpt and watched some videos. I did not find it hard, so I just created 
the project in the .git folder, which I made last week. The file structure is not optimal because now the src files are in 
TiraLab/TiraLab/TiraLab/. I do not find it bad anyways, so I think I just keep it like that. There are important files in 
each file.

I was thinking for some time how to create the file structure, but now it is getting more clear. I think it will still 
change a bit, but I have the main structure already planned. I am trying to keep classes small and try to follow 
the Single Responsibility Principle.

I decided to add all the strings to the ResX-file, where I can fetch them easily. It is crucial to have all the static 
strings in one place rather than shattered around the code base(I have some great experience of this :D). 
I am familiar with XML-files so implenting ResX-file system was easy. It is actually a very nice file format 
and have easy-to-use GUI.

### Thursday 9.11.2023
I decided to fix the file structure in the project. I moved all the files from TiraLab/TiraLab/... to root. 
Now, the structure is much nicer looking. I had a problem because Visual Studio could not find the project anymore, but 
it was easy to fix. I just needed to add it again to Solution Explorer and had to rebuild the project.

Now, the project has a name, PathFinder. I wanted the project to have some other name than just TiraLab. First I gave 
the name 2DPathFinder, but it would not be suited for the namespace name because it is technically invalid, so I change 
it to PathFinder. I changed all the file names and namespaces to it. I had a problem, but it was because of 
the ResourceManager and AllStrings.Designer.cs, which uses namespaces as reference, so I needed to fix them 
and now the program works.

I forgot to commit at some point, so I had to make a big commit. I added maps, Berlin, London, and Test. Map name choices
can now be printed when the user chooses the option "2. Choose map" from the main menu. I added classes FileLoader and 
FileModifier. These classes will help the user to load maps and choose which map to pick. OutputManager prints the map 
name and size. Printing and choosing maps took time because I had to work with lists and tuples. I am familiar 
with C# & C++ tuples, but handling them was sometimes confusing in scenarios where functions take one as input and
return the other. It is more complex than in Python, but I like this kind of programming more. This day was educational.

### Saturday 11.11.2023
I am sick, so I could not do anything yesterday. I sent an email to the instructor about this. Today, I was able to refactor the project 
to have a better structure. I commented all the classes. I used chatgpt to help find more professional words to describe the classes' 
functionalities better, but I wrote all the comments myself. Now, it is possible to load the map from ./Resources/Maps. 
The map is stored as a string in the 'currentMap' variable within the CommandManager. I am surprised I was able to do all this even that I
feel terrible. Next, I am going to start to create the algorithms and make unit tests.