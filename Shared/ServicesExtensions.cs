using Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Shared.ServicesImplementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class ServicesExtensions
    {
        public static void AddSharedLayerServices(this IServiceCollection services)
        {
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}
