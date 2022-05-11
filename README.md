# IFK AutoCAD Tools
This repository covers a suite of plug-ins for AutoCAD, in particular for constructive engineering of gang-nail truss roofs.
It provides functionality for specific drawing tasks, for creating plot layouts, and for managing and inserting project data, recurring annotations and details.
To this end, the tools can be connected to a MySQL database storing the according data.

## Dependencies:
For execution:
* AutoCAD 2015
* .NET Framework 4.8

For development:
* Visual Studio 2022
* MySQL Connector for .NET (Minimum 8.0.29)
* WiX Toolset and Extension for Visual Studio (Minimum 3.11)

## Setup for Development
To develop and compile the tools, ensure that the dependencies are installed and simply clone the repository and open the Visual Studio solution file.
When compiling the project, the generated DLLs are placed in the standard AutoCAD install directory. You can load the DLLs by hand after opening AutoCAD or you can run an installer of a released version before, which registers the DLL in the Windows registry to load it on startup.
Ensure that you have placed the additional data, such as customization files, plotter configurations etc., provided by the WiX installer, at the according places (most simply by running an installer of a released version).

To setup a database with a proper scheme to which the tools can be connected, use the SQL script within the _Setup_ folder.
