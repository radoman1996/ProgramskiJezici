using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProgramskiJezici
{
    class RadSaUzorcima
    {
        public int prikaziDokumenta(int idKolekcije, float OD, float DO, DataGridView dgv, SqlConnection conn)
        {
            String upit = "SELECT * FROM Dokument WHERE IdKolekcije=@idParam";
            if (OD != -1 && DO != -1)
            {
                upit += " AND (Velicina BETWEEN @odParam AND @doParam)";
            }
            DataTable table = new DataTable();
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters.Add("odParam", SqlDbType.Float);
                komanda.Parameters.Add("doParam", SqlDbType.Float);
                komanda.Parameters["idParam"].Value = idKolekcije;
                komanda.Parameters["odParam"].Value = OD;
                komanda.Parameters["doParam"].Value = DO;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = komanda;
                adapter.Fill(table);
                dgv.DataSource = table;
                dgv.Columns["IdKolekcije"].Visible = false;
                return 0;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri prikazivanju dokumenata za uzorak " + err);
                return 1;
            }
        }

        public bool postojeDokumenta(int idKolekcije, float OD, float DO, SqlConnection conn)
        {
            String upit = "SELECT COUNT(*) AS Broj FROM Dokument WHERE IdKolekcije=@idParam AND (Velicina BETWEEN @odParam AND @doParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters.Add("odParam", SqlDbType.Float);
                komanda.Parameters.Add("doParam", SqlDbType.Float);
                komanda.Parameters["idParam"].Value = idKolekcije;
                komanda.Parameters["odParam"].Value = OD;
                komanda.Parameters["doParam"].Value = DO;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int podatak = Int32.Parse(reader["Broj"].ToString());
                reader.Close();
                if (podatak > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception err)
            {
                MessageBox.Show("Greska pri prebrojavanju dokumenata BETWEEN velicina " + err);
                return false;
            }
        }

        public void slobodnaSelekcija(DataGridView dgv)
        {
            dgv.Enabled = true;
        }

        public void parnaSelekcija(DataGridView dgv)
        {
            Console.WriteLine(dgv.RowCount);
            for(int i = 0; i < dgv.RowCount; i++)
            {
                if (i % 2 != 0)
                {
                    dgv.Rows[i].Selected = true;
                    
                }
                else
                {
                    dgv.Rows[i].Selected = false;
                }
            }
            
        }

        public void neparnaSelekcija(DataGridView dgv)
        {
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (i % 2 == 0)
                {
                    dgv.Rows[i].Selected = true;
                }
                else
                {
                    dgv.Rows[i].Selected = false;
                }
            }
        }

        public void velicinaSelekcija(DataGridView dgv)
        {
            dgv.Enabled = true;
        }

    }
}
