namespace ProgramskiJezici
{
    partial class SlozenaKolekcijaFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxNazivSK = new System.Windows.Forms.TextBox();
            this.btnSacuvajSK = new System.Windows.Forms.Button();
            this.btnExitSK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(14, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(191, 18);
            this.label1.TabIndex = 0;
            this.label1.Text = "Naziv složene kolekcije:";
            // 
            // textBoxNazivSK
            // 
            this.textBoxNazivSK.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxNazivSK.Location = new System.Drawing.Point(213, 14);
            this.textBoxNazivSK.Name = "textBoxNazivSK";
            this.textBoxNazivSK.Size = new System.Drawing.Size(211, 24);
            this.textBoxNazivSK.TabIndex = 1;
            // 
            // btnSacuvajSK
            // 
            this.btnSacuvajSK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSacuvajSK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSacuvajSK.Image = global::ProgramskiJezici.Properties.Resources.Save_black_512;
            this.btnSacuvajSK.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnSacuvajSK.Location = new System.Drawing.Point(216, 54);
            this.btnSacuvajSK.Name = "btnSacuvajSK";
            this.btnSacuvajSK.Size = new System.Drawing.Size(165, 36);
            this.btnSacuvajSK.TabIndex = 3;
            this.btnSacuvajSK.Text = "Sačuvaj";
            this.btnSacuvajSK.UseVisualStyleBackColor = true;
            this.btnSacuvajSK.Click += new System.EventHandler(this.btnSacuvajSK_Click);
            // 
            // btnExitSK
            // 
            this.btnExitSK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExitSK.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExitSK.Image = global::ProgramskiJezici.Properties.Resources.door_exit_join_icon__27;
            this.btnExitSK.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnExitSK.Location = new System.Drawing.Point(43, 54);
            this.btnExitSK.Name = "btnExitSK";
            this.btnExitSK.Size = new System.Drawing.Size(165, 36);
            this.btnExitSK.TabIndex = 2;
            this.btnExitSK.Text = "Izlaz";
            this.btnExitSK.UseVisualStyleBackColor = true;
            this.btnExitSK.Click += new System.EventHandler(this.btnExitSK_Click);
            // 
            // SlozenaKolekcijaFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(446, 111);
            this.Controls.Add(this.btnSacuvajSK);
            this.Controls.Add(this.btnExitSK);
            this.Controls.Add(this.textBoxNazivSK);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(462, 150);
            this.MinimumSize = new System.Drawing.Size(462, 150);
            this.Name = "SlozenaKolekcijaFrm";
            this.Text = "Složena kolekcija";
            this.Load += new System.EventHandler(this.SlozenaKolekcijaFrm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxNazivSK;
        private System.Windows.Forms.Button btnExitSK;
        private System.Windows.Forms.Button btnSacuvajSK;
    }
}