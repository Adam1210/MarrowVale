using MarrowVale.Business.Contracts;
using MarrowVale.Business.Entities.Entities;
using MarrowVale.Common.Contracts;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MarrowVale.Business.Services
{
    public class PrintService : IPrintService
    {
        private readonly IGlobalItemsProvider _globalItemsProvider;
        private readonly IAppSettingsProvider _appSettingsProvider;
        private readonly int _typeSpeed;

        public PrintService(IGlobalItemsProvider globalItemsProvider, IAppSettingsProvider appSettingsProvider)
        {
            _globalItemsProvider = globalItemsProvider;
            _appSettingsProvider = appSettingsProvider;
            _typeSpeed = _appSettingsProvider.TypeSpeed;
        }

        public void PrintParagraph(string text)
        {
            var justifiedText = justifyContent(text, 120);
            Console.WriteLine($"{Environment.NewLine}{justifiedText}");
        }

        public void PrintCentered(string line)
        {
            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
            Console.WriteLine(line);
        }

        public void Type(string content, int milliSeconds = 0, bool onNewLine = false)
        {
            Thread.Sleep(milliSeconds);
            content = justifyContent(content, 120);

            if (onNewLine)
                Console.WriteLine();

            foreach (var letter in content)
            {
                Console.Write(letter);
                Thread.Sleep(_typeSpeed);
            }

            Console.WriteLine();
        }

        public void Print(string line, int milliSeconds = 0)
        {
            Thread.Sleep(milliSeconds);
            Console.WriteLine($"{Environment.NewLine}{line}{Environment.NewLine}");
        }

        public void PrintCentered(string line, int milliSeconds = 0)
        {
            Thread.Sleep(milliSeconds);

            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
            Console.WriteLine(line);
        }

        public void TypeCentered(string line, int milliSeconds = 0)
        {
            Thread.Sleep(milliSeconds);

            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);

            foreach (var letter in line)
            {
                Console.Write(letter);
                Thread.Sleep(_typeSpeed);
            }

            Console.WriteLine();
        }

        public string ReadInput()
        {
            Console.Write(">");
            //TODO Add Sanitation Logic
            return Console.ReadLine();
        }

        public void ClearConsole()
        {
            Console.Clear();
        }

        public string ReadDialogue(Player player)
        {
            Console.Write($"{player.Name}: ");
            //TODO Add Sanitation Logic
            return Console.ReadLine().Replace("\n", "").Replace("\r", "");
        }

        public void PrintDialogueResponse(Npc npc, string response)
        {
            Console.Write($"{npc.FullName}: ");
            Type(response, 0);
        }

        public void PrintDivineText(string text)
        {
            setDivineConsoleColors();
            Type(text, 250);
            resetConsoleColors();

        }

        private void setDivineConsoleColors()
        {
            //Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Red;
        }

        private void resetConsoleColors()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Green;
        }


        private string justifyContent(string content, int width)
        {
            var words = content.Split(" ");
            var wordCount = words.Length;

            var costArray = getCostArray(width, words);
            var optimalSplit = getOptimalSplit(wordCount, costArray);
            return applyJustification(words, wordCount, optimalSplit);
        }

        private string applyJustification(string[] words, int wordCount, int?[] optimalSplit)
        {
            var osIndex = 0;
            var justifiedContent = new StringBuilder();

            for (int i = 0; i < wordCount; i++)
            {
                if (optimalSplit[osIndex] == i)
                {
                    justifiedContent.AppendLine();
                    osIndex = i;
                }
                justifiedContent.Append(words[i] + " ");
            }
            return justifiedContent.ToString();
        }

        private int?[] getOptimalSplit(int wordCount, int?[,] costArray)
        {
            var minCostArray = new int?[wordCount];
            var optimalSplit = new int?[wordCount];
            minCostArray[wordCount - 1] = costArray[wordCount - 1, wordCount - 1];
            optimalSplit[wordCount - 1] = wordCount;

            for (int i = wordCount - 1; i >= 0; i--)
            {
                var splitLine = false;
                for (int j = wordCount - 1; j > i; j--)
                {
                    int? costOfInstance;
                    if (!splitLine)
                    {
                        costOfInstance = costArray[i, j];
                        if (costOfInstance == -1)
                        {
                            splitLine = true;
                        }
                        else
                        {
                            minCostArray[i] = costOfInstance;
                            optimalSplit[i] = j + 1;
                            j = i;
                            continue;
                        }

                    }

                    costOfInstance = costArray[i, j - 1];
                    if (costOfInstance == -1)
                    {
                        splitLine = true;
                        continue;
                    }

                    var localCost = costOfInstance + minCostArray[j];
                    if (minCostArray[i] == null)
                    {
                        minCostArray[i] = localCost;
                        optimalSplit[i] = j;
                    }
                    else if (minCostArray[i] > localCost)
                    {
                        minCostArray[i] = localCost;
                        optimalSplit[i] = j;
                    }


                }

            }
            return optimalSplit;
        }

        private int?[,] getCostArray(int width, string[] words)
        {
            var costArray = new int?[words.Length, words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                var length = 0;
                var spaceBetweenWords = 0;

                for (int j = i; j < words.Length; j++)
                {
                    length += words[j].Length + spaceBetweenWords;
                    var cost = length <= width ? Math.Pow(width - length, 2) : -1;
                    costArray[i, j] = Convert.ToInt32(cost);
                    spaceBetweenWords = 1;
                }
            }
            return costArray;
        }
    }
}
