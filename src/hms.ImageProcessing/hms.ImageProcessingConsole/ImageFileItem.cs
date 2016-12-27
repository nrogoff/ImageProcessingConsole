using System;

namespace hms.ImageProcessingConsole
{
    /// <summary>
    /// Holds details about the image files to process
    /// </summary>
    public class ImageFileItem
    {
        public string Folder { get; set; }
        public string FilePath { get; set; }
        public DateTime? OrigShootDate { get; set; }
        public bool ReplaceShootData { get; set; }
        public DateTime? newShootDate { get; set; }
        public bool Processed { get; set; }
        public string Error { get; set; }

    }
}