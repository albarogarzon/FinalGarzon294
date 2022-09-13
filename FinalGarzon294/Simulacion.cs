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
    public partial class Simulacion : Form
    {

        private double expPac;
        private int llenadoCte;
        private double expReg;
        private int caminaCte;
        private int mediaAtMed;
        private int desvAtMed;
        private double min;
        private double inicio;
        private double fin;

        double[] resultadoVariablesEstadisticas = new double[9];

        List<Paciente> listaPacientes = new List<Paciente>();

        public Simulacion(double expPacientes, int llenadoConstante, double expRegistro, int caminaConstante,int mediaAtMedico, int desvAtMedico, double minutos, double start=0,double end= 0)
        {
            InitializeComponent();
            expPac = expPacientes;
            llenadoCte = llenadoConstante;
            expReg = expRegistro;
            caminaCte = caminaConstante;
            mediaAtMed = mediaAtMedico;
            desvAtMed = desvAtMedico;
            min = minutos;
            inicio = start;
            fin = end > 0 ? (end > min ? min : end) : min;
        }

        Random rnd = new Random();
        object[] ultimaFilaParaEstadistica = new Object[38];
        private void Simulacion_Load(object sender, EventArgs e)
        {
            

            object[] vectorViejo = new Object[38];
            object[] vectorNuevo = new Object[38];
            object[] vectorPenulitmo = new Object[38];

            double RndLlegada = GenerarRandom();
            double TLlegada = Math.Round(Exponencial(expPac, RndLlegada), 2);
            object[] vectorInicio = Inicio(RndLlegada,TLlegada);

            //Asigno los valores iniciales
            vectorViejo = vectorInicio;
            dgvUrgencias.Rows.Add(vectorViejo);

            double tiempo = 0;
            int contandor = 0; //Contador Pacientes que llegaron
            int contadorAt = 0; //Contador Pacientes con atencion medica finalizada

            int contadorSalaEspera = 0;//pacientes que llegaron a sala de espera

            int contadorFinEsperaAtMedico = 0; //Pacientes que finalizaron la espera para at medica

            while (tiempo < min)
            {
                double[] reloj = new double[2];
                reloj = proximoEvento(vectorViejo);

                //asigno el nuevo clock
                vectorNuevo[1] = Math.Round((double)reloj[0], 2);



                //Ahora veo cual es la casilla que me genero el nuevo clock
                //para poder calculcar todo lo que sucede
                int valorCase = (int)reloj[1];

                switch (valorCase)
                {
//--------------------------------------------------------------------------------------------------------------------------
                    case 4:
                        //Significa que hay una nueva llegada de paciente
                        //Creo el paciente
                        contandor +=1;
                        Paciente paciente = new Paciente();
                        paciente.Numero = contandor;
                        paciente.Estado = null;
                        paciente.HoraLlegada = (double)vectorNuevo[1];
                        listaPacientes.Add((Paciente) paciente);

                        vectorNuevo[0] = "LlegPac";

                        //Me fijo si llenado forma esta libre
                        if ((string)vectorViejo[18] == "LI")
                        {
                            //Le cambio el estado de atencion llenado
                            //listaPacientes[contadorAt].Estado = "Llenando Form"; //Ver
                                                                                 
                           // contadorAt++;

                            //el servidor llenado form se ocupa
                            vectorNuevo[18] = "OC";
                            //La cola sigue igual( en cero ) 
                            vectorNuevo[19] = 0;
                            //Calculo el fin de llenado
                            vectorNuevo[5] = llenadoCte + (double)vectorNuevo[1];
                            //Los otros valores quedan iguales

                            vectorNuevo[6] = null;//rnd Registro
                            vectorNuevo[7] = null;//T registro
                            vectorNuevo[8] = vectorViejo[8];//Fin Reg 1
                            vectorNuevo[9] = vectorViejo[9];//Fin Reg 2
                            vectorNuevo[10] = vectorViejo[10];//Llegada Sala espera
                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//T at Med
                            vectorNuevo[14] = vectorViejo[14];//Fin At Medico1
                            vectorNuevo[15] = vectorViejo[15];//Fin At Medico2
                            vectorNuevo[16] = vectorViejo[16];//Fin At Medico3
                            vectorNuevo[17] = vectorViejo[17];//Fin At Medico4
                            vectorNuevo[20] = vectorViejo[20];//Estado Empleado 1
                            vectorNuevo[21] = vectorViejo[21];//Estado Empleado 2
                            vectorNuevo[22] = vectorViejo[22];//Cola Registro
                            vectorNuevo[23] = vectorViejo[23];//Estado PAsillo Sala Espera
                            vectorNuevo[24] = vectorViejo[24];//Cola Pasillo Sala Espera
                            vectorNuevo[25] = vectorViejo[25];//Estado Medico 1
                            vectorNuevo[26] = vectorViejo[26];//Estado Medico 2
                            vectorNuevo[27] = vectorViejo[27];//Estado Medico 3
                            vectorNuevo[28] = vectorViejo[28];//Estado Medico 4
                            vectorNuevo[29] = vectorViejo[29];//Cola Atencion medico
                            vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                            vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                            vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                            vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                            vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                            vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4
                            vectorNuevo[36] = vectorViejo[36];//Cant Pacientes At
                            vectorNuevo[37] = vectorViejo[37];//Cant Pacientes Fin Espera Medica


                        }
                        else
                        {
                            //Lo agrego a la cola y dejo en el mismo estado 
                            vectorNuevo[18] = vectorViejo[18];
                            vectorNuevo[19] = (int)vectorViejo[19] + 1;

                            //Los otros valores quedan iguales
                            vectorNuevo[5] = vectorViejo[5];//Fin llenado form

                            vectorNuevo[6] = null;//rnd Registro
                            vectorNuevo[7] = null;//T registro
                            vectorNuevo[8] = vectorViejo[8];//Fin Reg 1
                            vectorNuevo[9] = vectorViejo[9];//Fin Reg 2
                            vectorNuevo[10] = vectorViejo[10];//Llegada Sala espera
                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//T at Med
                            vectorNuevo[14] = vectorViejo[14];//Fin At Medico1
                            vectorNuevo[15] = vectorViejo[15];//Fin At Medico2
                            vectorNuevo[16] = vectorViejo[16];//Fin At Medico3
                            vectorNuevo[17] = vectorViejo[17];//Fin At Medico4
                            vectorNuevo[20] = vectorViejo[20];//Estado Empleado 1
                            vectorNuevo[21] = vectorViejo[21];//Estado Empleado 2
                            vectorNuevo[22] = vectorViejo[22];//Cola Registro
                            vectorNuevo[23] = vectorViejo[23];//Estado PAsillo Sala Espera
                            vectorNuevo[24] = vectorViejo[24];//Cola Pasillo Sala Espera
                            vectorNuevo[25] = vectorViejo[25];//Estado Medico 1
                            vectorNuevo[26] = vectorViejo[26];//Estado Medico 2
                            vectorNuevo[27] = vectorViejo[27];//Estado Medico 3
                            vectorNuevo[28] = vectorViejo[28];//Estado Medico 4
                            vectorNuevo[29] = vectorViejo[29];//Cola Atencion medico
                            vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                            vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                            vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                            vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                            vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                            vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4
                            vectorNuevo[36] = vectorViejo[36];//Cant Pacientes At
                            vectorNuevo[37] = vectorViejo[37];//Cant Pacientes Fin Espera Medica

                        }
                        //calculo la proxima llegada

                        double RNDlleg = GenerarRandom();
                        vectorNuevo[2] = RNDlleg;//RND lleg
                        vectorNuevo[3] = Math.Round(Exponencial(expPac, RNDlleg), 2);//T lleg
                        vectorNuevo[4] = (double)vectorNuevo[1] + (double)vectorNuevo[3]; ;//prox lleg
                        break;
 //--------------------------------------------------------------------------------------------------------------------------
                    case 5: //Fin llenado Forma

                        vectorNuevo[0] = "FinLlenForm";
                        //Pregunto si el Empleado de Registro 1 esta libre
                        if ((string)vectorViejo[20] == "LI")
                        {
                            vectorNuevo[20] = "OC"; //Paso a ocupado
                            vectorNuevo[22] = 0; //Cola en 0

                            double rndRegistro = GenerarRandom();

                            vectorNuevo[6] = Math.Round(rndRegistro, 2); //Rnd Reg
                            //Calculo el tiempo de registro 
                            vectorNuevo[7] = Math.Round(Exponencial(expReg, rndRegistro), 2);
                            //Calculo el fin de registro 1
                            vectorNuevo[8] = (double)vectorNuevo[1] + (double)vectorNuevo[7];

                        }
                        else if ((string)vectorViejo[21] == "LI")
                        {
                            vectorNuevo[21] = "OC"; //Paso a ocupado
                            vectorNuevo[22] = 0; //Cola en 0

                            double rndRegistro = GenerarRandom();

                            vectorNuevo[6] = Math.Round(rndRegistro, 2); //Rnd Reg
                            //Calculo el tiempo de registro 
                            vectorNuevo[7] = Math.Round(Exponencial(expReg, rndRegistro), 2);
                            //Calculo el fin de registro 2
                            vectorNuevo[9] = (double)vectorNuevo[1] + (double)vectorNuevo[7];
                        }else
                        {
                            //Lo agrego a la cola y dejo en el mismo estado a los empleados
                            vectorNuevo[20] = vectorViejo[20];
                            vectorNuevo[21] = vectorViejo[21];
                            vectorNuevo[22] = (int)vectorViejo[22] + 1;

                            vectorNuevo[6] = null;
                            vectorNuevo[7] = null;
                            vectorNuevo[9] = vectorViejo[9]; //Solo FinReg 2 mantengo? o Fin reg 1 tmb [8]?
                        }

                        // como termino un llenado forma, si tengo pacientes en cola, permito llenado form
                        if ((int)vectorViejo[19] >= 1) //Cola Llenado form
                        {
                            vectorNuevo[19] = (int)vectorViejo[19] - 1;
                            vectorNuevo[18] = vectorViejo[18]; //Estado sigue siendo OC de llenado form
                            

                            //Debo calcularle al nuevo paciente, fin llenado
                            //Calculo el fin de llenado
                            vectorNuevo[5] = llenadoCte + (double)vectorNuevo[1];

                            //Dejo los otros valores sin usar
                            vectorNuevo[2] = null;
                            vectorNuevo[3] = null;
                        }
                        else //La cola llenado form esta en 0
                        {
                            vectorNuevo[18] = "LI";
                            vectorNuevo[19] = vectorViejo[19];//La cola se mantiene en 0

                            vectorNuevo[2] = null;
                            vectorNuevo[3] = null;
                            vectorNuevo[5] = null; //Fin llenado

                        }
                        vectorNuevo[11] = null;
                        vectorNuevo[12] = null;
                        vectorNuevo[13] = null;


                        break;
 //--------------------------------------------------------------------------------------------------------------------------
                    case 8: //Fin Registro 1

                        vectorNuevo[0] = "FinReg1";

                        //Me fijo si Sala Espera esta esta libre
                        if ((string)vectorViejo[23] == "LI")
                        {
                            //Le cambio el estado a la sala
                            //listaPacientes[contadorAt].Estado = "En Camino Sala"; 

                            // contadorAt++;

                            //el servidor pasillo sala espera se ocupa
                            vectorNuevo[23] = "OC";
                            //La cola sigue igual( en cero ) 
                            vectorNuevo[24] = 0;

                            //Coloco tiempo en llegar a sala de espera
                            vectorNuevo[10] = caminaCte + (double)vectorNuevo[1]; //OJO
                            //Los otros valores quedan iguales

                            vectorNuevo[6] = null;//rnd Registro
                            vectorNuevo[7] = null;//T registro
                            vectorNuevo[8] = vectorViejo[8];//Fin Reg 1
                            vectorNuevo[9] = vectorViejo[9];//Fin Reg 2
                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//T at Med
                            vectorNuevo[14] = vectorViejo[14];//Fin At Medico1
                            vectorNuevo[15] = vectorViejo[15];//Fin At Medico2
                            vectorNuevo[16] = vectorViejo[16];//Fin At Medico3
                            vectorNuevo[17] = vectorViejo[17];//Fin At Medico4
                            vectorNuevo[20] = vectorViejo[20];//Estado Empleado 1
                            vectorNuevo[21] = vectorViejo[21];//Estado Empleado 2
                            vectorNuevo[22] = vectorViejo[22];//Cola Registro

                            vectorNuevo[25] = vectorViejo[25];//Estado Medico 1
                            vectorNuevo[26] = vectorViejo[26];//Estado Medico 2
                            vectorNuevo[27] = vectorViejo[27];//Estado Medico 3
                            vectorNuevo[28] = vectorViejo[28];//Estado Medico 4
                            vectorNuevo[29] = vectorViejo[29];//Cola Atencion medico
                            vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                            vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                            vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                            vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                            vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                            vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4
                            vectorNuevo[36] = vectorViejo[36];//Cant Pacientes At
                            vectorNuevo[37] = vectorViejo[37];//Cant Pacientes Fin Espera Medica

                        }
                        else
                        {
                            //Lo agrego a la cola y dejo en el mismo estado 
                            vectorNuevo[23] = vectorViejo[23];
                            vectorNuevo[24] = (int)vectorViejo[24] + 1;

                            //Los otros valores quedan iguales
                            vectorNuevo[10] = vectorViejo[10];//Fin Camino Sala espera

                            vectorNuevo[6] = vectorViejo[6];//rnd Registro
                            vectorNuevo[7] = vectorViejo[7];//T registro
                            vectorNuevo[8] = vectorViejo[8];//Fin Reg 1
                            vectorNuevo[9] = vectorViejo[9];//Fin Reg 2
                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//T at Med
                            vectorNuevo[14] = vectorViejo[14];//Fin At Medico1
                            vectorNuevo[15] = vectorViejo[15];//Fin At Medico2
                            vectorNuevo[16] = vectorViejo[16];//Fin At Medico3
                            vectorNuevo[17] = vectorViejo[17];//Fin At Medico4
                            vectorNuevo[20] = vectorViejo[20];//Estado Empleado 1
                            vectorNuevo[21] = vectorViejo[21];//Estado Empleado 2
                            vectorNuevo[22] = vectorViejo[22];//Cola Registro
                            vectorNuevo[25] = vectorViejo[25];//Estado Medico 1
                            vectorNuevo[26] = vectorViejo[26];//Estado Medico 2
                            vectorNuevo[27] = vectorViejo[27];//Estado Medico 3
                            vectorNuevo[28] = vectorViejo[28];//Estado Medico 4
                            vectorNuevo[29] = vectorViejo[29];//Cola Atencion medico
                            vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                            vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                            vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                            vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                            vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                            vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4
                            vectorNuevo[36] = vectorViejo[36];//Cant Pacientes At
                            vectorNuevo[37] = vectorViejo[37];//Cant Pacientes Fin Espera Medica

                        }

                        //Si hay pacientes en cola de regitro , atiendo
                        if ((int)vectorViejo[22] >= 1)
                        {
                            //Resto a la cola
                            vectorNuevo[22] = (int)vectorViejo[22] - 1;
                            //Mantengo el estado de ocupado
                            vectorNuevo[20] = vectorViejo[20]; //Estado Reg 1

                            //Calulo tiempo regitro al paciente 
                            double rndRegistro = GenerarRandom();

                            vectorNuevo[6] = Math.Round(rndRegistro, 2); //Rnd Reg
                            //Calculo el tiempo de registro 
                            vectorNuevo[7] = Math.Round(Exponencial(expReg, rndRegistro), 2);
                            //Calculo el fin de registro 1
                            vectorNuevo[8] = (double)vectorNuevo[1] + (double)vectorNuevo[7];



                        }
                        else //Cola en 0 de registro
                        {
                            //Mantengo cola 
                            vectorNuevo[22] = vectorViejo[22];
                            vectorNuevo[20] = "LI";

                            vectorNuevo[6] = null;
                            vectorNuevo[7] = null;
                            vectorNuevo[8] = null;

                        }
                        //Campos sin modificar
                        vectorNuevo[2] = null;
                        vectorNuevo[3] = null;
                        vectorNuevo[4] = vectorViejo[4]; //Mantengo prox lleg
                        vectorNuevo[5] = vectorViejo[5];//Fin llenado form

                        vectorNuevo[9] = vectorViejo[9];//Fin Reg 2

                        vectorNuevo[11] = null;
                        vectorNuevo[12] = null;
                        vectorNuevo[13] = null;
                        vectorNuevo[14] = vectorViejo[14];//Fin At Medico1
                        vectorNuevo[15] = vectorViejo[15];//Fin At Medico2
                        vectorNuevo[16] = vectorViejo[16];//Fin At Medico3
                        vectorNuevo[17] = vectorViejo[17];//Fin At Medico4

                        vectorNuevo[21] = vectorViejo[21];//Estado Empleado 2

                        vectorNuevo[25] = vectorViejo[25];//Estado Medico 1
                        vectorNuevo[26] = vectorViejo[26];//Estado Medico 2
                        vectorNuevo[27] = vectorViejo[27];//Estado Medico 3
                        vectorNuevo[28] = vectorViejo[28];//Estado Medico 4
                        vectorNuevo[29] = vectorViejo[29];//Cola Atencion medico
                        vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                        vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                        vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                        vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                        vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                        vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4
                        vectorNuevo[36] = vectorViejo[36];//Cant Pacientes At
                        vectorNuevo[37] = vectorViejo[37];//Cant Pacientes Fin Espera Medica

                        break;
 //--------------------------------------------------------------------------------------------------------------------------
                    case 9: //Fin Registro 2
                        vectorNuevo[0] = "FinReg2";


                        //Me fijo si Sala Espera esta esta libre
                        if ((string)vectorViejo[23] == "LI")
                        {
                            //Le cambio el estado a la sala
                            //listaPacientes[contadorAt].Estado = "En Camino Sala"; 

                            // contadorAt++;

                            //el servidor pasillo sala espera se ocupa
                            vectorNuevo[23] = "OC";
                            //La cola sigue igual( en cero ) 
                            vectorNuevo[24] = 0;

                            //Coloco tiempo en llegar a sala de espera
                            vectorNuevo[10] = caminaCte + (double)vectorNuevo[1]; //OJO
                            //Los otros valores quedan iguales

                            vectorNuevo[6] = null;//rnd Registro
                            vectorNuevo[7] = null;//T registro
                            vectorNuevo[8] = vectorViejo[8];//Fin Reg 1
                            vectorNuevo[9] = vectorViejo[9];//Fin Reg 2
                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//T at Med
                            vectorNuevo[14] = vectorViejo[14];//Fin At Medico1
                            vectorNuevo[15] = vectorViejo[15];//Fin At Medico2
                            vectorNuevo[16] = vectorViejo[16];//Fin At Medico3
                            vectorNuevo[17] = vectorViejo[17];//Fin At Medico4
                            vectorNuevo[20] = vectorViejo[20];//Estado Empleado 1
                            vectorNuevo[21] = vectorViejo[21];//Estado Empleado 2
                            vectorNuevo[22] = vectorViejo[22];//Cola Registro
                            vectorNuevo[25] = vectorViejo[25];//Estado Medico 1
                            vectorNuevo[26] = vectorViejo[26];//Estado Medico 2
                            vectorNuevo[27] = vectorViejo[27];//Estado Medico 3
                            vectorNuevo[28] = vectorViejo[28];//Estado Medico 4
                            vectorNuevo[29] = vectorViejo[29];//Cola Atencion medico
                            vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                            vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                            vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                            vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                            vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                            vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4
                            vectorNuevo[36] = vectorViejo[36];//Cant Pacientes At
                            vectorNuevo[37] = vectorViejo[37];//Cant Pacientes Fin Espera Medica

                        }
                        else
                        {
                            //Lo agrego a la cola y dejo en el mismo estado 
                            vectorNuevo[23] = vectorViejo[23]; 
                            vectorNuevo[24] = (int)vectorViejo[24] + 1;

                            //Los otros valores quedan iguales
                            vectorNuevo[10] = vectorViejo[10];//Fin Camino Sala espera

                            vectorNuevo[6] = vectorViejo[6];//rnd Registro
                            vectorNuevo[7] = vectorViejo[7];//T registro
                            vectorNuevo[8] = vectorViejo[8];//Fin Reg 1
                            vectorNuevo[9] = vectorViejo[9];//Fin Reg 2
                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//T at Med
                            vectorNuevo[14] = vectorViejo[14];//Fin At Medico1
                            vectorNuevo[15] = vectorViejo[15];//Fin At Medico2
                            vectorNuevo[16] = vectorViejo[16];//Fin At Medico3
                            vectorNuevo[17] = vectorViejo[17];//Fin At Medico4
                            vectorNuevo[20] = vectorViejo[20];//Estado Empleado 1
                            vectorNuevo[21] = vectorViejo[21];//Estado Empleado 2
                            vectorNuevo[22] = vectorViejo[22];//Cola Registro
                            vectorNuevo[25] = vectorViejo[25];//Estado Medico 1
                            vectorNuevo[26] = vectorViejo[26];//Estado Medico 2
                            vectorNuevo[27] = vectorViejo[27];//Estado Medico 3
                            vectorNuevo[28] = vectorViejo[28];//Estado Medico 4
                            vectorNuevo[29] = vectorViejo[29];//Cola Atencion medico
                            vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                            vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                            vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                            vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                            vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                            vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4
                            vectorNuevo[36] = vectorViejo[36];//Cant Pacientes At
                            vectorNuevo[37] = vectorViejo[37];//Cant Pacientes Fin Espera Medica

                        }

                        //Si hay pacientes en cola de regitro , atiendo
                        if ((int)vectorViejo[22] >= 1)
                        {
                            //Resto a la cola
                            vectorNuevo[22] = (int)vectorViejo[22] - 1;
                            //Mantengo el estado de ocupado
                            vectorNuevo[21] = vectorViejo[20]; //Estado Reg 1

                            //Calulo tiempo regitro al paciente 
                            double rndRegistro = GenerarRandom();

                            vectorNuevo[6] = Math.Round(rndRegistro, 2); //Rnd Reg
                            //Calculo el tiempo de registro 
                            vectorNuevo[7] = Math.Round(Exponencial(expReg, rndRegistro), 2);
                            //Calculo el fin de registro 1
                            vectorNuevo[9] = (double)vectorNuevo[1] + (double)vectorNuevo[7];



                        }
                        else //Cola en 0 de registro
                        {
                            //Mantengo cola 
                            vectorNuevo[22] = vectorViejo[22];
                            vectorNuevo[21] = "LI";

                            vectorNuevo[6] = null;
                            vectorNuevo[7] = null;
                            vectorNuevo[9] = null; //null? Fin reg 2

                        }
                        //Campos sin modificar
                        vectorNuevo[2] = null;
                        vectorNuevo[3] = null;
                        vectorNuevo[4] = vectorViejo[4]; //Mantengo prox lleg
                        vectorNuevo[5] = vectorViejo[5];//Fin llenado form

                        vectorNuevo[8] = vectorViejo[8];//Fin Reg 1

                        vectorNuevo[11] = vectorViejo[11];//rnd1
                        vectorNuevo[12] = vectorViejo[12];//rnd2
                        vectorNuevo[13] = vectorViejo[13];//T at Med
                        vectorNuevo[14] = vectorViejo[14];//Fin At Medico1
                        vectorNuevo[15] = vectorViejo[15];//Fin At Medico2
                        vectorNuevo[16] = vectorViejo[16];//Fin At Medico3
                        vectorNuevo[17] = vectorViejo[17];//Fin At Medico4
                        vectorNuevo[20] = vectorViejo[20];//Estado Empleado 1


                        vectorNuevo[25] = vectorViejo[25];//Estado Medico 1
                        vectorNuevo[26] = vectorViejo[26];//Estado Medico 2
                        vectorNuevo[27] = vectorViejo[27];//Estado Medico 3
                        vectorNuevo[28] = vectorViejo[28];//Estado Medico 4
                        vectorNuevo[29] = vectorViejo[29];//Cola Atencion medico
                        vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                        vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                        vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                        vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                        vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                        vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4
                        vectorNuevo[36] = vectorViejo[36];//Cant Pacientes At
                        vectorNuevo[37] = vectorViejo[37];//Cant Pacientes Fin Espera Medica

                        break;





//--------------------------------------------------------------------------------------------------------------------------
                        case 10: //Llegada Sala Espera Medico
                                 //Debe generar los rnd para la atencion de los medicos(Normal)
                                 //Pasar al medico a Ocupado
                                 //Limpiar el valor de la sala de espera
                                 //Registrar en Pacientes Estado "SAM1,2,3,4" y Hora Ini At 


                        vectorNuevo[0] = "LlegadaSalaEsp";
                        
         


                        //Pregunto si el Medico1  esta libre
                        if ((string)vectorViejo[25] == "LI")
                        {
                            vectorNuevo[25] = "OC"; //Paso a ocupado
                            vectorNuevo[29] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed,desvAtMed,rndNorm1,rndNorm2), 2);
                            //Calculo el fin de at medico 1
                            vectorNuevo[14] = (double)vectorNuevo[1] + (double)vectorNuevo[13];


                            listaPacientes[contadorSalaEspera].HoraIniAtMedico = (double)vectorNuevo[1];
                            listaPacientes[contadorSalaEspera].Estado = "SAM1";
                            contadorSalaEspera++;

                        }
                        else if ((string)vectorViejo[26] == "LI") //Medico 2 libre
                        {
                            vectorNuevo[26] = "OC"; //Paso a ocupado
                            vectorNuevo[29] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 2
                            vectorNuevo[15] = (double)vectorNuevo[1] + (double)vectorNuevo[13];

                            listaPacientes[contadorSalaEspera].HoraIniAtMedico = (double)vectorNuevo[1];
                            listaPacientes[contadorSalaEspera].Estado = "SAM2";
                            contadorSalaEspera++;
                        }
                        else if ((string)vectorViejo[27] == "LI") //Medico 3 libre
                        {
                            vectorNuevo[27] = "OC"; //Paso a ocupado
                            vectorNuevo[29] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 3
                            vectorNuevo[16] = (double)vectorNuevo[1] + (double)vectorNuevo[13];


                            listaPacientes[contadorSalaEspera].HoraIniAtMedico = (double)vectorNuevo[1];
                            listaPacientes[contadorSalaEspera].Estado = "SAM3";
                            contadorSalaEspera++;
                        }
                        else if ((string)vectorViejo[28] == "LI") //Medico 4 libre
                        {
                            vectorNuevo[28] = "OC"; //Paso a ocupado
                            vectorNuevo[29] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 4
                            vectorNuevo[17] = (double)vectorNuevo[1] + (double)vectorNuevo[13];

                            listaPacientes[contadorSalaEspera].HoraIniAtMedico = (double)vectorNuevo[1];
                            listaPacientes[contadorSalaEspera].Estado = "SAM4";
                            contadorSalaEspera++;
                        }
                        else //Si los 4 estan ocupados
                        {
                            //Lo agrego a la cola y dejo en el mismo estado a los medicos
                            vectorNuevo[25] = vectorViejo[25];
                            vectorNuevo[26] = vectorViejo[26];
                            vectorNuevo[27] = vectorViejo[27];
                            vectorNuevo[28] = vectorViejo[28];

                            vectorNuevo[29] = (int)vectorViejo[29] + 1; //Cola para at medicos

                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//t at

                            vectorNuevo[14] = vectorViejo[14];
                            vectorNuevo[15] = vectorViejo[15];
                            vectorNuevo[16] = vectorViejo[16];
                            vectorNuevo[17] = vectorViejo[17];

                            listaPacientes[contadorSalaEspera].HoraIniEsperaMedico = (double)vectorNuevo[1];
                            listaPacientes[contadorSalaEspera].Estado = "EAMedico";
                            contadorSalaEspera++;

                        }



                        //Si hay pacientes en cola de pasillo sala espera , atiendo
                        if ((int)vectorViejo[24] >= 1)
                        {
                            //Resto a la cola
                            vectorNuevo[24] = (int)vectorViejo[24] - 1;
                            //Mantengo el estado de ocupado
                            vectorNuevo[23] = vectorViejo[23]; //Estado Pasillo sala espera

                            //Calulo tiempo llegada sala espera al paciente 
                          
                            vectorNuevo[10] = caminaCte + (double)vectorNuevo[1];



                        }
                        else //Cola en 0 de pasillo sala espera
                        {
                            //Mantengo cola 
                            vectorNuevo[24] = vectorViejo[24];
                            vectorNuevo[23] = "LI";

                            vectorNuevo[6] = null;
                            vectorNuevo[7] = null;
                            vectorNuevo[10] = null; //null? llegada pasillo sala espera

                        }
                        //Campos sin modificar
                        vectorNuevo[2] = null;
                        vectorNuevo[3] = null;
                        vectorNuevo[4] = vectorViejo[4]; //Mantengo prox lleg
                        vectorNuevo[5] = vectorViejo[5];//Fin llenado form

                        vectorNuevo[8] = vectorViejo[8];//Fin Reg 1

                        //vectorNuevo[11] = vectorViejo[11];//rnd1
                        //vectorNuevo[12] = vectorViejo[12];//rnd2
                        //vectorNuevo[13] = vectorViejo[13];//T at Med
                        //vectorNuevo[14] = vectorViejo[14];//Fin At Medico1
                        //vectorNuevo[15] = vectorViejo[15];//Fin At Medico2
                        //vectorNuevo[16] = vectorViejo[16];//Fin At Medico3
                        //vectorNuevo[17] = vectorViejo[17];//Fin At Medico4
                        vectorNuevo[20] = vectorViejo[20];//Estado Empleado 1


                        //vectorNuevo[25] = vectorViejo[25];//Estado Medico 1
                        //vectorNuevo[26] = vectorViejo[26];//Estado Medico 2
                        //vectorNuevo[27] = vectorViejo[27];//Estado Medico 3
                        //vectorNuevo[28] = vectorViejo[28];//Estado Medico 4
                        //vectorNuevo[29] = vectorViejo[29];//Cola Atencion medico
                        vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                        vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                        vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                        vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                        vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                        vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4
                        vectorNuevo[36] = vectorViejo[36];//Cant Pacientes At
                        vectorNuevo[37] = vectorViejo[37];//Cant Pacientes Fin Espera Medica


                        break;

//--------------------------------------------------------------------------------------------------------------------------
                  
                        //14, 15, 16, 17
                    case 14: //Fin At Medico 1
                        //Actualizar Estado Medico 1 [24], si no hay pacientes en Cola -OK-
                        //Actualizar AC T At Pacientes, [30](cuanto demoro en atender paciente) OK
                        //Sumar 1 al contador/columna [36] de pacientes atendidos -OK-
                        //Actualizar Cola de Medicos[29],disminuye -OK-, y OJO con paciente con estado EAMedico
                        //Si hay paciente EAMadico, actualizar contador Ac T esp [31] OK

                        vectorNuevo[0] = "FinAtMed1";

                        vectorNuevo[36] = (int)vectorViejo[36] + 1;//Contador Pacientes At

                        //Busco el indice paciente SAM1
                        int indexSAM1 = listaPacientes.FindIndex(a => a.Estado == "SAM1");
                        if (indexSAM1 != -1)
                        {
                            //listaPacientes[indexSAM1].HoraIniAtMedico = (double)vectorNuevo[1];
                            listaPacientes[indexSAM1].Estado = "AtFin";
                            listaPacientes[indexSAM1].HoraSalida= (double)vectorNuevo[1];
                        }

                        //listaPacientes[contadorAt].Estado = "AtFin";

                        //Actualizar AC T At Pacientes                                       Revisar si va contAt como index
                        
                        vectorNuevo[30] = (double)vectorViejo[30] + Math.Round(((double)vectorNuevo[1] - listaPacientes[contadorAt].HoraIniAtMedico), 2); 


                        contadorAt++;


                        

                        //Si hay pacientes en cola de medicos , atiendo
                        if ((int)vectorViejo[29] >= 1)
                        {
                            //Busco el indice del 1er paciente con estado EAM
                            int indexFirstEnEsp = listaPacientes.FindIndex(a => a.Estado == "EAMedico");
                            if (indexFirstEnEsp != -1)
                            {
                                listaPacientes[indexFirstEnEsp].HoraIniAtMedico = (double)vectorNuevo[1];
                                listaPacientes[indexFirstEnEsp].Estado = "SAM1";
                                //contadorFinEsperaAtMedico++;
                                vectorNuevo[37] = (int)vectorViejo[37] + 1;//Contador Fin Espera At

                                //Actualizo Ac contador Espera 
                                //Actualizar AC T Espera                                     

                                vectorNuevo[31] = (double)vectorViejo[31] + Math.Round(((double)vectorNuevo[1] - listaPacientes[indexFirstEnEsp].HoraIniEsperaMedico), 2);
                            }

                           
                            

                            //Resto a la cola
                            vectorNuevo[29] = (int)vectorViejo[29] - 1;
                            //Mantengo el estado de ocupado
                            vectorNuevo[25] = vectorViejo[25]; //Estado Medico 1

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 1
                            vectorNuevo[14] = (double)vectorNuevo[1] + (double)vectorNuevo[13];

                        }
                        else //Cola en 0 de medicos
                        {
                            //Mantengo cola 
                            vectorNuevo[29] = vectorViejo[29];
                            vectorNuevo[25] = "LI";

                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//T at Med
                            vectorNuevo[14] = null;//Fin At Medi 1

                        }
                        //Campos sin modificar
                        vectorNuevo[2] = null;
                        vectorNuevo[3] = null;
                        vectorNuevo[4] = vectorViejo[4]; //Mantengo prox lleg
                        vectorNuevo[5] = vectorViejo[5];//Fin llenado form
                        vectorNuevo[6] = null;
                        vectorNuevo[7] = null;

                        vectorNuevo[9] = vectorViejo[9];//Fin Reg 2

                        vectorNuevo[16] = vectorViejo[16];//Fin At Medico2
                        vectorNuevo[17] = vectorViejo[17];//Fin At Medico3
                        vectorNuevo[18] = vectorViejo[18];//Fin At Medico4

                        vectorNuevo[22] = vectorViejo[22];//Estado Empleado 2

                        vectorNuevo[26] = vectorViejo[26];//Estado Medico 2
                        vectorNuevo[27] = vectorViejo[27];//Estado Medico 3
                        vectorNuevo[28] = vectorViejo[28];//Estado Medico 4

                        //vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                        //vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                        vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                        vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                        vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                        vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4

                        break;

                    //14, 15, 16, 17
                    case 15: //Fin At Medico 2
                        //Actualizar Estado Medico 2 [24], si no hay pacientes en Cola 
                        //Actualizar AC T At Pacientes, [30](cuanto demoro en atender paciente) 
                        //Sumar 1 al contador/columna [36] de pacientes atendidos 
                        //Actualizar Cola de Medicos[29],disminuye -, y OJO con paciente con estado EAMedico
                        //Si hay paciente EAMadico, actualizar contador Ac T esp [31] 

                        vectorNuevo[0] = "FinAtMed2";

                        vectorNuevo[36] = (int)vectorViejo[36] + 1;//Contador Pacientes At

                        //Busco el indice paciente SAM1
                        int indexSAM2 = listaPacientes.FindIndex(a => a.Estado == "SAM2");
                        if (indexSAM2 != -1)
                        {
                            //listaPacientes[indexSAM1].HoraIniAtMedico = (double)vectorNuevo[1];
                            listaPacientes[indexSAM2].Estado = "AtFin";
                            listaPacientes[indexSAM2].HoraSalida = (double)vectorNuevo[1];
                        }

                        //listaPacientes[contadorAt].Estado = "AtFin";

                        //Actualizar AC T At Pacientes                                       Revisar si va contAt como index
                        vectorNuevo[30] = (double)vectorViejo[30] + Math.Round(((double)vectorNuevo[1] - listaPacientes[contadorAt].HoraIniAtMedico), 2);
                        contadorAt++;




                        //Si hay pacientes en cola de medicos , atiendo
                        if ((int)vectorViejo[29] >= 1)
                        {
                            //Busco el indice del 1er paciente con estado EAM
                            int indexFirstEnEsp = listaPacientes.FindIndex(a => a.Estado == "EAMedico");
                            if (indexFirstEnEsp != -1)
                            {
                                listaPacientes[indexFirstEnEsp].HoraIniAtMedico = (double)vectorNuevo[1];
                                listaPacientes[indexFirstEnEsp].Estado = "SAM2";

                                vectorNuevo[37] = (int)vectorViejo[37] + 1;//Contador Fin Espera At
                                //Actualizo Ac contador Espera 
                                //Actualizar AC T Espera        
                                vectorNuevo[31] = (double)vectorViejo[31] + Math.Round(((double)vectorNuevo[1] - listaPacientes[indexFirstEnEsp].HoraIniEsperaMedico), 2);
                            }




                            //Resto a la cola
                            vectorNuevo[29] = (int)vectorViejo[29] - 1;
                            //Mantengo el estado de ocupado
                            vectorNuevo[26] = vectorViejo[26]; //Estado Medico 2

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 2
                            vectorNuevo[15] = (double)vectorNuevo[1] + (double)vectorNuevo[13];

                        }
                        else //Cola en 0 de medicos
                        {
                            //Mantengo cola 
                            vectorNuevo[29] = vectorViejo[29];
                            vectorNuevo[26] = "LI";

                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//T at Med
                            vectorNuevo[15] = null;//Fin At Medi 2

                        }
                        //Campos sin modificar
                        vectorNuevo[2] = null;
                        vectorNuevo[3] = null;
                        vectorNuevo[4] = vectorViejo[4]; //Mantengo prox lleg
                        vectorNuevo[5] = vectorViejo[5];//Fin llenado form

                        vectorNuevo[6] = null;
                        vectorNuevo[7] = null;

                        vectorNuevo[9] = vectorViejo[9];//Fin Reg 2

                        vectorNuevo[14] = vectorViejo[14];//Fin At Medico1

                        vectorNuevo[16] = vectorViejo[16];//Fin At Medico3
                        vectorNuevo[17] = vectorViejo[17];//Fin At Medico4
                        vectorNuevo[18] = vectorViejo[18];//Estado llenado form

                        vectorNuevo[22] = vectorViejo[22];//Estado Empleado 2

                        vectorNuevo[25] = vectorViejo[25];//Estado Medico 1

                        vectorNuevo[27] = vectorViejo[27];//Estado Medico 3
                        vectorNuevo[28] = vectorViejo[28];//Estado Medico 4

                        //vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                        //vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                        vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                        vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                        vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                        vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4

                        break;


                    //14, 15, 16, 17
                    case 16: //Fin At Medico 3
                        //Actualizar Estado Medico 2 [24], si no hay pacientes en Cola 
                        //Actualizar AC T At Pacientes, [30](cuanto demoro en atender paciente) 
                        //Sumar 1 al contador/columna [36] de pacientes atendidos 
                        //Actualizar Cola de Medicos[29],disminuye -, y OJO con paciente con estado EAMedico
                        //Si hay paciente EAMadico, actualizar contador Ac T esp [31] 

                        vectorNuevo[0] = "FinAtMed3";

                        vectorNuevo[36] = (int)vectorViejo[36] + 1;//Contador Pacientes At

                        //Busco el indice paciente SAM1
                        int indexSAM3 = listaPacientes.FindIndex(a => a.Estado == "SAM3");
                        if (indexSAM3 != -1)
                        {
                            //listaPacientes[indexSAM1].HoraIniAtMedico = (double)vectorNuevo[1];
                            listaPacientes[indexSAM3].Estado = "AtFin";
                            listaPacientes[indexSAM3].HoraSalida = (double)vectorNuevo[1];
                        }

                        //listaPacientes[contadorAt].Estado = "AtFin";

                        //Actualizar AC T At Pacientes                                       
                        vectorNuevo[30] = (double)vectorViejo[30] + Math.Round(((double)vectorNuevo[1] - listaPacientes[contadorAt].HoraIniAtMedico), 2);
                        contadorAt++;




                        //Si hay pacientes en cola de medicos , atiendo
                        if ((int)vectorViejo[29] >= 1)
                        {
                            //Busco el indice del 1er paciente con estado EAM
                            int indexFirstEnEsp = listaPacientes.FindIndex(a => a.Estado == "EAMedico");
                            if (indexFirstEnEsp != -1)
                            {
                                listaPacientes[indexFirstEnEsp].HoraIniAtMedico = (double)vectorNuevo[1];
                                listaPacientes[indexFirstEnEsp].Estado = "SAM3";

                                vectorNuevo[37] = (int)vectorViejo[37] + 1;//Contador Fin Espera At

                                //Actualizo Ac contador Espera 
                                //Actualizar AC T Espera                                     
                                vectorNuevo[31] = (double)vectorViejo[31] + Math.Round(((double)vectorNuevo[1] - listaPacientes[indexFirstEnEsp].HoraIniEsperaMedico), 2);
                            }




                            //Resto a la cola
                            vectorNuevo[29] = (int)vectorViejo[29] - 1;
                            //Mantengo el estado de ocupado
                            vectorNuevo[27] = vectorViejo[27]; //Estado Medico 3

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 3
                            vectorNuevo[16] = (double)vectorNuevo[1] + (double)vectorNuevo[13];

                        }
                        else //Cola en 0 de medicos
                        {
                            //Mantengo cola 
                            vectorNuevo[29] = vectorViejo[29];
                            vectorNuevo[27] = "LI";

                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//T at Med
                            vectorNuevo[16] = null;//Fin At Medi 3

                        }
                        //Campos sin modificar
                        vectorNuevo[2] = null;
                        vectorNuevo[3] = null;
                        vectorNuevo[4] = vectorViejo[4]; //Mantengo prox lleg
                        vectorNuevo[5] = vectorViejo[5];//Fin llenado form

                        vectorNuevo[6] = null;
                        vectorNuevo[7] = null;

                        vectorNuevo[9] = vectorViejo[9];//Fin Reg 2

                        vectorNuevo[14] = vectorViejo[14];//Fin At Medico1
                        vectorNuevo[15] = vectorViejo[15];//Fin At Medico2

                        vectorNuevo[17] = vectorViejo[17];//Fin At Medico4
                        vectorNuevo[18] = vectorViejo[18];//Estado llenado form

                        vectorNuevo[22] = vectorViejo[22];//Estado Empleado 2

                        vectorNuevo[25] = vectorViejo[25];//Estado Medico 1
                        vectorNuevo[26] = vectorViejo[26];//Estado Medico 2

                        vectorNuevo[28] = vectorViejo[28];//Estado Medico 4

                        //vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                        //vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                        vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                        vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                        vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                        vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4

                        break;

                    //14, 15, 16, 17
                    case 17: //Fin At Medico 3
                        //Actualizar Estado Medico 2 [24], si no hay pacientes en Cola 
                        //Actualizar AC T At Pacientes, [30](cuanto demoro en atender paciente) 
                        //Sumar 1 al contador/columna [36] de pacientes atendidos 
                        //Actualizar Cola de Medicos[29],disminuye -, y OJO con paciente con estado EAMedico
                        //Si hay paciente EAMadico, actualizar contador Ac T esp [31] 

                        vectorNuevo[0] = "FinAtMed4";

                        vectorNuevo[36] = (int)vectorViejo[36] + 1;//Contador Pacientes At

                        //Busco el indice paciente SAM1
                        int indexSAM4 = listaPacientes.FindIndex(a => a.Estado == "SAM4");
                        if (indexSAM4 != -1)
                        {
                            //listaPacientes[indexSAM1].HoraIniAtMedico = (double)vectorNuevo[1];
                            listaPacientes[indexSAM4].Estado = "AtFin";
                            listaPacientes[indexSAM4].HoraSalida = (double)vectorNuevo[1];
                        }

                        //listaPacientes[contadorAt].Estado = "AtFin";

                        //Actualizar AC T At Pacientes                                       
                        vectorNuevo[30] = (double)vectorViejo[30] + Math.Round(((double)vectorNuevo[1] - listaPacientes[contadorAt].HoraIniAtMedico), 2);
                        contadorAt++;




                        //Si hay pacientes en cola de medicos , atiendo
                        if ((int)vectorViejo[29] >= 1)
                        {
                            //Busco el indice del 1er paciente con estado EAM
                            int indexFirstEnEsp = listaPacientes.FindIndex(a => a.Estado == "EAMedico");
                            if (indexFirstEnEsp != -1)
                            {
                                listaPacientes[indexFirstEnEsp].HoraIniAtMedico = (double)vectorNuevo[1];
                                listaPacientes[indexFirstEnEsp].Estado = "SAM4";

                                vectorNuevo[37] = (int)vectorViejo[37] + 1;//Contador Fin Espera At

                                //Actualizo Ac contador Espera 
                                //Actualizar AC T Espera                                     
                                vectorNuevo[31] = (double)vectorViejo[31] + Math.Round(((double)vectorNuevo[1] - listaPacientes[indexFirstEnEsp].HoraIniEsperaMedico), 2);
                            }




                            //Resto a la cola
                            vectorNuevo[29] = (int)vectorViejo[29] - 1;
                            //Mantengo el estado de ocupado
                            vectorNuevo[28] = vectorViejo[28]; //Estado Medico 4

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 4
                            vectorNuevo[17] = (double)vectorNuevo[1] + (double)vectorNuevo[13];

                        }
                        else //Cola en 0 de medicos
                        {
                            //Mantengo cola 
                            vectorNuevo[29] = vectorViejo[29];
                            vectorNuevo[28] = "LI";

                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//T at Med
                            vectorNuevo[17] = null;//Fin At Medi 4

                        }
                        //Campos sin modificar
                        vectorNuevo[2] = null;
                        vectorNuevo[3] = null;
                        vectorNuevo[4] = vectorViejo[4]; //Mantengo prox lleg
                        vectorNuevo[5] = vectorViejo[5];//Fin llenado form

                        vectorNuevo[6] = null;
                        vectorNuevo[7] = null;

                        vectorNuevo[9] = vectorViejo[9];//Fin Reg 2

                        vectorNuevo[14] = vectorViejo[14];//Fin At Medico1
                        vectorNuevo[15] = vectorViejo[15];//Fin At Medico2
                        vectorNuevo[16] = vectorViejo[16];//Fin At Medico4

                        vectorNuevo[18] = vectorViejo[18];//Estado llenado form

                        vectorNuevo[22] = vectorViejo[22];//Estado Empleado 2

                        vectorNuevo[25] = vectorViejo[25];//Estado Medico 1
                        vectorNuevo[26] = vectorViejo[26];//Estado Medico 2
                        vectorNuevo[27] = vectorViejo[27];//Estado Medico 3


                        //vectorNuevo[30] = vectorViejo[30];//AcTAt Pacientes
                        //vectorNuevo[31] = vectorViejo[31];//Ac T Espera At Medico
                        vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 1
                        vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 2
                        vectorNuevo[34] = vectorViejo[34];//ac T Ocup Medico 3
                        vectorNuevo[35] = vectorViejo[35];//ac T Ocup Medico 4

                        break;

                    default:
                        break;
                }

                //Actualizo Acumuladores T Ocupacion medicos
                vectorNuevo = ActualizarAcTOcMedicos(vectorNuevo, vectorViejo);



                //Filtro para mostrar rango de  filas de la data grid view
                if ((double)vectorNuevo[1] <= fin && (double)vectorNuevo[1] >= inicio)
                {
                    dgvUrgencias.Rows.Add(vectorNuevo);
                }

                //Asigno el tiempo en una variable
                tiempo = (double)vectorNuevo[1];

                vectorPenulitmo = vectorViejo;

                vectorViejo = (object[])vectorNuevo.Clone();


            } //Fin WHILE

            //Comento esta linea si quiero que se corte la simulacion SIN actualizar 
            //los acumuladores 

            //dgvUrgencias.Rows.Add(Final(vectorPenulitmo)); //Evento Final

            //Necesito guardar el reloj final, T at medicos,t espera, cant paci atendidos,tOcMedico1,2,3,4
            //double[] resultadoVariablesEstadisticas = new double[8];

            ultimaFilaParaEstadistica = vectorPenulitmo; //Cortando en ultimo evento:vectorNuevo (o vectorPenulitmo)
                                                                //Con Evento final :Linea Final (funcion) Final(vectorPenulitmo)


        }

        //Funcion para calcular el proximo evento
        public double[] proximoEvento(object[] vectorViejo)
        {
            //Vector con las celdas que pueden alterar el reloj
            double[] numerico = new double[9];
            //Columnas qe pueden alterar el reloj
            int[] colAlteranReloj = { 4, 5, 8, 9, 10, 14, 15, 16,17 };//5, 8, 9, 10, 14, 15, 16, 17 }; 
            double num = 0;
            int numero_casilla = 0;
            //Vector que me devuelve el valor del clock actual,y el numero de casilla que lo generó
            double[] resultado = new double[2];
            for (int i = 0; i < numerico.Length; i++)
            {
                if (Convert.ToString(vectorViejo[colAlteranReloj[i]]) != string.Empty || vectorViejo[colAlteranReloj[i]] != null || Convert.ToDouble(vectorViejo[colAlteranReloj[i]]) != 0)
                {
                    // recorro cada celda de posible nextclok y las que tengan valor las pongo en un vector
                    num = Convert.ToDouble(vectorViejo[colAlteranReloj[i]]);

                }
                // aca las meto en el vector numerico
                numerico[i] = num;
            }
            //Determino el minimo del vector 
            double minimo = numerico.Min();
            //Determino la casilla del minimo del vector
            numero_casilla = Array.IndexOf(numerico, minimo);
            string min = Convert.ToString(minimo);
            //Valor minimo y cual es su casilla
            resultado[0] = minimo;
            resultado[1] = colAlteranReloj[numero_casilla];

            return resultado;
        }

        //asignacion inicial de valores
        public object[] Inicio(double RndLlegada, double Tllegada)
        {

            object[] vector = new object[38];
            vector[0] = "INI";//Evento
            vector[1] = 0; //Reloj
            vector[2] = RndLlegada;//RND lleg
            vector[3] = Tllegada;//T lleg
            vector[4] = Tllegada;//prox lleg
            vector[5] = null;//Fin llenado
            vector[6] = null;//rnd Registro
            vector[7] = null;//T registro
            vector[8] = null;//Fin Reg 1
            vector[9] = null;//Fin Reg 2
            vector[10] = null;//Llegada Sala espera
            vector[11] = null;//rnd1
            vector[12] = null;//rnd2
            vector[13] = null;//T at Med
            vector[14] = null;//Fin At Medico1
            vector[15] = null;//Fin At Medico2
            vector[16] = null;//Fin At Medico3
            vector[17] = null;//Fin At Medico4
            vector[18] = "LI";//Estado llenado form
            vector[19] = 0;//Cola llenado form
            vector[20] = "LI";//Estado Empleado 1
            vector[21] = "LI";//Estado Empleado 2
            vector[22] = 0;//Cola Registro

            vector[23] = "LI";//Estado Pasillo Sala Espera //NEW Hasta 22 igual, desde 23 sumar 2
            vector[24] = 0;//Cola Pasillo Sala Espera

            vector[25] = "LI";//Estado Medico 1
            vector[26] = "LI";//Estado Medico 2
            vector[27] = "LI";//Estado Medico 3
            vector[28] = "LI";//Estado Medico 4
            vector[29] = 0;//Cola Atencion medico
            vector[30] = 0.0;//AcTAt Pacientes
            vector[31] = 0.0;//Ac T Espera At Medico
            vector[32] = 0.0;//ac T Ocup Medico 1
            vector[33] = 0.0;//ac T Ocup Medico 2
            vector[34] = 0.0;//ac T Ocup Medico 3
            vector[35] = 0.0;//ac T Ocup Medico 4
            vector[36] = 0;//Cant Pacientes At
            vector[37] = 0;//Cant Pacientes Fin Espera

            return vector;

        }

        //asignacion inicial de valores
        public object[] Final(object[] ultimoVector)
        {

            object[] vectorFinal = new object[38];
            vectorFinal[0] = "FIN";//Evento
            vectorFinal[1] = min; //
            vectorFinal[2] = ultimoVector[2];//RND lleg
            vectorFinal[3] = ultimoVector[3];//T lleg
            vectorFinal[4] = ultimoVector[4];//prox lleg
            vectorFinal[5] = ultimoVector[5];//Fin llenado
            vectorFinal[6] = ultimoVector[6];//rnd Registro
            vectorFinal[7] = ultimoVector[7];//T registro
            vectorFinal[8] = ultimoVector[8];//Fin Reg 1
            vectorFinal[9] = ultimoVector[9];//Fin Reg 2
            vectorFinal[10] = ultimoVector[10];//Llegada Sala espera
            vectorFinal[11] = ultimoVector[11];//rnd1
            vectorFinal[12] = ultimoVector[12];//rnd2
            vectorFinal[13] = ultimoVector[13];//T at Med
            vectorFinal[14] = ultimoVector[14];//Fin At Medico1
            vectorFinal[15] = ultimoVector[15];//Fin At Medico2
            vectorFinal[16] = ultimoVector[16];//Fin At Medico3
            vectorFinal[17] = ultimoVector[17];//Fin At Medico4
            vectorFinal[18] = ultimoVector[18];//Estado llenado form
            vectorFinal[19] = ultimoVector[19];//Cola llenado form
            vectorFinal[20] = ultimoVector[20];//Estado Empleado 1
            vectorFinal[21] = ultimoVector[21];//Estado Empleado 2
            vectorFinal[22] = ultimoVector[22];//Cola Registro

            vectorFinal[23] = ultimoVector[23];//Estado Pasillo Sala Espera //NEW Hasta 22 igual, desde 23 sumar 2
            vectorFinal[24] = ultimoVector[24];//Cola Pasillo Sala Espera

            vectorFinal[25] = ultimoVector[25];//Estado Medico 1
            vectorFinal[26] = ultimoVector[26];//Estado Medico 2
            vectorFinal[27] = ultimoVector[27];//Estado Medico 3
            vectorFinal[28] = ultimoVector[28];//Estado Medico 4
            vectorFinal[29] = ultimoVector[29];//Cola Atencion medico

            //Actualizo los acumuladores

            vectorFinal[30] = (double)ultimoVector[30] > 0 ? (min - (double)ultimoVector[1]) + (double)ultimoVector[30] : 0; ;//AcTAt Pacientes
            vectorFinal[31] = (double)ultimoVector[31] > 0 ? (min - (double)ultimoVector[1]) + (double)ultimoVector[31] : 0; ;//Ac T Espera At Medico

            vectorFinal[32] = (double)ultimoVector[32] > 0 ? (min - (double)ultimoVector[1]) + (double)ultimoVector[32] : 0; //ac T Ocup Medico 1
            vectorFinal[33] = (double)ultimoVector[33] > 0 ? (min - (double)ultimoVector[1]) + (double)ultimoVector[33] : 0; //ac T Ocup Medico 2
            vectorFinal[34] = (double)ultimoVector[34] > 0 ? (min - (double)ultimoVector[1]) + (double)ultimoVector[34] : 0; //ac T Ocup Medico 3
            vectorFinal[35] = (double)ultimoVector[35] > 0 ? (min - (double)ultimoVector[1]) + (double)ultimoVector[35] : 0; //ac T Ocup Medico 4

            vectorFinal[36] = ultimoVector[36];//Cant Pacientes At
            vectorFinal[37] = ultimoVector[37];//Cant Pacientes Fin Espera

            return vectorFinal;

        }

        public object[] ActualizarAcTOcMedicos(object[] vectorNuevoLoc, object[] vectorViejoLoc)
        {
            //Actualizo Acumuladores T Ocupacion medicos



            if ((string)vectorNuevoLoc[25] == "OC" && (string)vectorViejoLoc[25] == "OC" || (string)vectorNuevoLoc[25] == "LI" && (string)vectorViejoLoc[25] == "OC")
            {
                vectorNuevoLoc[32] = ((double)vectorNuevoLoc[1] - (double)vectorViejoLoc[1]) + (double)vectorViejoLoc[32]; //ac T Ocup Medico 1
            }
            if ((string)vectorNuevoLoc[26] == "OC" && (string)vectorViejoLoc[26] == "OC" || (string)vectorNuevoLoc[26] == "LI" && (string)vectorViejoLoc[26] == "OC")
            {
                vectorNuevoLoc[33] = ((double)vectorNuevoLoc[1] - (double)vectorViejoLoc[1]) + (double)vectorViejoLoc[33]; //ac T Ocup Medico 2
            }
            if ((string)vectorNuevoLoc[27] == "OC" && (string)vectorViejoLoc[27] == "OC" || (string)vectorNuevoLoc[27] == "LI" && (string)vectorViejoLoc[27] == "OC")
            {
                vectorNuevoLoc[34] = ((double)vectorNuevoLoc[1] - (double)vectorViejoLoc[1]) + (double)vectorViejoLoc[34]; //ac T Ocup Medico 3
            }
            if ((string)vectorNuevoLoc[28] == "OC" && (string)vectorViejoLoc[28] == "OC" || (string)vectorNuevoLoc[28] == "LI" && (string)vectorViejoLoc[28] == "OC")
            {
                vectorNuevoLoc[35] = ((double)vectorNuevoLoc[1] - (double)vectorViejoLoc[1]) + (double)vectorViejoLoc[35]; //ac T Ocup Medico 4
            }

            return vectorNuevoLoc;
        }



        //Calcula Exponencial
        public double Exponencial(double exponencial, double rnd)
        {
            double valorExp = -exponencial * Math.Log(1 - rnd);
            return valorExp;
        }

        //Calcula Normal
        public double Normal(double media, double desv, double rnd1, double rnd2)
        {
            double valorNormal = (Math.Sqrt(-2 * Math.Log(1-rnd1))*Math.Cos(2*Math.PI*rnd2))*desv+media;
            //double valorExp = -exponencial * Math.Log(1 - rnd);
            return valorNormal;
        }

        private void btn_enunciado_Click(object sender, EventArgs e)
        {
            Enunciado enunciado = new Enunciado();
            enunciado.Show();
        }

        private void btnVerPacientes_Click(object sender, EventArgs e)
        {

            MostrarPacientes mostrar = new MostrarPacientes(listaPacientes, ultimaFilaParaEstadistica);
            mostrar.ShowDialog();
        }


        //Calcula valor random entre 0 y 0,99

        public double GenerarRandom()
        {
            double RndGenerado = Math.Round(rnd.NextDouble(), 2);
            if (RndGenerado != 1)
            {
                return RndGenerado;
            }
            return RndGenerado - 0.001;
                
        }


    }
}
