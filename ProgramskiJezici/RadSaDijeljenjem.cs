using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;

namespace ProgramskiJezici
{
    class RadSaDijeljenjem
    {
        public int prikaziDokumenta(int idKolekcije, DataGridView dgv, SqlConnection conn)
        {
            String upit = "SELECT * FROM Dokument WHERE IdKolekcije=@idParam";
            DataTable table = new DataTable();
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["idParam"].Value = idKolekcije;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = komanda;
                adapter.Fill(table);
                dgv.DataSource = table;
                dgv.Columns["IdKolekcije"].Visible = false;
                return 0;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri prikazivanju dokumenata DIJELJENJE " + err);
                return 1;
            }
        }

        public bool postojeDokumentaManje(int idKolekcije, float velicina, SqlConnection conn)
        {
            String upit = "SELECT COUNT(*) AS Broj FROM Dokument WHERE IdKolekcije=@idParam and Velicina<@velicinaParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters.Add("velicinaParam", SqlDbType.Float);
                komanda.Parameters["idParam"].Value = idKolekcije;
                komanda.Parameters["velicinaParam"].Value = velicina;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int podatak = Int32.Parse(reader["Broj"].ToString());
                reader.Close();
                if (podatak == 0)
                {
                    return false;
                }
                return true;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri provjeri MANJE " + err);
                return false;
            }
        }

        public bool postojeDokumentaVise(int idKolekcije, float velicina, SqlConnection conn)
        {
            String upit = "SELECT COUNT(*) AS Broj FROM Dokument WHERE IdKolekcije=@idParam and Velicina>@velicinaParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters.Add("velicinaParam", SqlDbType.Float);
                komanda.Parameters["idParam"].Value = idKolekcije;
                komanda.Parameters["velicinaParam"].Value = velicina;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int podatak = Int32.Parse(reader["Broj"].ToString());
                reader.Close();
                if (podatak == 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show("Greska pri provjeri VISE " + err);
                return false;
            }
        }

        public void ParNepar(DataGridView dgv)
        {
            for(int i = 0; i < dgv.RowCount; i++)
            {
                if (i % 2 == 0)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Red;//neparni redovi crveni
                }
                else
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Blue;//parni redovi plavi
                }
            }
        }

        public void Velicina(DataGridView dgv, float vel)
        {
            for(int i = 0; i < dgv.RowCount; i++)
            {
                float curr = float.Parse(dgv.Rows[i].Cells["Velicina"].Value.ToString());
                if (curr > vel)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
                else
                {
                    if (curr < vel)
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.Blue;
                    }
                }
            }
        }

    }
}
