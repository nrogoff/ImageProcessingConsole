// Copyright (c) 2016 Nicholas Rogoff  
// 
// Author: Nicholas Rogoff
// Created: 2016 - 12 - 26
// 
// Project: hms.ConsoleProcessorTemplate
// Filename: ConsoleProcessorTemplate.cs

using System;
using System.IO;
using CommandLine;

namespace hms.ConsoleProcessorTemplate
{
    public abstract class ConsoleProcessorTemplateBase<TOptions> where TOptions : new ()
    {

        protected TextWriter Error;
        protected TextReader Input;
        protected TextWriter Output;

        protected TOptions Options;

        protected ConsoleBusyIndicator BusyIndicator;
        
        /// <summary>
        /// The Template Method.
        /// 
        /// Defines the series of steps, some of which 
        /// may be overridden in derived classes.
        /// </summary>
        /// <param name="args">The command line arguments</param>
        /// <param name="input">The input stream that will be iterated over</param>
        /// <param name="output">The output stream</param>
        /// <param name="error">The error stream</param>
        public void Process(string[] args, TextReader input, TextWriter output, TextWriter error)
        {
            Error = error;
            Output = output;
            Input = input;

            BusyIndicator = new ConsoleBusyIndicator();

            ParseOptions(args);

            var isValidArguments = ValidateArguments();

            if (isValidArguments)
            {
                PreProcess();
                ProcessLines();
                PostProcess();
            }
        }

        private void ParseOptions(string[] args)
        {
            Options = new TOptions();

            Parser.Default.ParseArgumentsStrict(args, Options, OnFail);
        }

        /// <summary>
        /// Runs when argument validation fails
        /// </summary>
        private void OnFail()
        {
            Console.WriteLine("!! command arguements failed validation !!");
#if DEBUG
            Console.ReadKey();         
#endif
            Console.CursorVisible = true;
            Environment.Exit(-1);
        }

        private void ProcessLines()
        {
            var currentLine = Input.ReadLine();

            while (currentLine != null)
            {
                ProcessLine(currentLine);

                currentLine = Input.ReadLine();
            }
        }




        /// <summary>
        /// Override to perform additional argument validation
        /// and write validation error output to user.
        /// Defaults to true.
        /// </summary>
        /// <returns>t</returns>
        protected virtual bool ValidateArguments()
        {
            return true;
        }

        /// <summary>
        /// Override to perform one-time pre-processing
        /// that executes before the main processing loop.
        /// </summary>
        protected virtual void PreProcess()
        {
        }

        /// <summary>
        /// Override to perform processing on each line of input. 
        /// </summary>
        protected virtual void ProcessLine(string line)
        {
        }

        /// <summary>
        /// Override to perform one-time post-processing
        /// that executes after the main processing loop.
        /// </summary>
        protected virtual void PostProcess()
        {
        }
    }
}