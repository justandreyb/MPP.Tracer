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
    class Method
    {
        private String methodName;
        private String className;
        private int parametersNumber;
        private ArrayList includedMethods;
        private int currentThreadID;
        //Time
        public Method()
        {
            setClassName(null);
            setMethodName(null);
            includedMethods = new ArrayList(0);
        }

        public void setUp(String methodName, String className, int parametersNumber, int currentThreadID)
        {
            setMethodName(methodName);
            setClassName(className);
            setParametersNumber(parametersNumber);
            setCurrentThreadID(currentThreadID);
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
        public void setCurrentThreadID(int currentThreadID)
        {
            this.currentThreadID = currentThreadID;
        }
        public void addIncludedMethod(Method method)
        {
            includedMethods.Add(method);
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
        public int getCurrentThreadID()
        {
            return this.currentThreadID;
        }
        public ArrayList getIncludedMethods()
        {
            return includedMethods;
        }
        public Method getMethod()
        {
            return this;
        }
    }
}
