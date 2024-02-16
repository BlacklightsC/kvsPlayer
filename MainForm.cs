using System.Numerics;

using NAudio.Vorbis;
using NAudio.Wave;

namespace kvsPlayer
{
    public partial class MainForm : Form
    {
        private KvsFile kvs;
        private IWavePlayer player = new WaveOut(WaveCallbackInfo.FunctionCallback());
        private LoopStream reader;

        public MainForm()
        {
            InitializeComponent();
            player.Volume = 0.158489332f;
            player.PlaybackStopped += Player_PlaybackStopped;
        }

        private void Player_PlaybackStopped(object? sender, StoppedEventArgs e)
        {
            filePathView.Text = "";
            LoopState.Text = "";
            label1.Text = "";
            trackBar1.Value = 0;
            reader.Close();
            kvs.Dispose();
            timer1.Stop();
        }

        private void volSlider_VolumeChanged(object sender, EventArgs e)
        {
            player.Volume = volSlider.Volume;
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[]? files = e.Data?.GetData(DataFormats.FileDrop) as string[];
            if (files?.Length > 0 && File.Exists(files[0]))
            {
                try
                {
                    string path = files[0];
                    if (player.PlaybackState == PlaybackState.Playing)
                    {
                        timer1.Stop();
                        player.Stop();
                        reader.Close();
                        kvs.Dispose();
                    }
                    filePathView.Text = Path.GetFileNameWithoutExtension(path);
                    kvs = new KvsFile(path);
                    reader = new LoopStream(new VorbisWaveReader(kvs.OggFile)) { LoopPoint = kvs.Looptime };
                    if (kvs.Looptime.Ticks == 0)
                    {
                        reader.EnableLooping = false;
                        LoopState.Text = "No Loop";
                    }
                    else
                    {
                        LoopState.Text = $"{kvs.Looptime} ~ {reader.TotalTime}";
                    }
                    trackBar1.Maximum = (int)reader.TotalTime.TotalMilliseconds;
                    trackBar1.Value = 0;
                    player.Init(reader);
                    player.Play();
                    timer1.Start();
                    label1.Text = $"00:00:00 / {reader.TotalTime:hh\\:mm\\:ss}";
                }
                catch { }
            }
        }



        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data?.GetDataPresent(DataFormats.FileDrop) ?? false)
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        bool flag = false;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (player.PlaybackState == PlaybackState.Playing)
            {
                trackBar1.Value = (int)reader.CurrentTime.TotalMilliseconds;
                label1.Text = $"{reader.CurrentTime:hh\\:mm\\:ss} / {reader.TotalTime:hh\\:mm\\:ss}";
            }
        }

        private void trackBar1_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Stop();
            flag = true;
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            if (player.PlaybackState == PlaybackState.Playing && flag)
            {
                player.Pause();
                reader.CurrentTime = TimeSpan.FromMilliseconds(trackBar1.Value);
                label1.Text = $"{reader.CurrentTime:hh\\:mm\\:ss} / {reader.TotalTime:hh\\:mm\\:ss}";
                player.Play();
            }
        }

        private void trackBar1_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Start();
            flag = false;
        }
    }
}
