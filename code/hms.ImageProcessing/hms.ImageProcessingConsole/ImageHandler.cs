// Copyright (c) 2016 Nicholas Rogoff  
// 
// Author: Nicholas Rogoff
// Created: 2016 - 12 - 26
// 
// Project: hms.ImageProcessingConsole
// Filename: ImageHandler.cs

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;

namespace hms.ImageProcessingConsole
{
    public static class ImageHandler
    {
        public static DateTime GetAllMetaDate(string imagePath)
        {
            IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(imagePath);
            foreach (var directory in directories)
                foreach (var tag in directory.Tags)
                    Console.WriteLine($"{directory.Name} - {tag.Name} = {tag.Description}");
            return DateTime.Now;
        }

        /// <summary>
        /// Find the shoot data in an image file
        /// </summary>
        /// <param name="imagePath">The local file path to the image file</param>
        /// <returns>The date or null if no date found.</returns>
        public static DateTime? GetShootDate(string imagePath)
        {
            IEnumerable<Directory> directories = ImageMetadataReader.ReadMetadata(imagePath);
            var subIfdDirectory = directories.OfType<ExifSubIfdDirectory>().FirstOrDefault();
            if (subIfdDirectory != null)
            {
                var origDateTimeString = subIfdDirectory?.GetDescription(ExifDirectoryBase.TagDateTimeOriginal);
                DateTime origDateTime;
                if (DateTime.TryParseExact(origDateTimeString, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out origDateTime))
                {
                    return origDateTime;
                }
            }
            return null;
        }

        /// <summary>
        /// Updates the Date Taken / Shoot Date value of the images metadata
        /// </summary>
        /// <param name="imagePath"></param>
        /// <param name="newShootDate"></param>
        /// <param name="overwrite">Does not update if existing Shot TAken Date exists unless this is true</param>
        /// <returns></returns>
        public static int UpdateImageDateTaken(string imagePath, DateTime newShootDate, bool overwrite = false)
        {
            DateTime? origShootDate = GetShootDate(imagePath);

            if (origShootDate != null && !overwrite)
            {
                // don't update
                return 2;
            }
            var image = new JpegMetadataAdapter(imagePath);
            image.Metadata.DateTaken = newShootDate.ToString("u");
            image.Save();
            image.Dispose(); //clear memory
            return 1;                
        }



    }
}