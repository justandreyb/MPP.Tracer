using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tracer
{
    class ProgramThread
    {
        private int currentThreadID;
        private MethodsDictionary methods;

        private DateTime startTime;
        private DateTime finishTime;
        private long time;

        public ProgramThread()
        {
            startTime = new DateTime();
            currentThreadID = Thread.CurrentThread.ManagedThreadId;
            methods = new MethodsDictionary();
        }
        public void closeProgramThread()
        {
            finishTime = new DateTime();
            time = finishTime.Millisecond - startTime.Millisecond;
        }

        public void addMethod(String name, Method method)
        {
            Console.WriteLine("[*Program thread*]: {0}", method.getMethodName());
            methods.addMethod(name, method);
        }
        
        public bool isAdded(String name)
        {
            return methods.isAdded(name);
        }

        public int getCurrentThreadID()
        {
            return currentThreadID;
        }
        public Method getMethod(String name)
        {
            return methods.getMethod(name);
        }
        public ArrayList getMethods()
        {
            return methods.getMethods();
        }
        public long getTime()
        {
            return this.time; 
        }
    }
}
