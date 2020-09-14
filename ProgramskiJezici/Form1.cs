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
using System.IO;
using Ionic.Zip;

namespace ProgramskiJezici
{
    public partial class Form1 : Form
    {
        #region PODACI

        String konekcioniString = @"Server=KORISNIK\SQLEXPRESS; Database=ProgramskiJeziciBaza; Integrated Security=true";
        SqlConnection konekcija;
        PomocnaKlasa pomocna = new PomocnaKlasa();
        RadSaFajlovima radSaFajlovima = new RadSaFajlovima();
        String[] kolone = { "Ime", "BrojDokumenata", "Velicina", "Tip" };
        RadSaGrupama radSaGrupama = new RadSaGrupama();


        enum Kontrola
        {
            Nista,
            Moje,
            Slozene,
            Sve
        }

        enum Grupa
        {
            Greska,
            Moje,
            Sve
        }

        private Kontrola kontrola;
        private Grupa grupa;

        #endregion

        public Form1()
        {
            InitializeComponent();
            kontrola = Kontrola.Nista;
            grupa = Grupa.Greska;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.BackColor = Color.FromArgb(0, 153, 153);
            try
            {
                konekcija = new SqlConnection();
                konekcija.ConnectionString = konekcioniString;
                konekcija.Open();

                #region POZIVI POMOCNIH METODA
                pomocna.popuniGrad(comboBoxGradKorisnika, konekcija);
                #endregion

            }
            catch (Exception err)
            {
                MessageBox.Show("Greska prilikom konektovanja na bazu podataka!!!");
            }
        }

        #region LOGIN
        private void btnPrijaviSe_Click(object sender, EventArgs e)
        {
            if (pomocna.ispravnostNaloga(textBoxUsername, konekcija))
            {
                if (pomocna.validnostPodataka(textBoxUsername, textBoxPassword, konekcija))
                {
                    int admin = pomocna.provjeraAdmina(textBoxUsername, konekcija);
                    if(admin == 2)
                    {
                        return;
                    }
                    if(admin == 1)
                    {
                        panelAdministratora.Visible = true;
                        lblAdminNaziv.Text = textBoxUsername.Text;
                        return;
                    }
                    if(admin == 0)
                    {
                        panelKorisnika.Visible = true;
                        lblNazivKorisnika.Text = textBoxUsername.Text;
                        return;
                    }
                    
                }
                
            }
            
        }
        #endregion

        #region PANEL ADMINISTRATORA
        private void btnDodajNovogKorisnika_Click(object sender, EventArgs e)
        {
            panelPrikazSvihKorisnika.Visible = false;
            panelDodajKorisnika.Visible = true;
            pomocna.ponistiNovogKorisnika(textBoxIme, textBoxPrezime, textBoxJMBG, textBoxEmail, textBoxKorisnickoIme, textBoxSifra, comboBoxGradKorisnika, dateTimePickerDatumKorisnika);
        }

        private void btnPrikaziSveKorisnike_Click(object sender, EventArgs e)
        {
            pomocna.prikazSvihKorisnika(dataGridViewsviKorisnici, konekcija);
            panelDodajKorisnika.Visible = false;
            panelPrikazSvihKorisnika.Visible = true;
        }

        private void btnOdjaviAdmin_Click(object sender, EventArgs e)
        {
            panelDodajKorisnika.Visible = false;
            panelAdministratora.Visible = false;
            panelPrikazSvihKorisnika.Visible = false;
            lblAdminNaziv.Text = "";
            pomocna.ponistiLoginFormu(textBoxUsername, textBoxPassword);
        }

        #region PANEL DODAJ KORISNIKA
        private void btnSacuvajKorisnika_Click(object sender, EventArgs e)
        {
            int provjera = pomocna.postojiNalog(textBoxKorisnickoIme, textBoxEmail, konekcija);
            if(provjera == 1)
            {
                MessageBox.Show("Postoji nalog. Unesite druge podatke");
                return;
            }
            if(provjera == 2)
            {
                return;
            }
            provjera = pomocna.postojiKorisnik(textBoxJMBG, konekcija);
            if (provjera == 1)
            {
                MessageBox.Show("Postoji korisnik. Unesite druge podatke");
                return;
            }
            if (provjera == 2)
            {
                return;
            }
            pomocna.sacuvajKorisnika(textBoxIme, textBoxPrezime, textBoxJMBG, textBoxEmail, textBoxKorisnickoIme, textBoxSifra, comboBoxGradKorisnika, dateTimePickerDatumKorisnika, konekcija);
            radSaFajlovima.KreirajKorisnickiFolder(textBoxKorisnickoIme.Text);
            pomocna.ponistiNovogKorisnika(textBoxIme, textBoxPrezime, textBoxJMBG, textBoxEmail, textBoxKorisnickoIme, textBoxSifra, comboBoxGradKorisnika, dateTimePickerDatumKorisnika);
        }

        private void btnPonisti_Click(object sender, EventArgs e)
        {
            pomocna.ponistiNovogKorisnika(textBoxIme, textBoxPrezime, textBoxJMBG, textBoxEmail, textBoxKorisnickoIme, textBoxSifra, comboBoxGradKorisnika, dateTimePickerDatumKorisnika);
        }

        private void btnOtkazi_Click(object sender, EventArgs e)
        {
            panelDodajKorisnika.Visible = false;
            pomocna.ponistiNovogKorisnika(textBoxIme, textBoxPrezime, textBoxJMBG, textBoxEmail, textBoxKorisnickoIme, textBoxSifra, comboBoxGradKorisnika, dateTimePickerDatumKorisnika);
        }

        #endregion

        #endregion

        #region PANEL KORISNIKA
        private void btnOdjaviKorisnika_Click(object sender, EventArgs e)
        {
            panelKorisnika.Visible = false;
            panelPrikazKolekcijaUser.Visible = false;
            panelGrupeUser.Visible = false;
            panelMojeGrupeKomande.Visible = false;
            lblNazivKorisnika.Text = "";
            pomocna.ponistiLoginFormu(textBoxUsername, textBoxPassword);
        }

        private void btnNovaKolekcijaUser_Click(object sender, EventArgs e)
        {
            NovaKolekcijaFrm forma = new NovaKolekcijaFrm(konekcija, lblNazivKorisnika.Text);
            forma.ShowDialog();

            String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
            pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);

            pomocna.StyleGrid(dataGridViewKolekcijeUser);

        }

        private void btnMojeKolekcije_Click(object sender, EventArgs e)
        {
            kontrola = Kontrola.Moje;

            panelGrupeUser.Visible = false;
            String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
            dataGridViewKolekcijeUser.DataSource = null;
            pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
            panelPrikazKolekcijaUser.Visible = true;
            pomocna.StyleGrid(dataGridViewKolekcijeUser);
        }

        private void btnSlozeneKolekcije_Click(object sender, EventArgs e)
        {
            kontrola = Kontrola.Slozene;

            panelGrupeUser.Visible = false;
            String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
            dataGridViewKolekcijeUser.DataSource = null;
            pomocna.prikazSlozenihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
            panelPrikazKolekcijaUser.Visible = true;
            pomocna.StyleGrid(dataGridViewKolekcijeUser);
        }

        private void btnSveKolekcije_Click(object sender, EventArgs e)
        {
            kontrola = Kontrola.Sve;

            panelGrupeUser.Visible = false;
            String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
            dataGridViewKolekcijeUser.DataSource = null;
            pomocna.prikazSvihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
            panelPrikazKolekcijaUser.Visible = true;
        }

        private void btnKreirajGrupu_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            int id = r.Next(1000, 1000000);
            bool provjera = false;
            while (provjera == false)
            {
                if (radSaGrupama.ispravanId(id, konekcija))
                {
                    provjera = true;
                }
                else
                {
                    id = r.Next(1000, 1000000);
                }
            }
            KreirajGrupuFrm forma = new KreirajGrupuFrm(id, lblNazivKorisnika.Text, konekcija);
            forma.ShowDialog();
            String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
            if (grupa == Grupa.Sve)
            {
                radSaGrupama.prikaziSveGrupe(dataGridViewGrupeUser, jmbg, konekcija);
                return;
            }
            if (grupa == Grupa.Moje)
            {
                radSaGrupama.prikaziMojeGrupe(dataGridViewGrupeUser, jmbg, konekcija);
                return;
            }
        }

        private void btnPrikaziGrupe_Click(object sender, EventArgs e)
        {
            grupa = Grupa.Sve;
            panelPrikazKolekcijaUser.Visible = false;
            panelGrupeUser.Visible = true;
            String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
            radSaGrupama.prikaziSveGrupe(dataGridViewGrupeUser, jmbg, konekcija);
            radioButtonMojeGrupe.Checked = false;
            radioButtonSveGrupe.Checked = true;
        }

        #region UPRAVLJANJE KOLEKCIJAMA
        private void btnSadrzajKolekcije_Click(object sender, EventArgs e)
        {
            if (kontrola == Kontrola.Sve)
            {
                MessageBox.Show("Nije moguce pregledati sadrzaj drugih korisnika");
                return;
            }
            if(dataGridViewKolekcijeUser.SelectedRows.Count < 1)
            {
                MessageBox.Show("Potrebno je da izaberete kolekciju za pregled");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count > 1)
            {
                MessageBox.Show("Potrebno je da izaberete jednu kolekciju");
                return;
            }
            int brojDok = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["BrojDokumenata"].Value.ToString());
            int child = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Child"].Value.ToString());
            int idKolekcije = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Id"].Value.ToString());
            int kljuc = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Lock"].Value.ToString());
            String imeKolekcije = dataGridViewKolekcijeUser.SelectedRows[0].Cells["Ime"].Value.ToString();

            if (brojDok == 0 && (child == 0 || child == 1))
            {
                MessageBox.Show("Nema dokumenata/kolekcija za prikazivanje");
                return;
            }
            if(brojDok > 0 && child == 0)
            {
                PrikazivanjeDokumenataFrm forma = new PrikazivanjeDokumenataFrm(idKolekcije, lblNazivKorisnika.Text, imeKolekcije, kljuc, konekcija);
                forma.ShowDialog();
                String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
                pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
                pomocna.StyleGrid(dataGridViewKolekcijeUser);
                return;
            }
            if (brojDok > 0 && child == 1)
            {
                PrikazDokumenataSlozeneKolekcijeFrm forma = new PrikazDokumenataSlozeneKolekcijeFrm(lblNazivKorisnika.Text, imeKolekcije, dataGridViewKolekcijeUser, kljuc, konekcija);
                forma.ShowDialog();
                pomocna.StyleGrid(dataGridViewKolekcijeUser);
            }
        }

        private void btnDodajDokument_Click(object sender, EventArgs e) 
        {
            if (kontrola == Kontrola.Sve)
            {
                MessageBox.Show("Nije moguce dodavati dokumenta u kolekcije drugih korisnika");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count < 1)
            {
                MessageBox.Show("Potrebno je da izaberete kolekciju u koju dodajete dokumenta");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count > 1)
            {
                MessageBox.Show("Dokumenta mogu da se dodaju samo u jednu kolekciju");
                return;
            }
            int idKolekcije = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Id"].Value.ToString());
            String tip = dataGridViewKolekcijeUser.SelectedRows[0].Cells["Tip"].Value.ToString();
            String ime = dataGridViewKolekcijeUser.SelectedRows[0].Cells["Ime"].Value.ToString();
            int kljuc = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Lock"].Value.ToString());
            int child = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Child"].Value.ToString());
            if (kljuc == 1)
            {
                DialogResult dr = MessageBox.Show("Kolekcija je zakljucana.\n" + "Za dodavanje dokumenata prethodno otkljucajte kolekciju", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (child == 1)
            {
                PrikazDokumenataSlozeneKolekcijeFrm forma = new PrikazDokumenataSlozeneKolekcijeFrm(lblNazivKorisnika.Text, ime, dataGridViewKolekcijeUser, kljuc, konekcija);
                forma.ShowDialog();
                return;
            }
            #region UCITAVANJE I KOPIRANJE FAJLOVA
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "(*." + tip + ")|*." + tip;
            openFileDialog1.Multiselect = true;
            openFileDialog1.ShowDialog();
            RadSaFajlovima radSaFajlovima = new RadSaFajlovima();
            String[] putanje = openFileDialog1.FileNames;
            if (putanje.Length == 1)
            {
                if (openFileDialog1.SafeFileNames[0].Length == 0)
                {
                    return;
                }
            }
            String[] imena = radSaFajlovima.imeFajla(openFileDialog1.SafeFileNames);
            float[] velicine = radSaFajlovima.velicinaFajla(putanje);
            //int i = 0;
            ProgressBar pb = new ProgressBar(idKolekcije, tip, lblNazivKorisnika.Text, ime, velicine, imena, putanje, konekcija);
            pb.ShowDialog();
            //foreach (String fajl in imena)
            //{
            //    int provjera = pomocna.postojiDokument(idKolekcije, fajl, konekcija);
            //    if(provjera == 2)
            //    {
            //        return;
            //    }
            //    if (provjera == 0)
            //    {
            //        if(pomocna.sacuvajDokument(idKolekcije, fajl, tip, velicine[i], konekcija) == 0)
            //        {
            //            radSaFajlovima.kopirajFajl(fajl + "." + tip, putanje[i], lblNazivKorisnika.Text, ime);
            //        }
            //    }
            //    else
            //    {
            //        if (provjera == 1)
            //        {
            //            if(pomocna.sacuvajDokument(idKolekcije, fajl, tip, velicine[i], konekcija) == 0)
            //            {
            //                radSaFajlovima.izbrisiFajl(fajl + "." + tip, lblNazivKorisnika.Text, ime);
            //                radSaFajlovima.kopirajFajl(fajl + "." + tip, putanje[i], lblNazivKorisnika.Text, ime);
            //            }
            //        }
            //    }
            //    i += 1; 
            //}
            #endregion

            int brDoc = pomocna.brojDokumenata(idKolekcije, konekcija);
            if (brDoc == -1)
            {
                return;
            }
            float velKB = pomocna.velicinaKolekcijeKB(idKolekcije, konekcija);
            if (velKB == -1)
            {
                return;
            }
            int konacno = pomocna.updateKolekcijeDokumentaIVelicina(idKolekcije, brDoc, velKB, konekcija);
            if (konacno == 0)
            {
                int datum = pomocna.updateDatumModifikovanja(idKolekcije, konekcija);
                if (datum == 0)
                {
                    MessageBox.Show("Uspjesno ste dodali fajlove u kolekciju");
                }
            }
            String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
            pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
            pomocna.StyleGrid(dataGridViewKolekcijeUser);
        }

        private void btnZakljucajKolekciju_Click(object sender, EventArgs e)
        {
            if (kontrola == Kontrola.Sve)
            {
                MessageBox.Show("Nije moguce otkljucavati/zakljucavati kolekcije drugih korisnika");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count < 1)
            {
                MessageBox.Show("Potrebno je da izaberete kolekciju");
                return;
            }
            if(dataGridViewKolekcijeUser.SelectedRows.Count > 1)
            {
                MessageBox.Show("Potrebno je da izaberete jednu kolekciju");
                return;
            }
            int idKolekcije = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Id"].Value.ToString());
            int kljuc = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Lock"].Value.ToString());
            Console.WriteLine(idKolekcije);
            Console.WriteLine(kljuc);
            if (kljuc == 0)
            {
                DialogResult dr = MessageBox.Show("Da li ste sigurni da zelite da zakljucate kolekciju?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    int zakljucavanje = pomocna.updateLock(idKolekcije, kljuc, konekcija);
                    if (zakljucavanje == 0)
                    {
                        MessageBox.Show("Uspjesno ste zakljucali kolekciju");
                        String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
                        dataGridViewKolekcijeUser.DataSource = null;
                        pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
                        pomocna.StyleGrid(dataGridViewKolekcijeUser);
                        return;
                    }
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("Da li ste sigurni da zelite da otkljucate kolekciju?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    int zakljucavanje = pomocna.updateLock(idKolekcije, kljuc, konekcija);
                    if (zakljucavanje == 0)
                    {
                        MessageBox.Show("Uspjesno ste otkljucali kolekciju");
                        String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
                        dataGridViewKolekcijeUser.DataSource = null;
                        pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
                        pomocna.StyleGrid(dataGridViewKolekcijeUser);
                        return;
                    }
                }
            }
        }

        private void btnIzbrisiKolekciju_Click(object sender, EventArgs e)
        {
            if (kontrola == Kontrola.Sve)
            {
                MessageBox.Show("Nije moguce brisati kolekcije drugih korisnika");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count < 1)
            {
                MessageBox.Show("Potrebno je da izaberete kolekciju za brisanje");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count > 1)
            {
                MessageBox.Show("Izaberite jednu kolekciju za brisanje");
                return;
            }
            int idKolekcije = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Id"].Value.ToString());
            String naziv = dataGridViewKolekcijeUser.SelectedRows[0].Cells["Ime"].Value.ToString();
            int kljuc = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Lock"].Value.ToString());
            if (kljuc == 1)
            {
                DialogResult dr = MessageBox.Show("Kolekcija je zakljucana.\n" + "Brisanje kolekcije nije dozvoljeno", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int child = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Child"].Value.ToString());
            if(child == 0)
            {
                DialogResult dr = MessageBox.Show("Brisanjem kolekcije brisete i sva dokumenta u kolekciji.\n" + "Da li ste sigurni da zelite da izbrisete kolekciju?", "Obavjestenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (pomocna.brisanjeKolekcije(idKolekcije, konekcija) == 0)
                    {
                        radSaFajlovima.izbrisiJednuKolekciju(lblNazivKorisnika.Text, naziv);
                        String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
                        pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
                        pomocna.StyleGrid(dataGridViewKolekcijeUser);
                        MessageBox.Show("Uspjesno ste izbrisali dokumenta i kolekciju");
                        return;
                    }
                    if (pomocna.brisanjeKolekcije(idKolekcije, konekcija) == 1)
                    {
                        return;
                    }
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show("Brisanjem kolekcije brisete i sva dokumenta u kolekciji.\n" + "Da li ste sigurni da zelite da izbrisete kolekciju?", "Obavjestenje", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (pomocna.brisanjeKolekcije(idKolekcije, konekcija) == 0)
                    {
                        String putanja = "../../sve_kolekcije/" + lblNazivKorisnika.Text + "/" + naziv + "/";
                        DirectoryInfo di = new DirectoryInfo(putanja);
                        RadSaSlozenimKolekcijama rad = new RadSaSlozenimKolekcijama();
                        rad.izbrisiKolekciju(di);
                        di.Delete();
                        String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
                        pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
                        pomocna.StyleGrid(dataGridViewKolekcijeUser);
                        MessageBox.Show("Uspjesno ste izbrisali dokumenta i kolekciju");
                        return;
                    }
                    if (pomocna.brisanjeKolekcije(idKolekcije, konekcija) == 1)
                    {
                        return;
                    }
                }
                
            }
            
        }

        private void btnSpajanjeKolekcija_Click(object sender, EventArgs e)
        {
            if (kontrola == Kontrola.Sve)
            {
                MessageBox.Show("Nije moguce vrsiti spajanje sadrzaja");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count != 2)
            {
                MessageBox.Show("Potrebno je da izaberete tacno dvije kolekcije");
                return;
            }
            int ch1 = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Child"].Value.ToString());
            int ch2 = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[1].Cells["Child"].Value.ToString());
            if (ch1 != 0 || ch2 != 0)
            {
                MessageBox.Show("Nije moguce spajati slozenu kolekciju");
                return;
            }
            String tip1 = dataGridViewKolekcijeUser.SelectedRows[0].Cells["Tip"].Value.ToString();
            String tip2 = dataGridViewKolekcijeUser.SelectedRows[1].Cells["Tip"].Value.ToString();
            if (tip1 != tip2)
            {
                MessageBox.Show("Tip kolekcija mora biti isti");
                return;
            }
            String ime1 = dataGridViewKolekcijeUser.SelectedRows[0].Cells["Ime"].Value.ToString();
            String ime2 = dataGridViewKolekcijeUser.SelectedRows[1].Cells["Ime"].Value.ToString();
            NovaKolekcijaFrm forma = new NovaKolekcijaFrm(konekcija, lblNazivKorisnika.Text, tip1, ime1, ime2);
            forma.ShowDialog();
            String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
            pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
            pomocna.StyleGrid(dataGridViewKolekcijeUser);
        }

        private void btnDijeljenjeKolekcija_Click(object sender, EventArgs e)
        {
            if (kontrola == Kontrola.Sve)
            {
                MessageBox.Show("Nije moguce dijeliti sadrzaj");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count < 1)
            {
                MessageBox.Show("Potrebno je da izaberete kolekciju za dijeljenje");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count > 1)
            {
                MessageBox.Show("Potrebno je da izaberete jednu kolekciju");
                return;
            }
            int brojDok = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["BrojDokumenata"].Value.ToString());
            int child = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Child"].Value.ToString());
            int idKolekcije = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Id"].Value.ToString());
            int kljuc = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Lock"].Value.ToString());
            String imeKolekcije = dataGridViewKolekcijeUser.SelectedRows[0].Cells["Ime"].Value.ToString();
            String tip = dataGridViewKolekcijeUser.SelectedRows[0].Cells["Tip"].Value.ToString();

            if (brojDok == 0 && child == 0)
            {
                MessageBox.Show("Nema dokumenata pa nije moguce praviti podjelu");
                return;
            }
            if (brojDok > 0 && child == 0)
            {
                if (brojDok == 1)
                {
                    MessageBox.Show("Postoji samo jedan dokument pa nije moguce napraviti podjelu");
                    return;
                }
                DijeljenjeKolekcijeFrm forma = new DijeljenjeKolekcijeFrm(lblNazivKorisnika.Text, imeKolekcije, idKolekcije, tip, konekcija);
                forma.ShowDialog();
                String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
                pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
                pomocna.StyleGrid(dataGridViewKolekcijeUser);
                return;
            }
            if (child == 1)
            {
                MessageBox.Show("Nije moguce dijeliti slozenu kolekciju");
                return;
            }
        }

        private void btnKreiranjeUzorka_Click(object sender, EventArgs e)
        {
            if (kontrola == Kontrola.Sve)
            {
                MessageBox.Show("Nije moguce kreiranje uzorka od kolekcija drugih korisnika");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count < 1)
            {
                MessageBox.Show("Potrebno je da izaberete kolekciju za kreiranju uzorka");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count > 1)
            {
                MessageBox.Show("Potrebno je da izaberete jednu kolekciju");
                return;
            }
            int brojDok = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["BrojDokumenata"].Value.ToString());
            int child = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Child"].Value.ToString());
            int idKolekcije = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Id"].Value.ToString());
            int kljuc = Int32.Parse(dataGridViewKolekcijeUser.SelectedRows[0].Cells["Lock"].Value.ToString());
            String imeKolekcije = dataGridViewKolekcijeUser.SelectedRows[0].Cells["Ime"].Value.ToString();
            String tip = dataGridViewKolekcijeUser.SelectedRows[0].Cells["Tip"].Value.ToString();

            if (brojDok == 0 && child == 0)
            {
                MessageBox.Show("Nema dokumenata pa nije moguce praviti uzorak");
                return;
            }
            if (brojDok > 0 && child == 0)
            {
                KreiranjeUzorkaFrm forma = new KreiranjeUzorkaFrm(idKolekcije, lblNazivKorisnika.Text, tip, imeKolekcije, konekcija);
                forma.ShowDialog();
                String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
                pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
                pomocna.StyleGrid(dataGridViewKolekcijeUser);
                return;
            }
            if (child == 1)
            {
                MessageBox.Show("Nije moguce praviti uzorak od slozene kolekcije");
                return;
            }
            
        }

        private void btnSlozena_Click(object sender, EventArgs e)
        {
            if (kontrola == Kontrola.Sve)
            {
                MessageBox.Show("Nije moguce praviti slozenu kolekciju");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count == 0)
            {
                MessageBox.Show("Potrebno je da izaberete kolekcije");
                return;
            }
            int ukupno = dataGridViewKolekcijeUser.SelectedRows.Count;
            String[] naziviKolekcija = new String[ukupno]; //imena svih kolekcija koje su selektovane
            for(int i = 0; i < ukupno; i++)
            {
                String ime = dataGridViewKolekcijeUser.SelectedRows[i].Cells["Ime"].Value.ToString();
                naziviKolekcija[i] = ime;
            }
            DialogResult dr = MessageBox.Show("Da li zelite da napravite slozenu kolekciju?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dr == DialogResult.Yes)
            {
                SlozenaKolekcijaFrm forma = new SlozenaKolekcijaFrm(lblNazivKorisnika.Text, naziviKolekcija, dataGridViewKolekcijeUser, konekcija);
                forma.ShowDialog();
                pomocna.StyleGrid(dataGridViewKolekcijeUser);
            }
        }

        private void btnExportKolekcije_Click(object sender, EventArgs e)
        {
            if (kontrola == Kontrola.Sve)
            {
                MessageBox.Show("Nije moguce vrsiti export kolekcije");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count < 1)
            {
                MessageBox.Show("Potrebno je da izaberete kolekciju");
                return;
            }
            if (dataGridViewKolekcijeUser.SelectedRows.Count > 1)
            {
                MessageBox.Show("Izaberite tacno jednu kolekciju");
                return;
            }
            String imeKolekcije = dataGridViewKolekcijeUser.SelectedRows[0].Cells["Ime"].Value.ToString();
            String putanja = "../../sve_kolekcije/" + lblNazivKorisnika.Text + "/" + imeKolekcije;
            using(ZipFile zf = new ZipFile())
            {
                zf.AddDirectory(putanja);
                saveFileDialog1.FileName = imeKolekcije;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    zf.Save(saveFileDialog1.FileName + ".zip");
                }
            }
        }

        private void btnImportKolekcije_Click(object sender, EventArgs e)
        {
            if (kontrola == Kontrola.Sve)
            {
                MessageBox.Show("Nije moguce vrsiti import");
                return;
            }
            openFileDialog1.Filter = "Zip file(*.zip)|*.zip";
            openFileDialog1.Multiselect = false;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String putanjazipFajla = openFileDialog1.FileName;
                String naziv = openFileDialog1.SafeFileName; //imeFajla.zip
                int pos = naziv.LastIndexOf('.');
                String nazivKolekcije = naziv.Substring(0, pos);
                String putanja = "../../sve_kolekcije/" + lblNazivKorisnika.Text + "/" + nazivKolekcije + "/";
                if (Directory.Exists(putanja))
                {
                    MessageBox.Show("Postoji kolekcija sa istim imenom. Nije moguce izvrsiti import");
                    return;
                }
                using(ZipFile zf = ZipFile.Read(putanjazipFajla))
                {
                    foreach(ZipEntry ze in zf)
                    {
                        ze.Extract(putanja);
                    }
                }
                String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
                
                RadSaSlozenimKolekcijama rad = new RadSaSlozenimKolekcijama();
                if (rad.sacuvajKolekciju(nazivKolekcije, jmbg, "xxx", konekcija))
                {
                    pomocna.prikazMojihKolekcija(jmbg, dataGridViewKolekcijeUser, konekcija);
                    pomocna.StyleGrid(dataGridViewKolekcijeUser);
                    int id = pomocna.GetIdKolekcije(jmbg, nazivKolekcije, konekcija);
                    String putanja2 = "../../sve_kolekcije/" + lblNazivKorisnika.Text + "/";
                    rad.updateGrid(dataGridViewKolekcijeUser, putanja2, id, konekcija);
                }
            }
        }

        #endregion

        #region UPRAVLJANJE GRUPAMA

        private void radioButtonSveGrupe_CheckedChanged(object sender, EventArgs e)
        {
            btnPridruziSeNapusti.Text = "Pridruzi se grupi";
            panelMojeGrupeKomande.Visible = false;
            grupa = Grupa.Sve;
            String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
            radSaGrupama.prikaziSveGrupe(dataGridViewGrupeUser, jmbg, konekcija);
        }

        private void radioButtonMojeGrupe_CheckedChanged(object sender, EventArgs e)
        {
            btnPridruziSeNapusti.Text = "Napusti grupu";
            panelMojeGrupeKomande.Visible = true;
            grupa = Grupa.Moje;
            String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
            radSaGrupama.prikaziMojeGrupe(dataGridViewGrupeUser, jmbg, konekcija);
        }

        private void btnClanoviGrupe_Click(object sender, EventArgs e)
        {
            radioButtonMojeGrupe.Checked = false;
            radioButtonSveGrupe.Checked = false;
            panelMojeGrupeKomande.Visible = false;

            if (dataGridViewGrupeUser.SelectedRows.Count != 1)
            {
                MessageBox.Show("Potrebno je da izaberete jednu grupu za prikazivanje clanova");
                return;
            }
            int id = Int32.Parse(dataGridViewGrupeUser.SelectedRows[0].Cells["Id"].Value.ToString());
            radSaGrupama.prikaziSveClanoveGrupe(dataGridViewGrupeUser, id, konekcija);
        }

        private void btnSadrzajGrupe_Click(object sender, EventArgs e)
        {
            if (dataGridViewGrupeUser.SelectedRows.Count != 1)
            {
                MessageBox.Show("Izaberite jednu grupu");
                return;
            }
            String ime = dataGridViewGrupeUser.SelectedRows[0].Cells["Ime"].Value.ToString();
            PrikazGrupaFrm forma = new PrikazGrupaFrm(ime);
            forma.ShowDialog();
        }

        private void btnExportGrupe_Click(object sender, EventArgs e)
        {
            if (grupa == Grupa.Moje)
            {
                if (dataGridViewGrupeUser.SelectedRows.Count != 1)
                {
                    MessageBox.Show("Izaberite jedan red");
                    return;
                }
                String ime = dataGridViewGrupeUser.SelectedRows[0].Cells["Ime"].Value.ToString();
                String putanja = "../../sve_kolekcije/grupe/" + ime + "/";
                using(ZipFile zf = new ZipFile())
                {
                    zf.AddDirectory(putanja);
                    saveFileDialog1.FileName = ime;
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        zf.Save(saveFileDialog1.FileName + ".zip");
                    }
                }
            }
        }

        private void btnPridruziSeNapusti_Click(object sender, EventArgs e)
        {
            if (dataGridViewGrupeUser.SelectedRows.Count != 1)
            {
                MessageBox.Show("Izaberite jednu grupu");
                return;
            }
            String jmbg = pomocna.GetJmbgKorisnika(lblNazivKorisnika.Text, konekcija);
            int id = Int32.Parse(dataGridViewGrupeUser.SelectedRows[0].Cells["Id"].Value.ToString());
            int broj = Int32.Parse(dataGridViewGrupeUser.SelectedRows[0].Cells["BrojClanova"].Value.ToString());
            String ime = dataGridViewGrupeUser.SelectedRows[0].Cells["Ime"].Value.ToString();
            if (grupa == Grupa.Moje)
            {
                DialogResult dr = MessageBox.Show("Sigurni ste da zelite da napustite grupu?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    if (broj == 1)
                    {
                        if (radSaGrupama.napustiGrupu(id, jmbg, konekcija))
                        {
                            String putanja = "../../sve_kolekcije/grupe/" + ime + "/";
                            DirectoryInfo di = new DirectoryInfo(putanja);
                            radSaGrupama.izbrisiGrupuFolder(di);
                            di.Delete();
                            radSaGrupama.izbrisiGrupu(id, konekcija);
                            radSaGrupama.prikaziMojeGrupe(dataGridViewGrupeUser, jmbg, konekcija);
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {
                        if (radSaGrupama.napustiGrupu(id, jmbg, konekcija))
                        {
                            radSaGrupama.prikaziMojeGrupe(dataGridViewGrupeUser, jmbg, konekcija);
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                return;
            }
            if (grupa == Grupa.Sve)
            {
                PridruziSeGrupiFrm forma = new PridruziSeGrupiFrm(id, ime);
                forma.ShowDialog();
                radSaGrupama.sacuvajGrupaUser(id, jmbg, konekcija);
                radSaGrupama.prikaziSveGrupe(dataGridViewGrupeUser, jmbg, konekcija);
            }
        }

        #endregion

        #endregion

        #region IZVJESTAJI

        private void btnSviKorisniciRpt_Click(object sender, EventArgs e)
        {
            SelekcijaPrikazaRptFrm forma = new SelekcijaPrikazaRptFrm("");
            forma.ShowDialog();
            return;
        }

        private void btnOdredjeniKorisnikRpt_Click(object sender, EventArgs e)
        {
            if (dataGridViewsviKorisnici.SelectedRows.Count != 1)
            {
                MessageBox.Show("Potrebno je da izaberete jedan red");
                return;
            }
            String jmbg = dataGridViewsviKorisnici.SelectedRows[0].Cells["JMBG"].Value.ToString();
            SelekcijaPrikazaRptFrm forma = new SelekcijaPrikazaRptFrm(jmbg);
            forma.ShowDialog();
            return;
        }

        #endregion

        private void btnExitApp_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
