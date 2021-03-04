using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Uppgift_8__webbserver_
{
    
    public class Webserver
    {
        #region MEMBERS
        /// <summary>
        /// Time between each attemp at reading message queue by serve in seconds
        /// </summary>
        /// 
        private const int serverUpdateTimeInterval = 1;

        /// <summary>
        /// Timer used to run the servers tick method
        /// </summary>
        private Timer aTimer = new System.Timers.Timer(serverUpdateTimeInterval * 1000);

        /// <summary>
        /// Server message queue
        /// </summary>
        public Queue<Action> MyQueue { get; set; } = new Queue<Action>();
        #endregion

        #region METHODS
        public void StartServer()
        {
            aTimer.Elapsed += Tick;
            aTimer.Enabled = true;
        }

        private void Tick(object sender, ElapsedEventArgs e)
        {
            CheckQueue();
        }
       
        public void CheckQueue()
        {
            bool messageFound = false;

            if (MyQueue.Count > 0)
            {
                //Console.WriteLine("message handeled");
                MyQueue.Dequeue().Invoke();
                //Console.WriteLine($"{MyQueue.Count} messages are in queue\n");
                messageFound = true;
            }
            else
            {
                // Console.WriteLine("queue is empty\n");
            }

            OnQueueChecked(messageFound);
        }

        /// <summary>
        /// Event used to talk to UI and show message when queue is checked
        /// </summary>
        public event EventHandler<bool> QueueCheck;
        protected void OnQueueChecked(bool messageFound)
        {
            QueueCheck?.Invoke(this, messageFound);
        }
        #endregion
    }
}
