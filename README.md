![projectalpha logo](https://raw.githubusercontent.com/GridProtectionAlliance/projectalpha/master/Source/Documentation/readme%20files/Project-Alpha-Logo_70.png)

# About


Project Alpha is a Visual Studio solution that provides a jump start to developing new products from the [Grid Solutions Framework](https://github.com/GridProtectionAlliance/gsf) - [Time-Series Library](https://www.gridprotectionalliance.org/technology.asp#TSL).

After downloading the source code of this project you can run the "[rename project](https://github.com/GridProtectionAlliance/projectalpha/blob/master/RenameProject.bat)" script which will create your own personal service-based application that can manage and host time-series adapters. A full [WiX Toolset](https://wixtoolset.org/) based installation package is also included.

See [Building New Analytics with Project Alpha](https://sway.office.com/1k26ACsHhV97nLIG?ref=email&loc=play).

Some example projects that used Project Alpha as a starting point are:
* [substationSBG](https://github.com/GridProtectionAlliance/substationSBG)
* [PTPSync](https://github.com/GridProtectionAlliance/PTPSync)



![GPA Time-Series Library](https://raw.githubusercontent.com/GridProtectionAlliance/projectalpha/master/Source/Documentation/readme%20files/TSLoverview540.png)

# Documentation and Support

* Documentation for project alpha can be found [here](https://sway.com/1k26ACsHhV97nLIG).
* Get in contact with our development team on our new [discussion boards](http://discussions.gridprotectionalliance.org/c/gpa-products/project-alpha).

Some other useful links:
* [Library documentation for the Grid Solutions Framework](https://www.gridprotectionalliance.org/NightlyBuilds/GridSolutionsFramework/Help/html/N_GSF.htm)
* [Discussion board for the Grid Solutions Framework](http://discussions.gridprotectionalliance.org/c/gpa-products/gsf) - note: this is a new board, searching the history of existing threads on old CodePlex sites could provide insights into common questions:
  * [openPDC](http://openpdc.codeplex.com/discussions).
  * [Grid Solutions Framework](http://gsf.codeplex.com/discussions).
* [Major components of the Time-Series Library application](https://www.gridprotectionalliance.org/docs/products/gsf/tsl-components-2015.pdf).
* [How to create a custom adapter](https://github.com/GridProtectionAlliance/openPDC/blob/master/Source/Documentation/wiki/Developers_Custom_Adapters.md).
* [Custom adapter examples](https://github.com/GridProtectionAlliance/openPDC/blob/master/Source/Documentation/wiki/Developers_Custom_Adapters.md).
* [Source code for various Time-Series Library custom adapter implementations](https://github.com/GridProtectionAlliance/gsf/tree/master/Source/Libraries/Adapters).
* [A full history of GPA User Forum presentations](https://www.gridprotectionalliance.org/UserForum/).
* Some User Forum presentations of possible relevance:
  * [Custom Adapter Development (newer)](https://www.gridprotectionalliance.org/UserForum/2014/Tutorial%20Session%203%20(Carroll)%20-%202014%2008%2012.pdf).
  * [Custom Adapter Development (older)](https://www.gridprotectionalliance.org/UserForum/2012/Building%20Custom%20Adapters.pdf).
  * [Gateway Exchange Protocol](https://www.gridprotectionalliance.org/UserForum/2014/Tutorial%20Session%202%20(Carroll)%20-%202014%2008%2012.pdf).
* [GSF libraries on NuGet](https://www.nuget.org/packages?q=%22Grid+Solutions+Framework%22).

# Deployment
1. Make sure your system meets all the [requirements](#requirements) below.
* Download the project source code
* Pick a “name” for your project
* Rename project alpha source code to your selected project name
* Pick a color scheme for you manager application
* Rearrange the manager menu’s for your application’s needs
* Design your analytic – i.e., design your Action adapter
* Enjoy.

Read the [documentation](#documentation-and-support) above for more detailed information.

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
 
# License
Project Alpha is licensed under the [MIT License](https://opensource.org/licenses/MIT).

---

_Note that Project Alpha was inspired by the code contribution of "openPDC Lite" from [Washington State University](http://school.eecs.wsu.edu/) that was designed to allow easy development and debugging of analytic based action adapters. Project Alpha also expands on this notion by allowing developers to easily develop and debug their action adapters from within a single solution. In addition, Project Alpha adds the ability for users of the GSF Time-series Library to completely develop an independent package for an analytic that contains all the pieces needed to deploy the application including database scripts, configuration utilities, a manager application, debug host application / release service and an installation utility._
