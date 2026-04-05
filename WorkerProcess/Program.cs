using System;
using System.IO;

namespace WorkerProcess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Немає команди");
                return;
            }

            string command = args[0];

            if (command == "copy")
            {
                CopyFile(args[1], args[2]);
            }
            else if (command == "encrypt")
            {
                string encryptOutput;
                if (args.Length > 3)
                    encryptOutput = args[3];
                else
                    encryptOutput = args[1];

                EncryptFile(args[1], args[2], encryptOutput);
            }
            else
            {
                Console.WriteLine("Невідома команда");
            }
        }

        static void CopyFile(string pathtoOrigin, string destination)
        {
            File.Copy(pathtoOrigin, destination, true);
            Console.WriteLine("OK");
        }

        static void LoadCipherTable(string tablePath, out char[] keys, out char[] values)
        {
            string[] lines = File.ReadAllLines(tablePath);

            int count = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length < 3) continue;        
                if (lines[i][1] != ':') continue;         
                count++;
            }

            keys = new char[count];
            values = new char[count];

            int idx = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length < 3) continue;
                if (lines[i][1] != ':') continue;

                keys[idx] = lines[i][0]; 
                values[idx] = lines[i][2]; 
                idx++;
            }
        }

        static int FindIndex(char[] arr, char c)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == c)
                {
                    return i; 
                }
                else
                {
                    continue; 
                }
            }
            return -1; 
        }

        static void EncryptFile(string inputPath, string tablePath, string outputPath)
        {
            LoadCipherTable(tablePath, out char[] keys, out char[] values);

            string text = File.ReadAllText(inputPath); 
            string result = "";                         

            foreach (char c in text) 
            {
                int idx = FindIndex(keys, c); 

                if (idx >= 0)
                {
                    result = result + values[idx];
                }
                else
                {
                    result = result + c; 
                }
            }

            File.WriteAllText(outputPath, result); 
            Console.WriteLine("Все запрацювало");
        }
    }
}