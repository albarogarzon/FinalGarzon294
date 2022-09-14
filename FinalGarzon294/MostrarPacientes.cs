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
        object[] vector = new object[7];
        object[] filaUltima = new object[38];



        public MostrarPacientes(List<Paciente> listaPac,object[] ultimaFila)
        {
            InitializeComponent();
            listaPacientes = listaPac;
            filaUltima = ultimaFila;
        }


        private void MostrarPacientes_Load(object sender, EventArgs e)
        {
            int contadorPacientes = 0;
            double contadorTiempoEnSistema = 0;
            for (int i = 0; i < listaPacientes.Count; i++)
            {
                contadorPacientes++;

                vector[0] = listaPacientes[i].Numero;
                vector[1] = listaPacientes[i].Estado;
                vector[2] = listaPacientes[i].HoraIniAtMedico;
                vector[3] = listaPacientes[i].HoraIniEsperaMedico;
                vector[4] = listaPacientes[i].HoraLlegada;
                vector[5] = listaPacientes[i].HoraSalida;

                if (listaPacientes[i].HoraSalida != 0)
                {
                    contadorTiempoEnSistema += Math.Round((listaPacientes[i].HoraSalida - listaPacientes[i].HoraLlegada), 2);
                }
                else
                {
                    vector[5] = null;
                }
       


                dgvPacientes.Rows.Add(vector);

                                                                                     //AcTAt Pacientes                       //Cant Pacientes At
                double tiempoAtencionPacientes = (double)filaUltima[30] != 0 ? Math.Round(((double)filaUltima[30] / (int)filaUltima[36]), 2): 0;
                lblTAtencion.Text = (tiempoAtencionPacientes).ToString();

                //Variacion de enunciado propia.
                double tiempoPromEnSistema = contadorTiempoEnSistema != 0 ? Math.Round((contadorTiempoEnSistema / (int)filaUltima[36]), 2):0;
                lblTEnSistema.Text = (tiempoPromEnSistema).ToString();
                                                                                        //Ac T Espera At Medico       //Cant Pacientes Fin Espera
                double tiempoEsperaPacientes = (double)filaUltima[31] != 0 ? Math.Round(((double)filaUltima[31] / (int)filaUltima[37]), 2):0;
                lblTEspera.Text = (tiempoEsperaPacientes).ToString();

                double tiempoOcMed1 = Math.Round(((double)filaUltima[32] *100 / (double)filaUltima[1]), 2);
                double tiempoOcMed2 = Math.Round(((double)filaUltima[33] * 100 / (double)filaUltima[1]), 2);
                double tiempoOcMed3 = Math.Round(((double)filaUltima[34] * 100 / (double)filaUltima[1]), 2);
                double tiempoOcMed4 = Math.Round(((double)filaUltima[35] * 100 / (double)filaUltima[1]), 2);
                lblOcMedico1.Text = (tiempoOcMed1).ToString();
                lblOcMedico2.Text = (tiempoOcMed2).ToString();
                lblOcMedico3.Text = (tiempoOcMed3).ToString();
                lblOcMedico4.Text = (tiempoOcMed4).ToString();

            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }
    }
}
