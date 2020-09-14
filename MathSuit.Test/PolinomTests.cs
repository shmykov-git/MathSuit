using MathSuit.Maths;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Suit;
using Suit.Logs;

namespace MathSuit.Test
{
    [TestClass]
    public class PolinomTests
    {
        static PolinomTests()
        {
            IoC.Configure(IoCTest.Register);
        }

        private ILog log = IoC.Get<ILog>();

        [TestMethod]
        public void PolinomDivTest()
        {
            var a = new Polinom(1, 2, 5, 7, 0, 0, 3, 5);
            log.Debug($"a = {a}");

            var b = new Polinom(1, 2);
            log.Debug($"b = {b}");

            var c = a / b;
            log.Debug($"a / b = {c}");

            var cR = a % b;
            log.Debug($"a % b = {cR}");

            var aa = c * b + cR;
            log.Debug($"a = c*b + a%b = {aa}");

            Assert.AreEqual(a, aa, "Incorrect");
        }

        [TestMethod]
        public void RationalTest()
        {
            Rational a = (1, 3456);
            Rational b = 2;
            var c = a + b;
            var aa = c - b;
            var bb = c - a;

            Assert.AreEqual(a, aa, "Incorrect");
            Assert.AreEqual(b, bb, "Incorrect");
        }

        /// <summary>
        /// Once up on a time I was young and very clever
        /// I took the most strong task from my math professor to prove him that I maybe clever then I think about my self
        /// But... I couldn't do this task... and I just threw it away from my mind because I saw that it is very difficult for me
        /// That's was my beginning to become not so clever
        /// Day by day I was coming a little bit stupider and stupider
        /// So... one day I reach the mind position where the most stupid class ever can be implemented
        /// (I believe you will never find something like RationalPolinom class because there is no need to have it)
        /// So... The most stupid class had been implemented by me and I stated to feel that I was becoming clever day by day again...
        /// 
        /// I have already been becoming clever for some years so...
        /// The clever level of my mind is high enough now and maybe this is time for another one task?
        /// </summary>
        [TestMethod]
        public void RationalPolinomDivTest()
        {
            var a = new RationalPolinom(1, (1, 2), 5, 7, 0, 0, 3, 5);
            log.Debug($"a = {a}");

            var b = new RationalPolinom(1, 2);
            log.Debug($"b = {b}");

            var c = a / b;
            log.Debug($"a / b = {c}");

            var cR = a % b;
            log.Debug($"a % b = {cR}");

            var aa = c * b + cR;
            log.Debug($"a = c*b + a%b = {aa}");

            Assert.AreEqual(a, aa, "Incorrect");
        }
    }
}
