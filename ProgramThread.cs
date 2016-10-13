using System;
using System.Collections.Generic;
using System.Threading;

namespace Tracer
{
    class ProgramThread
    {
        private int currentThreadID;
        public LinkedList<Method> methods;

        private DateTime startTime;
        private DateTime finishTime;
        private long time;

        public ProgramThread()
        {
            startTime = new DateTime();
            currentThreadID = Thread.CurrentThread.ManagedThreadId;
            methods = new LinkedList<Method>();
        }
        public void closeProgramThread()
        {
            finishTime = new DateTime();
            time = finishTime.Millisecond - startTime.Millisecond;
        }

        public void addMethod(Method method)
        {
            methods.AddLast(method);
        }

        public int getCurrentThreadID()
        {
            return currentThreadID;
        }
        public long getTime()
        {
            return this.time; 
        }

        public Method getLastMethod()
        {
            if (methods.Count != 0) { return methods.Last.Value; }
            return null;
        }
        public void removeLastMethod()
        {
            methods.RemoveLast();
        }
        public LinkedList<Method> getMethods()
        {
            return methods;
        }

        public bool containsMethod(Method method)
        {
            return methods.Contains(method);
        }
        public bool isClear()
        {
            if (methods.Count > 0) { return false; }
            return true;
        }
    }
}
