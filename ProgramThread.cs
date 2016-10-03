using System;
using System.Collections.Generic;
using System.Threading;

namespace Tracer
{
    class ProgramThread
    {
        private String START_TRACE = "void startTrace()";
        private String STOP_TRACE = "void stopTrace()";

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
        
        public Method getMainMethod()
        { 
            LinkedList<Method> temp = new LinkedList<Method>();
            int embeddedLevel = 0;
            int preEmbeddedLevel = 0;

            foreach (Method method in methods)
            {
                if (isStart(method))
                {
                    embeddedLevel++;
                    break;
                }
                if (isStop(method))
                {
                    embeddedLevel--;
                    break;
                }

                temp.AddLast(method);

                if (embeddedLevel > 0)
                {
                    if (preEmbeddedLevel == embeddedLevel)
                    {
                        if (temp.Count >= 2)
                        {
                            Method lastMethod;
                            lastMethod = temp.Last.Value;
                            temp.RemoveLast();
                            temp.AddLast(lastMethod);
                            temp.Last.Previous.Value.addIncludedMethod(temp.Last.Value);
                        }
                    } else
                    {
                        temp.Last.Previous.Value.addIncludedMethod(temp.Last.Value);
                    }
                }
                preEmbeddedLevel = embeddedLevel;
            }

            Method mainMethod = temp.First.Value;
            temp = null;
            methods = null;

            return mainMethod;
        }
        private bool isStart(Method method)
        {
            if (method.getMethodName().Equals(START_TRACE)) return true;
            return false;
        }
        private bool isStop(Method method)
        {
            if (method.getMethodName().Equals(STOP_TRACE)) return true;
            return false;
        }
    }
}
