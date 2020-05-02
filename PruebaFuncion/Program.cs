using System;
using System.Linq;
using System.Collections.Generic; //Listas
using System.Diagnostics; //Console, Debbug
using System.IO; //File (leer y escribir archivos)
using ServicesLogic.Functions; 
using OptimizacionContinua.EstadoSimple;
using OptimizacionContinua.EstadoSimple.RS;
using OptimizacionContinua.EstadoSimple.TS;

namespace OptimizacionContinua
{
    class Program
    {
        static void Main()
        {
			//Definición de constantes
            const string salida = "salida.txt";   //Nombre del archivo que presenta los resultados
            const int maximoNumeroDePruebas = 30; //Para realizar los 30 experimentos por cada función y algoritmo
            const int maximasEFOs = 5000;         //Máximo número de evaluaciones de la función objetivo
            const int totalDimensiones = 200;     //Número de dimensiones (ejes) de las funciones objetivo
            const bool reporteDetallado = true;   //Permite un reporte más detallado de los resultados en el archivo de salida 

			//Variable lista para ejecutar el algoritmo con varias funciones
            var listaFunciones = new List<Funcion>  //Crea los objetos de la lista de la clase Funcion 
            {
                new Sphere(),                       //Asigna un primer objeto de la clase esfera
				new Step(),
                new Schwefel(),
                new Rastrigin(),
                new Griewank(),
                new Ackley()
            };
			
			//Variable lista para realizar la ejecucción con varios algoritmos
            var listaAlgoritmos = new List<Algoritmo>
            {
                new RS(),
                new TabuSearch(),
				//new HC(){Radio = 0.1},
                //new SAHC(){Radio = 0.6, MaximosVecinos = 20},                
            };
         
            if (File.Exists(salida)) File.Delete(salida); //Borra el archivo de salidad si existe
			
			//Formateo y escritura en archivo de salida
            var resultados = String.Format("{0,-10}", "Funcion");
            resultados += String.Format("{0,-10}", "Algoritmo");
            resultados += String.Format(" {0,3}", "D");
            resultados += String.Format(" {0,10}", "Pro.Ite.");
            resultados += String.Format(" {0,20}", "Mejor Optimo");
            resultados += String.Format(" {0,20}", "Peor Optimo");
            resultados += String.Format(" {0,20}", "Promedio Optimos");
            resultados += String.Format(" {0,20}", "Desviacion Optimos");
            resultados += String.Format(" {0,10}\r\n", "Tiempo Promedio");
            File.AppendAllText("salida.txt", resultados);

            foreach(var funcion in listaFunciones)
            {
                foreach (var algoritmo in listaAlgoritmos)
                {
                    var tiempo1 = DateTime.Now;
                    var optimos = new List<double>();     //Lista para calcular Minimo, maximo y promedio de los óptimos encontrados
                    var iteraciones = new List<double>(); //Lista para calcular promedio de iteraciones en el experimento

                    for (var test = 0; test < maximoNumeroDePruebas; test++)
                    {
                        var aleatorio = new Random(test);
                        algoritmo.Aleatorio = aleatorio; //Cambia el aleatorio del algoritmo actual (RS) para cada test
                        algoritmo.MiFuncion = funcion;   //Asigna la función actual al algoritmo actual (RS)
                        algoritmo.TotalDimensiones = totalDimensiones; //Asigna número de dimensiones al algoritmo actual(RS) 
                        algoritmo.MaximoNumeroEvaluacionesFuncionObjetivo = maximasEFOs;
                        algoritmo.Ejecutar();            //Ejecuta este método del algoritmo actual

						//Adiciona a lista iteraciones y óptimos
                        iteraciones.Add(algoritmo.EFOs);  //Cantidad de evaluaciones de la función objetivo en la iteración (test)
                        optimos.Add(algoritmo.MejorSolucion.Fitness); //Valor de la función objetivo de la mejor solución de la iteración (test)
                        if (reporteDetallado)
                            Console.WriteLine("Test:" + test + ") " + algoritmo.MejorSolucion.Fitness + " EFOs = " + algoritmo.EFOs);
                    }
					
					//Cálculos para el archivo de salida
                    var total = new TimeSpan(DateTime.Now.Ticks - tiempo1.Ticks);
                    var tiempoPromedio = total.TotalSeconds / maximoNumeroDePruebas;

                    var mejorOptimo = optimos.Min();
                    var peorOptimo = optimos.Max();
                    var promedioOptimo = optimos.Average();
                    var desviacionOptimos = optimos.Sum(d => Math.Pow(d - promedioOptimo, 2));
                    desviacionOptimos = Math.Sqrt((desviacionOptimos)/(optimos.Count() - 1));
                    var promedioIteraciones = iteraciones.Average();

                    resultados = String.Format("{0,-10}", funcion);
                    resultados += String.Format("{0,-10}", algoritmo);
                    resultados += String.Format(" {0,3}", algoritmo.TotalDimensiones);
                    resultados += String.Format(" {0,10:#0.00}", promedioIteraciones);
                    resultados += String.Format(" {0,20:#0.0000000000}", mejorOptimo);
                    resultados += String.Format(" {0,20:#0.0000000000}", peorOptimo);
                    resultados += String.Format(" {0,20:#0.0000000000}", promedioOptimo);
                    resultados += String.Format(" {0,20:#0.0000000000}", desviacionOptimos);
                    resultados += String.Format(" {0,10:#0.000}\r\n", tiempoPromedio);

                    File.AppendAllText("salida.txt", resultados);
                    //Console.ReadKey();

                } // fin del foreach de los algoritmos
                File.AppendAllText("salida.txt", "\r\n");
            } // fin del foreach de las funciones
            Process.Start(@"C:\Program Files (x86)\Notepad++\notepad++.exe", salida);
        } // fin del método Main
    } // fin del class
} // fin del namespaces