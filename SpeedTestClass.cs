using SpeedTest.Net;
using DataCapture;
using SpeedTest.Net.Models;

namespace DataCapture
{
    internal class SpeedTestClass
    {
        public static async Task DoSpeedTestAsync()
        {
            double speedD = -1;
            string unit = "kb/s";
            try
            {
                Server server = await SpeedTestClient.GetServer();
                Console.WriteLine(server.Host);

                DownloadSpeed speed = await SpeedTestClient.GetDownloadSpeed(server, unit: SpeedTest.Net.Enums.SpeedTestUnit.MegaBitsPerSecond);
                FileOperations.WriteToFile("Server", server.Host);

                speedD = Math.Round(speed.Speed, 2);
                unit = speed.Unit;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            FileOperations.WriteToFile("Speed", speedD);
        }
    }
}
