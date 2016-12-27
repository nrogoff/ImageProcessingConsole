using System;
using CommandLine;

namespace hms.ImageProcessingConsole
{
    /// <summary>
    /// Params parsing class
    /// </summary>
    public class RunOptions
    {
        [Option('f',"filepath",HelpText = "The full path of the image file, including the filename and extension. Or the folder to search and fix all images in. JPGs and PNGs only", Required = true)]
        public string FilePath { get; set; }

        [Option('i',"includeSubfolders", HelpText = "If you specified a folder, then to include subfolders.")]
        public bool IncludeSubFolders { get; set; }

        [Option('s',"shootdate",HelpText = "Set to this Shoot Date. yyyy-mm-dd hh:mm:ss . If not specified then the shoot date of the nearest file in the same folder (ordered by name) that has one will be used.")]
        public DateTime? ShootDate { get; set; }

        [Option('o', "overwrite", HelpText = "Force shoot data overwrite even if one exists.")]
        public bool Overwrite { get; set; }

        //ensures that errors are output when params don't parse
        [ParserState]
        public IParserState ParserState { get; set; }

        //[HelpOption]
        //public string DisplayHelp()
        //{
        //    //====== To customize help output we can do it manually like this
        //    //var help = new HelpText
        //    //{
        //    //    Copyright = new CopyrightInfo("Nicholas Rogoff",2016),
        //    //    Heading = "Image shoot date fixer",
        //    //    AddDashesToOption = true,
        //    //    AdditionalNewLineAfterOption = true
        //    //};
        //    //help.AddOptions(this);
        //    //return help;

        //    // ======= or if using the assemply attributes, but this is the default even if not specified.
        //    //return HelpText.AutoBuild(this);
        //}

    }
}