using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordBook.Contracts
{
    public interface IPasswordSheetFactory
    {
        bool LoadSheet(string password);

        IPasswordSheet Get();

        void SaveSheet();
    }
}
