using System;
using System.Threading;

namespace Aaron.Core.Threading
{
    public class ThreadManager : IThreadManager
    {
        Thread _thread;

        public Thread GetThread
        {
            get { return _thread; }
        }

        public void InitThread(ParameterizedThreadStart acquire, string name = null)
        {
            _thread = new Thread(acquire);
            if (name != null)
                _thread.Name = name;
        }

        public void Start()
        {
            if (_thread == null) throw new NotImplementedException();
            _thread.Start();
        }

        public void Abort()
        {
            if (_thread == null) throw new NotImplementedException();
            _thread.Abort();
        }

        public void Join(int millisecondsTimeout = 0)
        {
            if (_thread == null) throw new NotImplementedException();
            if (millisecondsTimeout > 0)
                _thread.Join(millisecondsTimeout);
            else 
                _thread.Join();
        }

        public bool IsAlive
        {
            get 
            {
                return (_thread == null) ? false : _thread.IsAlive;
            }
        }
    }
}