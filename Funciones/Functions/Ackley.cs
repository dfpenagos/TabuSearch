using System;
namespace ServicesLogic.Functions
{
    /// <summary>
    /// Implements the Ackley's function
    /// </summary>
    public class Ackley : Funcion
    {
        /// <summary>
        /// Establece los limites de optimización para esta función
        /// </summary>
        public Ackley()
        {
            LimiteInferior = -32;
            LimiteSuperior = 32;
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
            int dimensionNumber = csv.Length;
            double evaluationValue;

            if (dimensionNumber >= 1)
            {
                const double a = 20;
                const double b = 0.2;
                const double c = 2 * Math.PI;
                double x = 0;
                double y = 0;

                foreach (var value in csv)
                {
                    x += Math.Pow(value, 2);
                    y += Math.Cos(c * value);
                }

                evaluationValue = -a * Math.Exp(-b * Math.Sqrt(x / dimensionNumber)) -
                                  Math.Exp(y / dimensionNumber) +
                                  a + Math.Exp(1);
            }
            else
            {
                throw (new Exception("The function must have at least one dimension"));
            }
            return evaluationValue;
        }

        public override string ToString()
        {
            return "Ackley";
        }
    }
}