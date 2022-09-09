using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FinalGarzon294
{
    public partial class MostrarPacientes : Form
    {
        public List<Paciente> listaPacientes;
        object[] vector = new object[5];



        public MostrarPacientes(List<Paciente> listaPac)
        {
            InitializeComponent();
            listaPacientes = listaPac;
        }

        private void dgvPiezas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void MostrarPacientes_Load(object sender, EventArgs e)
        {

        }
    }
}
