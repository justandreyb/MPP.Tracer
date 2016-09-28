using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    class MethodsDictionary
    {
        private Dictionary<String, Method> methodsDictionary;

        public MethodsDictionary()
        {
            methodsDictionary = new Dictionary<string, Method>(0);
        }

        public bool isAdded(String name)
        {
            return methodsDictionary.ContainsKey(name);
        }

        public void addMethod(String name, Method method)
        {
            if (!isAdded(name))
            {//!!!!!!!!!!!!!!!!!!!!!!!!!
                Console.WriteLine("[Added method] {0}", method.getMethodName());
                if (method.getCaller() != null) {
                    Console.WriteLine("\t[Method caller] {0}", method.getCaller());
                }
                methodsDictionary.Add(name, method);
            }
        }
        public Method getMethod(String name)
        {
            Method method = new Method();
            Console.WriteLine("[*Name*]: {0}", name);
            foreach(String key in methodsDictionary.Keys)
            {
                Console.WriteLine("[*Element*]: {0}", methodsDictionary[key]);
            }
            
            if (isAdded(name))
            {
                Console.WriteLine("[DICT] {0}", methodsDictionary[name]);
                method = methodsDictionary[name];
            }
            return method;
        }
        public ArrayList getMethods()
        {
            ArrayList methodsList = new ArrayList(0);
            foreach (var key in methodsDictionary.Keys)
            {
                methodsList.Add(methodsDictionary[key]);
            }
            return methodsList;
        }
    }
}
