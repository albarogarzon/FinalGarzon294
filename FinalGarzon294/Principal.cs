using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalGarzon294
{
    public partial class Principal : Form
    {
        public Principal()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void btnSimular_Click(object sender, EventArgs e)
        {

            double expPacientes= Int32.Parse(txtPacientesExp.Text);
            int llenadoConstante = Int32.Parse(txtLlenado.Text);
            double expRegistro = Int32.Parse(txtRegistroExp.Text);
            int caminaConstante = Int32.Parse(txtCteEspera.Text);
            int mediaAtMedico = Int32.Parse(txtMediaMedico.Text);
            int desvAtMedico = Int32.Parse(txtDesvMedico.Text);
            double minutos = Int32.Parse(txtMinutos.Text);
            //double expPacientes, int llenadoConstante, double expRegistro, int caminaConstante,int mediaAtMedico, int desvAtMedico, double minutos

            Simulacion simulacion = new Simulacion(expPacientes, llenadoConstante, expRegistro, caminaConstante, mediaAtMedico, desvAtMedico, minutos);
            simulacion.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e) //Con Rango
        {
            double expPacientes = Int32.Parse(txtPacientesExp.Text);
            int llenadoConstante = Int32.Parse(txtLlenado.Text);
            double expRegistro = Int32.Parse(txtRegistroExp.Text);
            int caminaConstante = Int32.Parse(txtCteEspera.Text);
            int mediaAtMedico = Int32.Parse(txtMediaMedico.Text);
            int desvAtMedico = Int32.Parse(txtDesvMedico.Text);
            double minutos = Int32.Parse(txtMinutos.Text);
            double desde = Int32.Parse(txtDesde.Text);
            double hasta = Int32.Parse(txtHasta.Text);
            //double expPacientes, int llenadoConstante, double expRegistro, int caminaConstante,int mediaAtMedico, int desvAtMedico, double minutos

            Simulacion simulacion = new Simulacion(expPacientes, llenadoConstante, expRegistro, caminaConstante, mediaAtMedico, desvAtMedico, minutos,desde,hasta);
            simulacion.ShowDialog();
        }

        private void btn_enunciado_Click(object sender, EventArgs e)
        {
            Enunciado enunciado = new Enunciado();
            enunciado.Show();
        }
    }
}
