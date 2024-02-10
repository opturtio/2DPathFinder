# User Guide

#### This program is developed using Windows, thus it is recommended to use it.

## Installation

1. Install [Git](https://git-scm.com/download/win) for Windows.

2. Recommendation! Download and installing [Visual Studio 2022](https://visualstudio.microsoft.com/thank-you-downloading-visual-studio/?sku=Community&channel=Release&version=VS2022&source=VSLandingPage&passive=false&cid=2030). (Choose from workloads .NET desktop development)

3. Clone the project in Visual Studio 2022 or clone the repository using the command prompt:
```bash
git clone https://github.com/opturtio/PathFinder.git
```

4. Open the project in Visual Studio 2022 by navigating to ./PathFinder/PathFinder.sln. Dependencies should install automatically. If prompted, proceed with the installation of the .NET Runtime environment. Additionally, you can install it manually: [.NET for Windows](https://dotnet.microsoft.com/en-us/download/visual-studio-sdks). All set!


### Debugger: Character Representation

#### Wall: `@`

#### Visited: `#`

#### Current: `X`

#### Start: `E`

#### Goal: `G`

#### Jump Point: `J`


## How to use the program

### The main window

![](./Pics/MainScreen.png)

### Choosing the map

#### First, the user has to choose the map for comparison by selecting 2 and then choose the map by its ID.

![](./Pics/ChooseMap.png)

### Show the map

#### After choosing the map, you can see the map by pressing 3. This step is optional. Remember, you can zoom in and out on the console screen to see the maps better, which is essential for large maps.

![](./Pics/ShowMap.png)

### Algorithm comparison

#### Compare algorithms by pressing 1. Decide whether to turn on the debug mode by selecting `y` or `n`. Then, enter the coordinates.

![](./Pics/ChoosingCoordinates.png)

#### Choosing out-of-bound coordinates lets you choose coordinates again. The coordinates are given between zero and max value.

![](./Pics/WrongCoordinate.png)

### Result screen

#### Finally, view the search results. You may need to scroll up to see other algorithms' shortest paths.
![](./Pics/Result.png)