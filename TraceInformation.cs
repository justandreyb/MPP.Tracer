using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    class TraceInformation
    {
        private DateTime startTime;
        private Method method;

        public void setMethod(Method method)
        {
            this.method = method;
        }
        public void setTime(DateTime startTime)
        {
            this.startTime = startTime;
        }

        public DateTime getTime()
        {
            return this.startTime;
        }
        public Method getMethod()
        {
            return this.method;
        }
    }
}
