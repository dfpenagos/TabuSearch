using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OptimizacionContinua.EstadoSimple.TS
{
    public class TabuSearch : Algoritmo
    {
        private double iMinTweek;
        private double iMaxTweek;
        private int neighbors;
        private int tabuListLenght;

        public TabuSearch()
        {
            iMinTweek = -1;
            iMaxTweek = 1;
            neighbors = 2;
            tabuListLenght = 2;
        }
        public TabuSearch(double tweek, int neighbor, int listLength) {
            iMinTweek = Math.Abs(tweek) * -1;
            iMaxTweek = Math.Abs(tweek);
            neighbors = neighbor;
            tabuListLenght = listLength;
        }
        public override void Ejecutar()
        {
            EFOs = 0;                                                               // Variable de Inicio del ciclo
                                                                                    // Tamaño maximo de la lista
            var s = new Solucion(TotalDimensiones, this);                           //Crea un objeto de la clase solución para el algoritmo actual
            s.InicializarAleatorio();                                               //Inicializa el objeto solución
            MejorSolucion = new Solucion(s);                                        //Crea un nuevo objeto copiando el contenido del objeto s (Constructor de copia)

            List<Solucion> solutionList = new List<Solucion>();                     //Se declara lista de soluciones
            solutionList.Add(new Solucion(s));                                      //Se incluye la solución incial

            while (EFOs < MaximoNumeroEvaluacionesFuncionObjetivo)
            {

                if (solutionList.Count > tabuListLenght) {                           // Se evalua si la lista contiene una cantidad mayor de los elementos permitidos.
                    solutionList.RemoveAt(0);                                       // Cuando se llena la lista tabú se remueve el elemento más viejo.
                }
                var r = Tweek(s);

                for (int j = neighbors - 1; j>=0; j--) {
                    var w = Tweek(s);
                    if ((!ContainsSolutionInList(w, solutionList) && (w.Fitness < r.Fitness)) || 
                        (ContainsSolutionInList(r, solutionList))) {
                        r = new Solucion(w);
                    }
                }

                if ((!ContainsSolutionInList(r, solutionList)) && (r.Fitness < s.Fitness)) {
                    s = new Solucion(r);
                    solutionList.Add(r);
                }

                if (s.Fitness < MejorSolucion.Fitness) {                             //Si el nuevo valor es mejor se reemplaza la mejor solución (minimizando)
                    MejorSolucion = new Solucion(s);                                //Crea un nuevo objeto copiando el contenido del objeto s (Constructor de copia)
                }

                if (Math.Abs(s.Fitness - MiFuncion.FitnessOptimo) < 1e-12) break;   //Se sale del ciclo cuando se logra llegar a un valor 
            }
        }

        public override string ToString() //Recupera el nombre de este algoritmo
        {
            return "Tabu Search";
        }

        public Solucion Tweek(Solucion solution) {
            var newSolution = new Solucion(solution);
            for (int i = 0; i < TotalDimensiones; i++) {
                newSolution.Dimensiones[i] = newSolution.Dimensiones[i] + GetRandomNumber();
            }
            newSolution.CalcularFitness();
            return newSolution;
        }

        public double GetRandomNumber()
        {
            Random random = new Random();
            return random.NextDouble() * (iMaxTweek - iMinTweek) + iMinTweek;
        }

        public bool ContainsSolutionInList(Solucion oSolution, List<Solucion> list) {
            for (int i = 0; i < list.Count; i++) {
                if (list[i].Equals(oSolution)) {
                    return true;
                }
            }
            return false;
        }
    }
}
