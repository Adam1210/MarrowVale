using MarrowVale.Business.Entities.Entities;

namespace MarrowVale.Data.Contracts
{
    public interface IGameRepository
    {
        Game LoadGame(string fileName);
        void SaveGame(Game game, string oldFileName, string newFileName);
    }
}
