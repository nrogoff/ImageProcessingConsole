using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CommandLine;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("Image Processing Console")]
[assembly: AssemblyDescription("A command line tool for fixing issues with images")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Hard Medium Soft Ltd.")]
[assembly: AssemblyProduct("hms.ImageProcessingConsole")]
[assembly: AssemblyCopyright("Copyright © Nicholas Rogoff 2016")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: AssemblyLicense("MIT License")]
[assembly: AssemblyUsage("-------- Examples -------","- Update a single image",">ImageProcessingConsole -f \"c:\\myphotos\\myphoto.jpg\" -s 2003-11-17","- Update all images in a folder (inferred)", ">ImageProcessingConsole -f \"c:\\myphotos\\\\\"")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("1b7849b1-17ba-416e-a5ca-cce828276a56")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers 
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.1.*")]
[assembly: AssemblyFileVersion("1.0.1.0")]
