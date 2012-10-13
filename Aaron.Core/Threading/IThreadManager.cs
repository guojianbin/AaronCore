using System.Threading;

namespace Aaron.Core.Threading
{
    public interface IThreadManager
    {
        Thread GetThread { get; }
        bool IsAlive { get; }
        void InitThread(ParameterizedThreadStart acquire, string name = null);
        void Start();
        void Abort();
        void Join(int millisecondsTimeout = 0);
    }
}
