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

    /*
     * Проверка на существование потока + проверка на системный вызов:
           Если существует -> проверка linkedlist на наличие методов, 
                если есть - добавление текущего и в лист и в метод, 
                если нет - только в лист 
           если не существует - создание потока и добавление в лист текущего метода
        
       Если встречен stopTrace - выкинуть последний метод из листа текущего потока + подсчет времени метода
    */
    class Tracer : ITracer
    {
        TraceResult traceResult = new TraceResult();
        //how to get current called method ? (start , stop or another)
        public void startTrace()
        {
            lock (new Object())
            {
                StackTrace stackTrace = new StackTrace();
                StackFrame stackFrame = stackTrace.GetFrame(1);

                int currentThreadID = Thread.CurrentThread.ManagedThreadId; // it's real ID or only for Tracer.cs ?

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
                int currentThreadID = Thread.CurrentThread.ManagedThreadId; // or from currentMethod.getThreadID(); 
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

            //Console.ReadLine();
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

            //XMLCreator(traceResult);
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
            //doc.Root.Add(
              //      new XElement("thread",
               //         new XAttribute("id", thread.getCurrentThreadID()),
                //        new XAttribute("time", thread.getTime() + "ms"),
                //        MethodsToXML(thread.getMethods())
                  //  )
         //       );
            //doc.Root.Add(root);
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


        private XElement MethodsToXML(LinkedList<Method> methods)
        {
            XElement element = null;
            Console.WriteLine("I'm here (1)");
            foreach (Method method in methods)
            {
                try
                {
                    XAttribute name = new XAttribute("name", method.getMethodName());
                    Console.WriteLine("I'm here (1.2)");
                    XAttribute time = new XAttribute("time", method.getTime());
                    Console.WriteLine("I'm here (1.3)");
                    XAttribute paramsNumber = new XAttribute("params", method.getParametersNumber());

                    element = new XElement("method", name, time, paramsNumber);
                    Console.WriteLine("I'm here (2)");

                    element.Add(MethodsToXML(method.getIncludedMethods()));

                   
                }
                catch
                {
                    Console.WriteLine("Null");
                }
            }
            Console.WriteLine("I'm here (3)");
            Console.Read();
            return element;
        }
    }
}
