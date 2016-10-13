using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tracer
{
    //FIXME : удаление метода из LinkedList (остается в XML)
    class Method
    {
        private String methodName;
        private String className;
        private int parametersNumber;

        private long startTime;
        private long finishTime;

        private int currentThreadID;

        private LinkedList<Method> includedMethods;
        
        public Method()
        {
            setClassName(null);
            setMethodName(null);
            includedMethods = new LinkedList<Method>();
            this.startTime = DateTime.Now.Millisecond;
        }

        public void setUp(String methodName, String className, int parametersNumber, int currentThreadID)
        {
            setMethodName(methodName);
            setClassName(className);
            setParametersNumber(parametersNumber);
            setThreadID(currentThreadID);
        }
        
        public void setMethodName(String methodName)
        {
            this.methodName = methodName;
        }
        public void setClassName(String className)
        {
            this.className = className;
        }
        public void setParametersNumber(int parametersNumber)
        {
            this.parametersNumber = parametersNumber;
        }
        public void setThreadID(int threadID)
        {
            this.currentThreadID = threadID;
        }
        public void addIncludedMethod(Method method)
        {
            includedMethods.AddLast(method);
        }

        public void setFinishTime()
        {
            this.finishTime = DateTime.Now.Millisecond;
        }
        public long getTime()
        {
            long time = this.finishTime - this.startTime;
            return time;
        }


        public String getMethodName()
        {
            return this.methodName;
        }
        public String getClassName()
        {
            return this.className;
        }
        public int getParametersNumber()
        {
            return this.parametersNumber;
        }
        public int getThreadID()
        {
            return this.currentThreadID;
        }
        public LinkedList<Method> getIncludedMethods()
        {
            return includedMethods;
        }
        public Method getMethod()
        {
            return this;
        }
    }
}
