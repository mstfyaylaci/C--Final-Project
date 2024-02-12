using System;

namespace Business.DependecyResolvers.CCS
{
    public class DatabaseLogger : ILogger
    {
        public void Log()
        {
            Console.WriteLine("veri tabanına loglandı");
        }
    }
}
