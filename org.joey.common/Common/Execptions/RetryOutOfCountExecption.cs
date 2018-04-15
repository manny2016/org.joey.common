using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Joey.Common
{
    public class RetryOutOfCountExecption : Exception
    {
        public RetryOutOfCountExecption(int retryCount,  params object[] parameters)
            : base(string.Format("Retry out of {0} times. }", retryCount))
        {

        }
    }
}
