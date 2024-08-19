# Design Document
#### This document outlines the structure, components, and functionalities of the project. 

## **Design**
The PathFinder program is structured into six main parts: Pathfinding Algorithms for finding the shortest paths, Data Structures for graph and node management, Managers for coordinating algorithms and user interactions, Services for file handling, a User Interface for user interaction and display, and Resources to store all the resource files e.g. maps and strings used in the program.

## **Pathfinding Algorigms**

#### **Dijkstra**
Implements Dijkstra's algorithm for finding the shortest path in a graph. This algorithm explores all possible paths to find the shortest one, making it a reliable but potentially slower method depending on the graph.

#### **A* (A-star)**
Implements the A algorithm, that combines the benefits of Dijkstra's algorithm with a heuristic approach to guide the search process, often making it faster by prioritizing promising paths.

#### **JPS (Jump Point Search)**
Implements the Jump Point Search algorithm, which is an optimized version of A* designed for uniform grids. JPS speeds up the process by skipping over unnecessary nodes, making it faster in many grid-based scenarios.

## **Data Structures**
The Data Structures contains classes that are essential for creating the map and managing the paths for the pathfinding algorithms. 

#### **Binary Heap**
Used by the JPS algorithm to manage nodes, enabling quick insertion and removal of elements.

#### **Graph**
Represents a graph comprising nodes.

#### **Node**
Represents a node in the graph.

## **Managers**
Manager classes in PathFinder coordinate the application's logic and user interactions.

#### **AlgorithmComparisonManager**
Compares different pathfinding algorithms.

#### **CommandManager**
Handles user commands and interactions.

#### **FileManager**
Manages file operations, especially with map files.

#### **GraphBuilder**
Creates a graph from a given string.

#### **OutputManager**
Displays information and messages to the user.

#### **PathCoordinatesValidator**
Checks if the start and end points are valid and within the map.

### **PathVisualizer**
Visualizes how the pathfinding works on a map and helps with debugging.

### **ShortestPathBuilder**
Builds the shortest path from the end node back to the start.

## **Services**
Service classes in PathFinder provide functionalities such as file loading and modification.

#### **FileLoader**
Loads map files from a directory.

#### **FileModifier**
Modifies and cleans up map file names for display.

## **UI**
The User Interface is the front-end component that interacts with the user.

#### **UserInterface**
Handles user interactions, displays menus, and captures input. It uses OutputManager for displaying text and CommandManager for processing user inputs.

## **Resources**
The Resources folder holds resource files for the program, including maps for pathfinding and strings used in the application.

#### **AllStrings.resx**
Contains all the strings used in the program.

#### **Maps folder**
Contains all the maps used in the program.
