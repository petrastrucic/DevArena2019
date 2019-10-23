// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

// This application uses the Azure IoT Hub device SDK for .NET
// For samples see: https://github.com/Azure/azure-iot-sdk-csharp/tree/master/iothub/device/samples
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading.Tasks;

namespace SimulatedDeviceEventHub
{
    class Device
    {
        private static DeviceClient deviceClient;

        // The device connection string to authenticate the device with your IoT hub.
        private readonly static string deviceConnString = "HostName=ekobit-dev-system-iothub.azure-devices.net;DeviceId=device;SharedAccessKey=p6qWkP9pN8NlVLThrPROX939pE4MQTt9K+0PbTDm5P8=";

        /// <summary>
        /// Async method to send simulated telemetry.
        /// </summary>
        private static async void SendDeviceToCloudMessagesAsync()
        {
            // Initial telemetry values
            double minTemperature = 20;
            double minHumidity = 60;
            Random rand = new Random();

            while (true)
            {
                double currentTemperature = minTemperature + rand.NextDouble() * 15;
                double currentHumidity = minHumidity + rand.NextDouble() * 20;

                // Create JSON message
                var telemetryDataPoint = new
                {
                    temperature = currentTemperature,
                    humidity = currentHumidity
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                // Add a custom application property to the message.
                // An IoT hub can filter on these properties without access to the message body.
                message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

                // Connect to the IoT hub using the MQTT protocol
                deviceClient = DeviceClient.CreateFromConnectionString(deviceConnString, TransportType.Mqtt);
                // Send the tlemetry message
                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                await Task.Delay(1000);
            }
        }

        private static void Main()
        {
            Console.WriteLine("IoT Hub Quickstarts #2 - Simulated device. Ctrl-C to exit.\n");

            SendDeviceToCloudMessagesAsync();

            Console.ReadLine();
        }
    }
}
