using NAudio.Wave;

namespace kvsPlayer
{
    public class LoopStream(WaveStream sourceStream) : WaveStream
    {
        WaveStream sourceStream = sourceStream;

        public bool EnableLooping { get; set; } = true;

        public TimeSpan LoopPoint { get; set; }

        public override WaveFormat WaveFormat => sourceStream.WaveFormat;

        public override long Length => sourceStream.Length;

        public override long Position
        {
            get => sourceStream.Position;
            set => sourceStream.Position = value;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            int totalBytesRead = 0;

            while (totalBytesRead < count)
            {
                int bytesRead = sourceStream.Read(buffer, offset + totalBytesRead, count - totalBytesRead);
                if (bytesRead == 0)
                {
                    if (sourceStream.Position == 0 || !EnableLooping) break;
                    CurrentTime = LoopPoint;
                }
                totalBytesRead += bytesRead;
            }
            return totalBytesRead;
        }
    }
}
