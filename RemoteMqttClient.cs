using System;
using System.Collections.Generic;
using System.Text;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace MQTTSubscriber
{
    public class RemoteMqttClient : IRemoteDevice
    {
        private IMqttClient client;
        private Action reportHandlerFunc; 

        public RemoteMqttClient(Action reportRequestHandler)
        {
            reportHandlerFunc = reportRequestHandler;
            InitClient();
        }

        private void InitClient()
        {
            string connectionString = "tcp://localhost:1883";
            MqttFactory mqttfactory = new MqttFactory();

            client = mqttfactory.CreateMqttClient();
            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithClientId("IoTDevice1")
                .WithTcpServer("localhost", 1884)
                .WithCredentials("admin", "admin")
                .WithCleanSession()
                .Build();


            client.UseConnectedHandler(e => {
                Console.WriteLine("Connected successfully to MQTT Brokers");
                //Subscribe to Topic

                client.SubscribeAsync(new MqttTopicFilterBuilder().WithTopic("test").Build()).Wait();
            });
            client.UseDisconnectedHandler(e => {
                Console.WriteLine("Disconnected from MQTT Brokers");
            });
            client.UseApplicationMessageReceivedHandler(e =>
            {
                Console.WriteLine("Received Message");
                Console.WriteLine($"Topic = {e.ApplicationMessage.Topic}");
                Console.WriteLine($"Payload = {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
                Console.WriteLine($"QoS {e.ApplicationMessage.QualityOfServiceLevel}");
                Console.WriteLine();

                //Task.Run() => do something
                ProcessReportRequest();
                
            });

            //Connect
            client.ConnectAsync(options).Wait();
        }
        public void ProcessReportRequest()
        {
            reportHandlerFunc();

        }
        
        public void SendReport(string report)
        {
            string name = "machine1";


            var message = new MqttApplicationMessageBuilder()
                .WithTopic("testResponse")
                .WithPayload($"Payload: {name}, Report : {report}")
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            client.PublishAsync(message);
        }
    }
}
