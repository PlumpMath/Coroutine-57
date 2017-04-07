using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Coroutine
{
    class HiPerfTimer
    {
        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceCounter(
          out long lpPerformanceCount);

        [DllImport("Kernel32.dll")]
        private static extern bool QueryPerformanceFrequency(
          out long lpFrequency);

        private long startTime, stopTime;
        private long freq;

        public HiPerfTimer()
        {
            startTime = 0;
            stopTime = 0;

            if (QueryPerformanceFrequency(out freq) == false)
            {
                throw new Win32Exception();
            }
        }

        public void Start()
        {
            QueryPerformanceCounter(out startTime);
        }

        public void Stop()
        {
            QueryPerformanceCounter(out stopTime);
        }

        public double Duration
        {
            get
            {
                Stop();
                return (double)(stopTime - startTime) / (double)freq;
            }
        }
    }
}
