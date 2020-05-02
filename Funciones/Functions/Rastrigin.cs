using System;

namespace ServicesLogic.Functions
{
    /// <summary>
    /// Implements the function Rastrigin
    /// </summary>
    public class Rastrigin: Funcion
    {
        /// <summary>
        /// Establece los limites de optimización para esta función
        /// </summary>
        public Rastrigin()
        {
            LimiteInferior = -5.12;
            LimiteSuperior = 5.12;
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
            double evaluationValue = 0d;

            if (dimensionNumber >= 1)
            {
                for (var i = 0; i < dimensionNumber; i++)
                    evaluationValue += Math.Pow(csv[i], 2) - 10*Math.Cos(2*Math.PI*csv[i]) + 10;
            }
            else
            {
                throw (new Exception("The function must have at least one dimension"));
            }
            return evaluationValue;
        }

        public override string ToString()
        {
            return "Rastrigin";
        }
    }
}