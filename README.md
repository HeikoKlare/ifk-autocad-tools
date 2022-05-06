# IFK AutoCAD Tools
This repository covers a suite of plug-ins for AutoCAD, in particular for constructive engineering of gang-nail truss roofs.
It provides functionality for specific drawing tasks, for creating plot layouts, and for managing and inserting project data, recurring annotations and details.
To this end, the tools can be connected to a MySQL database storing the according data.

## Dependencies:
For execution:
* AutoCAD 2015
* .NET 4.5.2

For development:
* Visual Studio 2022
* MySQL Connector for Visual Studio
* WiX toolset for Visual Studio

## Setup for Development
To develop and compile the tools, ensure that the dependencies are installed and simply clone the repository and open the Visual Studio solution file.
When compiling the project, the generated DLLs are placed in the standard AutoCAD install directory, such that they are automatically loaded by AutoCAD.
Ensure that you have placed the additional data, such as customization files, plotter configurations etc., provided by the WiX installer at the according places (most simply by generating a setup file and executing it).

To setup a database with a proper scheme to which the tools can be connected, use the SQL script within the _Setup_ folder.
