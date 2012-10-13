using System.Collections.Generic;
using System.Threading;

namespace Aaron.Core.Threading
{
    public static class ThreadExtensions
    {
        public static void OnThreadStart(this IThreadManager threadManager, ParameterizedThreadStart acquire)
        {
            OnThreadStart(threadManager, null, acquire);
        }

        public static void OnThreadStart(this IThreadManager threadManager, string name, ParameterizedThreadStart acquire)
        {
            threadManager.InitThread(acquire, name);
            threadManager.Start();
        }
    }
}
