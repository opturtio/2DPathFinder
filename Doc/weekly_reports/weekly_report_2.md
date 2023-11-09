# Weekly report 2

### Wednesday 8.11.2023
This is my first all by myself program with C# so I decided to study first a bit how to start the project. I read some documentations, stackoverflow, chatgpt and watched some videos. I did not find it hard so I just created the project to .git folder which I made last week. The file structure is not optimal because now the src files are in TiraLab/TiraLab/TiraLab/. I do not find it bad anyways so I think I just keep it like that. There is important files in each file.

I was thinking some time how to create the file structure but now it is getting more clear. I think it still will change a bit but I have the main structure already planned. I am trying to keep classes small and try to follow the Single Responsibility Principle.

I decided to add all the strings to ResX-file where I can fetch them easily. It is crucial to have all the static strings in one place rather than shattered around the code base(have some great experience of this :D). I am familiar with XML-files so implenting ResX-file system was easy. It is actually very nice file format and have easy-to-use GUI.

### Thursday 9.11.2023
I decided to fix the file structure in the project. I moved all the files from TiraLab/TiraLab/... to root. Now the structure is much nicer looking. I had problem because Visual Studio could not find the project anymore but it was easy to fix. I just needed to add it again Solution Explorer and had to rebuild the project.

Now the project have a name PathFinder. I wanted the project have some other name than just TiraLab. First I gave the name 2DPathFinder but it would not be suited for namespace name because it is technically invalid so I change it to PathFinder. I changed all the file names and namespaces to it. I get a problem but it was because of the ResourceManager and AllStrings.Designer.cs which are using namespaces as reference so I needed to fix them and now the program works.

