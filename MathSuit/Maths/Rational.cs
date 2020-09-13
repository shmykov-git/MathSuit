using System;
using MathSuit.Extensions;

namespace MathSuit.Maths
{
    public class Rational : IEquatable<Rational>
    {
        public ExInt M { get; set; }
        public ExInt N { get; set; }

        private int[] mMults;
        private int[] nMults;

        public Rational(int m, int n)
        {
            M = m;
            N = n;
        }

        public Rational(ExInt m, ExInt n)
        {
            M = m;
            N = n;
        }

        public void Reduce()
        {
            var commonMults = M.FindMultipliers(N.Multipliers);
            M.Reduce(commonMults);
            N.Reduce(commonMults);
        }

        public bool Equals(Rational other)
        {
            if (other is null)
                return false;

            return M.V == other.M.V && N.V == other.N.V;
        }

        public override bool Equals(object other)
        {
            return !(other is null) && Equals((Rational)other);
        }

        public override int GetHashCode()
        {
            return Hash.GetHashCode(M.V, N.V);
        }

        public override string ToString()
        {
            if (N.V == 1)
                return $"{M}";

            if (M.V.Abs() < N.V)
                return $"{M}/{N}";

            var k = M.V.Sign() * (M.V.Abs() / N.V);
            var l = M.V.Abs() % N.V;

            return $"{k}({l}/{N})";
        }

        public Rational Abs()
        {
            return (M.V.Abs(), N.V.Abs());
        }

        public int Sign()
        {
            return M.V.Sign() * N.V.Sign();
        }

        public static Rational operator *(Rational a, Rational b)
        {
            Rational c = (a.M * b.M, a.N * b.N);
            c.Reduce();

            return c;
        }

        public static Rational operator /(Rational a, Rational b)
        {
            Rational c = (a.M * b.N, a.N * b.M);
            c.Reduce();

            return c;
        }

        public static Rational operator +(Rational a, Rational b)
        {
            Rational c = (a.M * b.N + b.M * a.N, a.N * b.N);
            c.Reduce();

            return c;
        }

        public static Rational operator -(Rational a, Rational b)
        {
            Rational c = (a.M * b.N - b.M * a.N, a.N * b.N);
            c.Reduce();

            return c;
        }

        public static bool operator >(Rational a, Rational b)
        {
            return a.M.V * b.N.V - b.M.V * a.N.V > 0;
        }

        public static bool operator >=(Rational a, Rational b)
        {
            return a.M.V * b.N.V - b.M.V * a.N.V >= 0;
        }

        public static bool operator <(Rational a, Rational b)
        {
            return a.M.V * b.N.V - b.M.V * a.N.V < 0;
        }

        public static bool operator <=(Rational a, Rational b)
        {
            return a.M.V * b.N.V - b.M.V * a.N.V <= 0;
        }

        public static bool operator ==(Rational a, Rational b)
        {
            return a.M.V * b.N.V - b.M.V * a.N.V == 0;
        }

        public static bool operator !=(Rational a, Rational b)
        {
            return !(a == b);
        }

        public static implicit operator Rational((ExInt, ExInt) a)
        {
            return new Rational(a.Item1, a.Item2);
        }

        public static implicit operator Rational(ExInt a)
        {
            return new Rational(a, 1);
        }

        public static implicit operator Rational((int, int) a)
        {
            return new Rational(a.Item1, a.Item2);
        }

        public static implicit operator Rational(int a)
        {
            return new Rational(a, 1);
        }
    }
}