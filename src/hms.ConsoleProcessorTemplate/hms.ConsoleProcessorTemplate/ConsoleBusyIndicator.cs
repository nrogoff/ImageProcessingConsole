using System;

namespace hms.ConsoleProcessorTemplate
{
    /// <summary>
    /// Progresses a spinning busy symbol
    /// </summary>
     public class ConsoleBusyIndicator
    {
        private int _currentBusySymbol;
        public char[] BusySymbols { get; set; }

        public ConsoleBusyIndicator()
        {
            BusySymbols = new[] {'|', '/', '-', '\\'};
            //BusySymbols = new[] {'^', '>', 'v', '<'};
        }

        public void UpdateProgress(string comment = "")
        {
            //Store the current curse position
            var originalX = Console.CursorLeft;
            var originalY = Console.CursorTop;

            ClearCurrentConsoleLine(originalX, originalY);

            // Write the next spinner animation symbol
            Console.Write(BusySymbols[_currentBusySymbol]);

            // Loop around all the animation frames
            _currentBusySymbol++;
            if (_currentBusySymbol == BusySymbols.Length)
            {
                //restart animation
                _currentBusySymbol = 0;
            }

            if(!string.IsNullOrEmpty(comment))
                Console.Write($" scanning for Shot Dates... {comment}");

            //Restore cursor to original position
            Console.SetCursorPosition(originalX, originalY);
        }

        public static void ClearCurrentConsoleLine(int originalX, int originalY)
        {
            Console.SetCursorPosition(originalX, originalY);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(originalX, originalY);
        }
    }
}