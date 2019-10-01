using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DevArena2019.Triggers
{
    [TestClass]
    public abstract class BaseTriggers
    {
        protected static string ServiceBusConnectionString;
        protected static string EmailQueue;

        [AssemblyInitialize]
        public static void Initialize(TestContext context)
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Testing.json")
                .Build();

            var serviceBus = config.GetSection("ServiceBus");
            ServiceBusConnectionString = serviceBus.GetValue<string>("ServiceBusConnectionString");
            EmailQueue = serviceBus.GetValue<string>("ServiceBusSendEmailQueue");
        }
    }
}