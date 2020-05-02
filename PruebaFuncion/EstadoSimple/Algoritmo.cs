using System;
using ServicesLogic.Functions;

namespace OptimizacionContinua.EstadoSimple
{
    public abstract class Algoritmo
    {
		//Definición de propiedades y métodos
        public Random Aleatorio { get; set; }
        public Funcion MiFuncion { get; set; }
        public int TotalDimensiones { get; set; }
        public int MaximoNumeroEvaluacionesFuncionObjetivo { get; set; } //Máximo número de evaluaciones de la función objetivo

        public int EFOs;									//Número de evaluaciones de la función objetivo
        public Solucion MejorSolucion;

        public abstract void Ejecutar();
    }
}