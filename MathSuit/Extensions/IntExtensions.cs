using System;

namespace MathSuit.Extensions
{
    public static class IntExtensions
    {
        public static int Sign(this int v)
        {
            return Math.Sign(v);
        }

        public static int Abs(this int v)
        {
            return Math.Abs(v);
        }

        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        public static bool IsOdd(this int value)
        {
            return value % 2 == 1;
        }
    }
}
