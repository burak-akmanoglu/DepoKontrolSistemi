using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Depo_Kontrol_Sistemi
{
    public partial class FormSatisList : Form
    {
        SqlConnection db = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Depo;Integrated Security=True");
        DataSet ds = new DataSet();
        private void SatisListele()
        {
            db.Open();
            SqlDataAdapter adr = new SqlDataAdapter("Select * from Satis", db);
            adr.Fill(ds, "Satis");
            dataGridView1.DataSource = ds.Tables["Satis"];
           
            db.Close();

        }
        public FormSatisList()
        {
            InitializeComponent();
        }

        private void FormSatisList_Load(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            // TODO: This line of code loads data into the 'satiskayit.Satis' table. You can move, or remove it, as needed.
            dataGridView1.Columns[0].Visible = false;
            SatisListele();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
        }
        int MouseX, MouseY;
        private void panel2_MouseDown(object sender, MouseEventArgs e)
        {
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            MouseX = MousePosition.X - this.Left;
            MouseY = MousePosition.Y - this.Top;

            timer1.Enabled = true;
        }

        private void panel2_MouseUp(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Left = MousePosition.X - MouseX;
            this.Top = MousePosition.Y - MouseY;
        }
    }
}
