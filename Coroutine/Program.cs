using System;
using System.Threading;

namespace Coroutine
{
    class Program
    {
        static void Main(string[] args)
        {
            HiPerfTimer timer = new HiPerfTimer();
            Scheduler scheduler = Scheduler.Instance;
            double perFrame = 1 / 60f;
            timer.Start();
            new Example().StartTest();
            while(true)
            {
                double delta = timer.Duration;
                if(delta >= perFrame)
                {
                    scheduler.Update(delta);
                    timer.Start();
                }
                else
                {
                    Thread.Sleep((int)((perFrame - delta) * 1000));
                }
            }
        }
    }
}
