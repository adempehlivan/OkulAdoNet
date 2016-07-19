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

namespace wfOKULAdoNet
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        SqlConnection conn = new SqlConnection("Data Source=WISSEN22\\MSSQLYAZ8; Initial Catalog=OKUL; uid=sa; pwd=123458");
        private void Form1_Load(object sender, EventArgs e)
        {
            SiniflariGetir();
        }
        private void SiniflariGetir()
        {
            SqlCommand comm = new SqlCommand("Select SinifAd from Siniflar", conn);
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr;
            try
            {
                dr = comm.ExecuteReader();
                while (dr.Read())
                {
                    cbSiniflar.Items.Add(dr["SinifAd"].ToString());
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }
        }

        private void cbSiniflar_SelectedIndexChanged(object sender, EventArgs e)
        {
            OgrencileriGetirBySinif();
        }
        private void OgrencileriGetirBySinif()
        {
            lvOgrenciler.Items.Clear();
            SqlCommand comm = new SqlCommand("Select Ad, Soyad, Telefon, Adres, SinifAd, Ogrenciler.ID from Ogrenciler inner join Siniflar on Ogrenciler.sinifID=Siniflar.ID where SinifAd=@SinifAd", conn);
            comm.Parameters.Add("@SinifAd", SqlDbType.VarChar).Value = cbSiniflar.SelectedItem.ToString();
            if (conn.State == ConnectionState.Closed) conn.Open();
            SqlDataReader dr;
            try
            {
                dr = comm.ExecuteReader();
                int i = 0;
                while (dr.Read())
                {
                    lvOgrenciler.Items.Add(dr[0].ToString());
                    lvOgrenciler.Items[i].SubItems.Add(dr[1].ToString());
                    lvOgrenciler.Items[i].SubItems.Add(dr[2].ToString());
                    lvOgrenciler.Items[i].SubItems.Add(dr[3].ToString());
                    lvOgrenciler.Items[i].SubItems.Add(dr[4].ToString());
                    lvOgrenciler.Items[i].SubItems.Add(dr[5].ToString());
                    i++;
                }
                dr.Close();
            }
            catch (SqlException ex)
            {
                string hata = ex.Message;
            }
            finally { conn.Close(); }

        }
    }
}
