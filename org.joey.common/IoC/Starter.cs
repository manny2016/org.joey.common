using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Joey.Common
{
    public static class Starter
    {
        public static IContainer CreateContainer(Action<ContainerBuilder> registrations)
        {
            var builder = new ContainerBuilder();
            return builder.Build();
        }
    }
}
