using System.Collections.Generic;
using System.Linq;

namespace MathSuit.Maths
{
    public class PolinomRoot
    {
        public Polinom Polinom { get; }
        public double[] Roots => roots.OrderBy(r => r).ToArray();
        public double? MinRoot => roots.OrderBy(r => r).FirstOrDefault();
        public double? MinPositiveRoot => roots.Where(r => r >= 0).OrderBy(r => r).FirstOrDefault();
        public bool HasRoots => roots.Count > 0;

        private List<double> roots = new List<double>();

        public void AddRoot(double root)
        {
            roots.Add(root);
        }

        public PolinomRoot(Polinom polinom)
        {
            Polinom = polinom;
        }
    }
}