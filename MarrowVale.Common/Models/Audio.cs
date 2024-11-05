using NAudio.Wave;

namespace MarrowVale.Common.Models
{
    public class Audio
    {
        public AudioFileReader Reader { get; set; }
        public WaveOutEvent Output { get; set; }
    }
}
