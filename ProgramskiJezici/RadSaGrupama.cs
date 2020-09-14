using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Ionic.Zip;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace ProgramskiJezici
{
    class RadSaGrupama
    {
        #region BAZA

        public bool ispravanId(int id, SqlConnection conn)
        {
            String upit = "SELECT COUNT(*) AS Broj FROM Grupa WHERE Id=@idParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["idParam"].Value = id;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int podatak = Int32.Parse(reader["Broj"].ToString());
                reader.Close();
                if (podatak != 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool ispravnoIme(String ime, SqlConnection conn)
        {
            String upit = "SELECT COUNT(*) AS Broj FROM Grupa WHERE Ime=@imeParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("imeParam", SqlDbType.VarChar);
                komanda.Parameters["imeParam"].Value = ime;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int podatak = Int32.Parse(reader["Broj"].ToString());
                reader.Close();
                if (podatak != 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void sacuvajGrupu(int id, String ime, SqlConnection conn)
        {
            String upit = "INSERT INTO Grupa VALUES (@idParam,@imeParam,@datumParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters.Add("imeParam", SqlDbType.VarChar);
                komanda.Parameters.Add("datumParam", SqlDbType.Date);
                komanda.Parameters["idParam"].Value = id;
                komanda.Parameters["imeParam"].Value = ime;
                komanda.Parameters["datumParam"].Value = DateTime.Now;
                komanda.ExecuteNonQuery();
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska insert u grupu " + err);
            }
        }

        public void sacuvajGrupaUser(int id, String jmbg, SqlConnection conn)
        {
            String upit = "INSERT INTO GrupaUser VALUES (@idParam,@jmbgParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters.Add("jmbgParam", SqlDbType.VarChar);
                komanda.Parameters["idParam"].Value = id;
                komanda.Parameters["jmbgParam"].Value = jmbg;
                komanda.ExecuteNonQuery();
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri cuvanju grupauser " + err);
            }
        }

        public void prikaziMojeGrupe(DataGridView dgv, String jmbg, SqlConnection conn)
        {
            String upit = "select Id,Ime,DatumKreiranja,BrojClanova from grupa, (select idg from GrupaUser where jmbg=@jmbgParam) as B, (select idg,count(*) as BrojClanova from GrupaUser group by idg) as A where grupa.id=b.IdG and grupa.id=a.idg and a.idg=b.idg order by ime asc";
            SqlCommand komanda = new SqlCommand();
            DataTable table = new DataTable();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("jmbgParam", SqlDbType.VarChar);
                komanda.Parameters["jmbgParam"].Value = jmbg;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = komanda;
                adapter.Fill(table);
                dgv.DataSource = table;
                dgv.Columns["Id"].Visible = false;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri prikazivanju mojih grupa " + err);
            }
        }

        public void prikaziSveGrupe(DataGridView dgv, String jmbg, SqlConnection conn)
        {
            String upit = "select Id,Ime,DatumKreiranja,BrojClanova from grupa,(select idg,count(*) as BrojClanova from GrupaUser group by idg) as A where not exists(select * from GrupaUser where GrupaUser.IdG=Grupa.Id and GrupaUser.JMBG=@jmbgParam)and Grupa.id=a.IdG";
            SqlCommand komanda = new SqlCommand();
            DataTable table = new DataTable();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("jmbgParam", SqlDbType.VarChar);
                komanda.Parameters["jmbgParam"].Value = jmbg;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = komanda;
                adapter.Fill(table);
                dgv.DataSource = table;
                dgv.Columns["Id"].Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show("Greska pri prikazivanju svih grupa " + err);
            }
        }

        public void prikaziSveClanoveGrupe(DataGridView dgv, int idGrupe, SqlConnection conn)
        {
            String upit = "select Ime,Prezime,korisnik.JMBG,Grad from korisnik, (select JMBG from GrupaUser where IdG=@idParam) as A where Korisnik.JMBG=A.JMBG";
            DataTable table = new DataTable();
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["idParam"].Value = idGrupe;
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = komanda;
                adapter.Fill(table);
                dgv.DataSource = table;
                dgv.Columns["JMBG"].Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show("Greska pri prikazivanju svih clanova " + err);
            }
        }

        public bool napustiGrupu(int id, String jmbg, SqlConnection conn)
        {
            String upit = "delete from GrupaUser where (IdG=@idParam and JMBG=@jmbgParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters.Add("jmbgParam", SqlDbType.VarChar);
                komanda.Parameters["idParam"].Value = id;
                komanda.Parameters["jmbgParam"].Value = jmbg;
                komanda.ExecuteNonQuery();
                return true;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri brisanju iz grupauser " + err);
                return false;
            }
        }

        public void izbrisiGrupu(int id, SqlConnection conn)
        {
            String upit = "delete from Grupa where Id=@idParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["idParam"].Value = id;
                komanda.ExecuteNonQuery();
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri brisanju iz grupe " + err);
            }
        }
        
        #endregion

        #region FAJLOVI

        public void kreirajGrupuFolder(String ime)
        {
            String putanja = "../../sve_kolekcije/grupe/" + ime + "/";
            DirectoryInfo di = new DirectoryInfo(putanja);
            if (di.Exists)
            {
                MessageBox.Show("Postoji grupa sa istim imenom");
                return;
            }
            di.Create();
        }

        public void izbrisiGrupuFolder(DirectoryInfo di)
        {
            FileInfo[] fi = di.GetFiles();
            foreach (FileInfo file in fi)
            {
                file.Delete();
            }
            DirectoryInfo[] dirs = di.GetDirectories();
            foreach (DirectoryInfo dir in dirs)
            {
                izbrisiGrupuFolder(dir);
                dir.Delete();
            }
        }

        #endregion

    }
}
