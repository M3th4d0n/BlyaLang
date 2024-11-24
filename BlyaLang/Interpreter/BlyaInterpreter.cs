using System;
using System.Collections.Generic;
using System.IO;

namespace BlyaLang.Interpreter
{
    public class BlyaInterpreter
    {
        private static Dictionary<string, int> variables = new Dictionary<string, int>();
        
        private static Dictionary<string, List<string>> functions = new Dictionary<string, List<string>>();

        public void Run(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            string currentFunction = null;
            List<string> currentFunctionBody = null;

            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("//"))
                {
                    continue;
                }

               
                if (line.StartsWith("ФУНКЦИЯ"))
                {
                    currentFunction = line.Replace("ФУНКЦИЯ", "").Trim();
                    currentFunctionBody = new List<string>();

                }
                else if (line.StartsWith("КОНЕЦ") && currentFunction != null)
                {
                    functions[currentFunction] = currentFunctionBody; 

                    currentFunction = null;
                    currentFunctionBody = null;
                }

                else if (currentFunction != null)
                {
                    currentFunctionBody.Add(line.Trim());
                }

                else if (line.Contains("(") && line.Contains(")"))
                {
                    var functionName = line.Split('(')[0].Trim() + "()"; 
                    ExecuteFunction(functionName);
                }

                else if (line.StartsWith("ПИЗДАНИ"))
                {
                    ExecuteSpeak(line);
                }
                else
                {
                    Console.WriteLine($"[WARNING] Unknown command: {line}");
                }
            }
        }


        private static void ExecuteSpeak(string line)
        {
            string text = line.Replace("ПИЗДАНИ", "").Trim(' ', '"');
            Console.WriteLine(text);
        }

        private static void ExecuteFunction(string functionName)
        {
            if (functions.ContainsKey(functionName))
            {
                var functionBody = functions[functionName];
                foreach (var line in functionBody)
                {
                    if (line.StartsWith("ПИЗДАНИ"))
                    {
                        ExecuteSpeak(line);
                    }
                    else
                    {
                        Console.WriteLine($"[WARNING] Unknown command in function: {line}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"[ERROR] Function '{functionName}' not found.");
            }
        }
    }
}
