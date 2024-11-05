using MarrowVale.Common.Models;

namespace MarrowVale.Data.Contracts
{
    public interface ISoundRepository
    {
        void PlayMusic(string fileName);
        void PlayMusicLooping(string fileName);
        void DisposeMusic();
    }
}
