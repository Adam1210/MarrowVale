using MarrowVale.Business.Entities.Enums;

namespace MarrowVale.Data.Contracts
{
    public interface IDrawingRepository
    {
        string[] GetTitleArt();
        string[] GetLoadSaveArt();
        string[] GetCharacterCreationStateArt(PlayerCreationStateEnum playerCreationState);
    }
}
