using MarrowVale.Data.Contracts;
using NAudio;
using NAudio.Wave;
using MarrowVale.Common.Contracts;
using Microsoft.Extensions.Logging;
using MarrowVale.Data.Models;


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
            var audioFile = new AudioFileReader(_appSettingsProvider.SoundFilesLocation + $"\\{fileName}");
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
            var audioFile = new AudioFileReader(_appSettingsProvider.SoundFilesLocation + $"\\{fileName}");
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
