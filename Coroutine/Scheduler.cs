using System;
using System.Collections;
using System.Collections.Generic;

namespace Coroutine
{
    class Scheduler
    {
        static Scheduler _instance = null;
        static public Scheduler Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Scheduler();
                return _instance;
            }
        }
        List<IEnumerator> unblockedCoroutines = new List<IEnumerator>();
        List<IEnumerator> shouldRunNextFrame = new List<IEnumerator>();
        List<IEnumerator> shouldRunAfterTimes = new List<IEnumerator>();

        public void Update(double delta)
        {
            UpdateCoroutine(delta);
        }

        public void StartCoroutine(IEnumerator enumerator)
        {
            unblockedCoroutines.Add(enumerator);
        }

        public void UpdateCoroutine(double delta)
        {
            foreach (IEnumerator coroutine in shouldRunNextFrame)
            {
                unblockedCoroutines.Add(coroutine);
            }
            shouldRunNextFrame.Clear();
            foreach (IEnumerator coroutine in unblockedCoroutines)
            {
                while (coroutine.MoveNext())
                {
                    if (coroutine.Current is WaitForSeconds)
                    {
                        shouldRunAfterTimes.Add(coroutine);
                        break;
                    }
                }
            }
            unblockedCoroutines.Clear();
            foreach (IEnumerator coroutine in shouldRunAfterTimes)
            {
                WaitForSeconds current = coroutine.Current as WaitForSeconds;
                current.WaitSeconds -= delta;
                if (current.WaitSeconds <= 0)
                {
                    shouldRunNextFrame.Add(coroutine);
                    shouldRunAfterTimes.Remove(coroutine);
                    break;
                }
            }
        }
    }
}
