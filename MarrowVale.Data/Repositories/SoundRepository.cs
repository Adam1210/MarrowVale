using MarrowVale.Common.Contracts;
using MarrowVale.Common.Models;
using MarrowVale.Data.Contracts;
using Microsoft.Extensions.Logging;
using NAudio.Wave;
using System.IO;

namespace MarrowVale.Data.Repositories
{
    public class SoundRepository : ISoundRepository
    {
        private readonly ILogger _logger;
        private readonly IAppSettingsProvider _appSettingsProvider;
        public SoundRepository(ILoggerFactory logger, IAppSettingsProvider appSettingsProvider)
        {
            _logger = logger.CreateLogger<SoundRepository>();
            _appSettingsProvider = appSettingsProvider;
        }

        public Audio GetMusic(string fileName)
        {
            var file = Path.Combine(_appSettingsProvider.SoundFilesLocation, fileName);
            var audioFile = new AudioFileReader(file);
            var outputDevice = new WaveOutEvent();
            
            outputDevice.Init(audioFile);
            outputDevice.Play();

            var audio = new Audio()
            {
                Reader = audioFile,
                Output = outputDevice
            };

            return audio;       
        }

        public Audio GetMusicLooping(string fileName)
        {
            var file = Path.Combine(_appSettingsProvider.SoundFilesLocation, fileName);
            
            var audioFile = new AudioFileReader(file);
            var loop = new LoopStream(audioFile);
            var outputDevice = new WaveOutEvent();

            outputDevice.Init(loop);
            outputDevice.Play();

            var audio = new Audio()
            {
                Reader = audioFile,
                Output = outputDevice
            };

            return audio;
        }

        public void DisposeMusic(Audio audio)
        {
            audio.Output.Stop();
            audio.Output.Dispose();
            audio.Reader.Dispose();
        }
    }
}
