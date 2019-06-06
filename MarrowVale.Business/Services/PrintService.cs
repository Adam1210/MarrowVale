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
            Console.WriteLine($"\n{line}\n");
        }

        public void PrintCentered(string line)
        {
            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
            Console.WriteLine(line);
        }

        public void Type(string line)
        {
            foreach (var letter in line)
            {
                Console.Write(letter);
                Thread.Sleep(50);
            }

            Console.WriteLine();
        }

        public void TypeCentered(string line)
        {
            Console.WindowWidth = _globalItemsProvider.WindowWidth;
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

            Console.WriteLine($"\n{line}\n");
        }

        public void PrintCentered(string line, int seconds)
        {
            Thread.Sleep(seconds * 1000);

            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
            Console.WriteLine(line);
        }

        public void Type(string line, int seconds)
        {
            Thread.Sleep(seconds * 1000);

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

            Console.WindowWidth = _globalItemsProvider.WindowWidth;
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
            return Console.ReadLine();
        }

        public void ClearConsole()
        {
            Console.Clear();
        }
    }
}
