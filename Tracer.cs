using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tracer
{
    class Tracer : ITracer
    {
        TraceResult traceResult = new TraceResult(); 
        public void startTrace()
        {
            traceResult.addThread();
            traceResult.setStartTime();
        }
        public void stopTrace()
        { 
            lock (new Object()) {
                traceResult.setFinishTime();

                int threadID = Thread.CurrentThread.ManagedThreadId;
                
                //traceResult.getThread(threadID).closeProgramThread();
                
                StackTrace stackTrace = new StackTrace();
                if (stackTrace.FrameCount != 0)
                {
                    for (int index = stackTrace.FrameCount - 1; index >= 0; index--)
                    {
                        String methodName = stackTrace.GetFrame(index).GetMethod().Name;
                        int parametersNumber = stackTrace.GetFrame(index).GetMethod().GetParameters().Length;
                        String className = stackTrace.GetFrame(index).GetMethod().DeclaringType.Name;
                        String callerName = "";
                        if (index < stackTrace.FrameCount - 1)
                        {
                             callerName = stackTrace.GetFrame(index + 1).GetMethod().Name;
                        }

                        Method currentMethod = new Method();
                        if (callerName.Equals(""))
                        {
                            currentMethod.setUp(methodName, className, parametersNumber, threadID);
                        } else
                        {
                            currentMethod.setUp(methodName, className, parametersNumber, threadID, callerName);
                        }

                        traceResult.getThread(threadID).addMethod(methodName, currentMethod);
                        //fix it:
                        //traceResult.getThread(threadID).getMethod(methodName).addMethodToDictionary();

                        Console.WriteLine("_____________________________________");
                        Console.WriteLine("Thread: {0}", threadID);
                        Console.WriteLine("Class: {0}", className);
                        Console.WriteLine("Method: {0}", methodName);
                        Console.WriteLine("Parameters number: {0}", parametersNumber);
                        if (index < stackTrace.FrameCount - 1)
                        {
                            Console.WriteLine("Caller name: {0}", currentMethod.getCaller());
                        }
                        Console.WriteLine("_____________________________________");
                        Console.WriteLine();
                        Console.WriteLine("Count: {0}", traceResult.getThread(threadID).getMethods().Count);
                    }
                }
               
                Console.ReadLine();
            }
        }
        public void getTraceResult()
        {  
        }
    }
}
