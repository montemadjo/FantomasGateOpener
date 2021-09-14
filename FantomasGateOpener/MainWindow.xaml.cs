using Microsoft.Azure.Devices;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace FantomasGateOpener
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Connection string for your IoT Hub
        // az iot hub show-connection-string --hub-name {your iot hub name} --policy-name service
        private static string s_connectionString = "HostName=maximus.azure-devices.net;SharedAccessKeyName=service;SharedAccessKey=2HPCBmrIaPnTtU4nX0zgRbxuqwmkol88k9pvv3ZMfCA=";

        private static ServiceClient s_serviceClient;

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void OpenGateButton_ClickAsync(object sender, RoutedEventArgs e)
        {
            rtbLog.AppendText("IoT Hub Quickstarts #2 - InvokeDeviceMethod application.", "green", true);

            // This sample accepts the service connection string as a parameter, if present
            //ValidateConnectionString(s_connectionString);

            // Create a ServiceClient to communicate with service-facing endpoint on your hub.
            s_serviceClient = ServiceClient.CreateFromConnectionString(s_connectionString);

            try
            {
                await InvokeMethodAsync();

                s_serviceClient.Dispose();
            }
            catch(Exception ex)
            {
                rtbLog.AppendText(ex.Message, "red", true);
            }

        }

        // Invoke the direct method on the device, passing the payload
        private async Task InvokeMethodAsync()
        {
            var methodInvocation = new CloudToDeviceMethod("PulseOpenRelay")
            {
                ResponseTimeout = TimeSpan.FromSeconds(30),
            };
            methodInvocation.SetPayloadJson("10");

            // Invoke the direct method asynchronously and get the response from the simulated device.
            var response = await s_serviceClient.InvokeDeviceMethodAsync("BandiciDevice", methodInvocation);

            rtbLog.AppendText($"Response status: {response.Status}, payload:\n\t{response.GetPayloadAsJson()}", "yellow", true);
        }

        //private static void ValidateConnectionString(string[] args)
        //{
        //    if (args.Any())
        //    {
        //        try
        //        {
        //            var cs = IotHubConnectionStringBuilder.Create(args[0]);
        //            s_connectionString = cs.ToString();
        //        }
        //        catch (Exception)
        //        {
        //            Console.WriteLine($"Error: Unrecognizable parameter '{args[0]}' as connection string.");
        //            Environment.Exit(1);
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            _ = IotHubConnectionStringBuilder.Create(s_connectionString);
        //        }
        //        catch (Exception)
        //        {
        //            Console.WriteLine("This sample needs a device connection string to run. Program.cs can be edited to specify it, or it can be included on the command-line as the only parameter.");
        //            Environment.Exit(1);
        //        }
        //    }
        //}
    }
}
