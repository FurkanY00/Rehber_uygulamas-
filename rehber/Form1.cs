using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace rehber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
       SqlConnection baglanti=new SqlConnection(@"Data Source=DESKTOP-KPC6PV7\SQLEXPRESS;Initial Catalog=Rehber;Integrated Security=True");
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da =new SqlDataAdapter("select *from kısıler",baglanti);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        void temizle()
        {
            txtad.Text = "";
            txtsoyad.Text = "";
            txtmail.Text = "";
            txtid.Text= "";
            msktelefon.Text = "";
            txtad.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            listele();
        }

        private void btnekle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into kısıler (ad,soyad,telefon,maıl)values(@p1,@p2,@p3,@p4) ", baglanti);
            komut.Parameters.AddWithValue("@p1",txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3",msktelefon.Text);
            komut.Parameters.AddWithValue("@p4",txtmail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kişi kaydedildi!!","bilgi",MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
            temizle();
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtad.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtsoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            msktelefon.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtmail.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();

        }

        private void btnguncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update kısıler set ad=@p1,soyad=@p2,telefon=@p3,maıl=@p4 where ıd=@p5", baglanti);
            komut.Parameters.AddWithValue("@p5",txtid.Text);
            komut.Parameters.AddWithValue("@p1",txtad.Text);
            komut.Parameters.AddWithValue("@p2", txtsoyad.Text);
            komut.Parameters.AddWithValue("@p3",msktelefon.Text);
            komut.Parameters.AddWithValue("@p4",txtmail.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("kişi güncellendi","uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void btnsil_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("kişi silinsinmi?", "uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Stop);
            if (result==DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from kısıler where ıd=" + txtid.Text, baglanti);
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("kişi rehberden silindi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                listele();
                temizle();
            }
            else
            {
                MessageBox.Show("kayıt silinmedi","uyarı",MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            
        }
    }
}
