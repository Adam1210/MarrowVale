using MarrowVale.Data.Models;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarrowVale.Data.Contracts
{
    public interface ISoundRepository
    {
        Audio GetMusic(string fileName);
        Audio GetMusicLooping(string fileName);
        void DisposeMusic(Audio audio);
    }
}
