using System;

namespace ServicesLogic.Functions
{
    /// <summary>
    /// Implements the function Step.
    /// </summary>
    public class Step : Funcion
    {
        /// <summary>
        /// Establece los limites de optimización para esta función
        /// </summary>
        public Step()
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
            double evaluationValue;
            if (dimensionNumber >= 1)
            {
                var sum = 0.0;
                for (var i = 0; i < dimensionNumber; i++)
                {
                    var val = Math.Truncate(csv[i] + 0.5);
                    sum += Math.Pow(val, 2);
                }
                evaluationValue = sum;
            }
            else
            {
                throw (new Exception("The function must have at least one dimension"));
            }
            return evaluationValue;
        }

        public override string ToString()
        {
            return "Step";
        }
    }
}