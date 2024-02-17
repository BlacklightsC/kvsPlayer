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
            byte[] buf = new byte[32];
            kvs.Read(buf, 0, 32);
            if (BitConverter.ToInt32(buf, 0) != 0x53564F4B) throw new InvalidDataException("Magic number must be 'KOVS'");
            int ogg_size = BitConverter.ToInt32(buf, 4);
            if (ogg_size < 0xFF) throw new InvalidDataException("ogg_size must be bigger then 255bytes");
            double loop_point = BitConverter.ToInt32(buf, 8);
            Loopable = buf[0x10] == 0;
            kvs.Seek(0x20, SeekOrigin.Begin);
            OggFile = new MemoryStream(ogg_size);
            for (i = 0; i <= 0xFF; i++)
                OggFile.WriteByte((byte)(kvs.ReadByte() ^ i));
            int len = 0;
            while ((i = kvs.Read(buf, 0, 32)) > 0)
            {
                if (len + i < ogg_size)
                {
                    len += i;
                    OggFile.Write(buf, 0, i);
                }
                else
                {
                    OggFile.Write(buf, 0, ogg_size - len);
                    len = ogg_size;
                    break;
                }
            }
            using var vr = new VorbisReader(OggFile, false);
            Looptime = TimeSpan.FromSeconds(loop_point / vr.SampleRate);
        }
        public TimeSpan Looptime { get; set; }

        public bool Loopable { get; set; }

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
