using System;
using CommandLine;
using CommandLine.Text;

namespace hms.ImageProcessingConsole
{
    /// <summary>
    /// Params parsing class
    /// </summary>
    public class RunOptions
    {
        [Option('f',"filepath",HelpText = "The full path of the image file, including the filename and extension (jpg only). Or the folder to search and fix all 'Date Taken' metadata in JPGs only. NOTE: You need to add an extra '\\' at the end when specifying a folder.", Required = true)]
        public string FilePath { get; set; }

        [Option('i',"includeSubfolders", HelpText = "Include subfolders. Use in combination with folder searches.")]
        public bool IncludeSubFolders { get; set; }

        [Option('s',"shottakendate",HelpText = "Set to this Shot Taken Date. Date must be formated as follows 'yyyy-mm-dd hh:mm:ss'. If this date is not specified then the 'Date Taken' (if exists) of the nearest file in the same folder (ordered by name) will be used.")]
        public DateTime? ShotDate { get; set; }

        [Option('o', "overwrite", HelpText = "Force overwrite of 'Date Taken' even if one already exists for the file.")]
        public bool Overwrite { get; set; }

        //ensures that errors are output when params don't parse
        [ParserState]
        public IParserState ParserState { get; set; }

        [HelpOption]
        public string DisplayHelp()
        {
            //====== To customize help output we can do it manually like this
            //var help = new HelpText
            //{
            //    Copyright = new CopyrightInfo("Nicholas Rogoff",2016),
            //    Heading = "Image shoot date fixer",
            //    AddDashesToOption = true,
            //    AdditionalNewLineAfterOption = true
            //};
            //help.AddOptions(this);
            //return help;

            // ======= or if using the assemply attributes, but this is the default even if not specified.
            return HelpText.AutoBuild(this);
        }

    }
}