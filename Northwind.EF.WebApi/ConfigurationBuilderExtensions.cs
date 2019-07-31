using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Northwind.EF.WebApi
{
    public static class ConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddNorthwindConfiguration(this IConfigurationBuilder builder, 
            string environmentName, string[] args = null, string path = "")
        {
            builder.AddJsonFile(Path.Combine(path, "appsetings.json"), false, true);
            builder.AddJsonFile(Path.Combine(path, $"appsetings.{environmentName}.json"), true, true);
            builder.AddJsonFile(Path.Combine(path, $"appsetings.local.json"), true, true);

            builder.AddEnvironmentVariables();
            builder.AddCommandLine(args ?? new string[0]);
            
            return builder;
        }
    }
}
