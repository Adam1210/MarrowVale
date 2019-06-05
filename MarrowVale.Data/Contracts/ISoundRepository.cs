using MarrowVale.Common.Models;

namespace MarrowVale.Data.Contracts
{
    public interface ISoundRepository
    {
        Audio GetMusic(string fileName);
        Audio GetMusicLooping(string fileName);
        void DisposeMusic(Audio audio);
    }
}
