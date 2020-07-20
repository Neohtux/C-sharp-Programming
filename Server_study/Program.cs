using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerCore
{
    //메모리 배리어
    // A) 코드 재배치 억제
    // B) 가시성

    // 1) Full Memory Barrier (ASM MFENCE, C# Thread.Barrier) : Store/Load 둘다 막는다.
    // 2) Store Memory Barrier (ASM SFENCE) : Store만 막는다
    // 3) Load Memory Barrier (ASM LFENCE) : Load만 막는다.
    class SpinLock
    {
        volatile int locked = 0;
        public void Acquire()
        {
            while(true)
            {
                //CAS Compare-AND-Swap
                int expected = 0;
                int desire = 1;
                if(Interlocked.CompareExchange(ref locked, desire, expected) == expected)
                    break;

            }
  
        }
        public void Release()
        {
            locked = 0;
        }
    }

    class Program
    {
        static int num = 0;
        static SpinLock _lock = new SpinLock();

        static void Thread_1()
        {
            for(int i=0; i<100000; ++i)
            {
                _lock.Acquire(); // (locked == true);
                num += 1;
                _lock.Release(); // (locked == false) lock 해제
            }
        }
        static void Thread_2()
        {
           for(int i=0; i<100000;++i)
            {
                _lock.Acquire();
                num -= 1;
                _lock.Release();
            }
        }
       
        static void Main(string[] args)
        {
            Task t1 = new Task(Thread_1);
            Task t2 = new Task(Thread_2);

            t1.Start();
            t2.Start();
            Task.WaitAll(t1, t2);

            Console.WriteLine(num); //0인지 확인
        }
    }
}
