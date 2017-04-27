using System;
using System.Collections;

namespace Coroutine
{
    class WaitForSeconds : IEnumerator
    {
        public WaitForSeconds(double time)
        {
            WaitSeconds = time;
        }

        public object Current { get; set; }
        public bool MoveNext()
        {
            return WaitSeconds > 0;
        }
        public void Reset()
        {

        }

        public double WaitSeconds { get;set;}
    }
}
