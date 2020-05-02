using System;

namespace ServicesLogic.Functions
{
    /// <summary>
    /// Implements the function Sphere
    /// </summary>
    public class Sphere : Funcion
    {
        /// <summary>
        /// Establece los limites del espacio de optimización para esta función y el mínimo óptimo
        /// </summary>
        public Sphere()
        {
            LimiteInferior = -100;
            LimiteSuperior = 100;
            FitnessOptimo = 0.0;
        }

        /// <summary>
        /// Evaluación de la función objetivo
        /// </summary>
        /// <param name="csv"> Un vector con los valores de las dimensiones para evaluar la función objetivo</param>        
        /// <returns>
        /// Rertorna el valor de la función objetivo evaluada
        /// </returns>
        public override double Evaluar(double[] csv)
        {
            var dimensionNumber = csv.Length;  //Longitud del vector solución
            var evaluationValue = 0d;          //Variable (doble) para sumar valor de la función

            if (dimensionNumber >= 1)
            {
                for (var i = 0; i < dimensionNumber; i++)
                    evaluationValue += Math.Pow(csv[i], 2);
            }
            else
            {
                throw (new Exception("The function must have at least one dimension"));
            }
            return evaluationValue;
        }
       
        public override string ToString()
        {
            return "Sphere";
        }
    }
}