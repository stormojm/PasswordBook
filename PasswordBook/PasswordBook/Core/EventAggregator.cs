using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PasswordBook.Contracts;
using System.Threading;

namespace PasswordBook.Core
{
    public class EventAggregator : IEventAggregator
    {
        private Dictionary<RuntimeTypeHandle, List<object>> _handlerList;
        private SpinLock _handlerListLock;

        public EventAggregator()
        {
            _handlerList = new Dictionary<RuntimeTypeHandle, List<object>>();
            _handlerListLock = new SpinLock();
        }

        public void Subscribe<T>(Action<T> handler)
        {
            RuntimeTypeHandle typeHandle = typeof(T).TypeHandle;

            bool lockTaken = false;
            _handlerListLock.Enter(ref lockTaken);

            List<object> messageHandlers;
            if (_handlerList.TryGetValue(typeHandle, out messageHandlers))
            {
                messageHandlers.Add(handler);
            }
            else
            {
                messageHandlers = new List<object> { handler };
                _handlerList.Add(typeHandle, messageHandlers);
            }

            _handlerListLock.Exit();
        }

        public void Send<T>(T message)
        {
            RuntimeTypeHandle typeHandle = typeof(T).TypeHandle;

            List<Action<T>> convertHandlers = null;
            bool lockTaken = false;
            _handlerListLock.Enter(ref lockTaken);

            List<object> messageHandlers;
            if (_handlerList.TryGetValue(typeHandle, out messageHandlers))
            {
                convertHandlers = messageHandlers.OfType<Action<T>>().ToList();
            }

            _handlerListLock.Exit();

            if (convertHandlers == null) return;

            foreach (Action<T> handler in convertHandlers)
            {
                try
                {
                    handler(message);
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
