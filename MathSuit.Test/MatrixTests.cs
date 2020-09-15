using MathSuit.Maths;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MathSuit.Test
{
    [TestClass]
    public class MatrixTests
    {
        [TestMethod]
        public void MatrixDTest()
        {
            var m = new Matrix(new[]
            {
                new double []{505, 65, 10},
                new double []{4355, 505, 65},
                new double []{39973, 4355, 505 }
            });

            var D = m.D();
            Assert.AreEqual((decimal)D, -435600, "Incorrect Matrix D");

            var v = new Vector(-28.4, -66, 367.2);
            var Da = m.InsCol(0, v).D();
            Assert.AreEqual((decimal)Da, -214170, "Incorrect Matrix Da");
        }

        /// <summary>
        /// This is very base method in 3D geometry
        /// To be able to find planes intersection for example
        /// </summary>
        [TestMethod]
        public void KramerTest()
        {
            var m = new Matrix(new[]
            {
                new double []{6, 2, 3},
                new double []{5, 6, 7},
                new double []{9, 10, 11 }
            });

            var d = new Vector(5, 5, 5);

            var D = m.D();

            var D1 = m.InsCol(0, d).D();
            var D2 = m.InsCol(1, d).D();
            var D3 = m.InsCol(2, d).D();

            var p = new Vector(D1 / D, D2 / D, D3 / D);
            Assert.AreEqual(p.X, 0, "Incorrect Kramer method");
            Assert.AreEqual(p.Y, -5, "Incorrect Kramer method");
            Assert.AreEqual(p.Z, 5, "Incorrect Kramer method");

            var a = new Vector(6, 2, 3);
            var b = new Vector(5, 6, 7);
            var c = new Vector(9, 10, 11);
            var p1 = Math2.Kramer(a, b, c, d);
            Assert.AreEqual(p1.X, 0, "Incorrect Kramer method");
            Assert.AreEqual(p1.Y, -5, "Incorrect Kramer method");
            Assert.AreEqual(p1.Z, 5, "Incorrect Kramer method");
        }
    }
}