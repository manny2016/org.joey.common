using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Org.Joey.Common
{
    public static class RetryAgent
    {
        public static void Execute<T1, T2, T3, T4>(
            Action<T1, T2, T3, T4> action,
            T1 arg1, T2 arg2, T3 arg3, T4 arg4,
            TimeSpan retryInterval, int retryCount = 5)
        {
            
        }   
    }
}
