namespace ServicesLogic.Functions
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Funcion
    {
        /// <summary>
        /// Limite Inferior para Optimizar esta función
        /// </summary>
        public double LimiteInferior;

        /// <summary>
        /// Limite Superior para Optimizar esta función
        /// </summary>
        public double LimiteSuperior;

        /// <summary>
        /// Si se conoce el valor óptimo global se define en este atributo
        /// </summary>
        public double FitnessOptimo = double.NaN;

        /// <summary>
        /// Hace la evauación de la función con los valores especificos en cada dimension
        /// </summary>
        /// <param name="csv"></param>
        /// <returns></returns>
        public abstract double Evaluar(double[] csv);
      
    }
}