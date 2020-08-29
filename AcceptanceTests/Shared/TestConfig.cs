using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;

namespace AcceptanceTests.Shared
{
    public class TestConfig
    {
        public const string BASEURL = @"http://localhost:48064";
        public const string EMAIL = "TestUse@Test.com";
        public const string PASSWORD = "TEST1234*";

        public static IConfiguration GetConfig()
        {

            var config = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.Test.json")
                            .Build();
            return config;
        }

        public static string GetTestRootPath()
        {
            var rootDir = Path.GetDirectoryName(Assembly.GetAssembly(typeof(TestConfig)).Location);
            return rootDir;
        }
    }
}
