using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Users.Application.Common.Interfaces;
using Users.Domain.Interfaces;
using Users.Infrastructure.Persistence;

namespace Users.Infrastructure;
public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IDataAccess, DataAccess>();
        services.AddSingleton<IRabbitMQClient, RabbitMQClient.RabbitMQClient>();

        return services;
    }
}
