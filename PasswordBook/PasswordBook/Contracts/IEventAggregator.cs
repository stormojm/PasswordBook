using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordBook.Contracts
{
    public interface IEventAggregator
    {
        void Subscribe<T>(Action<T> handler);

        void Send<T>(T message);
    }
}
