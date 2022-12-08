using static DataCapture.PingClass;
using static DataCapture.SpeedTestClass;
using static DataCapture.FileOperations;
using DataCapture;
using SpeedTest.Net.Models;
using SpeedTest.Net;

if (Environment.GetCommandLineArgs().Length > 1)
{
    string fileDirectory = Environment.GetCommandLineArgs()[1];

    if(!String.IsNullOrEmpty(fileDirectory))
    {
        FileOperations.directory = fileDirectory;
    }
}

var Done = false;
Task pingTask = new(async () =>
{
    Console.WriteLine("Starting Ping Tests");
    Server server = await SpeedTestClient.GetServer();
    while (!Done)
    {
        long result = PingTest(server.Host.Replace(":8080", ""));
        WriteToFile("Ping", result);

        DateTime d = DateTime.Now.ToLocalTime();
        Thread.Sleep(100);
    }
});

Task speedTask = new(() => { });
try
{
    Console.WriteLine("Starting Speed Test");
    speedTask = DoSpeedTestAsync();

    pingTask.Start();
    
}
catch(Exception e)
{
    Console.WriteLine(e.StackTrace);

}

speedTask.Wait();
Console.WriteLine("Finishing Speed Test");
Console.WriteLine("Finishing Ping Tests");
Done = true;



