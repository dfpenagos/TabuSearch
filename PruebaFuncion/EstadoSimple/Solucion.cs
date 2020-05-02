using System;
using ServicesLogic.Functions;

namespace OptimizacionContinua.EstadoSimple
{
    public class Solucion : IEquatable<Solucion>
    {
        protected Algoritmo MiContenedor; //Crea un objeto de la clase algoritmo, para que el objeto solución pueda acceder
									      //a propiedades y métodos del objeto algoritmo que lo creo.

        public double Fitness;			//Variable para almacenar el valor de aptitud de la solución
        public double[] Dimensiones;	//Vector solución de acuerdo al número de dimensiones

        public Solucion(int dimensiones, Algoritmo elContenedor)
        {
            Dimensiones = new double[dimensiones]; //Crea el vector con n dimensiones
            MiContenedor = elContenedor;  //Apunta al objeto algoritmo que lo creo
        }

        /// <summary>
        /// Constructor de copia
        /// </summary>
        /// <param name="inicial"></param>
        public Solucion(Solucion inicial)
        {
			MiContenedor = inicial.MiContenedor; //Copia el apuntador del objeto algoritmo que lo creo, para poder usar propiedades de ese objeto
            Fitness = inicial.Fitness;           //Copia la propiedad fitness
            Dimensiones = new double[inicial.Dimensiones.Length]; //Crea el vector con n dimensiones
            for (var i = 0; i < MiContenedor.TotalDimensiones; i++)
                Dimensiones[i] = inicial.Dimensiones[i]; //Copia el valor de las dimensiones
        }

        /// <summary>
        /// Permite definir los valores de la solución en cada dimensión basado en los
        /// limites inferior y superior del espacio de búsqueda
        /// </summary>
        public void InicializarAleatorio()
        {
			//Inicializa el objeto solución (vector) de forma aleatoria teniendo en cuenta los limites inferior y superior
            var aleatorio = MiContenedor.Aleatorio;
            var limiteInferior = MiContenedor.MiFuncion.LimiteInferior;
            var limiteSuperior = MiContenedor.MiFuncion.LimiteSuperior;
            for (var i = 0; i < MiContenedor.TotalDimensiones; i++)
                Dimensiones[i] = limiteInferior + (limiteSuperior - limiteInferior) * aleatorio.NextDouble();
            CalcularFitness(); //Invoca el cálculo de la función objetivo
        }

        public void CalcularFitness()  
        {
            Fitness = MiContenedor.MiFuncion.Evaluar(Dimensiones); //Invoca el método Evaluar del objeto función actual
            MiContenedor.EFOs++; //Aumenta el propiedad del algoritmo para llevar el número de evaluaciones de la función objetivo
        }

        public override string ToString() //Retorna una cadena con el contenido del objeto
        {
            var cadena = Fitness + ": ";
            for (var i = 0; i < Dimensiones.Length; i++)
                cadena += Dimensiones[i].ToString("#0.000") + " ";
            return cadena;
        }

        public bool Equals(Solucion other) //Sirve para saber si dos objetos solución son iguales y se debe colocarse el contrato IEquatable arriba
        {
            for (var j = 0; j < MiContenedor.TotalDimensiones; j++)
                if (Math.Abs(Dimensiones[j] - other.Dimensiones[j]) < 1e-10) // Por ser valores dobles, son similares cuando la diferencia es muy pequeña
                    return false;
            return true;
        }
    }
}