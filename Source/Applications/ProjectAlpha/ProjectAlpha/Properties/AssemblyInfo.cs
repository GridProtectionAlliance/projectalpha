﻿using System.Reflection;
using System.Runtime.InteropServices;

// Assembly identity attributes.
[assembly: AssemblyVersion("0.6.293.0")]

// Informational attributes.
[assembly: AssemblyCompany("Grid Protection Alliance")]
[assembly: AssemblyCopyright("Copyright © 2015.  All Rights Reserved.")]
[assembly: AssemblyProduct("ProjectAlpha")]

// Assembly manifest attributes.
#if DEBUG
[assembly: AssemblyConfiguration("Debug Build")]
#else
[assembly: AssemblyConfiguration("Release Build")]
#endif
[assembly: AssemblyDescription("Windows service that hosts input, action and output adapters.")]
[assembly: AssemblyTitle("ProjectAlpha Iaon Host")]

// Other configuration attributes.
[assembly: ComVisible(false)]
[assembly: Guid("c8a353e9-9383-46c5-8c19-8da53d307cde")]
