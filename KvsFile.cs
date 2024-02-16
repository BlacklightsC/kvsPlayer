using NVorbis;

namespace kvsPlayer
{
    internal class KvsFile : IDisposable
    {
        private bool disposedValue;

        public KvsFile(string FilePath)
        {
            int i;
            using var kvs = File.OpenRead(FilePath);
            byte[] buf = new byte[16];
            kvs.Read(buf, 0, 16);
            if (buf[0] != 'K' || buf[1] != 'O' || buf[2] != 'V' || buf[3] != 'S') throw new InvalidDataException("Magic number must be KOVS");
            int ogg_size = BitConverter.ToInt32(buf, 4);
            if (ogg_size < 0xFF) throw new InvalidDataException("ogg_size must be bigger then 255bytes");
            double loop_point = BitConverter.ToInt32(buf, 8);
            kvs.Seek(0x20, SeekOrigin.Begin);
            OggFile = new MemoryStream(ogg_size);
            for (i = 0; i <= 0xFF; i++)
                OggFile.WriteByte((byte)(kvs.ReadByte() ^ i));
            while ((i = kvs.Read(buf, 0, 16)) > 0)
                OggFile.Write(buf, 0, i);
            using var vr = new VorbisReader(OggFile, false);
            Looptime = TimeSpan.FromSeconds(loop_point / vr.SampleRate);
        }
        public TimeSpan Looptime { get; set; }

        public MemoryStream OggFile { get; private set; }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                    OggFile.Close();
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
