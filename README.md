![projectalpha logo](https://raw.githubusercontent.com/GridProtectionAlliance/projectalpha/master/Source/Documentation/readme%20files/Project-Alpha-Logo_70.png)

# About


Project Alpha is a Visual Studio solution that provides a jump start to developing new products from the [Grid Solutions Framework](https://github.com/GridProtectionAlliance/gsf) - [Time-Series Library](https://www.gridprotectionalliance.org/technology.asp#TSL).

Note that Project Alpha was inspired by the code contribution of "openPDC Lite" from [Washington State University](http://school.eecs.wsu.edu/) that was designed to allow easy development and debugging of analytic based action adapters. Project Alpha also expands on this notion by allowing developers to easily develop and debug their action adapters from within a single solution. In addition, Project Alpha adds the ability for users of the GSF Time-series Library to completely develop an independent package for an analytic that contains all the pieces needed to deploy the application including database scripts, configuration utilities, a manager application, debug host application / release service and an installation utility.

![GPA Time-Series Library](https://raw.githubusercontent.com/GridProtectionAlliance/projectalpha/master/Source/Documentation/readme%20files/TSLoverview540.png)

# Documentation

Documentation for project alpha can be found [here](https://sway.com/1k26ACsHhV97nLIG).

# Deployment
1. Make sure your system meets all the [requirements](#requirements) below.
* Download the project source code
* Pick a “name” for your project
* Rename project alpha source code to your selected project name
* Pick a color scheme for you manager application
* Rearrange the manager menu’s for your application’s needs
* Design your analytic – i.e., design your Action adapter
* Enjoy.

Read the [documentation](#documentation) above for more detailed information.

## Requirements
* 64-bit Windows 7 or newer.
* .NET 4.6 or newer.
* Visual Studio 2012 or newer.
* Database management system such as:
  * SQL Server (Recommended)
  * MySQL
  * Oracle
  * PostgreSQL
  * SQLite (Not recommended for production use) - included.

# Contributing
If you would like to contribute please:

1. Read our [styleguide.](https://www.gridprotectionalliance.org/docs/GPA_Coding_Guidelines_2011_03.pdf)
* Fork the repository.
* Work your magic.
* Create a pull request.
