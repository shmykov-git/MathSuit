namespace MathSuit.Maths
{
    public static class Math2
    {
        public static Vector Kramer(Vector a, Vector b, Vector c, Vector d)
        {
            return new Matrix(a, b, c).Kramer(d);
        }
    }
}