namespace TempMonitor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddHostedService<TempLogger>();
                })
                .Build();

            host.Run();
        }
    }
}