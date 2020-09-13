using System;
using MathSuit.Maths;

namespace MathSuit.Extensions
{
    public static class PolinomExtensions
    {
        public static PolinomRoot QuadraticRoots(this Polinom polinom)
        {
            if (polinom.Power != 2)
                throw new InvalidOperationException("Polinom power must be 2");

            var roots = new PolinomRoot(polinom);
            var a = polinom.A;
            var b = polinom.B;
            var c = polinom.C;

            var d2 = b * b - 4 * a * c;
            if (d2 < 0)
                return roots;

            var d = Math.Sqrt(d2);

            var x1 = (-b - d) / (2 * a);
            var x2 = (-b + d) / (2 * a);

            roots.AddRoot(x1);
            roots.AddRoot(x2);

            return roots;
        }

        public static PolinomDiv Divide(this Polinom polinom, Polinom divider)
        {
            var power = polinom.Power - divider.Power;
            var result = new Polinom();
            var reminder = polinom;
            (power+1).ForEachNum(i =>
            {
                if (reminder.Power + i == polinom.Power)
                {
                    var c = reminder.Root / divider.Root;
                    var subPolinom = (c * divider).PowerShiftUp(power - i);
                    reminder = reminder - subPolinom;
                    result.PowerUp(c);
                }
                else
                {
                    result.PowerUp(0);
                }
            });

            return new PolinomDiv() {Result = result, Remainder = reminder};
        }

        public static RationalPolinomDiv Divide(this RationalPolinom polinom, RationalPolinom divider)
        {
            var power = polinom.Power - divider.Power;
            var result = new RationalPolinom();
            var reminder = polinom;
            (power + 1).ForEachNum(i =>
            {
                if (reminder.Power + i == polinom.Power)
                {
                    var c = reminder.Root / divider.Root;
                    var subRationalPolinom = (c * divider).PowerShiftUp(power - i);
                    reminder = reminder - subRationalPolinom;
                    result.PowerUp(c);
                }
                else
                {
                    result.PowerUp(0);
                }
            });

            return new RationalPolinomDiv() { Result = result, Remainder = reminder };
        }
    }
}