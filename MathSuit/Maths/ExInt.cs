using System.Collections.Generic;
using System.Linq;

namespace MathSuit.Maths
{
    public class ExInt
    {
        public int V { get; set; }
        public List<int> Multipliers => multipliers ?? (multipliers = GetMulipliers());
        private List<int> multipliers = null;

        public ExInt(int v)
        {
            V = v;
        }

        private ExInt(int v, List<int> multipliers)
            : this(v)
        {
            this.multipliers = multipliers;
        }

        public void Reduce(List<int> mults)
        {
            mults.ForEach(m=>Reduce(m));
        }

        public bool Reduce(int mult)
        {
            if (!Multipliers.Contains(mult))
                return false;

            Multipliers.Remove(mult);
            V /= mult;
            return true;
        }

        public List<int> FindMultipliers(List<int> mults)
        {
            var myMults = Multipliers.ToList();
            var res = new List<int>();
            mults.ForEach(m =>
            {
                if (myMults.Contains(m))
                {
                    res.Add(m);
                    myMults.Remove(m);
                }
            });
            return res;
        }

        private List<int> GetMulipliers()
        {
            var mults = new List<int>();
            var mult = 2;
            var v = V;
            while (v > 1)
            {
                if (v % mult == 0)
                {
                    v /= mult;
                    mults.Add(mult);
                }
                else
                    mult++;
            }

            return mults;
        }

        public override string ToString()
        {
            return V.ToString();
        }

        public static ExInt operator *(ExInt a, ExInt b)
        {
            return new ExInt(a.V * b.V, a.Multipliers.Concat(b.Multipliers).ToList());
        }

        public static ExInt operator +(ExInt a, ExInt b)
        {
            return new ExInt(a.V + b.V);
        }

        public static ExInt operator -(ExInt a, ExInt b)
        {
            return new ExInt(a.V - b.V);
        }

        public static ExInt operator +(ExInt a, int b)
        {
            return new ExInt(a.V + b);
        }

        public static ExInt operator -(ExInt a, int b)
        {
            return new ExInt(a.V - b);
        }

        public static implicit operator ExInt(int a)
        {
            return new ExInt(a);
        }
    }
}