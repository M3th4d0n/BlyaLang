using System;
using BlyaLang.Interpreter;

namespace BlyaLang
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "";
            
            if (args.Length > 0 && args[0] == "-f" && args.Length > 1)
            {
                filePath = args[1];
            }
            else
            {
                Console.Write("Enter the .blya file name: ");
                filePath = Console.ReadLine();
            }
            
            if (!System.IO.File.Exists(filePath))
            {
                Console.WriteLine($"[ERROR] File '{filePath}' not found.");
                return;
            }

            Console.WriteLine($"[INFO] Processing file '{filePath}'...");


            var interpreter = new BlyaInterpreter();
            interpreter.Run(filePath);

        }
    }
}