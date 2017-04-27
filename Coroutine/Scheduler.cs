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
        Stack<IEnumerator> coroutineStack = new Stack<IEnumerator>();

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
            for (int i = 0; i < unblockedCoroutines.Count; i++)
            {
                IEnumerator coroutine = unblockedCoroutines[i];
                coroutineStack.Clear();
                IEnumerator node = coroutine;
                while (node != null)
                {
                    coroutineStack.Push(node);
                    node = node.Current as IEnumerator;
                }
                bool shouldBreak = false;
                while (coroutineStack.Count > 0 && !shouldBreak)
                {
                    node = coroutineStack.Pop();
                    while (node.MoveNext())
                    {
                        if (node.Current is WaitForSeconds)
                        {
                            shouldRunAfterTimes.Add(coroutine);
                            shouldBreak = true;
                            break;
                        }
                        else if (node.Current is IEnumerator)
                        {
                            coroutineStack.Push(node.Current as IEnumerator);
                            node = node.Current as IEnumerator;
                        }
                    }
                }
            }
            unblockedCoroutines.Clear();
            for (int i = 0; i < shouldRunAfterTimes.Count; )
            {
                IEnumerator coroutine = shouldRunAfterTimes[i];
                IEnumerator node = coroutine;
                while (node.Current is IEnumerator) node = node.Current as IEnumerator;
                WaitForSeconds current = node as WaitForSeconds;
                current.WaitSeconds -= delta;
                if (!current.MoveNext())
                {
                    shouldRunNextFrame.Add(coroutine);
                    shouldRunAfterTimes.Remove(coroutine);
                    continue;
                }
                i++;
            }
        }
    }
}
