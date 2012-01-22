using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordBook
{
    public interface IViewModelBehavior<T>
    {
        void Initialize(T viewModel);
    }
}
