//////////////////////////////////////////////////
//
// Myo.NET
// version 1.0.1
//
//////////////////////////////////////////////////

NuGet does not natively support binaries that target specific platforms.  
So for now this package shall serve as a redirection for the package Myo.Net.x86 until further notice.

It is recommended that you directly reference either of the x86 or x64 packages directly, if at all possible.

Be sure to select the platform that matches the settings in the Build > Configuration Manager.. 
You may have to add a new configuration by clicking drop down box under Platform next to the project referencing Myo.NET
and selecting <New...>

== Breaking Changes in v1.0.1 (Sorry!)

- Renamed Thalmic.Myo namespace to MyoNet.Myo
- Renamed IMyo.Pose to IMyo.PoseChanged.
- Renamed PoseEventArgs to PoseChangedEventArgs.
