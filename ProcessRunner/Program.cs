using System.Diagnostics;

namespace ProcessRunner
{
    internal class Program
    {
        const string W_path = @"C:\Users\Карина\source\repos\ProcessRunner\WorkerProcess\bin\Debug\net10.0\WorkerProcess.exe";

        static void Main(string[] args)
        {
            while (true)
                ShowMenu();
        }

        static void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("-------MENU--------");
            Console.WriteLine("1) Copy file");
            Console.WriteLine("2) Shifr file");
            Console.Write("Your choice: ");

            string choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1": 
                    Copy(); 
                    break;
                case "2": 
                    Shifr(); 
                    break;
                default:
                    Console.WriteLine("Wrong choice");
                    Console.ReadLine();
                    break;
            }
        }

        static void Copy()
        {
            Console.WriteLine("Який шлях до оригіналу?");
            string pathtoOrigin = Console.ReadLine()!;

            Console.WriteLine("Куди потрібно скопіювати файл?");
            string destination = Console.ReadLine()!;

            RunWork("Copy file", "copy", pathtoOrigin, destination);
        }

        static void Shifr()
        {
            Console.WriteLine("Який шлях до файлу, який ви хочете зашифрувати?");
            string destWhichShifr = Console.ReadLine()!;

            Console.WriteLine("Шлях до таблиці шифру:");
            string tableS = Console.ReadLine()!;

            Console.WriteLine("Шлях де створити зашифрований файл ");
            string pathWhereShifrFile = Console.ReadLine()!;

            if (string.IsNullOrEmpty(pathWhereShifrFile))
                RunWork("Shifr file", "encrypt", destWhichShifr, tableS);
            else
                RunWork("Shifr file", "encrypt", destWhichShifr, tableS, pathWhereShifrFile);
        }

        static void RunWork(string commandN, params string[] workArgs)
        {
            Console.WriteLine($"Очікуємо виконання команди: {commandN}");

            string arguments = "";
            for (int i = 0; i < workArgs.Length; i++)
            {
                if (i > 0) 
                    arguments = arguments + " ";
                arguments += "\"" + workArgs[i] + "\"";
            }

            var startInfo = new ProcessStartInfo
            {
                FileName = W_path,
                Arguments = arguments,
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true
            };

            using var process = Process.Start(startInfo)!;
            string output = process.StandardOutput.ReadToEnd();
            string errors = process.StandardError.ReadToEnd();
            process.WaitForExit();

            Console.Clear();

            if (!string.IsNullOrEmpty(errors))
                Console.WriteLine($"ПОМИЛКА: {errors}");
            else
                Console.WriteLine($"{commandN} виконано. Результат: {output}");

            Console.ReadLine();
        }
    }
}