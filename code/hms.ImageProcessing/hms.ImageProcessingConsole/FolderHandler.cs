// Copyright (c) 2016 Nicholas Rogoff  
// 
// Author: Nicholas Rogoff
// Created: 2016 - 12 - 26
// 
// Project: hms.ImageProcessingConsole
// Filename: FolderHandler.cs

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace hms.ImageProcessingConsole
{
    public class FolderHandler
    {

        public Action<string> ProgressUpdateAction { get; set; }

        /// <summary>
        /// Get all the images and return a fileitems list
        /// </summary>
        /// <param name="folderpath"></param>
        /// <param name="includeSubFolders">Whether to include sub folders in the search</param>
        /// <returns></returns>
        public List<ImageFileItem> GetAllImagesInFolder(string folderpath, bool includeSubFolders =false)
        {
            List<ImageFileItem> fileItems = new List<ImageFileItem>();

            string[] fileToEnumerate;
            if (!includeSubFolders)
            {
                fileToEnumerate = Directory.GetFiles(Path.GetDirectoryName(folderpath));
            }
            else
            {
                fileToEnumerate = Directory.GetFiles(Path.GetDirectoryName(folderpath), "*.*", SearchOption.AllDirectories);
            }

            foreach (var file in fileToEnumerate)
                {
                if (Path.GetExtension(file).ToLower() == ".jpg")
                {
                    ProgressUpdateAction?.Invoke(Path.GetFileName(file));

                    fileItems.Add(new ImageFileItem
                    {
                        Folder = Path.GetDirectoryName(file),
                        FilePath = file,
                        OrigShootDate = ImageHandler.GetShootDate(file),
                        Processed = false
                    });
                }
            }
            return fileItems;
        }
    }
}