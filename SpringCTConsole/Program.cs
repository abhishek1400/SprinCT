using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using System.Collections.Concurrent;

namespace SpringCTConsole
{
    class Program
    {
        private static readonly int ProducerCount = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ProducerCount"));
        private static readonly int ProducerSleepTime = Convert.ToInt32(ConfigurationManager.AppSettings.Get("ProducerSleepTime"));
        public static ConcurrentQueue<int> concurrentQueue = new ConcurrentQueue<int>();
        public static int value=0;
        static void Main(string[] args)
        {

            for (int i = 0; i < ProducerCount; i++)
            {
               // if (concurrentQueue.Count == 30000) { concurrentQueue = null; }
                Thread producerThread = new Thread(Producer);
                producerThread.Start();
            }
            Thread consumerThread = new Thread(Consumer);
            consumerThread.Start();
            Console.ReadLine();

        }
        public static void Producer()
        {
            Random random = new Random();
            int randVal= random.Next(10);
            
            Console.WriteLine(randVal + "       "+ ProducerSleepTime);
            concurrentQueue.Enqueue(randVal);
            Thread.Sleep(ProducerSleepTime);
            
        }

        static void Consumer()
        {
            int result;
            for (int i = 0; i < concurrentQueue.Count; i++)
            {
                concurrentQueue.TryDequeue(out result);
                value += result;
                if (value > 30000) { value = 0; }
            }

        }
    }
}
