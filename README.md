# Sonic Unleashed Particle Tool
A tool to convert Unleashed .Part-Bin and .P-Mat-Bin files into .Particle and .P-Material files and vice versa

## Usage
`SU-Particle-Tool <Path to .part-bin/.p-mat-bin>`\
You can also simply drag and drop supported file formats onto the executable.

> [!NOTE]
> The .Particle and .P-Material file formats can still be used in Unleashed as the game still supports them

> [!WARNING]
> Any .Particle files that were made by the tool before the next release will no longer work!<br>
> Converting them back at the moment will cause a crash as there are new parameters!<br>
> This is subject to change!!<br>
> <br>
> (This Could also apply to .P-Materials however it is just the removal of the UVDescType parameter, easily remedied)

## To Do
~~- Add XML to Binary support (if needed)~~

## Thanks
a HUGE thanks to [NextinHKRY](https://github.com/NextinMono) for all his help with coding and finding parameters!!

## Requirements
If the tool does nothing when used, you may need to install the [.NET Runtime](https://aka.ms/dotnet-core-applaunch?missing_runtime=true&arch=x64&rid=win10-x64&apphost_version=8.0) package that the tool needs to work
