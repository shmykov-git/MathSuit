using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using MathSuit.Extensions;
using MathSuit.Interfaces;
using Suit.Extensions;

namespace MathSuit.Maths
{
    public class RationalPolinom : IEnumerable<Rational>, IModifiable<Rational>, ICloneable<RationalPolinom>, IEquatable<RationalPolinom>
    {
        private List<Rational> Coefficients { get; }

        public int Power => Coefficients.Count - 1;
        public int Count => Coefficients.Count;

        public Rational Root => Coefficients[0];
        public Rational A => Coefficients[0];
        public Rational B => Coefficients[1];
        public Rational C => Coefficients[2];
        public Rational D => Coefficients[3];

        public Rational PreLast => Coefficients[Coefficients.Count - 2];
        public Rational Last => Coefficients[Coefficients.Count - 1];

        private int TailPower(int i) => Power - i;

        public Rational this[int i, bool fromRoot = true]
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


        public IEnumerator<Rational> GetEnumerator()
        {
            foreach (var coefficient in Coefficients)
            {
                yield return coefficient;
            }
        }

        public RationalPolinom PowerShiftUp(int c)
        {
            (c).ForEachNum(i=>PowerUp(0));
            return this;
        }

        public void PowerUp(Rational v)
        {
            Coefficients.Add(v);
        }

        public void PowerRootUp(Rational v)
        {
            Coefficients.Insert(0, v);
        }

        public void PowerRootDown()
        {
            Coefficients.RemoveAt(0);
        }

        public void ForEachModify(Func<Rational, Rational> modifyFn)
        {
            this.ToIndexArray().ForEach(i=>this[i] = modifyFn(this[i]));
        }

        public RationalPolinom Clone()
        {
            return new RationalPolinom(this.ToArray());
        }

        public bool Equals(RationalPolinom other)
        {
            if (Count != other.Count)
                return false;

            return (Power).Range().All(i => this[i] == other[i]);
        }

        public override bool Equals(object obj)
        {
            return obj != null && Equals((RationalPolinom) obj);
        }

        public override string ToString()
        {
            var str = this.SelectByIndex((i, v) => CoefStr(Power - i, v)).SJoin("");
            var signStarts = str.StartsWith("+") ? 1 : 0;
            var signEnds = str.EndsWith("+") || str.EndsWith("-") ? 1 : 0;
            return str.Substring(signStarts, str.Length - signStarts - signEnds);           
        }

        public string CoefStr(int power, Rational value)
        {
            if (value == 0)
                return "";

            var sign = value > 0 ? "+" : "";

            if (power == 0)
                return $"{sign}{value:0.###}";

            var sx = power > 1 ? $"x^{power}" : "x";

            if (value == 1)
                return $"{sign}{sx}";

            return $"{sign}{value:0.###}{sx}";
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void ClearRoot()
        {
            while (Root == 0 && Power > 0)
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

        public static RationalPolinom operator -(RationalPolinom a, RationalPolinom b)
        {
            var res = a.Clone();
            (b.Power + 1).ForEachNum(i => res[i, false] -= b[i, false]);
            res.ClearRoot();
            return res;
        }

        public static RationalPolinom operator *(RationalPolinom a, Rational k)
        {
            var res = a.Clone();
            res.ForEachModify(v => v * k);
            res.ClearRoot();
            return res;
        }

        public static RationalPolinom operator *(Rational k, RationalPolinom a)
        {
            return a * k;
        }

        public static RationalPolinom operator /(RationalPolinom a, Rational k)
        {
            var res = a.Clone();
            res.ForEachModify(v => v / k);
            return res;
        }

        public static RationalPolinom operator *(RationalPolinom a, RationalPolinom b)
        {
            var res = new RationalPolinom();
            (a.Count, b.Count).ForEachRange((i,j)=> { res[i + j] += a[i] * b[j]; });
            return res;
        }

        public static RationalPolinom operator /(RationalPolinom a, RationalPolinom b)
        {
            var res = a.Divide(b);
            return res.Result;
        }

        public static RationalPolinom operator %(RationalPolinom a, RationalPolinom b)
        {
            var res = a.Divide(b);
            return res.Remainder;
        }

        public static RationalPolinom operator +(RationalPolinom a, RationalPolinom b)
        {
            var res = a.Clone();
            (b.Power + 1).ForEachNum(i => res[i, false] += b[i, false]);
            res.ClearRoot();
            return res;
        }

        #region Ctor

        public RationalPolinom(params Rational[] coefficients)
        {
            Coefficients = coefficients.ToList();
        }

        public RationalPolinom(Rational a) : this(new[] { a }) { }
        public RationalPolinom(Rational a, Rational b) : this(new[] { a, b }) { }
        public RationalPolinom(Rational a, Rational b, Rational c) : this(new[] { a, b, c }) { }
        public RationalPolinom(Rational a, Rational b, Rational c, Rational d) : this(new[] { a, b, c, d }) { }

        #endregion
    }
}