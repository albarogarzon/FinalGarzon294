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

        private bool flagNormal = false;

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

            object[] filaAnterior = new Object[38];
            object[] filaActual = new Object[38];
            object[] filaPenulitma = new Object[38];

            double RndLlegada = GenerarRandom();
            double TLlegada = Math.Round(Exponencial(expPac, RndLlegada), 2);
            object[] filaInicio = Inicio(RndLlegada,TLlegada);

            //Asigno los valores iniciales
            filaAnterior = filaInicio;
            dgvUrgencias.Rows.Add(filaAnterior);

            double tiempo = 0;
            int contandor = 0; //Contador Pacientes que llegaron
            int contadorAt = 0; //Contador Pacientes con atencion medica finalizada

            int contadorSalaEspera = 0;//pacientes que llegaron a sala de espera

            while (tiempo < min)
            {
                double[] reloj = new double[2];
                reloj = proximoEvento(filaAnterior); //Devuelve el valor minimo y la columna que lo posee.
                //asigno el nuevo reloj
                filaActual[1] = Math.Round((double)reloj[0], 2);

                //Ahora veo cual es la columna   que me genero el nuevo reloj
                //para poder calculcar todo lo que sucede
                int columnaEvento = (int)reloj[1];

                switch (columnaEvento)
                {
//--------------------------------------------------------------------------------------------------------------------------
                    case 4:
                        //Significa que hay una nueva llegada de paciente
                        //Creo el paciente
                        contandor +=1;
                        Paciente paciente = new Paciente();
                        paciente.Numero = contandor;
                        paciente.Estado = null;
                        paciente.HoraLlegada = (double)filaActual[1];
                        listaPacientes.Add((Paciente) paciente);

                        filaActual[0] = "LlegPac";

                        //Me fijo si llenado forma esta libre
                        if ((string)filaAnterior[18] == "LI")
                        {
                            //Le cambio el estado de atencion llenado
                            //listaPacientes[contadorAt].Estado = "Llenando Form"; //Ver
                                                                                 
                           // contadorAt++;

                            //el servidor llenado form se ocupa
                            filaActual[18] = "OC";
                            //La cola sigue igual( en cero ) 
                            filaActual[19] = 0;
                            //Calculo el fin de llenado
                            filaActual[5] = llenadoCte + (double)filaActual[1];
                            //Los otros valores quedan iguales

                            filaActual[6] = null;//rnd Registro
                            filaActual[7] = null;//T registro
                            filaActual[8] = filaAnterior[8];//Fin Reg 1
                            filaActual[9] = filaAnterior[9];//Fin Reg 2
                            filaActual[10] = filaAnterior[10];//Llegada Sala espera
                            filaActual[11] = null;//rnd1
                            filaActual[12] = null;//rnd2
                            filaActual[13] = null;//T at Med
                            filaActual[14] = filaAnterior[14];//Fin At Medico1
                            filaActual[15] = filaAnterior[15];//Fin At Medico2
                            filaActual[16] = filaAnterior[16];//Fin At Medico3
                            filaActual[17] = filaAnterior[17];//Fin At Medico4
                            filaActual[20] = filaAnterior[20];//Estado Empleado 1
                            filaActual[21] = filaAnterior[21];//Estado Empleado 2
                            filaActual[22] = filaAnterior[22];//Cola Registro
                            filaActual[23] = filaAnterior[23];//Estado PAsillo Sala Espera
                            filaActual[24] = filaAnterior[24];//Cola Pasillo Sala Espera
                            filaActual[25] = filaAnterior[25];//Estado Medico 1
                            filaActual[26] = filaAnterior[26];//Estado Medico 2
                            filaActual[27] = filaAnterior[27];//Estado Medico 3
                            filaActual[28] = filaAnterior[28];//Estado Medico 4
                            filaActual[29] = filaAnterior[29];//Cola Atencion medico
                            filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                            filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                            filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                            filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                            filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                            filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4
                            filaActual[36] = filaAnterior[36];//Cant Pacientes At
                            filaActual[37] = filaAnterior[37];//Cant Pacientes Fin Espera Medica


                        }
                        else
                        {
                            //Lo agrego a la cola y dejo en el mismo estado 
                            filaActual[18] = filaAnterior[18];
                            filaActual[19] = (int)filaAnterior[19] + 1;

                            //Los otros valores quedan iguales
                            filaActual[5] = filaAnterior[5];//Fin llenado form

                            filaActual[6] = null;//rnd Registro
                            filaActual[7] = null;//T registro
                            filaActual[8] = filaAnterior[8];//Fin Reg 1
                            filaActual[9] = filaAnterior[9];//Fin Reg 2
                            filaActual[10] = filaAnterior[10];//Llegada Sala espera
                            filaActual[11] = null;//rnd1
                            filaActual[12] = null;//rnd2
                            filaActual[13] = null;//T at Med
                            filaActual[14] = filaAnterior[14];//Fin At Medico1
                            filaActual[15] = filaAnterior[15];//Fin At Medico2
                            filaActual[16] = filaAnterior[16];//Fin At Medico3
                            filaActual[17] = filaAnterior[17];//Fin At Medico4
                            filaActual[20] = filaAnterior[20];//Estado Empleado 1
                            filaActual[21] = filaAnterior[21];//Estado Empleado 2
                            filaActual[22] = filaAnterior[22];//Cola Registro
                            filaActual[23] = filaAnterior[23];//Estado PAsillo Sala Espera
                            filaActual[24] = filaAnterior[24];//Cola Pasillo Sala Espera
                            filaActual[25] = filaAnterior[25];//Estado Medico 1
                            filaActual[26] = filaAnterior[26];//Estado Medico 2
                            filaActual[27] = filaAnterior[27];//Estado Medico 3
                            filaActual[28] = filaAnterior[28];//Estado Medico 4
                            filaActual[29] = filaAnterior[29];//Cola Atencion medico
                            filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                            filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                            filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                            filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                            filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                            filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4
                            filaActual[36] = filaAnterior[36];//Cant Pacientes At
                            filaActual[37] = filaAnterior[37];//Cant Pacientes Fin Espera Medica

                        }
                        //calculo la proxima llegada

                        double RNDlleg = GenerarRandom();
                        filaActual[2] = RNDlleg;//RND lleg
                        filaActual[3] = Math.Round(Exponencial(expPac, RNDlleg), 2);//T lleg
                        filaActual[4] = (double)filaActual[1] + (double)filaActual[3]; ;//prox lleg
                        break;
 //--------------------------------------------------------------------------------------------------------------------------
                    case 5: //Fin llenado Forma

                        filaActual[0] = "FinLlenForm";
                        //Pregunto si el Empleado de Registro 1 esta libre
                        if ((string)filaAnterior[20] == "LI")
                        {
                            filaActual[20] = "OC"; //Paso a ocupado
                            filaActual[22] = 0; //Cola en 0

                            double rndRegistro = GenerarRandom();

                            filaActual[6] = Math.Round(rndRegistro, 2); //Rnd Reg
                            //Calculo el tiempo de registro 
                            filaActual[7] = Math.Round(Exponencial(expReg, rndRegistro), 2);
                            //Calculo el fin de registro 1
                            filaActual[8] = (double)filaActual[1] + (double)filaActual[7];

                        }
                        else if ((string)filaAnterior[21] == "LI")
                        {
                            filaActual[21] = "OC"; //Paso a ocupado
                            filaActual[22] = 0; //Cola en 0

                            double rndRegistro = GenerarRandom();

                            filaActual[6] = Math.Round(rndRegistro, 2); //Rnd Reg
                            //Calculo el tiempo de registro 
                            filaActual[7] = Math.Round(Exponencial(expReg, rndRegistro), 2);
                            //Calculo el fin de registro 2
                            filaActual[9] = (double)filaActual[1] + (double)filaActual[7];
                        }else
                        {
                            //Lo agrego a la cola y dejo en el mismo estado a los empleados
                            filaActual[20] = filaAnterior[20];
                            filaActual[21] = filaAnterior[21];
                            filaActual[22] = (int)filaAnterior[22] + 1;

                            filaActual[6] = null;
                            filaActual[7] = null;
                            filaActual[9] = filaAnterior[9]; //Solo FinReg 2 mantengo? o Fin reg 1 tmb [8]?
                        }

                        // como termino un llenado forma, si tengo pacientes en cola, permito llenado form
                        if ((int)filaAnterior[19] >= 1) //Cola Llenado form
                        {
                            filaActual[19] = (int)filaAnterior[19] - 1;
                            filaActual[18] = filaAnterior[18]; //Estado sigue siendo OC de llenado form
                            

                            //Debo calcularle al nuevo paciente, fin llenado
                            //Calculo el fin de llenado
                            filaActual[5] = llenadoCte + (double)filaActual[1];

                            //Dejo los otros valores sin usar
                            filaActual[2] = null;
                            filaActual[3] = null;
                        }
                        else //La cola llenado form esta en 0
                        {
                            filaActual[18] = "LI";
                            filaActual[19] = filaAnterior[19];//La cola se mantiene en 0

                            filaActual[2] = null;
                            filaActual[3] = null;
                            filaActual[5] = null; //Fin llenado

                        }
                        filaActual[11] = null;
                        filaActual[12] = null;
                        filaActual[13] = null;


                        break;
 //--------------------------------------------------------------------------------------------------------------------------
                    case 8: //Fin Registro 1

                        filaActual[0] = "FinReg1";

                        //Me fijo si Sala Espera esta esta libre
                        if ((string)filaAnterior[23] == "LI")
                        {
                            //Le cambio el estado a la sala
                            //listaPacientes[contadorAt].Estado = "En Camino Sala"; 

                            // contadorAt++;

                            //el servidor pasillo sala espera se ocupa
                            filaActual[23] = "OC";
                            //La cola sigue igual( en cero ) 
                            filaActual[24] = 0;

                            //Coloco tiempo en llegar a sala de espera
                            filaActual[10] = caminaCte + (double)filaActual[1]; //OJO
                            //Los otros valores quedan iguales

                            filaActual[6] = null;//rnd Registro
                            filaActual[7] = null;//T registro
                            filaActual[8] = filaAnterior[8];//Fin Reg 1
                            filaActual[9] = filaAnterior[9];//Fin Reg 2
                            filaActual[11] = null;//rnd1
                            filaActual[12] = null;//rnd2
                            filaActual[13] = null;//T at Med
                            filaActual[14] = filaAnterior[14];//Fin At Medico1
                            filaActual[15] = filaAnterior[15];//Fin At Medico2
                            filaActual[16] = filaAnterior[16];//Fin At Medico3
                            filaActual[17] = filaAnterior[17];//Fin At Medico4
                            filaActual[20] = filaAnterior[20];//Estado Empleado 1
                            filaActual[21] = filaAnterior[21];//Estado Empleado 2
                            filaActual[22] = filaAnterior[22];//Cola Registro

                            filaActual[25] = filaAnterior[25];//Estado Medico 1
                            filaActual[26] = filaAnterior[26];//Estado Medico 2
                            filaActual[27] = filaAnterior[27];//Estado Medico 3
                            filaActual[28] = filaAnterior[28];//Estado Medico 4
                            filaActual[29] = filaAnterior[29];//Cola Atencion medico
                            filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                            filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                            filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                            filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                            filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                            filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4
                            filaActual[36] = filaAnterior[36];//Cant Pacientes At
                            filaActual[37] = filaAnterior[37];//Cant Pacientes Fin Espera Medica

                        }
                        else
                        {
                            //Lo agrego a la cola y dejo en el mismo estado 
                            filaActual[23] = filaAnterior[23];
                            filaActual[24] = (int)filaAnterior[24] + 1;

                            //Los otros valores quedan iguales
                            filaActual[10] = filaAnterior[10];//Fin Camino Sala espera

                            filaActual[6] = filaAnterior[6];//rnd Registro
                            filaActual[7] = filaAnterior[7];//T registro
                            filaActual[8] = filaAnterior[8];//Fin Reg 1
                            filaActual[9] = filaAnterior[9];//Fin Reg 2
                            filaActual[11] = null;//rnd1
                            filaActual[12] = null;//rnd2
                            filaActual[13] = null;//T at Med
                            filaActual[14] = filaAnterior[14];//Fin At Medico1
                            filaActual[15] = filaAnterior[15];//Fin At Medico2
                            filaActual[16] = filaAnterior[16];//Fin At Medico3
                            filaActual[17] = filaAnterior[17];//Fin At Medico4
                            filaActual[20] = filaAnterior[20];//Estado Empleado 1
                            filaActual[21] = filaAnterior[21];//Estado Empleado 2
                            filaActual[22] = filaAnterior[22];//Cola Registro
                            filaActual[25] = filaAnterior[25];//Estado Medico 1
                            filaActual[26] = filaAnterior[26];//Estado Medico 2
                            filaActual[27] = filaAnterior[27];//Estado Medico 3
                            filaActual[28] = filaAnterior[28];//Estado Medico 4
                            filaActual[29] = filaAnterior[29];//Cola Atencion medico
                            filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                            filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                            filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                            filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                            filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                            filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4
                            filaActual[36] = filaAnterior[36];//Cant Pacientes At
                            filaActual[37] = filaAnterior[37];//Cant Pacientes Fin Espera Medica

                        }

                        //Si hay pacientes en cola de regitro , atiendo
                        if ((int)filaAnterior[22] >= 1)
                        {
                            //Resto a la cola
                            filaActual[22] = (int)filaAnterior[22] - 1;
                            //Mantengo el estado de ocupado
                            filaActual[20] = filaAnterior[20]; //Estado Reg 1

                            //Calulo tiempo regitro al paciente 
                            double rndRegistro = GenerarRandom();

                            filaActual[6] = Math.Round(rndRegistro, 2); //Rnd Reg
                            //Calculo el tiempo de registro 
                            filaActual[7] = Math.Round(Exponencial(expReg, rndRegistro), 2);
                            //Calculo el fin de registro 1
                            filaActual[8] = (double)filaActual[1] + (double)filaActual[7];



                        }
                        else //Cola en 0 de registro
                        {
                            //Mantengo cola 
                            filaActual[22] = filaAnterior[22];
                            filaActual[20] = "LI";

                            filaActual[6] = null;
                            filaActual[7] = null;
                            filaActual[8] = null;

                        }
                        //Campos sin modificar
                        filaActual[2] = null;
                        filaActual[3] = null;
                        filaActual[4] = filaAnterior[4]; //Mantengo prox lleg
                        filaActual[5] = filaAnterior[5];//Fin llenado form

                        filaActual[9] = filaAnterior[9];//Fin Reg 2

                        filaActual[11] = null;
                        filaActual[12] = null;
                        filaActual[13] = null;
                        filaActual[14] = filaAnterior[14];//Fin At Medico1
                        filaActual[15] = filaAnterior[15];//Fin At Medico2
                        filaActual[16] = filaAnterior[16];//Fin At Medico3
                        filaActual[17] = filaAnterior[17];//Fin At Medico4

                        filaActual[21] = filaAnterior[21];//Estado Empleado 2

                        filaActual[25] = filaAnterior[25];//Estado Medico 1
                        filaActual[26] = filaAnterior[26];//Estado Medico 2
                        filaActual[27] = filaAnterior[27];//Estado Medico 3
                        filaActual[28] = filaAnterior[28];//Estado Medico 4
                        filaActual[29] = filaAnterior[29];//Cola Atencion medico
                        filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                        filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                        filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                        filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                        filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                        filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4
                        filaActual[36] = filaAnterior[36];//Cant Pacientes At
                        filaActual[37] = filaAnterior[37];//Cant Pacientes Fin Espera Medica

                        break;
 //--------------------------------------------------------------------------------------------------------------------------
                    case 9: //Fin Registro 2
                        filaActual[0] = "FinReg2";


                        //Me fijo si Sala Espera esta esta libre
                        if ((string)filaAnterior[23] == "LI")
                        {
                            //Le cambio el estado a la sala
                            //listaPacientes[contadorAt].Estado = "En Camino Sala"; 

                            // contadorAt++;

                            //el servidor pasillo sala espera se ocupa
                            filaActual[23] = "OC";
                            //La cola sigue igual( en cero ) 
                            filaActual[24] = 0;

                            //Coloco tiempo en llegar a sala de espera
                            filaActual[10] = caminaCte + (double)filaActual[1]; //OJO
                            //Los otros valores quedan iguales

                            filaActual[6] = null;//rnd Registro
                            filaActual[7] = null;//T registro
                            filaActual[8] = filaAnterior[8];//Fin Reg 1
                            filaActual[9] = filaAnterior[9];//Fin Reg 2
                            filaActual[11] = null;//rnd1
                            filaActual[12] = null;//rnd2
                            filaActual[13] = null;//T at Med
                            filaActual[14] = filaAnterior[14];//Fin At Medico1
                            filaActual[15] = filaAnterior[15];//Fin At Medico2
                            filaActual[16] = filaAnterior[16];//Fin At Medico3
                            filaActual[17] = filaAnterior[17];//Fin At Medico4
                            filaActual[20] = filaAnterior[20];//Estado Empleado 1
                            filaActual[21] = filaAnterior[21];//Estado Empleado 2
                            filaActual[22] = filaAnterior[22];//Cola Registro
                            filaActual[25] = filaAnterior[25];//Estado Medico 1
                            filaActual[26] = filaAnterior[26];//Estado Medico 2
                            filaActual[27] = filaAnterior[27];//Estado Medico 3
                            filaActual[28] = filaAnterior[28];//Estado Medico 4
                            filaActual[29] = filaAnterior[29];//Cola Atencion medico
                            filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                            filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                            filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                            filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                            filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                            filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4
                            filaActual[36] = filaAnterior[36];//Cant Pacientes At
                            filaActual[37] = filaAnterior[37];//Cant Pacientes Fin Espera Medica

                        }
                        else
                        {
                            //Lo agrego a la cola y dejo en el mismo estado 
                            filaActual[23] = filaAnterior[23]; 
                            filaActual[24] = (int)filaAnterior[24] + 1;

                            //Los otros valores quedan iguales
                            filaActual[10] = filaAnterior[10];//Fin Camino Sala espera

                            filaActual[6] = filaAnterior[6];//rnd Registro
                            filaActual[7] = filaAnterior[7];//T registro
                            filaActual[8] = filaAnterior[8];//Fin Reg 1
                            filaActual[9] = filaAnterior[9];//Fin Reg 2
                            filaActual[11] = null;//rnd1
                            filaActual[12] = null;//rnd2
                            filaActual[13] = null;//T at Med
                            filaActual[14] = filaAnterior[14];//Fin At Medico1
                            filaActual[15] = filaAnterior[15];//Fin At Medico2
                            filaActual[16] = filaAnterior[16];//Fin At Medico3
                            filaActual[17] = filaAnterior[17];//Fin At Medico4
                            filaActual[20] = filaAnterior[20];//Estado Empleado 1
                            filaActual[21] = filaAnterior[21];//Estado Empleado 2
                            filaActual[22] = filaAnterior[22];//Cola Registro
                            filaActual[25] = filaAnterior[25];//Estado Medico 1
                            filaActual[26] = filaAnterior[26];//Estado Medico 2
                            filaActual[27] = filaAnterior[27];//Estado Medico 3
                            filaActual[28] = filaAnterior[28];//Estado Medico 4
                            filaActual[29] = filaAnterior[29];//Cola Atencion medico
                            filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                            filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                            filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                            filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                            filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                            filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4
                            filaActual[36] = filaAnterior[36];//Cant Pacientes At
                            filaActual[37] = filaAnterior[37];//Cant Pacientes Fin Espera Medica

                        }

                        //Si hay pacientes en cola de regitro , atiendo
                        if ((int)filaAnterior[22] >= 1)
                        {
                            //Resto a la cola
                            filaActual[22] = (int)filaAnterior[22] - 1;
                            //Mantengo el estado de ocupado
                            filaActual[21] = filaAnterior[20]; //Estado Reg 1

                            //Calulo tiempo regitro al paciente 
                            double rndRegistro = GenerarRandom();

                            filaActual[6] = Math.Round(rndRegistro, 2); //Rnd Reg
                            //Calculo el tiempo de registro 
                            filaActual[7] = Math.Round(Exponencial(expReg, rndRegistro), 2);
                            //Calculo el fin de registro 1
                            filaActual[9] = (double)filaActual[1] + (double)filaActual[7];



                        }
                        else //Cola en 0 de registro
                        {
                            //Mantengo cola 
                            filaActual[22] = filaAnterior[22];
                            filaActual[21] = "LI";

                            filaActual[6] = null;
                            filaActual[7] = null;
                            filaActual[9] = null; //null? Fin reg 2

                        }
                        //Campos sin modificar
                        filaActual[2] = null;
                        filaActual[3] = null;
                        filaActual[4] = filaAnterior[4]; //Mantengo prox lleg
                        filaActual[5] = filaAnterior[5];//Fin llenado form

                        filaActual[8] = filaAnterior[8];//Fin Reg 1

                        filaActual[11] = filaAnterior[11];//rnd1
                        filaActual[12] = filaAnterior[12];//rnd2
                        filaActual[13] = filaAnterior[13];//T at Med
                        filaActual[14] = filaAnterior[14];//Fin At Medico1
                        filaActual[15] = filaAnterior[15];//Fin At Medico2
                        filaActual[16] = filaAnterior[16];//Fin At Medico3
                        filaActual[17] = filaAnterior[17];//Fin At Medico4
                        filaActual[20] = filaAnterior[20];//Estado Empleado 1


                        filaActual[25] = filaAnterior[25];//Estado Medico 1
                        filaActual[26] = filaAnterior[26];//Estado Medico 2
                        filaActual[27] = filaAnterior[27];//Estado Medico 3
                        filaActual[28] = filaAnterior[28];//Estado Medico 4
                        filaActual[29] = filaAnterior[29];//Cola Atencion medico
                        filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                        filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                        filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                        filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                        filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                        filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4
                        filaActual[36] = filaAnterior[36];//Cant Pacientes At
                        filaActual[37] = filaAnterior[37];//Cant Pacientes Fin Espera Medica

                        break;





//--------------------------------------------------------------------------------------------------------------------------
                        case 10: //Llegada Sala Espera Medico
                                 //Debe generar los rnd para la atencion de los medicos(Normal)
                                 //Pasar al medico a Ocupado
                                 //Limpiar el valor de la sala de espera
                                 //Registrar en Pacientes Estado "SAM1,2,3,4" y Hora Ini At 


                        filaActual[0] = "LlegadaSalaEsp";

                        //Pregunto si el Medico1  esta libre
                        if ((string)filaAnterior[25] == "LI")
                        {
                            filaActual[25] = "OC"; //Paso a ocupado
                            filaActual[29] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            filaActual[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            filaActual[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            filaActual[13] = Math.Round(Normal(mediaAtMed,desvAtMed,rndNorm1,rndNorm2), 2);
                            //Calculo el fin de at medico 1
                            filaActual[14] = (double)filaActual[1] + (double)filaActual[13];


                            listaPacientes[contadorSalaEspera].HoraIniAtMedico = (double)filaActual[1];
                            listaPacientes[contadorSalaEspera].Estado = "SAM1";
                            contadorSalaEspera++;

                        }
                        else if ((string)filaAnterior[26] == "LI") //Medico 2 libre
                        {
                            filaActual[26] = "OC"; //Paso a ocupado
                            filaActual[29] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            filaActual[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            filaActual[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            filaActual[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 2
                            filaActual[15] = (double)filaActual[1] + (double)filaActual[13];

                            listaPacientes[contadorSalaEspera].HoraIniAtMedico = (double)filaActual[1];
                            listaPacientes[contadorSalaEspera].Estado = "SAM2";
                            contadorSalaEspera++;
                        }
                        else if ((string)filaAnterior[27] == "LI") //Medico 3 libre
                        {
                            filaActual[27] = "OC"; //Paso a ocupado
                            filaActual[29] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            filaActual[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            filaActual[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            filaActual[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 3
                            filaActual[16] = (double)filaActual[1] + (double)filaActual[13];


                            listaPacientes[contadorSalaEspera].HoraIniAtMedico = (double)filaActual[1];
                            listaPacientes[contadorSalaEspera].Estado = "SAM3";
                            contadorSalaEspera++;
                        }
                        else if ((string)filaAnterior[28] == "LI") //Medico 4 libre
                        {
                            filaActual[28] = "OC"; //Paso a ocupado
                            filaActual[29] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            filaActual[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            filaActual[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            filaActual[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 4
                            filaActual[17] = (double)filaActual[1] + (double)filaActual[13];

                            listaPacientes[contadorSalaEspera].HoraIniAtMedico = (double)filaActual[1];
                            listaPacientes[contadorSalaEspera].Estado = "SAM4";
                            contadorSalaEspera++;
                        }
                        else //Si los 4 estan ocupados
                        {
                            //Lo agrego a la cola y dejo en el mismo estado a los medicos
                            filaActual[25] = filaAnterior[25];
                            filaActual[26] = filaAnterior[26];
                            filaActual[27] = filaAnterior[27];
                            filaActual[28] = filaAnterior[28];

                            filaActual[29] = (int)filaAnterior[29] + 1; //Cola para at medicos

                            filaActual[11] = null;//rnd1
                            filaActual[12] = null;//rnd2
                            filaActual[13] = null;//t at

                            filaActual[14] = filaAnterior[14];
                            filaActual[15] = filaAnterior[15];
                            filaActual[16] = filaAnterior[16];
                            filaActual[17] = filaAnterior[17];

                            listaPacientes[contadorSalaEspera].HoraIniEsperaMedico = (double)filaActual[1];
                            listaPacientes[contadorSalaEspera].Estado = "EAMedico";
                            contadorSalaEspera++;

                        }



                        //Si hay pacientes en cola de pasillo sala espera , atiendo
                        if ((int)filaAnterior[24] >= 1)
                        {
                            //Resto a la cola
                            filaActual[24] = (int)filaAnterior[24] - 1;
                            //Mantengo el estado de ocupado
                            filaActual[23] = filaAnterior[23]; //Estado Pasillo sala espera

                            //Calulo tiempo llegada sala espera al paciente 
                          
                            filaActual[10] = caminaCte + (double)filaActual[1];



                        }
                        else //Cola en 0 de pasillo sala espera
                        {
                            //Mantengo cola 
                            filaActual[24] = filaAnterior[24];
                            filaActual[23] = "LI";

                            filaActual[6] = null;
                            filaActual[7] = null;
                            filaActual[10] = null; //null? llegada pasillo sala espera

                        }
                        //Campos sin modificar
                        filaActual[2] = null;
                        filaActual[3] = null;
                        filaActual[4] = filaAnterior[4]; //Mantengo prox lleg
                        filaActual[5] = filaAnterior[5];//Fin llenado form

                        filaActual[8] = filaAnterior[8];//Fin Reg 1

                        //filaActual[11] = filaAnterior[11];//rnd1
                        //filaActual[12] = filaAnterior[12];//rnd2
                        //filaActual[13] = filaAnterior[13];//T at Med
                        //filaActual[14] = filaAnterior[14];//Fin At Medico1
                        //filaActual[15] = filaAnterior[15];//Fin At Medico2
                        //filaActual[16] = filaAnterior[16];//Fin At Medico3
                        //filaActual[17] = filaAnterior[17];//Fin At Medico4
                        filaActual[20] = filaAnterior[20];//Estado Empleado 1


                        //filaActual[25] = filaAnterior[25];//Estado Medico 1
                        //filaActual[26] = filaAnterior[26];//Estado Medico 2
                        //filaActual[27] = filaAnterior[27];//Estado Medico 3
                        //filaActual[28] = filaAnterior[28];//Estado Medico 4
                        //filaActual[29] = filaAnterior[29];//Cola Atencion medico
                        filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                        filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                        filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                        filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                        filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                        filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4
                        filaActual[36] = filaAnterior[36];//Cant Pacientes At
                        filaActual[37] = filaAnterior[37];//Cant Pacientes Fin Espera Medica


                        break;

//--------------------------------------------------------------------------------------------------------------------------
                  
                        //14, 15, 16, 17
                    case 14: //Fin At Medico 1
                        //Actualizar Estado Medico 1 [24], si no hay pacientes en Cola -OK-
                        //Actualizar AC T At Pacientes, [30](cuanto demoro en atender paciente) OK
                        //Sumar 1 al contador/columna [36] de pacientes atendidos -OK-
                        //Actualizar Cola de Medicos[29],disminuye -OK-, y OJO con paciente con estado EAMedico
                        //Si hay paciente EAMadico, actualizar contador Ac T esp [31] OK

                        filaActual[0] = "FinAtMed1";

                        filaActual[36] = (int)filaAnterior[36] + 1;//Contador Pacientes At

                        //Busco el indice paciente SAM1
                        int indexSAM1 = listaPacientes.FindIndex(a => a.Estado == "SAM1");
                        if (indexSAM1 != -1)
                        {
                            //listaPacientes[indexSAM1].HoraIniAtMedico = (double)filaActual[1];
                            listaPacientes[indexSAM1].Estado = "AtFin";
                            listaPacientes[indexSAM1].HoraSalida= (double)filaActual[1];
                        }

                        //listaPacientes[contadorAt].Estado = "AtFin";

                        //Actualizar AC T At Pacientes                                       Revisar si va contAt como index
                        
                        filaActual[30] = (double)filaAnterior[30] + Math.Round(((double)filaActual[1] - listaPacientes[contadorAt].HoraIniAtMedico), 2); 


                        contadorAt++;


                        

                        //Si hay pacientes en cola de medicos , atiendo
                        if ((int)filaAnterior[29] >= 1)
                        {
                            //Busco el indice del 1er paciente con estado EAM
                            int indexFirstEnEsp = listaPacientes.FindIndex(a => a.Estado == "EAMedico");
                            if (indexFirstEnEsp != -1)
                            {
                                listaPacientes[indexFirstEnEsp].HoraIniAtMedico = (double)filaActual[1];
                                listaPacientes[indexFirstEnEsp].Estado = "SAM1";
                                //contadorFinEsperaAtMedico++;
                                filaActual[37] = (int)filaAnterior[37] + 1;//Contador Fin Espera At

                                //Actualizo Ac contador Espera 
                                //Actualizar AC T Espera                                     

                                filaActual[31] = (double)filaAnterior[31] + Math.Round(((double)filaActual[1] - listaPacientes[indexFirstEnEsp].HoraIniEsperaMedico), 2);
                            }

                           
                            

                            //Resto a la cola
                            filaActual[29] = (int)filaAnterior[29] - 1;
                            //Mantengo el estado de ocupado
                            filaActual[25] = filaAnterior[25]; //Estado Medico 1

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            filaActual[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            filaActual[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            filaActual[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 1
                            filaActual[14] = (double)filaActual[1] + (double)filaActual[13];

                        }
                        else //Cola en 0 de medicos
                        {
                            //Mantengo cola 
                            filaActual[29] = filaAnterior[29];
                            filaActual[25] = "LI";

                            filaActual[11] = null;//rnd1
                            filaActual[12] = null;//rnd2
                            filaActual[13] = null;//T at Med
                            filaActual[14] = null;//Fin At Medi 1

                        }
                        //Campos sin modificar
                        filaActual[2] = null;
                        filaActual[3] = null;
                        filaActual[4] = filaAnterior[4]; //Mantengo prox lleg
                        filaActual[5] = filaAnterior[5];//Fin llenado form
                        filaActual[6] = null;
                        filaActual[7] = null;

                        filaActual[9] = filaAnterior[9];//Fin Reg 2

                        filaActual[16] = filaAnterior[16];//Fin At Medico2
                        filaActual[17] = filaAnterior[17];//Fin At Medico3
                        filaActual[18] = filaAnterior[18];//Fin At Medico4

                        filaActual[22] = filaAnterior[22];//Estado Empleado 2

                        filaActual[26] = filaAnterior[26];//Estado Medico 2
                        filaActual[27] = filaAnterior[27];//Estado Medico 3
                        filaActual[28] = filaAnterior[28];//Estado Medico 4

                        //filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                        //filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                        filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                        filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                        filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                        filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4

                        break;

//--------------------------------------------------------------------------------------------------------------------------
                    //14, 15, 16, 17
                    case 15: //Fin At Medico 2
                        //Actualizar Estado Medico 2 [24], si no hay pacientes en Cola 
                        //Actualizar AC T At Pacientes, [30](cuanto demoro en atender paciente) 
                        //Sumar 1 al contador/columna [36] de pacientes atendidos 
                        //Actualizar Cola de Medicos[29],disminuye -, y OJO con paciente con estado EAMedico
                        //Si hay paciente EAMadico, actualizar contador Ac T esp [31] 

                        filaActual[0] = "FinAtMed2";

                        filaActual[36] = (int)filaAnterior[36] + 1;//Contador Pacientes At

                        //Busco el indice paciente SAM1
                        int indexSAM2 = listaPacientes.FindIndex(a => a.Estado == "SAM2");
                        if (indexSAM2 != -1)
                        {
                            //listaPacientes[indexSAM1].HoraIniAtMedico = (double)filaActual[1];
                            listaPacientes[indexSAM2].Estado = "AtFin";
                            listaPacientes[indexSAM2].HoraSalida = (double)filaActual[1];
                        }

                        //listaPacientes[contadorAt].Estado = "AtFin";

                        //Actualizar AC T At Pacientes                                       Revisar si va contAt como index
                        filaActual[30] = (double)filaAnterior[30] + Math.Round(((double)filaActual[1] - listaPacientes[contadorAt].HoraIniAtMedico), 2);
                        contadorAt++;




                        //Si hay pacientes en cola de medicos , atiendo
                        if ((int)filaAnterior[29] >= 1)
                        {
                            //Busco el indice del 1er paciente con estado EAM
                            int indexFirstEnEsp = listaPacientes.FindIndex(a => a.Estado == "EAMedico");
                            if (indexFirstEnEsp != -1)
                            {
                                listaPacientes[indexFirstEnEsp].HoraIniAtMedico = (double)filaActual[1];
                                listaPacientes[indexFirstEnEsp].Estado = "SAM2";

                                filaActual[37] = (int)filaAnterior[37] + 1;//Contador Fin Espera At
                                //Actualizo Ac contador Espera 
                                //Actualizar AC T Espera        
                                filaActual[31] = (double)filaAnterior[31] + Math.Round(((double)filaActual[1] - listaPacientes[indexFirstEnEsp].HoraIniEsperaMedico), 2);
                            }




                            //Resto a la cola
                            filaActual[29] = (int)filaAnterior[29] - 1;
                            //Mantengo el estado de ocupado
                            filaActual[26] = filaAnterior[26]; //Estado Medico 2

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            filaActual[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            filaActual[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            filaActual[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 2
                            filaActual[15] = (double)filaActual[1] + (double)filaActual[13];

                        }
                        else //Cola en 0 de medicos
                        {
                            //Mantengo cola 
                            filaActual[29] = filaAnterior[29];
                            filaActual[26] = "LI";

                            filaActual[11] = null;//rnd1
                            filaActual[12] = null;//rnd2
                            filaActual[13] = null;//T at Med
                            filaActual[15] = null;//Fin At Medi 2

                        }
                        //Campos sin modificar
                        filaActual[2] = null;
                        filaActual[3] = null;
                        filaActual[4] = filaAnterior[4]; //Mantengo prox lleg
                        filaActual[5] = filaAnterior[5];//Fin llenado form

                        filaActual[6] = null;
                        filaActual[7] = null;

                        filaActual[9] = filaAnterior[9];//Fin Reg 2

                        filaActual[14] = filaAnterior[14];//Fin At Medico1

                        filaActual[16] = filaAnterior[16];//Fin At Medico3
                        filaActual[17] = filaAnterior[17];//Fin At Medico4
                        filaActual[18] = filaAnterior[18];//Estado llenado form

                        filaActual[22] = filaAnterior[22];//Estado Empleado 2

                        filaActual[25] = filaAnterior[25];//Estado Medico 1

                        filaActual[27] = filaAnterior[27];//Estado Medico 3
                        filaActual[28] = filaAnterior[28];//Estado Medico 4

                        //filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                        //filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                        filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                        filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                        filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                        filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4

                        break;

//--------------------------------------------------------------------------------------------------------------------------
                    //14, 15, 16, 17
                    case 16: //Fin At Medico 3
                        //Actualizar Estado Medico 2 [24], si no hay pacientes en Cola 
                        //Actualizar AC T At Pacientes, [30](cuanto demoro en atender paciente) 
                        //Sumar 1 al contador/columna [36] de pacientes atendidos 
                        //Actualizar Cola de Medicos[29],disminuye -, y OJO con paciente con estado EAMedico
                        //Si hay paciente EAMadico, actualizar contador Ac T esp [31] 

                        filaActual[0] = "FinAtMed3";

                        filaActual[36] = (int)filaAnterior[36] + 1;//Contador Pacientes At

                        //Busco el indice paciente SAM1
                        int indexSAM3 = listaPacientes.FindIndex(a => a.Estado == "SAM3");
                        if (indexSAM3 != -1)
                        {
                            //listaPacientes[indexSAM1].HoraIniAtMedico = (double)filaActual[1];
                            listaPacientes[indexSAM3].Estado = "AtFin";
                            listaPacientes[indexSAM3].HoraSalida = (double)filaActual[1];
                        }

                        //listaPacientes[contadorAt].Estado = "AtFin";

                        //Actualizar AC T At Pacientes                                       
                        filaActual[30] = (double)filaAnterior[30] + Math.Round(((double)filaActual[1] - listaPacientes[contadorAt].HoraIniAtMedico), 2);
                        contadorAt++;




                        //Si hay pacientes en cola de medicos , atiendo
                        if ((int)filaAnterior[29] >= 1)
                        {
                            //Busco el indice del 1er paciente con estado EAM
                            int indexFirstEnEsp = listaPacientes.FindIndex(a => a.Estado == "EAMedico");
                            if (indexFirstEnEsp != -1)
                            {
                                listaPacientes[indexFirstEnEsp].HoraIniAtMedico = (double)filaActual[1];
                                listaPacientes[indexFirstEnEsp].Estado = "SAM3";

                                filaActual[37] = (int)filaAnterior[37] + 1;//Contador Fin Espera At

                                //Actualizo Ac contador Espera 
                                //Actualizar AC T Espera                                     
                                filaActual[31] = (double)filaAnterior[31] + Math.Round(((double)filaActual[1] - listaPacientes[indexFirstEnEsp].HoraIniEsperaMedico), 2);
                            }




                            //Resto a la cola
                            filaActual[29] = (int)filaAnterior[29] - 1;
                            //Mantengo el estado de ocupado
                            filaActual[27] = filaAnterior[27]; //Estado Medico 3

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            filaActual[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            filaActual[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            filaActual[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 3
                            filaActual[16] = (double)filaActual[1] + (double)filaActual[13];

                        }
                        else //Cola en 0 de medicos
                        {
                            //Mantengo cola 
                            filaActual[29] = filaAnterior[29];
                            filaActual[27] = "LI";

                            filaActual[11] = null;//rnd1
                            filaActual[12] = null;//rnd2
                            filaActual[13] = null;//T at Med
                            filaActual[16] = null;//Fin At Medi 3

                        }
                        //Campos sin modificar
                        filaActual[2] = null;
                        filaActual[3] = null;
                        filaActual[4] = filaAnterior[4]; //Mantengo prox lleg
                        filaActual[5] = filaAnterior[5];//Fin llenado form

                        filaActual[6] = null;
                        filaActual[7] = null;

                        filaActual[9] = filaAnterior[9];//Fin Reg 2

                        filaActual[14] = filaAnterior[14];//Fin At Medico1
                        filaActual[15] = filaAnterior[15];//Fin At Medico2

                        filaActual[17] = filaAnterior[17];//Fin At Medico4
                        filaActual[18] = filaAnterior[18];//Estado llenado form

                        filaActual[22] = filaAnterior[22];//Estado Empleado 2

                        filaActual[25] = filaAnterior[25];//Estado Medico 1
                        filaActual[26] = filaAnterior[26];//Estado Medico 2

                        filaActual[28] = filaAnterior[28];//Estado Medico 4

                        //filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                        //filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                        filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                        filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                        filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                        filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4

                        break;
//--------------------------------------------------------------------------------------------------------------------------
                    //14, 15, 16, 17
                    case 17: //Fin At Medico 3
                        //Actualizar Estado Medico 2 [24], si no hay pacientes en Cola 
                        //Actualizar AC T At Pacientes, [30](cuanto demoro en atender paciente) 
                        //Sumar 1 al contador/columna [36] de pacientes atendidos 
                        //Actualizar Cola de Medicos[29],disminuye -, y OJO con paciente con estado EAMedico
                        //Si hay paciente EAMadico, actualizar contador Ac T esp [31] 

                        filaActual[0] = "FinAtMed4";

                        filaActual[36] = (int)filaAnterior[36] + 1;//Contador Pacientes At

                        //Busco el indice paciente SAM1
                        int indexSAM4 = listaPacientes.FindIndex(a => a.Estado == "SAM4");
                        if (indexSAM4 != -1)
                        {
                            //listaPacientes[indexSAM1].HoraIniAtMedico = (double)filaActual[1];
                            listaPacientes[indexSAM4].Estado = "AtFin";
                            listaPacientes[indexSAM4].HoraSalida = (double)filaActual[1];
                        }

                        //listaPacientes[contadorAt].Estado = "AtFin";

                        //Actualizar AC T At Pacientes                                       
                        filaActual[30] = (double)filaAnterior[30] + Math.Round(((double)filaActual[1] - listaPacientes[contadorAt].HoraIniAtMedico), 2);
                        contadorAt++;




                        //Si hay pacientes en cola de medicos , atiendo
                        if ((int)filaAnterior[29] >= 1)
                        {
                            //Busco el indice del 1er paciente con estado EAM
                            int indexFirstEnEsp = listaPacientes.FindIndex(a => a.Estado == "EAMedico");
                            if (indexFirstEnEsp != -1)
                            {
                                listaPacientes[indexFirstEnEsp].HoraIniAtMedico = (double)filaActual[1];
                                listaPacientes[indexFirstEnEsp].Estado = "SAM4";

                                filaActual[37] = (int)filaAnterior[37] + 1;//Contador Fin Espera At

                                //Actualizo Ac contador Espera 
                                //Actualizar AC T Espera                                     
                                filaActual[31] = (double)filaAnterior[31] + Math.Round(((double)filaActual[1] - listaPacientes[indexFirstEnEsp].HoraIniEsperaMedico), 2);
                            }




                            //Resto a la cola
                            filaActual[29] = (int)filaAnterior[29] - 1;
                            //Mantengo el estado de ocupado
                            filaActual[28] = filaAnterior[28]; //Estado Medico 4

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            filaActual[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            filaActual[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            filaActual[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 4
                            filaActual[17] = (double)filaActual[1] + (double)filaActual[13];

                        }
                        else //Cola en 0 de medicos
                        {
                            //Mantengo cola 
                            filaActual[29] = filaAnterior[29];
                            filaActual[28] = "LI";

                            filaActual[11] = null;//rnd1
                            filaActual[12] = null;//rnd2
                            filaActual[13] = null;//T at Med
                            filaActual[17] = null;//Fin At Medi 4

                        }
                        //Campos sin modificar
                        filaActual[2] = null;
                        filaActual[3] = null;
                        filaActual[4] = filaAnterior[4]; //Mantengo prox lleg
                        filaActual[5] = filaAnterior[5];//Fin llenado form

                        filaActual[6] = null;
                        filaActual[7] = null;

                        filaActual[9] = filaAnterior[9];//Fin Reg 2

                        filaActual[14] = filaAnterior[14];//Fin At Medico1
                        filaActual[15] = filaAnterior[15];//Fin At Medico2
                        filaActual[16] = filaAnterior[16];//Fin At Medico4

                        filaActual[18] = filaAnterior[18];//Estado llenado form

                        filaActual[22] = filaAnterior[22];//Estado Empleado 2

                        filaActual[25] = filaAnterior[25];//Estado Medico 1
                        filaActual[26] = filaAnterior[26];//Estado Medico 2
                        filaActual[27] = filaAnterior[27];//Estado Medico 3


                        //filaActual[30] = filaAnterior[30];//AcTAt Pacientes
                        //filaActual[31] = filaAnterior[31];//Ac T Espera At Medico
                        filaActual[32] = filaAnterior[32];//ac T Ocup Medico 1
                        filaActual[33] = filaAnterior[33];//ac T Ocup Medico 2
                        filaActual[34] = filaAnterior[34];//ac T Ocup Medico 3
                        filaActual[35] = filaAnterior[35];//ac T Ocup Medico 4

                        break;

                    default:
                        break;
                }

                //Actualizo Acumuladores T Ocupacion medicos
                filaActual = ActualizarAcTOcMedicos(filaActual, filaAnterior);



                //Filtro para mostrar rango de  filas de la data grid view
                if ((double)filaActual[1] <= fin && (double)filaActual[1] >= inicio)
                {
                    dgvUrgencias.Rows.Add(filaActual);
                }

                //Asigno el tiempo en una variable
                tiempo = (double)filaActual[1];

                filaPenulitma = filaAnterior;

                filaAnterior = (object[])filaActual.Clone();


            }//Fin WHILE

//--------------------------------------------------------------------------------------------------------------------------

            //Para mostrar ultima fila, si se aplico filtro desde hasta

            if (fin != min) //O sea que se cambió el "hasta"
            {
                dgvUrgencias.Rows.Add(filaActual);
                //dgvUrgencias.Rows[-1].DefaultCellStyle.BackColor = Color.Yellow;
                //dgvUrgencias.RowsDefaultCellStyle.BackColor = Color.LightGray;
            }

            //Comento esta linea si quiero que se corte la simulacion SIN actualizar 
            //los acumuladores 

            //dgvUrgencias.Rows.Add(Final(filaPenulitma)); //Evento Final

            //Necesito guardar el reloj final, T at medicos,t espera, cant paci atendidos,tOcMedico1,2,3,4
            //double[] resultadoVariablesEstadisticas = new double[8];

            ultimaFilaParaEstadistica = filaPenulitma; //Cortando en ultimo evento:filaActual (o filaPenulitma)
                                                                //Con Evento final :Linea Final (funcion) Final(filaPenulitma)


        }

        //Funcion para calcular el proximo evento
        public double[] proximoEvento(object[] filaAnterior)
        {
            //Vector con las columans que pueden alterar el reloj
            double[] numerico = new double[9];
            //Columnas qe pueden alterar el reloj
            int[] colAlteranReloj = { 4, 5, 8, 9, 10, 14, 15, 16,17 };//5, 8, 9, 10, 14, 15, 16, 17 }; 
            double num = 0;
            int numero_columna = 0;
            //Vector que me devuelve el valor del reloj actual,y el numero de columna que lo generó
            double[] resultado = new double[2];

            for (int i = 0; i < numerico.Length; i++) //Recorre 9 veces
            {
                if (Convert.ToString(filaAnterior[colAlteranReloj[i]]) != string.Empty || filaAnterior[colAlteranReloj[i]] != null || Convert.ToDouble(filaAnterior[colAlteranReloj[i]]) != 0)
                {
                    // recorro cada celda de posible proximo reloj y las que tengan valor las pongo en un vector
                    num = Convert.ToDouble(filaAnterior[colAlteranReloj[i]]);

                }
                // aca las meto en el vector numerico
                numerico[i] = num;
            }
            //Determino el minimo del vector 
            double minimo = numerico.Min();
            //Determino la columna del minimo del vector
            numero_columna = Array.IndexOf(numerico, minimo);
            //string min = Convert.ToString(minimo);
            //Valor minimo y cual es su columna
            resultado[0] = minimo;
            resultado[1] = colAlteranReloj[numero_columna];

            return resultado;
        }

        //asignacion inicial de valores
        public object[] Inicio(double RndLlegada, double Tllegada)
        {

            object[] fila = new object[38];
            fila[0] = "INI";//Evento
            fila[1] = 0; //Reloj
            fila[2] = RndLlegada;//RND lleg
            fila[3] = Tllegada;//T lleg
            fila[4] = Tllegada;//prox lleg
            fila[5] = null;//Fin llenado
            fila[6] = null;//rnd Registro
            fila[7] = null;//T registro
            fila[8] = null;//Fin Reg 1
            fila[9] = null;//Fin Reg 2
            fila[10] = null;//Llegada Sala espera
            fila[11] = null;//rnd1
            fila[12] = null;//rnd2
            fila[13] = null;//T at Med
            fila[14] = null;//Fin At Medico1
            fila[15] = null;//Fin At Medico2
            fila[16] = null;//Fin At Medico3
            fila[17] = null;//Fin At Medico4
            fila[18] = "LI";//Estado llenado form
            fila[19] = 0;//Cola llenado form
            fila[20] = "LI";//Estado Empleado 1
            fila[21] = "LI";//Estado Empleado 2
            fila[22] = 0;//Cola Registro

            fila[23] = "LI";//Estado Pasillo Sala Espera //NEW Hasta 22 igual, desde 23 sumar 2
            fila[24] = 0;//Cola Pasillo Sala Espera

            fila[25] = "LI";//Estado Medico 1
            fila[26] = "LI";//Estado Medico 2
            fila[27] = "LI";//Estado Medico 3
            fila[28] = "LI";//Estado Medico 4
            fila[29] = 0;//Cola Atencion medico
            fila[30] = 0.0;//AcTAt Pacientes
            fila[31] = 0.0;//Ac T Espera At Medico
            fila[32] = 0.0;//ac T Ocup Medico 1
            fila[33] = 0.0;//ac T Ocup Medico 2
            fila[34] = 0.0;//ac T Ocup Medico 3
            fila[35] = 0.0;//ac T Ocup Medico 4
            fila[36] = 0;//Cant Pacientes At
            fila[37] = 0;//Cant Pacientes Fin Espera

            return fila;

        }

        //asignacion Final de valores
        public object[] Final(object[] ultimaFila)
        {

            object[] filaFinal = new object[38];
            filaFinal[0] = "FIN";//Evento
            filaFinal[1] = min; //
            filaFinal[2] = ultimaFila[2];//RND lleg
            filaFinal[3] = ultimaFila[3];//T lleg
            filaFinal[4] = ultimaFila[4];//prox lleg
            filaFinal[5] = ultimaFila[5];//Fin llenado
            filaFinal[6] = ultimaFila[6];//rnd Registro
            filaFinal[7] = ultimaFila[7];//T registro
            filaFinal[8] = ultimaFila[8];//Fin Reg 1
            filaFinal[9] = ultimaFila[9];//Fin Reg 2
            filaFinal[10] = ultimaFila[10];//Llegada Sala espera
            filaFinal[11] = ultimaFila[11];//rnd1
            filaFinal[12] = ultimaFila[12];//rnd2
            filaFinal[13] = ultimaFila[13];//T at Med
            filaFinal[14] = ultimaFila[14];//Fin At Medico1
            filaFinal[15] = ultimaFila[15];//Fin At Medico2
            filaFinal[16] = ultimaFila[16];//Fin At Medico3
            filaFinal[17] = ultimaFila[17];//Fin At Medico4
            filaFinal[18] = ultimaFila[18];//Estado llenado form
            filaFinal[19] = ultimaFila[19];//Cola llenado form
            filaFinal[20] = ultimaFila[20];//Estado Empleado 1
            filaFinal[21] = ultimaFila[21];//Estado Empleado 2
            filaFinal[22] = ultimaFila[22];//Cola Registro

            filaFinal[23] = ultimaFila[23];//Estado Pasillo Sala Espera //NEW Hasta 22 igual, desde 23 sumar 2
            filaFinal[24] = ultimaFila[24];//Cola Pasillo Sala Espera

            filaFinal[25] = ultimaFila[25];//Estado Medico 1
            filaFinal[26] = ultimaFila[26];//Estado Medico 2
            filaFinal[27] = ultimaFila[27];//Estado Medico 3
            filaFinal[28] = ultimaFila[28];//Estado Medico 4
            filaFinal[29] = ultimaFila[29];//Cola Atencion medico

            //Actualizo los acumuladores

            filaFinal[30] = (double)ultimaFila[30] > 0 ? (min - (double)ultimaFila[1]) + (double)ultimaFila[30] : 0; ;//AcTAt Pacientes
            filaFinal[31] = (double)ultimaFila[31] > 0 ? (min - (double)ultimaFila[1]) + (double)ultimaFila[31] : 0; ;//Ac T Espera At Medico

            filaFinal[32] = (double)ultimaFila[32] > 0 ? (min - (double)ultimaFila[1]) + (double)ultimaFila[32] : 0; //ac T Ocup Medico 1
            filaFinal[33] = (double)ultimaFila[33] > 0 ? (min - (double)ultimaFila[1]) + (double)ultimaFila[33] : 0; //ac T Ocup Medico 2
            filaFinal[34] = (double)ultimaFila[34] > 0 ? (min - (double)ultimaFila[1]) + (double)ultimaFila[34] : 0; //ac T Ocup Medico 3
            filaFinal[35] = (double)ultimaFila[35] > 0 ? (min - (double)ultimaFila[1]) + (double)ultimaFila[35] : 0; //ac T Ocup Medico 4

            filaFinal[36] = ultimaFila[36];//Cant Pacientes At
            filaFinal[37] = ultimaFila[37];//Cant Pacientes Fin Espera

            return filaFinal;

        }

        public object[] ActualizarAcTOcMedicos(object[] filaNuevaLoc, object[] filaAnteriorLoc)
        {
            //Actualizo Acumuladores T Ocupacion medicos



            if ((string)filaNuevaLoc[25] == "OC" && (string)filaAnteriorLoc[25] == "OC" || (string)filaNuevaLoc[25] == "LI" && (string)filaAnteriorLoc[25] == "OC")
            {
                filaNuevaLoc[32] = ((double)filaNuevaLoc[1] - (double)filaAnteriorLoc[1]) + (double)filaAnteriorLoc[32]; //ac T Ocup Medico 1
            }
            if ((string)filaNuevaLoc[26] == "OC" && (string)filaAnteriorLoc[26] == "OC" || (string)filaNuevaLoc[26] == "LI" && (string)filaAnteriorLoc[26] == "OC")
            {
                filaNuevaLoc[33] = ((double)filaNuevaLoc[1] - (double)filaAnteriorLoc[1]) + (double)filaAnteriorLoc[33]; //ac T Ocup Medico 2
            }
            if ((string)filaNuevaLoc[27] == "OC" && (string)filaAnteriorLoc[27] == "OC" || (string)filaNuevaLoc[27] == "LI" && (string)filaAnteriorLoc[27] == "OC")
            {
                filaNuevaLoc[34] = ((double)filaNuevaLoc[1] - (double)filaAnteriorLoc[1]) + (double)filaAnteriorLoc[34]; //ac T Ocup Medico 3
            }
            if ((string)filaNuevaLoc[28] == "OC" && (string)filaAnteriorLoc[28] == "OC" || (string)filaNuevaLoc[28] == "LI" && (string)filaAnteriorLoc[28] == "OC")
            {
                filaNuevaLoc[35] = ((double)filaNuevaLoc[1] - (double)filaAnteriorLoc[1]) + (double)filaAnteriorLoc[35]; //ac T Ocup Medico 4
            }

            return filaNuevaLoc;
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
            double valorNormal;
            if (flagNormal == false)
            {
                valorNormal = (Math.Sqrt(-2 * Math.Log(1 - rnd1)) * Math.Cos(2 * Math.PI * rnd2)) * desv + media;
                flagNormal = true;
            }
            else
            {
                valorNormal = (Math.Sqrt(-2 * Math.Log(1 - rnd1)) * Math.Sin(2 * Math.PI * rnd2)) * desv + media;
                flagNormal = false;
            }
            
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
