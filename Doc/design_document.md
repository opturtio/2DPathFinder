# Design Document
#### This document outlines the structure, components, and functionalities of the project. 

## **Design**
The PathFinder program is structured into six main parts: Pathfinding Algorithms for finding the shortest paths, Data Structures for graph and node management, Managers for coordinating algorithms and user interactions, Services for file handling, User Interface for user interaction and display, and Resources to store all the resource files.

## **Pathfinding Algorithms**
**The Pathfinding Algorithms contains classes to determine the shortest or most efficient paths in a graph.**

### **Dijkstra**
Implements Dijkstra's algorithm for finding the shortest path in a graph. This algorithm explores all possible paths to find the shortest one, making it a reliable but potentially slower method depending on the graph.

### **A\* (A-star)**
Implements the A algorithm, that combines the benefits of Dijkstra's algorithm with a heuristic approach to guide the search process, often making it faster by prioritizing promising paths.

### **JPS (Jump Point Search)**
Implements the Jump Point Search algorithm, which is an optimized version of A* designed for unweighted uniform grids. JPS speeds up the process by skipping over unnecessary nodes, making it faster in many grid-based scenarios.

## **Data Structures**
**The Data Structures contains classes that are essential for setting up, representing, and managing the graph and its elements, which are used by the pathfinding algorithms.**

### **Binary Heap**
Used by the JPS algorithm to manage nodes, enabling quick insertion and removal of elements.

### **Graph**
Represents a graph comprising nodes.

### **Node**
Represents a node in the graph.

## **Managers**
**Manager classes in PathFinder coordinate the application's logic and user interactions.**

### **GraphBuilder**
Creates a graph from a given string.

### **PathVisualizer**
LEGACY CODE! A base class that provides minimal functionality for path visualization, mainly serving as a foundation for the more detailed PathVisualizerWPF implementation.

### **ShortestPathBuilder**
Builds the shortest path from the end node back to the start.

## **Services**
**Service classes in PathFinder provide functionalities such as file loading and modification.**

### **FileLoader**
Loads map files from a directory.

### **FileModifier**
LEGACY CODE! Left for the future. Modifies and cleans up map file names for display.

## **UI**
**The User Interface is the front-end component that interacts with the user.**

### **MainWindow.xaml**
The `MainWindow.xaml` file defines the UI layout, including buttons for running the algorithms, a canvas for visualizing paths, and controls for adjusting the speed of the visualization.

### **MainWindow.xaml.cs**
The `MainWindow.xaml.cs` file is the code-behind for the `MainWindow.xaml` file and manages the user interactions and the dynamic behavior of the UI. It initializes the graph and visualizer, handles user inputs, and connects the UI controls with the corresponding pathfinding algorithms.

### **PathVisualizerWPF**
`PathVisualizerWPF` is a specialized class that extends the base `PathVisualizer` class to provide visual feedback on a WPF canvas. It is responsible for rendering the pathfinding process in real-time on the UI, showing the progression of the algorithms visually.

## **Resources**
**The Resources folder holds resource files for the program, including maps for pathfinding.**

### **Maps folder**
Contains all the maps used in the program.
