using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MathSuit.Extensions;
using MathSuit.Interfaces;
using Suit.Extensions;

namespace MathSuit.Maths
{
    public class Polinom : IEnumerable<double>, IModifiable<double>, ICloneable<Polinom>, IEquatable<Polinom>
    {
        private List<double> Coefficients { get; }

        public int Power => Coefficients.Count - 1;
        public int Count => Coefficients.Count;

        public double Root => Coefficients[0];
        public double A => Coefficients[0];
        public double B => Coefficients[1];
        public double C => Coefficients[2];
        public double D => Coefficients[3];

        public double PreLast => Coefficients[Coefficients.Count - 2];
        public double Last => Coefficients[Coefficients.Count - 1];
        private int TailPower(int i) => Power - i;

        public double this[int i, bool fromRoot = true]
        {
            get
            {
                if (i > Power || i < 0)
                    return 0;
                else
                    return Coefficients[fromRoot ? i : Power - i];
            }
            set
            {
                SetRootPower(i, fromRoot);
                Coefficients[fromRoot ? i : Power - i] = value;
            }
        }

        public IEnumerator<double> GetEnumerator()
        {
            foreach (var coefficient in Coefficients)
            {
                yield return coefficient;
            }
        }

        public Polinom PowerShiftUp(int c)
        {
            (c).ForEachNum(i=>PowerUp(0));
            return this;
        }

        public void PowerUp(double v)
        {
            Coefficients.Add(v);
        }

        public void PowerRootUp(double v)
        {
            Coefficients.Insert(0, v);
        }

        public void PowerRootDown()
        {
            Coefficients.RemoveAt(0);
        }

        public void ForEachModify(Func<double, double> modifyFn)
        {
            this.ToIndexArray().ForEach(i=>this[i] = modifyFn(this[i]));
        }

        public Polinom Clone()
        {
            return new Polinom(this.ToArray());
        }

        public bool Equals(Polinom other)
        {
            if (Count != other.Count)
                return false;

            return (Power).Range().All(i => this[i] - other[i] < DoubleExtensions.Epsilon);
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals((Polinom) obj);
        }

        public override string ToString()
        {
            var str = this.SelectByIndex((i, v) => CoefStr(Power - i, v)).SJoin("");
            var signStarts = str.StartsWith("+") ? 1 : 0;
            var signEnds = str.EndsWith("+") || str.EndsWith("-") ? 1 : 0;
            return str.Substring(signStarts, str.Length - signStarts - signEnds);           
        }

        public string CoefStr(int power, double value)
        {
            if (value.Abs() < DoubleExtensions.Epsilon)
                return "";

            var sign = value > 0 ? "+" : "";

            if (power == 0)
                return $"{sign}{value:0.###}";

            var sx = power > 1 ? $"x^{power}" : "x";

            if ((value.Abs()-1).Abs() < DoubleExtensions.Epsilon)
                return $"{sign}{sx}";

            return $"{sign}{value:0.###}{sx}";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ClearRoot()
        {
            while (Root.Abs() < DoubleExtensions.Epsilon && Power > 0)
            {
                PowerRootDown();
            }
        }

        private void SetRootPower(int i, bool fromRoot)
        {
            var j = fromRoot ? i : Power - i;
            while (Power < j)
            {
                if (fromRoot)
                    PowerUp(0);
                else
                    PowerRootUp(0);
            }
        }

        public static Polinom operator -(Polinom a, Polinom b)
        {
            var res = a.Clone();
            (b.Power + 1).ForEachNum(i => res[i, false] -= b[i, false]);
            res.ClearRoot();
            return res;
        }

        public static Polinom operator *(Polinom a, double k)
        {
            var res = a.Clone();
            res.ForEachModify(v => v * k);
            res.ClearRoot();
            return res;
        }

        public static Polinom operator *(double k, Polinom a)
        {
            return a * k;
        }

        public static Polinom operator /(Polinom a, double k)
        {
            var res = a.Clone();
            res.ForEachModify(v => v / k);
            return res;
        }

        public static Polinom operator *(Polinom a, Polinom b)
        {
            var res = new Polinom();
            (a.Count, b.Count).ForEachRange((i,j)=> { res[i + j] += a[i] * b[j]; });
            return res;
        }

        public static Polinom operator /(Polinom a, Polinom b)
        {
            var res = a.Divide(b);
            return res.Result;
        }

        public static Polinom operator %(Polinom a, Polinom b)
        {
            var res = a.Divide(b);
            return res.Remainder;
        }

        public static Polinom operator +(Polinom a, Polinom b)
        {
            var res = a.Clone();
            (b.Power + 1).ForEachNum(i => res[i, false] += b[i, false]);
            res.ClearRoot();
            return res;
        }

        #region Ctor

        public Polinom(params double[] coefficients)
        {
            Coefficients = coefficients.ToList();
        }

        public Polinom(double a) : this(new[] { a }) { }
        public Polinom(double a, double b) : this(new[] { a, b }) { }
        public Polinom(double a, double b, double c) : this(new[] { a, b, c }) { }
        public Polinom(double a, double b, double c, double d) : this(new[] { a, b, c, d }) { }

        #endregion
    }
}