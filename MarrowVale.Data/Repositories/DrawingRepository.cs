using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Data.Repositories
{
    public class DrawingRepository : IDrawingRepository
    {
        private readonly IGlobalItemsProvider _globalItemsProvider;
        public DrawingRepository(IGlobalItemsProvider globalItemsProvider)
        {
            _globalItemsProvider = globalItemsProvider;
        }

        public string[] GetTitleArt()
        {
            var title = new[]
            {
                @"                                                  _                                                          ",
                @"  /\/\   __ _ _ __ _ __ _____      __ /\   /\__ _| | ___                                                     ",
                @" /    \ / _` | '__| '__/ _ \ \ /\ / / \ \ / / _` | |/ _ \                                                    ",
                @"/ /\/\ \ (_| | |  | | | (_) \ V  V /   \ V / (_| | |  __/                                                    ",
                @"\/    \/\__,_|_|  |_|  \___/ \_/\_/     \_/ \__,_|_|\___|                                                    ",
                @"                                                                                                             ",
                @"                              __            _                    __      ___                                 ",
                @"                             /__\ ___  __ _| |_ __ ___     ___  / _|    /   \_ __ __ _  __ _  ___  _ __  ___ ",
                @"                            / \/// _ \/ _` | | '_ ` _ \   / _ \| |_    / /\ / '__/ _` |/ _` |/ _ \| '_ \/ __|",
                @"                           / _  \  __/ (_| | | | | | | | | (_) |  _|  / /_//| | | (_| | (_| | (_) | | | \__ \",
                @"                           \/ \_/\___|\__,_|_|_| |_| |_|  \___/|_|   /___,' |_|  \__,_|\__, |\___/|_| |_|___/",
                @"                                                                                       |___/                 "
            };

            return title;
        }

        public void PrintArtCentered(string[] art)
        {
            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            Console.WriteLine("\n");
            foreach (string line in art)
            {
                Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
                Console.WriteLine(line);
            }

            Console.WriteLine("\n\n");
        }

        public void PrintArt(string[] art)
        {
            Console.WindowWidth = _globalItemsProvider.WindowWidth;
            Console.WriteLine("\n");
            foreach (string line in art)
            {
                Console.SetCursorPosition((Console.WindowWidth - line.Length) / 2, Console.CursorTop);
                Console.WriteLine(line);
            }

            Console.WriteLine("\n\n");
        }
    }
}
