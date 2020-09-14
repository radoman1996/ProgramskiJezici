using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;
using System.Drawing;

namespace ProgramskiJezici
{
    class PomocnaKlasa
    {
        public void popuniGrad(ComboBox c, SqlConnection conn)
        {
            String upit = "SELECT * FROM Grad";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.Connection = conn;
                komanda.CommandText = upit;
                SqlDataReader reader = komanda.ExecuteReader();
                while (reader.Read())
                {
                    c.Items.Add(reader["Naziv"]);
                }
                reader.Close();
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska popunjavanja grada " + err);
            }
        }

        public void ponistiNovogKorisnika(TextBox ime, TextBox prezime, TextBox jmbg, TextBox email, TextBox user, TextBox pass, ComboBox grad, DateTimePicker datum)
        {
            ime.Text = "";
            prezime.Text = "";
            jmbg.Text = "";
            email.Text = "";
            user.Text = "";
            pass.Text = "";
            grad.SelectedIndex = -1;
            datum.ResetText();
        }

        public int postojiNalog(TextBox user, TextBox email, SqlConnection conn)
        {
            String upit = "SELECT COUNT(*) AS Broj FROM Nalog where Nalog=@nalogParam OR Email=@mailParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.Connection = conn;
                komanda.CommandText = upit;
                komanda.Parameters.Add("nalogParam", SqlDbType.VarChar);
                komanda.Parameters.Add("mailParam", SqlDbType.VarChar);
                komanda.Parameters["nalogParam"].Value = user.Text;
                komanda.Parameters["mailParam"].Value = email.Text;

                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int ukupno = Int32.Parse(reader["Broj"].ToString());
                reader.Close();
                if(ukupno == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }

            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri provjeri naloga " + err);
                return 2;
            }
            
        }

        public int postojiKorisnik(TextBox jmbg, SqlConnection conn)
        {
            String upit = "SELECT COUNT(*) AS Broj FROM Korisnik WHERE JMBG=@jmbgParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("jmbgParam", SqlDbType.VarChar);
                komanda.Parameters["jmbgParam"].Value = jmbg.Text;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int ukupno = Int32.Parse(reader["Broj"].ToString());
                reader.Close();
                if(ukupno == 0)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri provjeri korisnika " + err);
                return 2;
            }
        }

        public void sacuvajKorisnika(TextBox ime, TextBox prezime, TextBox jmbg, TextBox email, TextBox user, TextBox pass, ComboBox grad, DateTimePicker datum, SqlConnection conn)
        {
            if(ime.Text == "" || prezime.Text == "" || jmbg.Text == "" || email.Text == "" || pass.Text == "")
            {
                MessageBox.Show("Potrebno je da popunite sva polja");
                return;
            }
            if(grad.SelectedIndex == -1)
            {
                MessageBox.Show("Potrebno je da izaberete grad");
                return;
            }
            int duzinaJMBG = (jmbg.Text).Length;
            if(duzinaJMBG != 13)
            {
                MessageBox.Show("Potrebno je da unesete pravilan JMBG");
                return;
            }
            String upit1 = "INSERT INTO Nalog VALUES(@nalogParam, @emailParam, @sifraParam, @adminParam)";
            String upit2 = "INSERT INTO Korisnik VALUES(@imeParam, @prezimeParam, @jmbgParam, @datumParam, @gradParam, @nalogKParam)";

            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.Connection = conn;
                komanda.CommandText = upit1;
                komanda.Parameters.Add("nalogParam", SqlDbType.VarChar);
                komanda.Parameters.Add("emailParam", SqlDbType.VarChar);
                komanda.Parameters.Add("sifraParam", SqlDbType.VarChar);
                komanda.Parameters.Add("adminParam", SqlDbType.Int);
                komanda.Parameters["nalogParam"].Value = user.Text;
                komanda.Parameters["emailParam"].Value = email.Text;
                komanda.Parameters["sifraParam"].Value = pass.Text;
                komanda.Parameters["adminParam"].Value = 0;
                komanda.ExecuteNonQuery();

                komanda.CommandText = upit2;
                komanda.Parameters.Add("imeParam", SqlDbType.VarChar);
                komanda.Parameters.Add("prezimeParam", SqlDbType.VarChar);
                komanda.Parameters.Add("jmbgParam", SqlDbType.VarChar);
                komanda.Parameters.Add("datumParam", SqlDbType.Date);
                komanda.Parameters.Add("gradParam", SqlDbType.VarChar);
                komanda.Parameters.Add("nalogKParam", SqlDbType.VarChar);
                komanda.Parameters["imeParam"].Value = ime.Text;
                komanda.Parameters["prezimeParam"].Value = prezime.Text;
                komanda.Parameters["jmbgParam"].Value = jmbg.Text;
                komanda.Parameters["datumParam"].Value = datum.Text;
                komanda.Parameters["gradParam"].Value = grad.SelectedItem.ToString();
                komanda.Parameters["nalogKParam"].Value = user.Text;
                komanda.ExecuteNonQuery();

                MessageBox.Show("Cuvanje uspjesno");
            }
            catch(Exception err)
            {
                MessageBox.Show("Nije uspjesno cuvanje korisnika " + err);
            }
        }

        public bool ispravnostNaloga(TextBox user, SqlConnection conn)
        {
            String upit = "SELECT COUNT(*) AS Broj FROM Nalog WHERE Nalog=@nalogParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("nalogParam", SqlDbType.VarChar);
                komanda.Parameters["nalogParam"].Value = user.Text;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int ukupno = Int32.Parse(reader["Broj"].ToString());
                reader.Close();
                if (ukupno < 1)
                {
                    MessageBox.Show("Nije ispravno korisnicko ime");
                    return false;
                }
                return true;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greskaa " + err);
                return false;
            }
        }

        public bool validnostPodataka(TextBox user, TextBox pass, SqlConnection conn)
        {
            String upit = "SELECT * FROM Nalog WHERE Nalog=@nalogParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("nalogParam", SqlDbType.VarChar);
                komanda.Parameters["nalogParam"].Value = user.Text;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                if(reader["Sifra"].ToString() == pass.Text)
                {
                    reader.Close();
                    return true;
                }
                reader.Close();
                MessageBox.Show("Sifra nije pravilna");
                return false;
            }
            catch(Exception err)
            {
                MessageBox.Show("Ponovo greska " + err);
                return false;
            }
        }

        public int provjeraAdmina(TextBox user, SqlConnection conn)
        {
            String upit = "SELECT Admin FROM Nalog WHERE Nalog=@nalogParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("nalogParam", SqlDbType.VarChar);
                komanda.Parameters["nalogParam"].Value = user.Text;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int admin = Int32.Parse(reader["Admin"].ToString());
                if(admin == 1)
                {
                    reader.Close();
                    return 1;
                }
                if(admin == 0)
                {
                    reader.Close();
                    return 0;
                }
                reader.Close();
                return 2;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska ponovo sa adminom " + err);
                return 2;
            }
        }

        public string nalogAdmina(SqlConnection conn)
        {
            String upit = "SELECT Nalog FROM Nalog WHERE Admin=1";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                String nalog = reader["Nalog"].ToString();
                reader.Close();
                return nalog;
            }
            catch(Exception err)
            {
                MessageBox.Show("Fatalna greska " + err);
                return "error";
            }
        }

        public void prikazSvihKorisnika(DataGridView dgv, SqlConnection conn)
        {
            DataTable table = new DataTable();
            String upit = "SELECT * FROM Korisnik WHERE Nalog<>@nalogParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("nalogParam", SqlDbType.VarChar);
                komanda.Parameters["nalogParam"].Value = nalogAdmina(conn);

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = komanda;
                adapter.Fill(table);
                dgv.DataSource = table;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska prilikom prikazivanja svih korisnika " + err);
            }
        }

        public void ponistiLoginFormu(TextBox user, TextBox pass)
        {
            user.Text = "";
            pass.Text = "";
        }

        public void popuniTip(ComboBox c, SqlConnection conn)
        {
            String upit = "SELECT * FROM Tip";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                SqlDataReader reader = komanda.ExecuteReader();
                while (reader.Read())
                {
                    if (reader["Naziv"].ToString() != "xxx")
                    {
                        c.Items.Add(reader["Naziv"].ToString());
                    }
                }
                reader.Close();
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri popunjavanju tipa " + err);
            }
        }

        public string GetJmbgKorisnika(String username, SqlConnection conn)
        {
            String upit = "SELECT JMBG FROM Korisnik WHERE Nalog=@nalogParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("nalogParam", SqlDbType.VarChar);
                komanda.Parameters["nalogParam"].Value = username;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                String podatak = reader["JMBG"].ToString();
                reader.Close();
                return podatak;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska prilikom vracanje jmbg korisnika " + err);
                return "";
            }
        }

        public bool sacuvajKolekciju(TextBox ime, String jmbg, ComboBox tip, SqlConnection conn)
        {
            if(ime.Text == "")
            {
                MessageBox.Show("Nepravilan naziv kolekcije");
                return false;
            }
            if(tip.SelectedIndex == -1)
            {
                MessageBox.Show("Nepravilan tip kolekcije");
                return false;
            }
            String upit = "INSERT INTO Kolekcija VALUES(@imeParam, @korisnikParam, @datumKreiranjaParam, @datumModifikovanjaParam, @tipParam, @brojParam, @velicinaParam, @childParam, @lockParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("imeParam", SqlDbType.VarChar);
                komanda.Parameters.Add("korisnikParam", SqlDbType.VarChar);
                komanda.Parameters.Add("datumKreiranjaParam", SqlDbType.Date);
                komanda.Parameters.Add("datumModifikovanjaParam", SqlDbType.Date);
                komanda.Parameters.Add("tipParam", SqlDbType.VarChar);
                komanda.Parameters.Add("brojParam", SqlDbType.Int);
                komanda.Parameters.Add("velicinaParam", SqlDbType.Float);
                komanda.Parameters.Add("childParam", SqlDbType.Int);
                komanda.Parameters.Add("lockParam", SqlDbType.Int);

                komanda.Parameters["imeParam"].Value = ime.Text;
                komanda.Parameters["korisnikParam"].Value = jmbg;
                komanda.Parameters["datumKreiranjaParam"].Value = DateTime.Today;
                komanda.Parameters["datumModifikovanjaParam"].Value = DateTime.Today;
                komanda.Parameters["tipParam"].Value = tip.SelectedItem.ToString();
                komanda.Parameters["brojParam"].Value = 0;
                komanda.Parameters["velicinaParam"].Value = 0;
                komanda.Parameters["childParam"].Value = 0;
                komanda.Parameters["lockParam"].Value = 0;

                komanda.ExecuteNonQuery();
                MessageBox.Show("Uspjesno cuvanje kolekcije");
                return true;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri cuvanju kolekcije " + err);
                return false;
            }
        }

        public bool sacuvajKolekciju(String ime, String jmbg, String tip, SqlConnection conn)
        {
            String upit = "INSERT INTO Kolekcija VALUES(@imeParam, @korisnikParam, @datumKreiranjaParam, @datumModifikovanjaParam, @tipParam, @brojParam, @velicinaParam, @childParam, @lockParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("imeParam", SqlDbType.VarChar);
                komanda.Parameters.Add("korisnikParam", SqlDbType.VarChar);
                komanda.Parameters.Add("datumKreiranjaParam", SqlDbType.Date);
                komanda.Parameters.Add("datumModifikovanjaParam", SqlDbType.Date);
                komanda.Parameters.Add("tipParam", SqlDbType.VarChar);
                komanda.Parameters.Add("brojParam", SqlDbType.Int);
                komanda.Parameters.Add("velicinaParam", SqlDbType.Float);
                komanda.Parameters.Add("childParam", SqlDbType.Int);
                komanda.Parameters.Add("lockParam", SqlDbType.Int);

                komanda.Parameters["imeParam"].Value = ime;
                komanda.Parameters["korisnikParam"].Value = jmbg;
                komanda.Parameters["datumKreiranjaParam"].Value = DateTime.Today;
                komanda.Parameters["datumModifikovanjaParam"].Value = DateTime.Today;
                komanda.Parameters["tipParam"].Value = tip;
                komanda.Parameters["brojParam"].Value = 0;
                komanda.Parameters["velicinaParam"].Value = 0;
                komanda.Parameters["childParam"].Value = 0;
                komanda.Parameters["lockParam"].Value = 0;

                komanda.ExecuteNonQuery();
                MessageBox.Show("Uspjesno cuvanje kolekcije");
                return true;
            }
            catch (Exception err)
            {
                MessageBox.Show("Greska pri cuvanju kolekcije " + err);
                return false;
            }
        }

        public int postojiKolekcija(String ime, String jmbg, SqlConnection conn)
        {
            if (ime == "")
            {
                MessageBox.Show("Nepravilan naziv kolekcije");
                return 2;
            }
            String upit = "SELECT COUNT(*) AS Ukupno FROM Kolekcija WHERE (Ime=@imeParam AND Korisnik=@jmbgParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("imeParam", SqlDbType.VarChar);
                komanda.Parameters.Add("jmbgParam", SqlDbType.VarChar);
                komanda.Parameters["imeParam"].Value = ime;
                komanda.Parameters["jmbgParam"].Value = jmbg;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int rez = Int32.Parse(reader["Ukupno"].ToString());
                reader.Close();
                if (rez < 1)
                {
                    return 0;
                }
                MessageBox.Show("Kolekcija vec postoji");
                return 1;
            }
            catch(Exception err)
            {
                MessageBox.Show("Neuspjesno citanje kolekcije iz baze " + err);
                return 2;
            }
        }

        public void prikazMojihKolekcija(String jmbg, DataGridView dgv, SqlConnection conn)
        {
            DataTable table = new DataTable();
            String upit = "SELECT * FROM Kolekcija WHERE Korisnik=@jmbgParam";
            SqlCommand komanda = new SqlCommand();
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
                dgv.Columns["Korisnik"].Visible = false;
                dgv.Columns["Lock"].Visible = false;
                dgv.Columns["Child"].Visible = false;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska prilikom prikazivanje mojih kolekcija " + err);
            }
        }

        public void prikazSvihKolekcija(String jmbg, DataGridView dgv, SqlConnection conn)
        {
            DataTable table = new DataTable();
            String upit = "select kol.id,kol.ime as ImeKolekcije,kol.datumKreiranja,kol.datumModifikovanja,kol.tip,kol.brojDokumenata,kol.velicina,kol.child,kol.lock,kor.ime,kor.prezime,kor.jmbg from kolekcija as kol inner join korisnik as kor on kol.Korisnik = kor.JMBG where kor.jmbg<>@jmbgParam";
            SqlCommand komanda = new SqlCommand();
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
                dgv.Columns["jmbg"].Visible = false;
                dgv.Columns["Lock"].Visible = false;
                dgv.Columns["Child"].Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show("Greska prilikom prikazivanje mojih kolekcija " + err);
            }
        }

        public int GetIdKolekcije(String jmbg, String naziv, SqlConnection conn)
        {
            String upit = "SELECT Id FROM Kolekcija WHERE (Korisnik=@jmbgParam AND Ime=@nazivParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("jmbgParam", SqlDbType.VarChar);
                komanda.Parameters.Add("nazivParam", SqlDbType.VarChar);
                komanda.Parameters["jmbgParam"].Value = jmbg;
                komanda.Parameters["nazivParam"].Value = naziv;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int id = Int32.Parse(reader["Id"].ToString());
                reader.Close();
                return id;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri vracanju id kolekcije " + err);
                return -1;
            }
        }

        public int postojiDokument(int idKolekcije, String ime, SqlConnection conn)
        {
            String upit = "SELECT COUNT(*) AS Broj FROM Dokument WHERE (Naziv=@nazivParam AND IdKolekcije=@idParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("nazivParam", SqlDbType.VarChar);
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["nazivParam"].Value = ime;
                komanda.Parameters["idParam"].Value = idKolekcije;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                int podatak = Int32.Parse(reader["Broj"].ToString());
                reader.Close();
                if (podatak != 0)
                {
                    return 1;
                }
                return 0;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska.... " + err);
                return 2;
            }
        }

        public int sacuvajDokument(int idKolekcije, String ime, String tip, float velicina, SqlConnection conn)
        {
            String upit = "INSERT INTO Dokument VALUES (@nazivParam, @velicinaParam, @tipParam, @idParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("nazivParam", SqlDbType.VarChar);
                komanda.Parameters.Add("velicinaParam", SqlDbType.Float);
                komanda.Parameters.Add("tipParam", SqlDbType.VarChar);
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["nazivParam"].Value = ime;
                komanda.Parameters["velicinaParam"].Value = velicina;
                komanda.Parameters["tipParam"].Value = tip;
                komanda.Parameters["idParam"].Value = idKolekcije;
                komanda.ExecuteNonQuery();
                return 0; //sve je ok
            }
            catch(Exception err)
            {
                int provjera = postojiDokument(idKolekcije, ime, conn);
                if (provjera == 0)
                {
                    MessageBox.Show("Greska prilikom cuvanja dokumenta " + err);
                    return 2;
                }
                if(provjera == 1)
                {
                    DialogResult dr = MessageBox.Show("Postoji fajl sa istim imenom.\n" + "Zelite li da zamjenite fajlove?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dr == DialogResult.Yes)
                    {
                        String upit2 = "DELETE FROM Dokument WHERE (Naziv=@naziv2Param AND IdKolekcije=@id2Param)";
                        try
                        {
                            komanda.CommandText = upit2;
                            komanda.Connection = conn;
                            komanda.Parameters.Add("naziv2Param", SqlDbType.VarChar);
                            komanda.Parameters.Add("id2Param", SqlDbType.Int);
                            komanda.Parameters["naziv2Param"].Value = ime;
                            komanda.Parameters["id2Param"].Value = idKolekcije;
                            komanda.ExecuteNonQuery();
                            sacuvajDokument(idKolekcije, ime, tip, velicina, conn);
                            return 0;
                        }
                        catch(Exception er)
                        {
                            MessageBox.Show("Greska prilikom brisanja dokumenta " + er);
                            return 3;
                        }
                    }
                    if(dr == DialogResult.No)
                    {
                        return 1;
                    }
                }
                return 0;
            }
        }

        public int brojDokumenata(int idKolekcije, SqlConnection conn)
        {
            int ukupno = 0;
            String upit = "SELECT COUNT(*) AS Broj FROM Dokument WHERE IdKolekcije=@idParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["idParam"].Value = idKolekcije;
                SqlDataReader reader = komanda.ExecuteReader();
                reader.Read();
                ukupno += Int32.Parse(reader["Broj"].ToString());
                reader.Close();
                return ukupno;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska prilikom vracanja broj dokumenata " + err);
                return -1;
            }
        }

        public float velicinaKolekcijeKB(int idKolekcije, SqlConnection conn)
        {
            float podatak = 0;
            String upit = "SELECT SUM(d.Velicina) AS KB FROM Dokument AS d WHERE d.IdKolekcije=@idParam";
            SqlCommand komanda = new SqlCommand();
            SqlDataReader reader;
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["idParam"].Value = idKolekcije;
                reader = komanda.ExecuteReader();
                reader.Read();
                podatak += float.Parse(reader["KB"].ToString());
                reader.Close();
                return podatak;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska prilikom racunanja velicine kolekcije " + err);
                return -1;
            }
        }

        public int updateKolekcijeDokumentaIVelicina(int idKolekcije, int brojDokumenata, float velicina, SqlConnection conn)
        {
            String upit = "UPDATE Kolekcija SET BrojDokumenata=@brDokparam, Velicina=@velicinaParam WHERE Id=@idParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("brDokparam", SqlDbType.Int);
                komanda.Parameters.Add("velicinaParam", SqlDbType.Float);
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["brDokparam"].Value = brojDokumenata;
                komanda.Parameters["velicinaParam"].Value = velicina;
                komanda.Parameters["idParam"].Value = idKolekcije;
                komanda.ExecuteNonQuery();
                return 0;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska prilikom update broja dokumenata i velicine " + err);
                return 1;
            }
        }

        public int brisanjeKolekcije(int idKolekcije, SqlConnection conn)
        {
            String upit = "DELETE FROM Dokument WHERE IdKolekcije=@idParam";
            String upit2 = "DELETE FROM Kolekcija WHERE Id=@id2Param";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["idParam"].Value = idKolekcije;
                komanda.ExecuteNonQuery();
                komanda.CommandText = upit2;
                komanda.Connection = conn;
                komanda.Parameters.Add("id2Param", SqlDbType.Int);
                komanda.Parameters["id2Param"].Value = idKolekcije;
                komanda.ExecuteNonQuery();
                return 0;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska prilikom brisanja dokumenata i kolekcije");
                return 1;
            }
        }

        public int updateLock(int idKolekcije, int kljuc, SqlConnection conn)
        {
            String upit = "";
            if (kljuc == 0)
            {
                upit += "UPDATE Kolekcija SET Lock=1 WHERE Id=@idParam";
            }
            if(kljuc == 1)
            {
                upit += "UPDATE Kolekcija SET Lock=0 WHERE Id=@idParam";
            }
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["idParam"].Value = idKolekcije;
                komanda.ExecuteNonQuery();
                return 0;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri update-u zakljucavanja " + err);
                return 1;
            }
        }

        public int updateDatumModifikovanja(int idKolekcije, SqlConnection conn)
        {
            String upit = "UPDATE Kolekcija SET DatumModifikovanja=@datumParam WHERE Id=@idParam";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("datumParam", SqlDbType.Date);
                komanda.Parameters.Add("idParam", SqlDbType.Int);
                komanda.Parameters["datumParam"].Value = DateTime.Today;
                komanda.Parameters["idParam"].Value = idKolekcije;
                komanda.ExecuteNonQuery();
                return 0;
            }
            catch(Exception err)
            {
                MessageBox.Show("Greska pri modifikovanju datuma " + err);
                return 1;
            }
        }

        public void sacuvajKolekcijuUnija(String naziv, String tip, String jmbg, SqlConnection conn)
        {
            String upit = "INSERT INTO Kolekcija VALUES(@imeParam, @korisnikParam, @datumKreiranjaParam, @datumModifikovanjaParam, @tipParam, @brojParam, @velicinaParam, @childParam, @lockParam)";
            SqlCommand komanda = new SqlCommand();
            try
            {
                komanda.CommandText = upit;
                komanda.Connection = conn;
                komanda.Parameters.Add("imeParam", SqlDbType.VarChar);
                komanda.Parameters.Add("korisnikParam", SqlDbType.VarChar);
                komanda.Parameters.Add("datumKreiranjaParam", SqlDbType.Date);
                komanda.Parameters.Add("datumModifikovanjaParam", SqlDbType.Date);
                komanda.Parameters.Add("tipParam", SqlDbType.VarChar);
                komanda.Parameters.Add("brojParam", SqlDbType.Int);
                komanda.Parameters.Add("velicinaParam", SqlDbType.Float);
                komanda.Parameters.Add("childParam", SqlDbType.Int);
                komanda.Parameters.Add("lockParam", SqlDbType.Int);

                komanda.Parameters["imeParam"].Value = naziv;
                komanda.Parameters["korisnikParam"].Value = jmbg;
                komanda.Parameters["datumKreiranjaParam"].Value = DateTime.Today;
                komanda.Parameters["datumModifikovanjaParam"].Value = DateTime.Today;
                komanda.Parameters["tipParam"].Value = tip;
                komanda.Parameters["brojParam"].Value = 0;
                komanda.Parameters["velicinaParam"].Value = 0;
                komanda.Parameters["childParam"].Value = 0;
                komanda.Parameters["lockParam"].Value = 0;

                komanda.ExecuteNonQuery();
                MessageBox.Show("Uspjesno cuvanje kolekcije unije");
            }
            catch (Exception err)
            {
                MessageBox.Show("Greska pri cuvanju kolekcije unija " + err);
            }
        }

        public void prikazSlozenihKolekcija(String jmbg, DataGridView dgv, SqlConnection conn)
        {
            DataTable table = new DataTable();
            String upit = "SELECT * FROM Kolekcija WHERE Korisnik=@jmbgParam AND Child=1";
            SqlCommand komanda = new SqlCommand();
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
                dgv.Columns["Korisnik"].Visible = false;
                dgv.Columns["Lock"].Visible = false;
                dgv.Columns["Child"].Visible = false;
            }
            catch (Exception err)
            {
                MessageBox.Show("Greska prilikom prikazivanje slozenih kolekcija " + err);
            }
        }

        public void StyleGrid(DataGridView dgv)
        {
            for(int i = 0; i < dgv.RowCount; i++)
            {
                int Lock = Int32.Parse(dgv.Rows[i].Cells["Lock"].Value.ToString());
                int Child = Int32.Parse(dgv.Rows[i].Cells["Child"].Value.ToString());
                if (Lock == 1)
                {
                    dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(255, 179, 179);
                }
                else
                {
                    if (Child == 1)
                    {
                        dgv.Rows[i].DefaultCellStyle.BackColor = Color.FromArgb(179, 179, 255);
                    }
                }
            }
        }
    }
}
