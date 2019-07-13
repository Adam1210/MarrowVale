using MarrowVale.Business.Contracts;
using MarrowVale.Common.Contracts;
using System;
using System.Threading;

namespace MarrowVale.Business.Services
{
    public class PrintService : IPrintService
    {
        private readonly IGlobalItemsProvider _globalItemsProvider;
        public PrintService(IGlobalItemsProvider globalItemsProvider)
        {
            _globalItemsProvider = globalItemsProvider;
        }

        public void Print(string line)
        {
            Console.WriteLine($"{Environment.NewLine}{line}{Environment.NewLine}");
        }

        public void PrintCentered(string line)
        {
            #if Windows
            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            #endif
            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
            Console.WriteLine(line);
        }

        public void Type(string line)
        {
            Console.WriteLine();

            foreach (var letter in line)
            {
                Console.Write(letter);
                Thread.Sleep(50);
            }

            Console.WriteLine();
        }

        public void TypeCentered(string line)
        {
            #if Windows
            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            #endif
            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);

            foreach (var letter in line)
            {
                Console.Write(letter);
                Thread.Sleep(50);
            }

            Console.WriteLine();
        }

        public void Print(string line, int seconds)
        {
            Thread.Sleep(seconds * 1000);

            Console.WriteLine($"{Environment.NewLine}{line}{Environment.NewLine}");
        }

        public void PrintCentered(string line, int seconds)
        {
            Thread.Sleep(seconds * 1000);

            #if Windows
            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            #endif
            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
            Console.WriteLine(line);
        }

        public void Type(string line, int seconds)
        {
            Thread.Sleep(seconds * 1000);

            Console.WriteLine();

            foreach (var letter in line)
            {
                Console.Write(letter);
                Thread.Sleep(100);
            }

            Console.WriteLine();
        }

        public void TypeCentered(string line, int seconds)
        {
            Thread.Sleep(seconds * 1000);

            #if Windows
            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            #endif
            
            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);

            foreach (var letter in line)
            {
                Console.Write(letter);
                Thread.Sleep(100);
            }

            Console.WriteLine();
        }

        public string ReadInput()
        {
            Console.Write(">");
            return Console.ReadLine();
        }

        public void ClearConsole()
        {
            Console.Clear();
        }
    }
}
