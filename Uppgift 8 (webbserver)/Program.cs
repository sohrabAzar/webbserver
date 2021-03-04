using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace Uppgift_8__webbserver_
{
    class Program
    {
        /// <summary>
        /// Checks Console to see if a keyboard key has been pressed, if so returns it as uppercase, otherwise returns '\0'.
        /// </summary>  
        static char ReadKeyIfExists() => Console.KeyAvailable ? Console.ReadKey(intercept: true).Key.ToString().ToUpper()[0] : '\0';
        static void Main(string[] args)
        {
            myWebServer.QueueCheck += QueueCheckEventHandler;
            // Set to control the interval at which a message is send to server
            const int sendMessageToServerTimeInterval = 100;

            bool quit = false;
            char pressedKey;
          
            // Server starts listening for messages 
            myWebServer.StartServer();

            while (!quit)
            {
                // quit program on Q press
                sendMessageTimeCounter += 1;
                pressedKey = ReadKeyIfExists();
                if (pressedKey == 'Q')
                {
                    quit = true;
                    break;
                }

                // Simulate Client sending a message into the queue
                SendNewMessageToServer(sendMessageToServerTimeInterval);
               
                Thread.Sleep(10);
            }
        }

        #region SERVER
        private static Webserver myWebServer = new Webserver();

        /// <summary>
        /// Show message on UI when server checks queue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="messageFound"></param>
        private static void QueueCheckEventHandler(object sender, bool messageFound)
        {
            if (messageFound)
            {
                Console.WriteLine("message handeled");
                Console.WriteLine($"{myWebServer.MyQueue.Count} messages are in queue\n");

            }
            else
            {
                Console.WriteLine("queue is empty");
            }
            // Console.WriteLine(messageFound? "message handeled": "queue is empty");
        }
        #endregion  

        #region CLIENT
        // field used to contol the amount of time messages send to server
        internal static int sendMessageTimeCounter = 0;

        // dummy variable to increase the work load of the message send to server 
        private static int dummy = 0;

        /// <summary>
        /// Send message to server in specified time intervals
        /// </summary>
        /// <param name="timeInterval"></param>
        private static void SendNewMessageToServer(int timeInterval)
        {
            if (sendMessageTimeCounter > timeInterval)
            {
                Action x = new Action(ActionHandler_HeavyAsync);
                myWebServer.MyQueue.Enqueue(x);
                Console.WriteLine("\nClient sent a message\n");
                sendMessageTimeCounter = 0;
            }
        }

        // Three types of actions when send message to server, one heavy and one light
        private static void ActionHandler_HeavyAsync()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 1000000000; i++)
                {
                    dummy += 1;
                }
                Console.WriteLine("\nClient sent a message\n");
                dummy = 0;
            });

        }
        private static void ActionHandler_Heavy()
        {
            for (int i = 0; i < 1000000000; i++)
            {
                dummy += 1;
            }
            Console.WriteLine("\nClient sent a message\n");
            dummy = 0;

        }
        private static void ActionHandler_Light()
        {
            dummy += 1;
        }
        #endregion
    }
}
