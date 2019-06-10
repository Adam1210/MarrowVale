using MarrowVale.Business.Entities.Enums;
using MarrowVale.Common.Contracts;
using MarrowVale.Data.Contracts;

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

        public string[] GetLoadSaveArt()
        {
            return new[]
            {
                 @"--------------------------",
                 @"Character Save Selection",
                 @"--------------------------"
            };
        }

        public string[] GetCharacterCreationStateArt(PlayerCreationStateEnum playerCreationState)
        {
            switch (playerCreationState)
            {
                case PlayerCreationStateEnum.Gender:
                    return new[]
                    {
                        @"--------------------------",
                        @"Character Gender Selection",
                        @"--------------------------"
                    };
                case PlayerCreationStateEnum.Race:
                    return new[]
                    {
                        @"------------------------",
                        @"Character Race Selection",
                        @"------------------------"
                    };
                case PlayerCreationStateEnum.Name:
                    return new[]
                    {
                        @"------------------------",
                        @"Character Name Selection",
                        @"------------------------"
                    };
                case PlayerCreationStateEnum.Class:
                    return new[]
                    {
                        @"-------------------------",
                        @"Character Class Selection",
                        @"-------------------------"
                    };
                default: return null;
            }
        }
    }
}
