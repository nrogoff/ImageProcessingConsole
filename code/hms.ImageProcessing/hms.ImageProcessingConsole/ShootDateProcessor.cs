// Copyright (c) 2016 Nicholas Rogoff  
// 
// Author: Nicholas Rogoff
// Created: 2016 - 12 - 26
// 
// Project: hms.ImageProcessingConsole
// Filename: ShootDateProcessor.cs

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using hms.ConsoleProcessorTemplate;

namespace hms.ImageProcessingConsole
{
    public class ShootDateProcessor:ConsoleProcessorTemplateBase<RunOptions>
    {
        private List<ImageFileItem> _fileList = new List<ImageFileItem>();

        protected override void PreProcess()
        {
            try
            {
                if (Path.HasExtension(Options.FilePath))
                {
                    //single file
                    Output.WriteLine("Processing single image...");

                    //check that file type is valid
                    if (Path.GetExtension(Options.FilePath).ToLower() != ".jpg")
                    {
                        Error.WriteLine("Error: The file is not a JPG.");
                        Console.CursorVisible = true;
#if DEBUG
                        Console.ReadKey();
#endif
                        Environment.Exit(-1);
                    }

                    //check file exists
                    if (!File.Exists(Options.FilePath))
                    {
                        Error.WriteLine("Error: The file cannot be found. Check that the path is correct.");
                        Console.CursorVisible = true;
#if DEBUG
                        Console.ReadKey();
#endif
                        Environment.Exit(-1);
                    }

                    var origShootDate = ImageHandler.GetShootDate(Options.FilePath);

                    //see if date is specified in command options
                    if (Options.ShootDate == null)
                    {
                        //find closest image file and read shoot date working back.
                        var folderHandler = new FolderHandler();
                        _fileList = folderHandler.GetAllImagesInFolder(Options.FilePath);

                        DateTime? newShootDate = GetNearestShootData(_fileList, Options.FilePath);
                        if (newShootDate != null)
                        {
                            //get the single passed file to update
                            var fileToUpdate = _fileList.FirstOrDefault(f => f.FilePath == Options.FilePath);
                            fileToUpdate.newShootDate = newShootDate;
                            fileToUpdate.ReplaceShootData = true;
                        }
                        else
                        {
                            Error.WriteLine(
                                "Error: No new Shoot Date (-s) was given and no other images in the folder contained a valid Sht Taken Date to use. You must specify a date.");
                            Console.CursorVisible = true;
#if DEBUG
                            Console.ReadKey();
#endif
                            Environment.Exit(-1);
                        }
                    }
                    else
                    {
                        _fileList.Add(new ImageFileItem
                        {
                            Folder = Path.GetDirectoryName(Options.FilePath),
                            FilePath = Options.FilePath,
                            OrigShootDate = origShootDate,
                            newShootDate = Options.ShootDate.Value,
                            ReplaceShootData = true,
                            Processed = false
                        });

                    }
                }
                else //folder specified
                {
                    Output.WriteLine($"Processing all images in folder {Options.FilePath}");
                    if (Options.IncludeSubFolders)
                        Output.WriteLine("...including all subfolders.");

                    var folderHandler = new FolderHandler
                    {
                        ProgressUpdateAction = s => { BusyIndicator.UpdateProgress(s); }
                    };
                    _fileList = folderHandler.GetAllImagesInFolder(Options.FilePath, Options.IncludeSubFolders);

                    foreach (var imageFileItem in _fileList)
                    {
                        DateTime? newShootDate = null;
                        if (Options.ShootDate == null)
                        {
                            newShootDate = GetNearestShootData(_fileList, imageFileItem.FilePath);
                        }
                        else
                        {
                            newShootDate = Options.ShootDate;
                        }
                        if (newShootDate != null)
                        {
                            if (imageFileItem.OrigShootDate != null && !Options.Overwrite)
                            {
                                imageFileItem.ReplaceShootData = false;
                            }
                            else
                            {
                                imageFileItem.ReplaceShootData = true;
                            }
                            imageFileItem.newShootDate = newShootDate;
                        }
                        else
                        {
                            Error.WriteLine(
                                "Error: No new Shoot Date (-s) was given and no other images in the folder contained a valid Shot Taken Date to use. You must specify a date.");
                            Console.CursorVisible = true;
                            Environment.Exit(-1);
                        }
                    }
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine();
                Console.WriteLine(@"!! Error probably caused by last slash on a folder path. To resolve this issue add an extra '\' at the end. e.g. c:\temp\\");
                Console.CursorVisible = true;
#if DEBUG
                Console.ReadKey();
#endif
                Environment.Exit(-1);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("!!--------- Unhandled Exception ---------!!");
                Console.WriteLine(ex);
                Console.WriteLine("-------------------------------------------");
                Console.CursorVisible = true;
#if DEBUG
                Console.ReadKey();
#endif
                Environment.Exit(-1);
            }

            StringBuilder builder = new StringBuilder();
            foreach (var imageFileItem in _fileList.Where(f => f.ReplaceShootData))
            {
                builder.AppendLine(imageFileItem.FilePath);
            }
            Input = new StringReader(builder.ToString());
        }

        /// <summary>
        /// Finds the shoot date of the nearest image in the folder list. Nearest ascending, if not found then nearest descending 
        /// looping back and forth until found. If none found then null returned.
        /// </summary>
        /// <param name="fileList"></param>
        /// <param name="fileToUpdatePath"></param>
        /// <returns></returns>
        private DateTime? GetNearestShootData(List<ImageFileItem> fileList, string fileToUpdatePath)
        {
            var fileToUpdateIndex = fileList.FindIndex(f => f.FilePath == fileToUpdatePath);

            for (int i = 1; i < fileList.Count-1; i++)
            {
                DateTime? newShootDate = null;
                if ((i % 2) == 0) //even search after
                {
                    if((fileToUpdateIndex + i) < fileList.Count)
                        newShootDate = ImageHandler.GetShootDate(fileList[fileToUpdateIndex + i].FilePath);
                }
                else //odd seacrh before
                {
                    if((fileToUpdateIndex - i) > 0)
                        newShootDate = ImageHandler.GetShootDate(fileList[fileToUpdateIndex - i].FilePath);
                }
                if (newShootDate != null)
                {
                    return newShootDate.Value;
                }
            }
            return null;
        }

        protected override void ProcessLine(string line)
        {
            Output.Write($"Processing {Path.GetFileName(line)}...");
            //Get record new date
            var imageFileInfo = _fileList.FirstOrDefault(f => f.FilePath == line);

            if (imageFileInfo?.newShootDate != null)
            {
                var result = ImageHandler.UpdateImageDateTaken(line, imageFileInfo.newShootDate.Value, Options.Overwrite);
                if (result == 1)
                {
                    imageFileInfo.Processed = true;
                    Output.WriteLine($"{imageFileInfo.newShootDate.Value.ToString("yyyy-MMM-dd")} ...success");                   
                }
                else if (result == 2)
                {
                    Output.WriteLine($"Shoot date exists. Use -force.");
                }
            }
            else
            {
                Output.WriteLine($"not updated!");
            }
        }

        protected override void PostProcess()
        {
            Console.WriteLine();
            Console.WriteLine("===== Completed =====");
            Console.WriteLine($"Processed {_fileList.Count(f => f.Processed)} files.");
            Console.CursorVisible = true;
        }
    }
}