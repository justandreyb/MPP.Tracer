using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Tracer
{
    class TraceResult
    {
        private long startTime;
        private long finishTime;
        private Dictionary<int, ProgramThread> threads;

        public TraceResult()
        {
            threads = new Dictionary<int, ProgramThread>(0);
        }

        private long getCurrentTime()
        {
            return DateTime.Now.Millisecond;
        }

        public int createThread()
        {
            ProgramThread thread = new ProgramThread();
            if (!containsThread(thread.getCurrentThreadID()))
            {
                threads.Add(thread.getCurrentThreadID(), thread);
            }
            return thread.getCurrentThreadID();
        }
        public void setStartTime()
        {
            this.startTime = getCurrentTime();
        }
        public void setFinishTime()
        {
            this.finishTime = getCurrentTime();
        }

        public bool isStarted()
        {
            if (this.startTime != 0) return true;
            return false;
        }
        public bool isFinished()
        {
            if (this.finishTime != 0) return true;
            return false;
        }
        public long getTime()
        {
            setFinishTime();
            if ((!isStarted()) && (!isFinished()))
            {
                return (this.finishTime - this.startTime);
            }
            else
            {
                return 0;
            }
        }

        public ProgramThread getThread(int threadID)
        {
            ProgramThread targetThread = null;
            if (containsThread(threadID))
            {
                targetThread = threads[threadID];
            }

            return targetThread;
        }
        public bool containsThread(int threadID)
        {
            return threads.ContainsKey(threadID);
        }
        public ArrayList getThreads()
        {
            ArrayList list = new ArrayList(threads.Count);
            foreach (ProgramThread thread in threads.Values)
            {
                list.Add(thread);
            }
            return list;
        }
    }
}
