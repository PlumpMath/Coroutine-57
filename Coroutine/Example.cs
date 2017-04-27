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
            Console.WriteLine("Test1 begin. Wait 2 seconds now.");
            yield return new WaitForSeconds(2);
            Console.WriteLine("Test1 2 seconds have done.");

            //StartCoroutine(Test2());
            yield return Test2();
            
            Console.WriteLine("Test1 begin again. Now wait 4 seconds.");
            yield return new WaitForSeconds(4);
            Console.WriteLine("Test1 4 seconds have done.");
           
            Console.WriteLine("Test1 end.");
        }

        IEnumerator Test2()
        {
            Console.WriteLine("Test2 begin. Wait 6 seconds now.");
            yield return new WaitForSeconds(6);
            Console.WriteLine("Test2 end.");

            yield return Test3();
        }

        IEnumerator Test3()
        {
            Console.WriteLine("Test3 begin. Wait 5 seconds now.");
            yield return new WaitForSeconds(5);
            Console.WriteLine("Test3 end.");
        }

        void StartCoroutine(IEnumerator coroutine)
        {
            Scheduler.Instance.StartCoroutine(coroutine);
        }
    }
}
