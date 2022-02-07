using System;
using System.Collections.Generic;
using System.Text;

namespace MQTTSubscriber
{
    public interface IRemoteDevice
    {
        public void SendReport(string report);
        public void ProcessReportRequest();
    }
}
