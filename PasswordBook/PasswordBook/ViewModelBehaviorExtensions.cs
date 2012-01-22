using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordBook
{
    public static class ViewModelBehaviorExtensions
    {
        public static void InitializeAll<T>(this IEnumerable<IViewModelBehavior<T>> behaviors, T value)
        {
            foreach (IViewModelBehavior<T> behavior in behaviors)
            {
                behavior.Initialize(value);
            }
        }
    }
}
