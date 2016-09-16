using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace IoT_Creation_Device
{
    class Program
    {
        static RegistryManager registryManager;
        static string connectionString = "HostName=exitIoTHub1.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=J00wsFUsl1IbxNhbmhv7hKu3elnxN5x0g5BniTKe2rM=";

        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }

        private static async Task AddDeviceAsync()
        {
            string deviceId = "exitDeviceA";
            Device device;

            try
            {
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
            }
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
            }
            Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
        }
    }
}
