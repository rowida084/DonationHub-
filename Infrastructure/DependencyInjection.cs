using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public static class DependencyInjection
    {
      public static IServiceCollection AddInfrastructureService
            (this IServiceCollection services)
        {
            return services;
        }
    }
}
