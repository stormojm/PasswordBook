using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordBook.Contracts
{
    public interface IPasswordSheet
    {
        void Create(PasswordEntry entry);

        PasswordEntry Get(Guid id);

        IEnumerable<PasswordEntry> GetAll();

        void Update(Guid id, PasswordEntry newData);

        void Delete(Guid id);
    }
}
