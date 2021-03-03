using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Uppgift_8__webbserver_
{
    
    public class Webserver
    {

        private Timer aTimer = new System.Timers.Timer(2000);

        public void StartServer()
        {
            aTimer.Elapsed += Tick;
            aTimer.Enabled = true;
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            Task task = Task.Run(CheckQueue);
            // CheckQueue();
        }

        public Queue<Action> MyQueue { get; set; } = new Queue<Action>();

        public void CheckQueue()
        {
            
            if (MyQueue.Count > 0)
            {                
                Console.WriteLine("message handeled");
                MyQueue.Dequeue().Invoke();
                Console.WriteLine($"{MyQueue.Count} messages are in queue\n");              
            }
            else
            {
                Console.WriteLine("queue is empty\n");
            }

        }

    }
}
