using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramskiJezici
{
    public partial class PridruziSeGrupiFrm : Form
    {

        int idGrupe;
        String nazivGrupe;

        public PridruziSeGrupiFrm(int idGrupe, String nazivGrupe)
        {
            InitializeComponent();
            this.idGrupe = idGrupe;
            this.nazivGrupe = nazivGrupe;
        }

        private void PridruziSeGrupiFrm_Load(object sender, EventArgs e)
        {
            textBoxImeGrupe.Text = this.nazivGrupe;
        }

        private void btnPridruziSe_Click(object sender, EventArgs e)
        {
            if (textBoxIDGrupe.Text == "")
            {
                MessageBox.Show("Za pristup unesite ID grupe");
                return;
            }
            int id = Int32.Parse(textBoxIDGrupe.Text);
            if (id == this.idGrupe)
            {
                MessageBox.Show("Sada ste clan grupe " + this.nazivGrupe);
                this.Close();
            }
            else
            {
                MessageBox.Show("Potrebno je da unesete ispravan ID");
                return;
            }
        }

        private void btnIzlaz_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
