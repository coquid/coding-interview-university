#if DEBUG && TRACE
#define DEBUG_TRACE
#endif

using System;
using System.Diagnostics;
using System.Configuration;
using VsProjectPracticeLibrary;

namespace VsProjectPractice
{
    class Program
    {
        [Conditional("TRACE")]
        static void ConditionalMethod()
        {
            Console.WriteLine("It is TRACE");
        }


        [Conditional("DEBUG_TRACE")]
        static void ConditionalMethod2()
        {
            Console.WriteLine("It is DEBUG && TRACE");
        }

        static void Main(string[] args)
        {
            LogWriter logWriter = new LogWriter();
            logWriter.Write("First log");

            ConditionalMethod();
            ConditionalMethod2();
        }
    }
}
