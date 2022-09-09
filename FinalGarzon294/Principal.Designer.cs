namespace FinalGarzon294
{
    partial class Principal
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
            this.label4 = new System.Windows.Forms.Label();
            this.txtPacientesExp = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtLlenado = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.btnSimular = new System.Windows.Forms.Button();
            this.txtMinutos = new System.Windows.Forms.TextBox();
            this.txtRegistroExp = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtCteEspera = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtMediaMedico = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.txtDesvMedico = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label1.Location = new System.Drawing.Point(57, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(221, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Llegada de Pacientes";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Calibri", 13F);
            this.label4.Location = new System.Drawing.Point(12, 112);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(174, 22);
            this.label4.TabIndex = 2;
            this.label4.Text = "Exponencial de media:";
            // 
            // txtPacientesExp
            // 
            this.txtPacientesExp.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPacientesExp.Location = new System.Drawing.Point(192, 108);
            this.txtPacientesExp.Name = "txtPacientesExp";
            this.txtPacientesExp.Size = new System.Drawing.Size(146, 26);
            this.txtPacientesExp.TabIndex = 5;
            this.txtPacientesExp.Text = "6";
            this.txtPacientesExp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label5.Location = new System.Drawing.Point(397, 69);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(188, 26);
            this.label5.TabIndex = 6;
            this.label5.Text = "Fin Llenado Form:";
            // 
            // txtLlenado
            // 
            this.txtLlenado.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLlenado.Location = new System.Drawing.Point(512, 112);
            this.txtLlenado.Name = "txtLlenado";
            this.txtLlenado.Size = new System.Drawing.Size(138, 26);
            this.txtLlenado.TabIndex = 8;
            this.txtLlenado.Text = "5";
            this.txtLlenado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Calibri", 13F);
            this.label2.Location = new System.Drawing.Point(392, 112);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(116, 22);
            this.label2.TabIndex = 7;
            this.label2.Text = "Constante de :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label3.Location = new System.Drawing.Point(64, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(129, 26);
            this.label3.TabIndex = 14;
            this.label3.Text = "Fin Registro";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label9.Location = new System.Drawing.Point(472, 312);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(187, 26);
            this.label9.TabIndex = 12;
            this.label9.Text = "Minutos a Simular";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Calibri", 13F);
            this.label10.Location = new System.Drawing.Point(475, 356);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 22);
            this.label10.TabIndex = 10;
            this.label10.Text = "Cantidad:";
            // 
            // btnSimular
            // 
            this.btnSimular.BackColor = System.Drawing.SystemColors.ControlText;
            this.btnSimular.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSimular.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnSimular.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSimular.ForeColor = System.Drawing.Color.Cornsilk;
            this.btnSimular.Location = new System.Drawing.Point(573, 397);
            this.btnSimular.Name = "btnSimular";
            this.btnSimular.Size = new System.Drawing.Size(129, 45);
            this.btnSimular.TabIndex = 18;
            this.btnSimular.Text = "Simular";
            this.btnSimular.UseVisualStyleBackColor = false;
            this.btnSimular.Click += new System.EventHandler(this.btnSimular_Click);
            // 
            // txtMinutos
            // 
            this.txtMinutos.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMinutos.Location = new System.Drawing.Point(561, 352);
            this.txtMinutos.Name = "txtMinutos";
            this.txtMinutos.Size = new System.Drawing.Size(138, 26);
            this.txtMinutos.TabIndex = 17;
            this.txtMinutos.Text = "60";
            this.txtMinutos.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRegistroExp
            // 
            this.txtRegistroExp.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRegistroExp.Location = new System.Drawing.Point(192, 238);
            this.txtRegistroExp.Name = "txtRegistroExp";
            this.txtRegistroExp.Size = new System.Drawing.Size(146, 26);
            this.txtRegistroExp.TabIndex = 20;
            this.txtRegistroExp.Text = "7";
            this.txtRegistroExp.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Calibri", 13F);
            this.label8.Location = new System.Drawing.Point(12, 238);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(174, 22);
            this.label8.TabIndex = 19;
            this.label8.Text = "Exponencial de media:";
            // 
            // txtCteEspera
            // 
            this.txtCteEspera.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCteEspera.Location = new System.Drawing.Point(512, 237);
            this.txtCteEspera.Name = "txtCteEspera";
            this.txtCteEspera.Size = new System.Drawing.Size(138, 26);
            this.txtCteEspera.TabIndex = 23;
            this.txtCteEspera.Text = "2";
            this.txtCteEspera.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCteEspera.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Calibri", 13F);
            this.label6.Location = new System.Drawing.Point(392, 237);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(116, 22);
            this.label6.TabIndex = 22;
            this.label6.Text = "Constante de :";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label7.Location = new System.Drawing.Point(397, 194);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(262, 26);
            this.label7.TabIndex = 21;
            this.label7.Text = "Llegada a Sala de Espera";
            this.label7.Click += new System.EventHandler(this.label7_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Calibri", 13F);
            this.label11.Location = new System.Drawing.Point(656, 112);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 22);
            this.label11.TabIndex = 24;
            this.label11.Text = "(Min)";
            this.label11.Click += new System.EventHandler(this.label11_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Calibri", 13F);
            this.label12.Location = new System.Drawing.Point(338, 108);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(48, 22);
            this.label12.TabIndex = 25;
            this.label12.Text = "(Min)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Calibri", 13F);
            this.label13.Location = new System.Drawing.Point(338, 239);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(48, 22);
            this.label13.TabIndex = 26;
            this.label13.Text = "(Min)";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Calibri", 13F);
            this.label14.Location = new System.Drawing.Point(651, 238);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(48, 22);
            this.label14.TabIndex = 27;
            this.label14.Text = "(Min)";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Calibri", 13F);
            this.label15.Location = new System.Drawing.Point(705, 353);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(48, 22);
            this.label15.TabIndex = 28;
            this.label15.Text = "(Min)";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Calibri", 13F);
            this.label16.Location = new System.Drawing.Point(338, 337);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(48, 22);
            this.label16.TabIndex = 32;
            this.label16.Text = "(Min)";
            // 
            // txtMediaMedico
            // 
            this.txtMediaMedico.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMediaMedico.Location = new System.Drawing.Point(192, 336);
            this.txtMediaMedico.Name = "txtMediaMedico";
            this.txtMediaMedico.Size = new System.Drawing.Size(146, 26);
            this.txtMediaMedico.TabIndex = 31;
            this.txtMediaMedico.Text = "20";
            this.txtMediaMedico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Calibri", 13F);
            this.label17.Location = new System.Drawing.Point(63, 337);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 22);
            this.label17.TabIndex = 30;
            this.label17.Text = "Normal Media:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F);
            this.label18.Location = new System.Drawing.Point(64, 292);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(145, 26);
            this.label18.TabIndex = 29;
            this.label18.Text = "Fin At Medico";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Calibri", 13F);
            this.label19.Location = new System.Drawing.Point(65, 375);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(118, 22);
            this.label19.TabIndex = 33;
            this.label19.Text = "Desv Estandar:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Calibri", 13F);
            this.label20.Location = new System.Drawing.Point(338, 376);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(48, 22);
            this.label20.TabIndex = 35;
            this.label20.Text = "(Min)";
            // 
            // txtDesvMedico
            // 
            this.txtDesvMedico.Font = new System.Drawing.Font("Calibri", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDesvMedico.Location = new System.Drawing.Point(192, 375);
            this.txtDesvMedico.Name = "txtDesvMedico";
            this.txtDesvMedico.Size = new System.Drawing.Size(146, 26);
            this.txtDesvMedico.TabIndex = 34;
            this.txtDesvMedico.Text = "3";
            this.txtDesvMedico.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Principal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.txtDesvMedico);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.txtMediaMedico);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtCteEspera);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtRegistroExp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.btnSimular);
            this.Controls.Add(this.txtMinutos);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtLlenado);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtPacientesExp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label1);
            this.Name = "Principal";
            this.Text = "Principal";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPacientesExp;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtLlenado;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnSimular;
        private System.Windows.Forms.TextBox txtMinutos;
        private System.Windows.Forms.TextBox txtRegistroExp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtCteEspera;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtMediaMedico;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox txtDesvMedico;
    }
}