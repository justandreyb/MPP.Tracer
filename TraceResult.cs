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
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        public void addThread()
        {
            ProgramThread thread = new ProgramThread();
            if (!isAdded(thread.getCurrentThreadID())) 
            {
                threads.Add(thread.getCurrentThreadID(), thread);
            }
        }
        public void setStartTime()
        {
            this.startTime = getCurrentTime() ;
        }
        public void setFinishTime()
        {
            this.finishTime = getCurrentTime();
        }

        public long getTime()
        {
            setFinishTime();
            if ((this.startTime != 0) && (this.finishTime != 0))
            {
                return (this.finishTime - this.startTime); 
            } else
            {
                return 0;
            }    
        }
        public bool isAdded(int threadID)
        {
            return threads.ContainsKey(threadID);
        }
        public ProgramThread getThread(int threadID)
        {
            ProgramThread targetThread = null;
            if (isAdded(threadID))
            {
                targetThread = threads[threadID]; 
            }

            return targetThread;
        }
    }
}
