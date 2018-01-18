﻿using System;
using System.Reflection;
using Autofac;

namespace MyFX.Core.DI
{
    /// <summary>
    /// 依赖注入引导程序
    /// </summary>
    public static class DIBootstrapper
    {
        private static DIContainer _container;

        /// <summary>
        /// DI容器
        /// </summary>
        public static DIContainer Container
        {
            get
            {
                if (_container == null)
                { throw new Exception("请在程序入口调用DIBootstrapper.Initialise();启用DI容器"); }
                return _container;
            }
        }

        /// <summary>
        /// 初始化DI容器
        /// </summary>
        /// <returns></returns>
        /// <param name="assemblyStrings">注册程序集类型的名称</param>
        public static IContainer Initialise(params string[] assemblyStrings)
        {
            return Initialise(null, assemblyStrings);
        }

        /// <summary>
        /// 初始化DI容器
        /// </summary>
        /// <param name="register">需注册的组件:
        /// MVC写法:x=>x.RegisterControllers(Assembly.GetExecutingAssembly())</param>
        /// <param name="assemblyStrings">注册程序集类型的名称</param>
        /// <returns></returns>
        public static IContainer Initialise(Action<ContainerBuilder> register, params string[] assemblyStrings)
        {
            var length = assemblyStrings.Length;
            Assembly[] assemblies = new Assembly[length];
            for (int i = 0; i < length; i++)
            {
                assemblies[i] = Assembly.Load(assemblyStrings[i]);
            }
            return Initialise(register, assemblies);
        }

        /// <summary>
        /// 初始化DI容器
        /// </summary>
        /// <param name="assemblies">注册程序集类型</param>
        public static IContainer Initialise(params Assembly[] assemblies)
        {
            return Initialise(null, assemblies);
        }
        /// <summary>
        /// 初始化DI容器
        /// </summary> 
        /// <param name="register">需注册的组件:
        /// MVC写法:x=>x.RegisterControllers(Assembly.GetExecutingAssembly())</param>
        /// <param name="assemblies">注册程序集类型</param>
        public static IContainer Initialise(Action<ContainerBuilder> register, params Assembly[] assemblies)
        {
            var builder = new ContainerBuilder();
            Type baseType = typeof(IDependency);
            builder.RegisterAssemblyTypes(assemblies)
                .Where(type => baseType.IsAssignableFrom(type) && !type.IsAbstract)
                .AsImplementedInterfaces().InstancePerLifetimeScope();//InstancePerLifetimeScope 保证对象生命周期基于请求
            if (register != null)
            {
                register(builder);
            }
       
            var container = builder.Build();
            _container = new DIContainer(container);
            return container;
        }

    }
}
