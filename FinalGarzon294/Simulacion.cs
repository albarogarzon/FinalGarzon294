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


        List<Paciente> listaPacientes = new List<Paciente>();

        public Simulacion(double expPacientes, int llenadoConstante, double expRegistro, int caminaConstante,int mediaAtMedico, int desvAtMedico, double minutos)
        {
            InitializeComponent();
            expPac = expPacientes;
            llenadoCte = llenadoConstante;
            expReg = expRegistro;
            caminaCte = caminaConstante;
            mediaAtMed = mediaAtMedico;
            desvAtMed = desvAtMedico;
            min = minutos;
        }

        Random rnd = new Random();
        private void Simulacion_Load(object sender, EventArgs e)
        {
            

            object[] vectorViejo = new Object[35];
            object[] vectorNuevo = new Object[35];

            double RndLlegada = GenerarRandom();
            double TLlegada = Math.Round(Exponencial(expPac, RndLlegada), 2);
            object[] vectorInicio = Inicio(RndLlegada,TLlegada);

            //Asigno los valores iniciales
            vectorViejo = vectorInicio;
            dgvUrgencias.Rows.Add(vectorViejo);

            double tiempo = 0;
            int contandor = 0;
            int contadorAt = 0;

            while(tiempo < min)
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
                        paciente.Estado = "";
                        listaPacientes.Add((Paciente) paciente);

                        vectorNuevo[0] = "LlegPac";

                        //Me fijo si llenado forma esta libre
                        if ((string)vectorViejo[18] == "LI")
                        {
                            //Le cambio el estado de atencion llenado
                            listaPacientes[contadorAt].Estado = "Llenando Form";
                            contadorAt++;
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
                            vectorNuevo[23] = vectorViejo[23];//Estado Medico 1
                            vectorNuevo[24] = vectorViejo[24];//Estado Medico 2
                            vectorNuevo[25] = vectorViejo[25];//Estado Medico 3
                            vectorNuevo[26] = vectorViejo[26];//Estado Medico 4
                            vectorNuevo[27] = vectorViejo[27];//Cola Atencion medico
                            vectorNuevo[28] = vectorViejo[28];//AcTAt Pacientes
                            vectorNuevo[29] = vectorViejo[29];//Ac T Espera At Medico
                            vectorNuevo[30] = vectorViejo[30];//ac T Ocup Medico 1
                            vectorNuevo[31] = vectorViejo[31];//ac T Ocup Medico 2
                            vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 3
                            vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 4
                            vectorNuevo[34] = vectorViejo[34];//Cant Pacientes At

                        }
                        else
                        {
                            //Lo agrego a la cola y dejo en el mismo estado 
                            vectorNuevo[18] = vectorViejo[18];
                            vectorNuevo[19] = (int)vectorViejo[19] + 1;

                            //Los otros valores quedan iguales
                            vectorNuevo[5] = vectorViejo[5];//Fin llenado form

                            vectorNuevo[6] = vectorViejo[6];//rnd Registro
                            vectorNuevo[7] = vectorViejo[7];//T registro
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
                            vectorNuevo[23] = vectorViejo[23];//Estado Medico 1
                            vectorNuevo[24] = vectorViejo[24];//Estado Medico 2
                            vectorNuevo[25] = vectorViejo[25];//Estado Medico 3
                            vectorNuevo[26] = vectorViejo[26];//Estado Medico 4
                            vectorNuevo[27] = vectorViejo[27];//Cola Atencion medico
                            vectorNuevo[28] = vectorViejo[28];//AcTAt Pacientes
                            vectorNuevo[29] = vectorViejo[29];//Ac T Espera At Medico
                            vectorNuevo[30] = vectorViejo[30];//ac T Ocup Medico 1
                            vectorNuevo[31] = vectorViejo[31];//ac T Ocup Medico 2
                            vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 3
                            vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 4
                            vectorNuevo[34] = vectorViejo[34];//Cant Pacientes At

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
                        //Coloco tiempo en llegar a sala de espera
                        vectorNuevo[10]= caminaCte + (double)vectorNuevo[1];

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

                        vectorNuevo[23] = vectorViejo[23];//Estado Medico 1
                        vectorNuevo[24] = vectorViejo[24];//Estado Medico 2
                        vectorNuevo[25] = vectorViejo[25];//Estado Medico 3
                        vectorNuevo[26] = vectorViejo[26];//Estado Medico 4
                        vectorNuevo[27] = vectorViejo[27];//Cola Atencion medico
                        vectorNuevo[28] = vectorViejo[28];//AcTAt Pacientes
                        vectorNuevo[29] = vectorViejo[29];//Ac T Espera At Medico
                        vectorNuevo[30] = vectorViejo[30];//ac T Ocup Medico 1
                        vectorNuevo[31] = vectorViejo[31];//ac T Ocup Medico 2
                        vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 3
                        vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 4
                        vectorNuevo[34] = vectorViejo[34];//Cant Pacientes At

                        break;
 //--------------------------------------------------------------------------------------------------------------------------
                    case 9: //Fin Registro 2
                        vectorNuevo[0] = "FinReg2";
                        //Coloco tiempo en llegar a sala de espera
                        vectorNuevo[10] = caminaCte + (double)vectorNuevo[1];

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


                        vectorNuevo[23] = vectorViejo[23];//Estado Medico 1
                        vectorNuevo[24] = vectorViejo[24];//Estado Medico 2
                        vectorNuevo[25] = vectorViejo[25];//Estado Medico 3
                        vectorNuevo[26] = vectorViejo[26];//Estado Medico 4
                        vectorNuevo[27] = vectorViejo[27];//Cola Atencion medico
                        vectorNuevo[28] = vectorViejo[28];//AcTAt Pacientes
                        vectorNuevo[29] = vectorViejo[29];//Ac T Espera At Medico
                        vectorNuevo[30] = vectorViejo[30];//ac T Ocup Medico 1
                        vectorNuevo[31] = vectorViejo[31];//ac T Ocup Medico 2
                        vectorNuevo[32] = vectorViejo[32];//ac T Ocup Medico 3
                        vectorNuevo[33] = vectorViejo[33];//ac T Ocup Medico 4
                        vectorNuevo[34] = vectorViejo[34];//Cant Pacientes At

                        break;





//--------------------------------------------------------------------------------------------------------------------------
                        case 10: //Llegada Sala Espera Medico
                                 //Debe generar los rnd para la atencion de los medicos(Normal)
                                 //Pasar al medico a Ocupado
                                 //Limpiar el valor de la sala de espera
                                 //Registrar en Pacientes Estado "SAM1,2,3,4" y Hora Ini At 

                        vectorNuevo[0] = "LlegadaSalaEsp";
                        vectorNuevo[10] = null; //Limpio valor llegada sala
         


                        //Pregunto si el Medico1  esta libre
                        if ((string)vectorViejo[23] == "LI")
                        {
                            vectorNuevo[23] = "OC"; //Paso a ocupado
                            vectorNuevo[27] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed,desvAtMed,rndNorm1,rndNorm2), 2);
                            //Calculo el fin de at medico 1
                            vectorNuevo[14] = (double)vectorNuevo[1] + (double)vectorNuevo[13];

                        }
                        else if ((string)vectorViejo[24] == "LI") //Medico 2 libre
                        {
                            vectorNuevo[24] = "OC"; //Paso a ocupado
                            vectorNuevo[27] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 2
                            vectorNuevo[15] = (double)vectorNuevo[1] + (double)vectorNuevo[13];
                        }
                        else if ((string)vectorViejo[25] == "LI") //Medico 3 libre
                        {
                            vectorNuevo[25] = "OC"; //Paso a ocupado
                            vectorNuevo[27] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 3
                            vectorNuevo[16] = (double)vectorNuevo[1] + (double)vectorNuevo[13];
                        }
                        else if ((string)vectorViejo[26] == "LI") //Medico 4 libre
                        {
                            vectorNuevo[26] = "OC"; //Paso a ocupado
                            vectorNuevo[27] = 0; //Cola en 0

                            double rndNorm1 = GenerarRandom();
                            double rndNorm2 = GenerarRandom();

                            vectorNuevo[11] = Math.Round(rndNorm1, 2); //Rnd Norm 1
                            vectorNuevo[12] = Math.Round(rndNorm2, 2); //Rnd Norm 2
                            //Calculo el tiempo de at medico 
                            vectorNuevo[13] = Math.Round(Normal(mediaAtMed, desvAtMed, rndNorm1, rndNorm2), 2);
                            //Calculo el fin de at medico 4
                            vectorNuevo[17] = (double)vectorNuevo[1] + (double)vectorNuevo[13];
                        }
                        else //Si los 4 estan ocupados
                        {
                            //Lo agrego a la cola y dejo en el mismo estado a los medicos
                            vectorNuevo[23] = vectorViejo[23];
                            vectorNuevo[24] = vectorViejo[24];
                            vectorNuevo[25] = vectorViejo[25];
                            vectorNuevo[26] = vectorViejo[26];

                            vectorNuevo[27] = (int)vectorViejo[27] + 1; //Cola para at medicos

                            vectorNuevo[11] = null;//rnd1
                            vectorNuevo[12] = null;//rnd2
                            vectorNuevo[13] = null;//t at

                            vectorNuevo[14] = vectorViejo[14];
                            vectorNuevo[15] = vectorViejo[15];
                            vectorNuevo[16] = vectorViejo[16];
                            vectorNuevo[17] = vectorViejo[17];

                        }

                        break;


                    default:
                        break;
                }

                //Actualizo Acumuladores T Ocupacion medicos
                //vector[23] = "LI";//Estado Medico 1
                //vector[24] = "LI";//Estado Medico 2
                //vector[25] = "LI";//Estado Medico 3
                //vector[26] = "LI";//Estado Medico 4


                if ((string)vectorNuevo[23] == "OC" && (string)vectorViejo[23] == "OC")
                {
                    vectorNuevo[30] = ((double)vectorNuevo[1] - (double)vectorViejo[1]) + (double)vectorViejo[30]; //ac T Ocup Medico 1
                }
                if ((string)vectorNuevo[24] == "OC" && (string)vectorViejo[24] == "OC")
                {
                    vectorNuevo[31] = ((double)vectorNuevo[1] - (double)vectorViejo[1]) + (double)vectorViejo[31]; //ac T Ocup Medico 2
                }
                if ((string)vectorNuevo[25] == "OC" && (string)vectorViejo[25] == "OC")
                {
                    vectorNuevo[32] = ((double)vectorNuevo[1] - (double)vectorViejo[1]) + (double)vectorViejo[32]; //ac T Ocup Medico 3
                }
                if ((string)vectorNuevo[26] == "OC" && (string)vectorViejo[26] == "OC")
                {
                    vectorNuevo[33] = ((double)vectorNuevo[1] - (double)vectorViejo[1]) + (double)vectorViejo[33]; //ac T Ocup Medico 4
                }


                //vector[32] = 0;//ac T Ocup Medico 3
                //vector[33] = 0;//ac T Ocup Medico 4


                dgvUrgencias.Rows.Add(vectorNuevo);
                //Asigno el tiempo en una variable
                tiempo = (double)vectorNuevo[1];

                vectorViejo = (object[])vectorNuevo.Clone();


            }

        }

        //Funcion para calcular el proximo evento
        public double[] proximoEvento(object[] vectorViejo)
        {
            //Vector con las celdas que pueden alterar el clock
            double[] numerico = new double[5];
            int[] celdas = { 4,5, 8,9,10 };//5, 8, 9, 10, 14, 15, 16, 17 }; //celdas qe pueden alterar el clock
            double num = 0;
            int numero_casilla = 0;
            //Vector que me devuelve el valor del clock actual,y el numero de casilla que lo generó
            double[] resultado = new double[2];
            for (int i = 0; i < numerico.Length; i++)
            {
                if (Convert.ToString(vectorViejo[celdas[i]]) != string.Empty || vectorViejo[celdas[i]] != null || Convert.ToDouble(vectorViejo[celdas[i]]) != 0)
                {
                    // recorro cada celda de posible nextclok y las que tengan valor las pongo en un vector
                    num = Convert.ToDouble(vectorViejo[celdas[i]]);

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
            resultado[1] = celdas[numero_casilla];

            return resultado;
        }

        //asignacion inicial de valores
        public object[] Inicio(double RndLlegada, double Tllegada)
        {

            object[] vector = new object[35];
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
            vector[23] = "LI";//Estado Medico 1
            vector[24] = "LI";//Estado Medico 2
            vector[25] = "LI";//Estado Medico 3
            vector[26] = "LI";//Estado Medico 4
            vector[27] = 0;//Cola Atencion medico
            vector[28] = 0;//AcTAt Pacientes
            vector[29] = 0;//Ac T Espera At Medico
            vector[30] = 0.0;//ac T Ocup Medico 1
            vector[31] = 0.0;//ac T Ocup Medico 2
            vector[32] = 0.0;//ac T Ocup Medico 3
            vector[33] = 0.0;//ac T Ocup Medico 4
            vector[34] = 0;//Cant Pacientes At

            return vector;

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
            double valorNormal = (Math.Sqrt(-2 * Math.Log(rnd1))*Math.Cos(2*Math.PI*rnd2))*desv+media;
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
            MostrarPacientes mostrar = new MostrarPacientes(listaPacientes);
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
