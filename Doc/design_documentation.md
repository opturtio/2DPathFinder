# Design Document
This document outlines the structure, components, and functionalities of the project.

## Design
The PathFinder program is structured into five main parts: Data Structures for graph and node management, Managers for coordinating algorithms and user interactions, Services for file handling, a User Interface for user interaction and display, and Resources to store all the resource files e.g. maps and strings used in the program.

### Data Structures
The Data Structures contains classes that are essential for creating the map and managing the paths for the pathfinding algorithms. 

##### Dijkstra
Implements Dijkstra's algorithm for finding the shortest path in a graph.

##### Graph
Represents a graph comprising nodes.

##### GraphBuilder
Creates a graph from a given string.

##### Node
Represents a node in the graph.

### Managers
Manager classes in PathFinder coordinate the application's logic and user interactions.

##### AlgorithmComparisonManager
Compares different pathfinding algorithms.

##### CommandManager
Handles user commands and interactions.

##### FileManager
Manages file operations, especially with map files.

##### OutputManager
Displays information and messages to the user.

### Services
Service classes in PathFinder provide functionalities such as file loading and modification.

##### FileLoader
Loads map files from a directory.

##### FileModifier
Modifies and cleans up map file names for display.

### UI
The User Interface is the front-end component that interacts with the user.

##### UserInterface
Handles user interactions, displays menus, and captures input. It uses OutputManager for displaying text and CommandManager for processing user inputs.

### Resources
The Resources folder holds resource files for the program, including maps for pathfinding and strings used in the application.

##### AllStrings.resx
Contains all the strings used in the program.

##### Maps folder
Contains all the maps used in the program.
