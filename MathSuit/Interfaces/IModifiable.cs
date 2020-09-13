using System;

namespace MathSuit.Interfaces
{
    public interface IModifiable<T>
    {
        void ForEachModify(Func<T, T> modifyFn);
    }
}