namespace kvsPlayer
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            volSlider = new NAudio.Gui.VolumeSlider();
            filePathView = new Label();
            label1 = new Label();
            LoopState = new Label();
            trackBar1 = new TrackBar();
            timer1 = new System.Windows.Forms.Timer(components);
            ((System.ComponentModel.ISupportInitialize)trackBar1).BeginInit();
            SuspendLayout();
            // 
            // volSlider
            // 
            volSlider.Location = new Point(12, 12);
            volSlider.Name = "volSlider";
            volSlider.Size = new Size(96, 16);
            volSlider.TabIndex = 1;
            volSlider.Volume = 0.158489332F;
            volSlider.VolumeChanged += volSlider_VolumeChanged;
            // 
            // filePathView
            // 
            filePathView.AutoSize = true;
            filePathView.Location = new Point(11, 41);
            filePathView.Name = "filePathView";
            filePathView.Size = new Size(0, 15);
            filePathView.TabIndex = 2;
            // 
            // label1
            // 
            label1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            label1.Location = new Point(128, 41);
            label1.Name = "label1";
            label1.Size = new Size(224, 15);
            label1.TabIndex = 3;
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // LoopState
            // 
            LoopState.Location = new Point(124, 12);
            LoopState.Name = "LoopState";
            LoopState.Size = new Size(228, 15);
            LoopState.TabIndex = 4;
            LoopState.TextAlign = ContentAlignment.MiddleRight;
            // 
            // trackBar1
            // 
            trackBar1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            trackBar1.LargeChange = 60;
            trackBar1.Location = new Point(11, 73);
            trackBar1.Maximum = 60;
            trackBar1.Name = "trackBar1";
            trackBar1.Size = new Size(341, 45);
            trackBar1.SmallChange = 5;
            trackBar1.TabIndex = 5;
            trackBar1.TickFrequency = 10;
            trackBar1.TickStyle = TickStyle.Both;
            trackBar1.ValueChanged += trackBar1_ValueChanged;
            trackBar1.MouseDown += trackBar1_MouseDown;
            trackBar1.MouseUp += trackBar1_MouseUp;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // Form1
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(364, 130);
            Controls.Add(trackBar1);
            Controls.Add(LoopState);
            Controls.Add(label1);
            Controls.Add(filePathView);
            Controls.Add(volSlider);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "Form1";
            Text = "kvsPlayer";
            DragDrop += Form1_DragDrop;
            DragEnter += Form1_DragEnter;
            ((System.ComponentModel.ISupportInitialize)trackBar1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private NAudio.Gui.VolumeMeter volumeMeter1;
        private NAudio.Gui.VolumeSlider volSlider;
        private NAudio.Gui.PanSlider panSlider1;
        private Label filePathView;
        private Label label1;
        private Label LoopState;
        private TrackBar trackBar1;
        private System.Windows.Forms.Timer timer1;
    }
}
