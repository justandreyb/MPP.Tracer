﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;
using System.Threading;
namespace Tester
{
    class SingleThreadTest : ITest
    {
        private ITracer tracer = new Tracer.Tracer();
        public void Run()
        {
            tracer.StartTrace();

            Thread.Sleep(500);
            RunCycle(500);

            tracer.StopTrace();

            PrintTestResults();
        }

        private void RunCycle(int sleepTime)
        {
            tracer.StartTrace();

            Thread.Sleep(sleepTime);

            tracer.StopTrace();
        }

        private void PrintTestResults()
        {
            var traceResult = tracer.GetTraceResult();
            foreach (TraceResultItem analyzedItem in traceResult)
            {
                analyzedItem.PrintToConsole();
            }
        }

    }
}
