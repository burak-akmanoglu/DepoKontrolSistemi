using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace Depo_Kontrol_Sistemi
{
    public partial class FrmBarkod : Form
    {
        public static string barkodnumara = "";
        FilterInfoCollection FIC;
        VideoCaptureDevice VCD;

        public FrmBarkod()
        {
            InitializeComponent();
        }

        private void FrmBarkod_Load(object sender, EventArgs e)
        {
            FIC = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo item in FIC)
            {
                comboBox1.Items.Add(item.Name);
                comboBox1.SelectedIndex = 0;
            }
            VCD = new VideoCaptureDevice(FIC[comboBox1.SelectedIndex].MonikerString);
            VCD.NewFrame += VCD_NewFrame;
            VCD.Start();
            timer1.Start();

        }


        private void btn_Camera_Click(object sender, EventArgs e)
        {
            VCD = new VideoCaptureDevice(FIC[comboBox1.SelectedIndex].MonikerString);
            VCD.NewFrame += VCD_NewFrame;
            VCD.Start();
            timer1.Start();

        }

        private void VCD_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            pictureBox1.Image = (Bitmap)eventArgs.Frame.Clone();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                BarcodeReader brd = new BarcodeReader();
                Result sonuc = brd.Decode((Bitmap)pictureBox1.Image);
                if (sonuc != null)
                {
                    richTextBox1.Text = sonuc.ToString();
                    timer1.Stop();
                    btn_Barkod_Click(sender,e);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            VCD.Start();
            timer1.Start();

            timer1.Stop();
            VCD.Stop();
        }

        private void btn_Barkod_Click(object sender, EventArgs e)
        {
            if (richTextBox1.Text != "")
            {
                barkodnumara = richTextBox1.Text;
                VCD.Start();
                timer1.Start();

                timer1.Stop();
                VCD.Stop();
                Zen.Barcode.Code128BarcodeDraw barcode = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                pictureBox1.Image = barcode.Draw(richTextBox1.Text, 550);
            }
            else
            {
                MessageBox.Show("Barkod alanı boş");
            }

        }
        int MouseX, MouseY;
        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            timer2.Enabled = false;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            this.Left = MousePosition.X - MouseX;
            this.Top = MousePosition.Y - MouseY;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {

            VCD.Start();
            timer1.Start();
            VCD.Stop();




            timer1.Stop();

            this.Close();
        }

        private void btn_max_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void btn_min_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }

        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            MouseX = MousePosition.X - this.Left;
            MouseY = MousePosition.Y - this.Top;

            timer2.Enabled = true;
        }
    }
}
