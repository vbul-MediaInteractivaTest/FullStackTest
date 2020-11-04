using System;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace MediaInteractivaTest.ApiTests
{
    public class TestsHelper
    {
        private static readonly Random Random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }

        public static Configuration GetApplicationConfiguration(string outputPath)
        {
            var config = new Configuration();
            var iConfig = GetIConfigurationRoot(outputPath);
            iConfig.GetSection("General").Bind(config);
            return config;
        }

        private static IConfigurationRoot GetIConfigurationRoot(string outputPath)
        {
            return new ConfigurationBuilder()
                .SetBasePath(outputPath)
                .AddJsonFile("appsettings.json", optional: true)
                .Build();
        }
    }
}
