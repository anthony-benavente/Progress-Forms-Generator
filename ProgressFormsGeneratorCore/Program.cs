using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProgressFormsGeneratorCore.Parser;
using ProgressFormsGeneratorCore.Parser.Writers;
using System.IO;

namespace ProgressFormsGeneratorCore
{
    public class Program
    {
        /// <summary>
        /// This is the index in the args array where we can find the in file argument
        /// </summary>
        private static readonly int ARG_IN_FILE = 0;
        
        /// <summary>
        /// This is the index in the arg array where we can find the out file argument
        /// </summary>
        private static readonly int ARG_OUT_FILE = 1;

        public static void Main(string[] args)
        {
            string inFile = @"C:\Users\Anthony\Documents\beyondacademics.json";
            string outFile = "test.txt";

            if (args.Length < 2)
            {
                // We weren't given enough args. Show the usage message.
                usage();
            }
            else
            {
                inFile = args[ARG_IN_FILE];
                outFile = args[ARG_OUT_FILE];

                new FileInfo(outFile).;
            }

            HtmlWriter writer = new HtmlWriter(outFile);
            ProgressFormsParser parser = new ProgressFormsParser(writer);
            parser.Parse(inFile);
        }

        private static void usage()
        {
            Console.WriteLine("usage: ProgressFormsDocumentParser <in_file> <out_file>");
        }
    }
}
