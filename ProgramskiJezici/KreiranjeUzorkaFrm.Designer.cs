namespace ProgramskiJezici
{
    partial class KreiranjeUzorkaFrm
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.KUpanel1 = new System.Windows.Forms.Panel();
            this.textBoxImeKolekcije = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.KUpanel2 = new System.Windows.Forms.Panel();
            this.panelPoljaVelicine = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxDOkb = new System.Windows.Forms.TextBox();
            this.textBoxODkb = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radiobtnVelicina = new System.Windows.Forms.RadioButton();
            this.radiobtnNeparnaSelekcija = new System.Windows.Forms.RadioButton();
            this.radiobtnParnaSelekcija = new System.Windows.Forms.RadioButton();
            this.radiobtnSlobodnaSelekcija = new System.Windows.Forms.RadioButton();
            this.KUpanel3 = new System.Windows.Forms.Panel();
            this.dataGridViewSelekcija = new System.Windows.Forms.DataGridView();
            this.KUpanel1.SuspendLayout();
            this.KUpanel2.SuspendLayout();
            this.panelPoljaVelicine.SuspendLayout();
            this.KUpanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelekcija)).BeginInit();
            this.SuspendLayout();
            // 
            // btnNext
            // 
            this.btnNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNext.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::ProgramskiJezici.Properties.Resources.next_512;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(237, 217);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(170, 32);
            this.btnNext.TabIndex = 0;
            this.btnNext.Text = "Dalje";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnExit
            // 
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.Image = global::ProgramskiJezici.Properties.Resources.door_exit_join_icon__27;
            this.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExit.Location = new System.Drawing.Point(51, 217);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(170, 32);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "Izlaz";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // KUpanel1
            // 
            this.KUpanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.KUpanel1.Controls.Add(this.textBoxImeKolekcije);
            this.KUpanel1.Controls.Add(this.label1);
            this.KUpanel1.Location = new System.Drawing.Point(0, 0);
            this.KUpanel1.Name = "KUpanel1";
            this.KUpanel1.Size = new System.Drawing.Size(459, 211);
            this.KUpanel1.TabIndex = 2;
            // 
            // textBoxImeKolekcije
            // 
            this.textBoxImeKolekcije.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxImeKolekcije.Location = new System.Drawing.Point(182, 54);
            this.textBoxImeKolekcije.Name = "textBoxImeKolekcije";
            this.textBoxImeKolekcije.Size = new System.Drawing.Size(225, 26);
            this.textBoxImeKolekcije.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(53, 57);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ime kolekcije: ";
            // 
            // KUpanel2
            // 
            this.KUpanel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.KUpanel2.Controls.Add(this.panelPoljaVelicine);
            this.KUpanel2.Controls.Add(this.radiobtnVelicina);
            this.KUpanel2.Controls.Add(this.radiobtnNeparnaSelekcija);
            this.KUpanel2.Controls.Add(this.radiobtnParnaSelekcija);
            this.KUpanel2.Controls.Add(this.radiobtnSlobodnaSelekcija);
            this.KUpanel2.Location = new System.Drawing.Point(0, 0);
            this.KUpanel2.Name = "KUpanel2";
            this.KUpanel2.Size = new System.Drawing.Size(459, 211);
            this.KUpanel2.TabIndex = 3;
            this.KUpanel2.Visible = false;
            // 
            // panelPoljaVelicine
            // 
            this.panelPoljaVelicine.Controls.Add(this.label5);
            this.panelPoljaVelicine.Controls.Add(this.label4);
            this.panelPoljaVelicine.Controls.Add(this.textBoxDOkb);
            this.panelPoljaVelicine.Controls.Add(this.textBoxODkb);
            this.panelPoljaVelicine.Controls.Add(this.label3);
            this.panelPoljaVelicine.Controls.Add(this.label2);
            this.panelPoljaVelicine.Location = new System.Drawing.Point(113, 141);
            this.panelPoljaVelicine.Name = "panelPoljaVelicine";
            this.panelPoljaVelicine.Size = new System.Drawing.Size(201, 52);
            this.panelPoljaVelicine.TabIndex = 4;
            this.panelPoljaVelicine.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(144, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "KB";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(144, 7);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "KB";
            // 
            // textBoxDOkb
            // 
            this.textBoxDOkb.Location = new System.Drawing.Point(38, 26);
            this.textBoxDOkb.Name = "textBoxDOkb";
            this.textBoxDOkb.Size = new System.Drawing.Size(100, 20);
            this.textBoxDOkb.TabIndex = 3;
            // 
            // textBoxODkb
            // 
            this.textBoxODkb.Location = new System.Drawing.Point(38, 4);
            this.textBoxODkb.Name = "textBoxODkb";
            this.textBoxODkb.Size = new System.Drawing.Size(100, 20);
            this.textBoxODkb.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(8, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Do:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(27, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Od:";
            // 
            // radiobtnVelicina
            // 
            this.radiobtnVelicina.AutoSize = true;
            this.radiobtnVelicina.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radiobtnVelicina.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiobtnVelicina.Location = new System.Drawing.Point(51, 113);
            this.radiobtnVelicina.Name = "radiobtnVelicina";
            this.radiobtnVelicina.Size = new System.Drawing.Size(84, 22);
            this.radiobtnVelicina.TabIndex = 3;
            this.radiobtnVelicina.TabStop = true;
            this.radiobtnVelicina.Text = "Veličina";
            this.radiobtnVelicina.UseVisualStyleBackColor = true;
            this.radiobtnVelicina.CheckedChanged += new System.EventHandler(this.radiobtnVelicina_CheckedChanged);
            // 
            // radiobtnNeparnaSelekcija
            // 
            this.radiobtnNeparnaSelekcija.AutoSize = true;
            this.radiobtnNeparnaSelekcija.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radiobtnNeparnaSelekcija.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiobtnNeparnaSelekcija.Location = new System.Drawing.Point(51, 83);
            this.radiobtnNeparnaSelekcija.Name = "radiobtnNeparnaSelekcija";
            this.radiobtnNeparnaSelekcija.Size = new System.Drawing.Size(160, 22);
            this.radiobtnNeparnaSelekcija.TabIndex = 2;
            this.radiobtnNeparnaSelekcija.TabStop = true;
            this.radiobtnNeparnaSelekcija.Text = "Neparna selekcija";
            this.radiobtnNeparnaSelekcija.UseVisualStyleBackColor = true;
            this.radiobtnNeparnaSelekcija.CheckedChanged += new System.EventHandler(this.radiobtnNeparnaSelekcija_CheckedChanged);
            // 
            // radiobtnParnaSelekcija
            // 
            this.radiobtnParnaSelekcija.AutoSize = true;
            this.radiobtnParnaSelekcija.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radiobtnParnaSelekcija.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiobtnParnaSelekcija.Location = new System.Drawing.Point(51, 53);
            this.radiobtnParnaSelekcija.Name = "radiobtnParnaSelekcija";
            this.radiobtnParnaSelekcija.Size = new System.Drawing.Size(141, 22);
            this.radiobtnParnaSelekcija.TabIndex = 1;
            this.radiobtnParnaSelekcija.TabStop = true;
            this.radiobtnParnaSelekcija.Text = "Parna selekcija";
            this.radiobtnParnaSelekcija.UseVisualStyleBackColor = true;
            this.radiobtnParnaSelekcija.CheckedChanged += new System.EventHandler(this.radiobtnParnaSelekcija_CheckedChanged);
            // 
            // radiobtnSlobodnaSelekcija
            // 
            this.radiobtnSlobodnaSelekcija.AutoSize = true;
            this.radiobtnSlobodnaSelekcija.Cursor = System.Windows.Forms.Cursors.Hand;
            this.radiobtnSlobodnaSelekcija.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.radiobtnSlobodnaSelekcija.Location = new System.Drawing.Point(51, 23);
            this.radiobtnSlobodnaSelekcija.Name = "radiobtnSlobodnaSelekcija";
            this.radiobtnSlobodnaSelekcija.Size = new System.Drawing.Size(168, 22);
            this.radiobtnSlobodnaSelekcija.TabIndex = 0;
            this.radiobtnSlobodnaSelekcija.TabStop = true;
            this.radiobtnSlobodnaSelekcija.Text = "Slobodna selekcija";
            this.radiobtnSlobodnaSelekcija.UseVisualStyleBackColor = true;
            this.radiobtnSlobodnaSelekcija.CheckedChanged += new System.EventHandler(this.radiobtnSlobodnaSelekcija_CheckedChanged);
            // 
            // KUpanel3
            // 
            this.KUpanel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(179)))), ((int)(((byte)(179)))));
            this.KUpanel3.Controls.Add(this.dataGridViewSelekcija);
            this.KUpanel3.Location = new System.Drawing.Point(0, 0);
            this.KUpanel3.Name = "KUpanel3";
            this.KUpanel3.Size = new System.Drawing.Size(459, 211);
            this.KUpanel3.TabIndex = 4;
            this.KUpanel3.Visible = false;
            // 
            // dataGridViewSelekcija
            // 
            this.dataGridViewSelekcija.AllowUserToAddRows = false;
            this.dataGridViewSelekcija.AllowUserToDeleteRows = false;
            this.dataGridViewSelekcija.AllowUserToResizeColumns = false;
            this.dataGridViewSelekcija.AllowUserToResizeRows = false;
            this.dataGridViewSelekcija.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewSelekcija.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewSelekcija.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewSelekcija.DefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewSelekcija.EnableHeadersVisualStyles = false;
            this.dataGridViewSelekcija.Location = new System.Drawing.Point(0, 0);
            this.dataGridViewSelekcija.Name = "dataGridViewSelekcija";
            this.dataGridViewSelekcija.Size = new System.Drawing.Size(459, 211);
            this.dataGridViewSelekcija.TabIndex = 0;
            this.dataGridViewSelekcija.Click += new System.EventHandler(this.dataGridViewSelekcija_Click);
            // 
            // KreiranjeUzorkaFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(153)))), ((int)(((byte)(153)))));
            this.ClientSize = new System.Drawing.Size(459, 261);
            this.Controls.Add(this.KUpanel3);
            this.Controls.Add(this.KUpanel2);
            this.Controls.Add(this.KUpanel1);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnNext);
            this.MaximumSize = new System.Drawing.Size(475, 300);
            this.MinimumSize = new System.Drawing.Size(475, 300);
            this.Name = "KreiranjeUzorkaFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kreiranje uzorka";
            this.Load += new System.EventHandler(this.KreiranjeUzorkaFrm_Load);
            this.KUpanel1.ResumeLayout(false);
            this.KUpanel1.PerformLayout();
            this.KUpanel2.ResumeLayout(false);
            this.KUpanel2.PerformLayout();
            this.panelPoljaVelicine.ResumeLayout(false);
            this.panelPoljaVelicine.PerformLayout();
            this.KUpanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelekcija)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Panel KUpanel1;
        private System.Windows.Forms.TextBox textBoxImeKolekcije;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel KUpanel2;
        private System.Windows.Forms.Panel panelPoljaVelicine;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBoxDOkb;
        private System.Windows.Forms.TextBox textBoxODkb;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radiobtnVelicina;
        private System.Windows.Forms.RadioButton radiobtnNeparnaSelekcija;
        private System.Windows.Forms.RadioButton radiobtnParnaSelekcija;
        private System.Windows.Forms.RadioButton radiobtnSlobodnaSelekcija;
        private System.Windows.Forms.Panel KUpanel3;
        private System.Windows.Forms.DataGridView dataGridViewSelekcija;
    }
}