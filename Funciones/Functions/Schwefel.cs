using System;

namespace ServicesLogic.Functions
{
    /// <summary>
    /// Implements the function  Schwefel’s Problem 1.2
    /// </summary>
    public class Schwefel : Funcion
    {
        /// <summary>
        /// Establece los limites de optimización para esta función
        /// </summary>
        public Schwefel()
        {
            LimiteInferior = -100;
            LimiteSuperior = 100;
            FitnessOptimo = 0.0;
        }

        /// <summary>
        /// Returns the value of the optimization function
        /// </summary>
        /// <param name="csv">An array with dimension data to assess the optimization function</param>        
        /// <returns>
        /// The value of the function evaluated
        /// </returns>
        public override double Evaluar(double[] csv)
        {
            var dimensionNumber = csv.Length;
            var evaluationValue = 0d;
            if (dimensionNumber >= 1)
            {
                for (var i = 0; i < dimensionNumber; i++)
                {
                    var sum = 0d;
                    for (var j = 0; j <= i; j++)
                        sum += csv[j];
                    evaluationValue += Math.Pow(sum, 2);
                }
            }
            else
            {
                throw (new Exception("The function must have at least one dimension"));
            }
            return evaluationValue;
        }

        public override string ToString()
        {
            return "Schwefel";
        }
     }
}