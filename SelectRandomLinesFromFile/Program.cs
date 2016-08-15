using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SelectRandomLinesFromFile
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("File Path as first argument is required, Number of random lines as Args 2 is required");
                return;
            }
            try
            {
                List<int> selectedLinesByNumbers = new List<int>();
                List<string> selectedLines = new List<string>();
                string fileName = Path.GetFileName(args[0]);

                var filePath = args[0];
                var file = File.ReadLines(filePath).ToList();
                int count = file.Count();
                int numOfLines = Int32.Parse(args[1]);
                for (int i = 1; i <= numOfLines || selectedLines.Count < numOfLines; i++  )
                {
                    int skip = RandomNumber(0, count);
                    if (!selectedLinesByNumbers.Contains(skip))
                    {
                        selectedLinesByNumbers.Add(skip);
                        string line = file.Skip(skip).First();
                        selectedLines.Add(line);
                        Console.WriteLine("#" + i + " " + line.Substring(0, 100) + "... - " + " Selected");
                    }                    
                }

                System.IO.File.WriteAllLines("results_" + fileName + "_" +args[1] +".txt", selectedLines);
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(ex);
            }
        }

        private static readonly Random random = new Random();
        private static readonly object syncLock = new object();
        public static int RandomNumber(int min, int max)
        {
            lock (syncLock)
            { // synchronize
                return random.Next(min, max);
            }
        }
    }
}
