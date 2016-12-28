# ImageProcessingConsole
Image Processing Console Application in .Net

All contributors welcome

## Introduction
This command line app is designed to help post-process and fix issues with images files (currently only JPGs).

### Download
You can download the executable here. [ImageProcessingConsole.exe](https://github.com/nrogoff/ImageProcessingConsole/blob/master/download/ImageProcessingConsole.exe)

##### Platform code

* [Solution File](https://github.com/nrogoff/ImageProcessingConsole/blob/master/src/hms.ImageProcessing/hms.ImageProcessing.sln)
* [src folder](https://github.com/nrogoff/ImageProcessingConsole/tree/master/src)

## Functionality and How to use
As new functionality comes online, then this sections will be updated.
### Update EXIF 'Date Taken'
For various reasons photographic images sometimes do not have their **'Date Taken'** (also known as 'Digitized Date' or 'Original Date'). 
Cataloging tools (e.g. OneDrive or Google Drive Photos or Windows Photos App) will then usually resort to sorting and cataloging your photos by Created or Modified date. This can be pretty confusing, especially when you have copied and/or edited digital photo's or created photos by scanning old film and slides.

This tool has the capability to update one or more images 'Date Taken' EXIF metadata.

By default, if an image already has a 'Shot Taken Date' then any images files will NOT be updated.
If you do not supply a new 'Shot Taken Date' then the tool will search for an existing image (nearest first) in the same folder that has a valid date.

#### Use

The command line syntax (you can use the short or long form option switches. Note: long form require two preceeding dashes '--'):
```
>ImageProcessingConsole --help
======= HMS Image Processing Console App =======
Image Processing Console 1.0.1.21358
Copyright © Nicholas Rogoff 2016
MIT License
-------- Examples -------
- Update a single image
>ImageProcessingConsole -f "c:\myphotos\myphoto.jpg" -s 2003-11-17
- Update all images in a folder (inferred)
>ImageProcessingConsole -f "c:\myphotos\\"

  -f, --filepath             Required. The full path of the image file,
                             including the filename and extension (jpg only).
                             Or the folder to search and fix all 'Date Taken'
                             metadata in JPGs only. NOTE: You need to add an
                             extra '\' at the end when specifying a folder.

  -i, --includeSubfolders    Include subfolders. Use in combination with folder
                             searches.

  -s, --shottakendate        Set to this Shot Taken Date. Date must be formated
                             as follows 'yyyy-mm-dd hh:mm:ss'. If this date is
                             not specified then the 'Date Taken' (if exists) of
                             the nearest file in the same folder (ordered by
                             name) will be used.

  -o, --overwrite            Force overwrite of 'Date Taken' even if one
                             already exists for the file.

  --help                     Display this help screen.

```
##### Examples
To update a single images to a date from the nearest valid image file that has one.
```
>ImageProcessingConsole -f "c:\myphotos\myphoto.jpg"
```
To update a single images to a given date. (this will only update the file if it does not have an existing one).
```
>ImageProcessingConsole -f "c:\myphotos\myphoto.jpg" -s 2016-12-27
```
To update a single images to a given date, overwriting even if a date exists.
```
>ImageProcessingConsole -f "c:\myphotos\myphoto.jpg" -s 2016-12-27 -o
```
To update all the images in a folder from the nearest valid image file that has one. (NOTE: The final '\' must be followed by another.
```
>ImageProcessingConsole -f "c:\myphotos\\"
```
To update all the images in a folder AND all sub-folders (-i) from the nearest valid image file that has one. (NOTE: The final '\' must be doubled up.
```
>ImageProcessingConsole -f "c:\myphotos\\" -i
```
To update all the images in a folder AND all sub-folders (-i) with a specific date (not overwrite if one exists). (NOTE: The final '\' must be doubled up.
```
>ImageProcessingConsole -f "c:\myphotos\\" -i -s 2016-12-27
```
To update all the images in a folder AND all sub-folders (-i) with a specific date (overwrite if one exists). (NOTE: The final '\' must be doubled up.
```
>ImageProcessingConsole -f "c:\myphotos\\" -i -s 2016-12-27 -o
```
