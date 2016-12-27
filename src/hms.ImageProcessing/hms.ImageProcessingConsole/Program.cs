using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CommandLine.Text;

namespace hms.ImageProcessingConsole
{
    class Program
    {
        static void Main(string[] args)
        {

            // For debug - Full command line
            // var fullCommandLineString = Environment.CommandLine;

            //Hide Cursor and get orig foreground color
            Console.Title = "HMS Image Processing Console App";
            Console.CursorVisible = false;
            var origForegroundColor = Console.ForegroundColor;

            Console.WriteLine("======= HMS Image Processing Console App =======");

            //instantiate processing code
            var processor = new ShootDateProcessor();
            processor.Process(args, Console.In, Console.Out,Console.Error);
            
            //Restore cursor visiblility and colors
            Console.ForegroundColor = origForegroundColor;
            Console.CursorVisible = true;
#if DEBUG
            Console.ReadKey();
#endif

        }
    }
}
