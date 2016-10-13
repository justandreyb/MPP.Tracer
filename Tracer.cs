using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tracer
{
    class Tracer : ITracer
    {
        TraceResult traceResult = new TraceResult();
        public void startTrace()
        {
            lock (new Object())
            {
                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame = stackTrace.GetFrame(1);

                int currentThreadID = Thread.CurrentThread.ManagedThreadId;

                Method method = createMethod(stackFrame, currentThreadID);

                if (traceResult.containsThread(currentThreadID))
                {
                    ProgramThread currentThread = traceResult.getThread(currentThreadID);
                    if (currentThread.isClear())
                    {
                        currentThread.addMethod(method);
                    }
                    else
                    {
                        currentThread.getLastMethod().addIncludedMethod(method);
                        currentThread.addMethod(method);
                    }
                }
                else
                {
                    traceResult.createThread();
                    ProgramThread currentThread = traceResult.getThread(currentThreadID);
                    currentThread.addMethod(method);
                }
            }
        }

        public void stopTrace()
        {
            lock (new Object())
            {
                int currentThreadID = Thread.CurrentThread.ManagedThreadId;
                ProgramThread currentThread = traceResult.getThread(currentThreadID);
                if (currentThread.getLastMethod() != null)
                {
                    currentThread.getLastMethod().setFinishTime();
                    if (currentThread.getMethods().Count != 1)
                    {
                        currentThread.removeLastMethod();
                    }
                }
            }
        }

        private Method createMethod(StackFrame stackFrame, int threadID)
        {
            Method method = new Method();
            string methodName = stackFrame.GetMethod().Name;
            int parametersNumber = stackFrame.GetMethod().GetParameters().Length;
            String className = stackFrame.GetMethod().DeclaringType.Name;

            method.setUp(methodName, className, parametersNumber, threadID);

            return method;
        }

        public void getTraceResult()
        {
            if (!traceResult.isFinished()) { traceResult.setFinishTime(); }
            XDocument doc = new XDocument();
            XElement root = new XElement("root");
            doc.Add(root);
            foreach (ProgramThread thread in traceResult.getThreads())
            {
                foreach (Method method in thread.getMethods())
                {
                    XElement temp = getMethodXML(method);
                    root.Add(temp);
                }

            }
            doc.Save("TraceResult.xml");
        }

        private XElement getMethodXML(Method method)
        {
            XElement element = null;
            XAttribute name = new XAttribute("name", method.getMethodName());
            XAttribute time = new XAttribute("time", method.getTime());
            XAttribute paramsNumber = new XAttribute("params", method.getParametersNumber());
            element = new XElement("method", name, time, paramsNumber);
            foreach (Method nestyMethod in method.getIncludedMethods())
            {
                XElement temp;
                temp = getMethodXML(nestyMethod);
                element.Add(temp);
            }
            return element;
            
        }
    }
}
