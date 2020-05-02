using System;
using System.Diagnostics;

namespace OptimizacionContinua.EstadoSimple.RS
{
    /// <summary>
    /// Random Search
    /// </summary>
    public class RS: Algoritmo
    {
        public override void Ejecutar()
        {
            EFOs = 0;
            var s = new Solucion(TotalDimensiones, this); //Crea un objeto de la clase solución para el algoritmo actual
            s.InicializarAleatorio();                     //Inicializa el objeto solución
            MejorSolucion = new Solucion(s);              //Crea un nuevo objeto copiando el contenido del objeto s (Constructor de copia)

            while (EFOs < MaximoNumeroEvaluacionesFuncionObjetivo)
            {
                s.InicializarAleatorio(); //Al mismo objeto solución le modifica el valor de las dimensiones y el valor del fitness
                if (s.Fitness < MejorSolucion.Fitness)  //Si el nuevo valor es mejor se reemplaza la mejor solución (minimizando)
                    MejorSolucion = new Solucion(s);	//Crea un nuevo objeto copiando el contenido del objeto s (Constructor de copia)
                //Debug.WriteLine(MejorSolucion.Fitness);
                if (Math.Abs(s.Fitness - MiFuncion.FitnessOptimo) < 1e-12) break; //Se sale del ciclo cuando se logra llegar a un valor 
				                                    //muy cercano al fitness optimo (una propiedad de la función actual)
            }
        }

        public override string ToString() //Recupera el nombre de este algoritmo
        {
            return "RS";
        }
    }
}