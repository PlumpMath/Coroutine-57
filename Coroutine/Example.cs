using System;
using System.Collections;
namespace Coroutine
{
    class Example
    {
        public void StartTest()
        {
            StartCoroutine(Test1());
        }

        IEnumerator Test1()
        {
            Console.WriteLine("Test1 begin.");

            Console.WriteLine("Now wait 2 seconds.");
            yield return new WaitForSeconds(2);
            Console.WriteLine("2 seconds have done.");
            
            Console.WriteLine("Now wait 4 seconds.");
            yield return new WaitForSeconds(4);
            Console.WriteLine("4 seconds have done.");
           
            Console.WriteLine("Test1 end.");
        }

        void StartCoroutine(IEnumerator coroutine)
        {
            Scheduler.Instance.StartCoroutine(coroutine);
        }
    }
}
