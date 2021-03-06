﻿using System.Collections.Generic;
using Autofac;

namespace MyFX.Core.DI
{
    public class DIContainer
    {
        private IContainer _container;
        public DIContainer(IContainer container)
        {
            this._container = container;
        }

        public  TService Resolve<TService>() 
            where TService : class
        {
            return this._container.Resolve<TService>();
        }


        public IEnumerable<TService> ResolveAll<TService>()
            where TService : class
        {
          return  _container.Resolve<IEnumerable<TService>>();
        }

        public IContainer Container {
            get { return _container; }
        }
    }
}
