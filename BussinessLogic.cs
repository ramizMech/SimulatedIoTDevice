using System;
using System.Collections.Generic;
using System.Text;

namespace MQTTSubscriber
{
    public class BussinessLogic
    {
        private readonly IRemoteDevice remoteDevice;
        private int timeleft = 120;
        private int status = 0;

        public BussinessLogic()
        {
            remoteDevice = new RemoteMqttClient(()=> ProcessReportRequest());
        }
        public void WashClothes()
        {
            //start
            //prewash
            //wash
            //dry
            //end
        }

        public void ProcessReportRequest()
        {
            string report = GenerateReport();
            SendReport(report);
        }

        public string GenerateReport()
        {
            
            Console.WriteLine("Generate report");
            string report = $"StatusCode:{status}; TimeLeft {timeleft}";

            return report;
        }

        public void SendReport(string report)
        {
            Console.WriteLine("Report generated");
            Console.WriteLine($"Sending report at {DateTime.Now}");
            remoteDevice.SendReport(report);
        }
    }
}
