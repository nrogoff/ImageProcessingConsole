# ImageProcessingConsole
Image Processing Console Application in .Net

All contributors welcome

## Introduction
This command line app is designed to help post-process and fix issues with images files (currently only JPGs).

### Download
You can download the executable here. [ImageProcessingConsole.exe](https://github.com/nrogoff/ImageProcessingConsole/blob/master/download/ImageProcessingConsole.exe)

##### Platform code

* [Solution File](https://github.com/nrogoff/ImageProcessingConsole/blob/master/src/hms.ImageProcessing/hms.ImageProcessing.sln)
* [Links to platform specific renderers or concrete implementations in the repo](#)

## Functionality and How to use
As new functionality comes online, then this sections will be updated.
### Update EXIF 'Shot Taken' date
For various reasons photographic images sometimes do not have their 'Shot Taken Date' (also known as 'Digitized Date', 'Date Taken' or 'Original Date'). 
Cataloging tools (e.g. OneDrive or Google Drive Photos or Windows Photos App) will then usually resort to sorting and cataloging your photos by Created or Modified date. This can be pretty confusing, especially when you have copied and/or edited digital photo's or created photos by scanning old film and slides.

This tool has the capability to update one or more image EXIF metadata 'Shot Taken Date's.

By default, if an image already has a 'Shot Taken Date' then any images files will NOT be updated.
If you do not supply a new 'Shot Taken Date' then the tool will search for an existing image (nearest first) in the same folder that has a valid date.

#### Use

The command line syntax (you can use the short or long form option switches. Note: long form require two preceeding dashes '--'):
```
-f, --filepath             
Required. The full path of the image file, including the filename and extension. 
Or the folder to search and fix all images in. JPGs and PNGs only

-i, --includeSubfolders    
If you specified a folder, then to include subfolders.

-s, --shootdate            
Set to this Shot Taken Date. yyyy-mm-dd hh:mm:ss . 
If not specified then the shoot date of the nearest file in the same folder (ordered by name) that has one will be used.

-o, --overwrite            
Force Shot Taken Date overwrite even if one exists.
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
