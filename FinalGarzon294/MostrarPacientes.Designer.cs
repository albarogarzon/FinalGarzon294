namespace FinalGarzon294
{
    partial class MostrarPacientes
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dgvPiezas = new System.Windows.Forms.DataGridView();
            this.pacNumero = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.estadoFinal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.horaIniAt = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.horaIniEspera = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPiezas)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvPiezas
            // 
            this.dgvPiezas.AllowUserToAddRows = false;
            this.dgvPiezas.AllowUserToDeleteRows = false;
            this.dgvPiezas.AllowUserToOrderColumns = true;
            this.dgvPiezas.AllowUserToResizeRows = false;
            this.dgvPiezas.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPiezas.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.MediumTurquoise;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Calibri", 15F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.Red;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvPiezas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvPiezas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPiezas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.pacNumero,
            this.estadoFinal,
            this.horaIniAt,
            this.horaIniEspera});
            this.dgvPiezas.Cursor = System.Windows.Forms.Cursors.Arrow;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Calibri", 12F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvPiezas.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvPiezas.EnableHeadersVisualStyles = false;
            this.dgvPiezas.Location = new System.Drawing.Point(12, 12);
            this.dgvPiezas.Name = "dgvPiezas";
            this.dgvPiezas.ReadOnly = true;
            this.dgvPiezas.RowHeadersWidth = 20;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Calibri", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dgvPiezas.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvPiezas.Size = new System.Drawing.Size(959, 411);
            this.dgvPiezas.TabIndex = 1;
            this.dgvPiezas.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPiezas_CellContentClick);
            // 
            // pacNumero
            // 
            this.pacNumero.HeaderText = "Paciente Nro";
            this.pacNumero.Name = "pacNumero";
            this.pacNumero.ReadOnly = true;
            // 
            // estadoFinal
            // 
            this.estadoFinal.HeaderText = "Estado Final";
            this.estadoFinal.Name = "estadoFinal";
            this.estadoFinal.ReadOnly = true;
            // 
            // horaIniAt
            // 
            this.horaIniAt.HeaderText = "Hora Ini At";
            this.horaIniAt.Name = "horaIniAt";
            this.horaIniAt.ReadOnly = true;
            // 
            // horaIniEspera
            // 
            this.horaIniEspera.HeaderText = "Hora Ini Espera";
            this.horaIniEspera.Name = "horaIniEspera";
            this.horaIniEspera.ReadOnly = true;
            // 
            // MostrarPacientes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(983, 450);
            this.Controls.Add(this.dgvPiezas);
            this.Name = "MostrarPacientes";
            this.Text = "MostrarPacientes";
            this.Load += new System.EventHandler(this.MostrarPacientes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPiezas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvPiezas;
        private System.Windows.Forms.DataGridViewTextBoxColumn pacNumero;
        private System.Windows.Forms.DataGridViewTextBoxColumn estadoFinal;
        private System.Windows.Forms.DataGridViewTextBoxColumn horaIniAt;
        private System.Windows.Forms.DataGridViewTextBoxColumn horaIniEspera;
    }
}