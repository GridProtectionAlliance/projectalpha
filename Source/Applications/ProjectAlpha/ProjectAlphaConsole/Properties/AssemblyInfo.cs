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
[assembly: AssemblyDescription("Remote console application for windows service that hosts input, action and output adapters.")]
[assembly: AssemblyTitle("ProjectAlpha Remote Console")]

// Other configuration attributes.
[assembly: ComVisible(false)]
[assembly: Guid("1db0288f-db88-45d5-9187-5ab60e4dba69")]
