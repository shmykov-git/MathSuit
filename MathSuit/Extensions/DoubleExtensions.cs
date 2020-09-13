using System;

namespace MathSuit.Extensions
{
    public static class DoubleExtensions
    {
        public const double Epsilon = 1E-10;

        public static int Sign(this double v)
        {
            return v.Abs() < Epsilon ? 0 : Math.Sign(v);
        }

        public static double Abs(this double v)
        {
            return Math.Abs(v);
        }

        public static decimal Decimal(this double v)
        {
            return (decimal) v;
        }

        public static int RoundToInt(this double x)
        {
            return (int)Math.Round(x);
        }

        public static int SignedRoundToInt(this double x)
        {
            return x.Sign() * x.Abs().RoundToInt();
        }
    }
}