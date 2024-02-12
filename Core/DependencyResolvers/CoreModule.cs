﻿using Autofac.Core;
using Core.CrossCuttingconcerns.Caching;
using Core.CrossCuttingconcerns.Caching.Microsoft;
using Core.Utilities.IOC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection servicesCollection)
        {
            servicesCollection.AddMemoryCache();
            servicesCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            servicesCollection.AddSingleton<ICacheManager, MemoryCacheManager>();// başka bir teknonoji kullanılacağı zaman değişecek(elastik)
            servicesCollection.AddSingleton<Stopwatch>();
        }
    }
}