using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace CompositApplication
{
    public static class UnityContainerExtention
    {
        public static void RegisterType(this IUnityContainer source, Type fromType, Type toType, bool registerAsSingleton)
        {
            if (registerAsSingleton)
            {
                source.RegisterType(fromType, toType, new ContainerControlledLifetimeManager());
            }
            else
            {
                source.RegisterType(fromType, toType);
            }
        }
    }
}