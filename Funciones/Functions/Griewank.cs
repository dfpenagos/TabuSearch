using System;
using System.Linq;

namespace ServicesLogic.Functions
{
    /// <summary>
    /// Implements the function Griewank
    /// </summary>
    public class Griewank: Funcion
    {
        /// <summary>
        /// Establece los limites de optimización para esta función
        /// </summary>
        public Griewank()
        {
            LimiteInferior = -600;
            LimiteSuperior = 600;
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
            double evaluationValue;
            if (dimensionNumber >= 1)
            {
                const double a = 4000;                
                double b = 1;

                var sum = csv.Sum(x => Math.Pow(x, 2));

                for (var i = 0; i < dimensionNumber; i++)
                {
                    b *= Math.Cos(csv[i] / (Math.Sqrt(i + 1)));
                }

                evaluationValue = (sum/a) - b + 1;
            }
            else
            {
                throw (new Exception("The function must have at least one dimension"));
            }
            return evaluationValue;
        }

        public override string ToString()
        {
            return "Griewank";
        }
    }
}