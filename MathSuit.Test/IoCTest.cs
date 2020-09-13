using Suit;
using Suit.Logs;
using Unity;

namespace MathSuit.Test
{
    public static class IoCTest
    {
        public static void Register(UnityContainer container)
        {
            container.RegisterType<ILog, LogToConsoleAndDebug>();
        }
    }
}