using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTSubscriber;
using System;
using System.Text;
using System.Threading.Tasks;

//https://www.codeproject.com/Articles/5283088/MQTT-Message-Queue-Telemetry-Transport-Protocol-wi

namespace MQTTReceiver
{
    class Program
    {
        
        static int count = 1233;
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            BussinessLogic bussinessLogic = new BussinessLogic();
            

            Console.WriteLine("Press key to exit");
            Console.ReadLine();
        }

        
               
    }
}
