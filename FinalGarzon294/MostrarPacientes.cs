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
            int contadorPacientes = 0;
            for (int i = 0; i < listaPacientes.Count; i++)
            {
                contadorPacientes++;
                vector[0] = listaPacientes[i].Numero;
                vector[1] = listaPacientes[i].Estado;
                vector[2] = listaPacientes[i].HoraIniAtMedico;
                vector[3] = listaPacientes[i].HoraIniEsperaMedico;
                //if (listaPacientes[i].HoraFinal != 0)
                //{
                //    vector[4] = Math.Round((listaPacientes[i].HoraFinal - listaPacientes[i].HoraLlegada), 2);
                //}
                //else
                //{
                //    vector[4] = null;
                //}
                dgvPacientes.Rows.Add(vector);

            }
        }
    }
}
