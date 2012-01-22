using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordBook.Contracts
{
    public class PasswordEntry : IEquatable<PasswordEntry>
    {
        public PasswordEntry() 
        {
        }

        public PasswordEntry(Guid id)
        {
            Id = id;
        }

        public PasswordEntry(PasswordEntry entry)
        {
            if (entry == null) throw new ArgumentNullException("entry");

            Id = entry.Id;
            Title = entry.Title;
            UserName = entry.UserName;
            Password = entry.Password;
        }

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public override bool Equals(object obj)
        {
            return Equals(obj as PasswordEntry);
        }

        public bool Equals(PasswordEntry other)
        {
            if (other == null) return false;

            return Object.Equals(Id, other.Id);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public override string ToString()
        {
            return Title ?? Id.ToString();
        }
    }
}
