using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChainOfResponsibilityPattern.ChainOfResponsibility
{
    public abstract class ProcessHandler : IProcessHandler
    {
        private IProcessHandler nextProcessHandler;

        public virtual object HandleAsync(object o)
        {
            if (nextProcessHandler != null)
            {
                return nextProcessHandler.HandleAsync(o);
            }

            return null;
        }

        public IProcessHandler SetNext(IProcessHandler processHandler)
        {
            nextProcessHandler = processHandler;
            return nextProcessHandler;
        }
    }
}
