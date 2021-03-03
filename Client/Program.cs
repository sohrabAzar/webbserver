using System;
using System.Threading;
using Uppgift_8__webbserver_;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Thread.Sleep(100);
                Action x = new Action(() => Console.WriteLine("*** Message"));
                Uppgift_8__webbserver_.Program.myWebServer.MyQueue.Enqueue(x);
            }
        }
    }
}
