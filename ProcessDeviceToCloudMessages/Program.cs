using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceBus.Messaging;

namespace ProcessDeviceToCloudMessages
{
    class Program
    {
        static void Main(string[] args)
        {
            string iotHubConnectionString = "HostName=exitIoTHub1.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=J00wsFUsl1IbxNhbmhv7hKu3elnxN5x0g5BniTKe2rM=";
            string iotHubD2cEndpoint = "messages/events";
            StoreEventProcessor.StorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=exitstorageaccount1;AccountKey=JigTXrXvn8njEHDcIGgJfeglNKzzwKkYZbB/WwVnvKjfwCtTL31VfoVVeD0scQTQJKVEvIiP2DUhsf7Su7ZooA==";
            StoreEventProcessor.ServiceBusConnectionString = "Endpoint=sb://exitnstest.servicebus.windows.net/;SharedAccessKeyName=send;SharedAccessKey=Z19CSw6d0CLCvoFopGnk6pXJUd3DiWhPScz1Avi22Gw=;EntityPath=d2ctutorial";

            string eventProcessorHostName = Guid.NewGuid().ToString();
            EventProcessorHost eventProcessorHost = new EventProcessorHost(eventProcessorHostName, iotHubD2cEndpoint, EventHubConsumerGroup.DefaultGroupName, iotHubConnectionString, StoreEventProcessor.StorageConnectionString, "messages-events");
            Console.WriteLine("Registering EventProcessor...");
            eventProcessorHost.RegisterEventProcessorAsync<StoreEventProcessor>().Wait();

            Console.WriteLine("Receiving. Press enter key to stop worker.");
            Console.ReadLine();
            eventProcessorHost.UnregisterEventProcessorAsync().Wait();
        }
    }
}
