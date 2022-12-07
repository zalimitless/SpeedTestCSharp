using static DataCapture.PingClass;
using static DataCapture.SpeedTestClass;
using static DataCapture.FileOperations;
using DataCapture;


if (Environment.GetCommandLineArgs().Length > 1)
{
    string fileDirectory = Environment.GetCommandLineArgs()[1];

    if(!String.IsNullOrEmpty(fileDirectory))
    {
        FileOperations.directory = fileDirectory;
    }
}

var Done = false;
Task pingTask = new(() =>
{
    Console.WriteLine("Starting Ping Tests");
    while(!Done)
    {
        long result = PingTest("8.8.8.8");
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



