using Aaron.Core.Infrastructure;

namespace Aaron.Core.Threading
{
    public abstract class Task : ITask
    {
        IThreadManager _thread;

        public Task()
        {
            _thread = IoC.Resolve<IThreadManager>();
        }

        public abstract void DefineTask(object stateInfo);

        /// <summary>
        /// Execute task
        /// </summary>
        public void Execute()
        {
            _thread.OnThreadStart((x) => 
            {
                DefineTask(x);
            });
        }
    }
}
