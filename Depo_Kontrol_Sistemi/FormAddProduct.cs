using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Depo_Kontrol_Sistemi
{
    public partial class FormAddProduct : Form
    {

        public FormAddProduct()
        {
            InitializeComponent();
        }
        SqlConnection con = new SqlConnection("Data Source=.\\SQLEXPRESS;Initial Catalog=Depo;Integrated Security=True;MultipleActiveResultSets=True;");
        SqlDataReader dr;
        SqlDataReader cc;
        private void getCategori()
        {
            con.Open();
            SqlCommand kmt = new SqlCommand("Select * from Kategori", con);
            SqlDataReader read = kmt.ExecuteReader();
            while (read.Read())
            {
                cmbKategori.Items.Add(read["Kategoriler"].ToString());
            }
            con.Close();
        }
        bool durum;
        private void UrunKontrol()
        {
            durum = true;
            con.Open();
            SqlCommand komut = new SqlCommand("select * from  Urun", con);
            SqlDataReader rd = komut.ExecuteReader();
            while (rd.Read())
            {
                if (txtBarkod.Text == rd["Barkodno"].ToString() || txtBarkod.Text == "")
                {
                    durum = false;
                }

            }
            con.Close();
        }
        private void FormAddProduct_Load(object sender, EventArgs e)
        {
            getCategori();

        }

        private void cmbKategori_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMarka.Items.Clear();
            cmbMarka.Text = "";
            con.Open();
            SqlCommand kmt = new SqlCommand("Select * from Marka where Kategoriler='" + cmbKategori.SelectedItem + "'", con);
            SqlDataReader read = kmt.ExecuteReader();
            while (read.Read())
            {
                cmbMarka.Items.Add(read["Marka"].ToString());
            }
            con.Close();

        }
        bool depodurum;
        private void DolumUyarısı()
        {
            depodurum = true;
            con.Open();
            SqlCommand komut = new SqlCommand("select Sum(Miktari) as totalmiktar from Urun where Kategori = '" + cmbKategori.Text + "'", con);
            SqlCommand cmd = new SqlCommand("select KategoriDepoMax from Dolum Where KategoriAd='" + cmbKategori.Text + "'", con);
            SqlDataReader rd = komut.ExecuteReader();
            SqlDataReader dr = cmd.ExecuteReader();
            if (rd.Read()&& dr.Read())
            {
                int totalmiktar = Convert.ToInt32(rd["totalmiktar"]);
                int miktar = Convert.ToInt32(txtMiktar.Text);
                int maxmiktar = Convert.ToInt32(dr["KategoriDepoMax"]);
                if (totalmiktar + miktar > maxmiktar)
                {
                    depodurum = false;
                }

            }
            con.Close();
        }
        bool Depodurum2;
        private void DolumUyarısı2()
        {
            Depodurum2 = true;
            con.Open();
            SqlCommand komut = new SqlCommand("select Sum(Miktari) as totalmiktar2 from Urun where Kategori = '" + V_txtKategori.Text + "'", con);
            SqlCommand cmd = new SqlCommand("select KategoriDepoMax from Dolum Where KategoriAd='" + V_txtKategori.Text + "'", con);
            SqlDataReader rd = komut.ExecuteReader();
            SqlDataReader dr = cmd.ExecuteReader();
            if (rd.Read()&& dr.Read())
            {
                int totalmiktar2 = Convert.ToInt32(rd["totalmiktar2"]);
                int miktar2 = Convert.ToInt32(V_txtMiktar.Text);
                int maxmiktar = Convert.ToInt32(dr["KategoriDepoMax"]);
                if (totalmiktar2 + miktar2 > maxmiktar)
                {
                    Depodurum2 = false;
                }

            }
            con.Close();
        }
        private void btnNewProductAdd_Click(object sender, EventArgs e)
        {
            
            DolumUyarısı();
            if (depodurum == true)
            {

                UrunKontrol();
                if (durum == true)
                {

                    con.Open();
                    SqlCommand komut = new SqlCommand("insert into Urun(Barkodno,Kategori,Marka,Urun_Adi,Miktari,Alis_Fiyati,Satis_Fiyati,Tarih) Values(@Barkodno,@Kategori,@Marka,@Urun_Adi,@Miktari,@Alis_Fiyati,@Satis_Fiyati,@Tarih)", con);

                    komut.Parameters.AddWithValue("@Barkodno", txtBarkod.Text);
                    komut.Parameters.AddWithValue("@Kategori", cmbKategori.Text);
                    komut.Parameters.AddWithValue("@Marka", cmbMarka.Text);
                    komut.Parameters.AddWithValue("@Urun_Adi", txtUrunAd.Text);
                    komut.Parameters.AddWithValue("@Miktari", int.Parse(txtMiktar.Text));
                    komut.Parameters.AddWithValue("@Alis_Fiyati", decimal.Parse(txtAlisFiyat.Text));
                    komut.Parameters.AddWithValue("@Satis_Fiyati", decimal.Parse(txtSatisFiyati.Text));
                    komut.Parameters.AddWithValue("@Tarih", DateTime.Now.ToString());
                    komut.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Yeni Ürün Kaydı Eklendi");


                }
                else
                {
                    MessageBox.Show("Bu Barkod Numarasına Sahip Ürün Var", "Uyarı");
                }
            }
            else
            {
                MessageBox.Show("Depo Alanı Bu Kadar Miktarı Karşılayamaz");
            }

            cmbMarka.Items.Clear();
            foreach (Control item in groupBox1.Controls)
            {
                if (item is TextBox)
                {
                    item.Text = "";
                }
                if (item is ComboBox)
                {
                    item.Text = "";
                }

            }
        }

        private void V_txtBarkod_TextChanged(object sender, EventArgs e)
        {
            if (V_txtBarkod.Text == "")
            {
                lblMiktar.Text = "";
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }

            con.Open();
            SqlCommand komut = new SqlCommand("select * from Urun where Barkodno like '" + V_txtBarkod.Text + "'", con);
            SqlDataReader oku = komut.ExecuteReader();
            while (oku.Read())
            {
                V_txtKategori.Text = oku["Kategori"].ToString();
                V_txtMarka.Text = oku["Marka"].ToString();
                V_txtUrunAd.Text = oku["Urun_Adi"].ToString();
                lblMiktar.Text = oku["Miktari"].ToString();
                V_txtAlisFiyati.Text = oku["Alis_Fiyati"].ToString();
                V_SatisFiyati.Text = oku["Satis_Fiyati"].ToString();
            }
            con.Close();
        }

        private void btnProductAdd_Click(object sender, EventArgs e)
        {
            DolumUyarısı2();
            if (Depodurum2 == true)
            {

                int miktar = Convert.ToInt32(V_txtMiktar.Text);
                con.Open();

                SqlCommand cmd = new SqlCommand("update Urun set Miktari= Miktari+ '" + miktar + "' where Barkodno='" + V_txtBarkod.Text + "' ", con);
                cmd.ExecuteNonQuery();
                con.Close();
                foreach (Control item in groupBox2.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                MessageBox.Show("Ürüne Ekleme Yapıldı");
            }
            else
            {
                MessageBox.Show("Depo Alanı Bu Kadar Miktarı Karşılayamaz");
            }

        }


        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

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

        private void btn_barkod_Click(object sender, EventArgs e)
        {
            FrmBarkod ekle = new FrmBarkod();
            ekle.ShowDialog();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            txtBarkod.Text = FrmBarkod.barkodnumara.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmBarkod ekle = new FrmBarkod();
            ekle.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            V_txtBarkod.Text = FrmBarkod.barkodnumara.ToString();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.Left = MousePosition.X - MouseX;
            this.Top = MousePosition.Y - MouseY;

        }
    }
}
