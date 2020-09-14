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
    public partial class DokumentaFrmRpt : Form
    {

        String jmbg;

        public DokumentaFrmRpt(String jmbg)
        {
            InitializeComponent();
            this.jmbg = jmbg;
        }

        private void DokumentaFrmRpt_Load(object sender, EventArgs e)
        {
            if (this.jmbg == "")
            {
                SvaDokumentaRpt sdr = new SvaDokumentaRpt();
                this.crystalReportViewer1.ReportSource = sdr;
            }
            else
            {
                SvaDokumentaKorisnik sdk = new SvaDokumentaKorisnik();
                sdk.SetParameterValue("jmbg", this.jmbg);
                this.crystalReportViewer1.ReportSource = sdk;
            }
        }
    }
}
