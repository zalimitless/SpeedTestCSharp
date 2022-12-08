using System.Net.NetworkInformation;

namespace DataCapture
{
    internal class PingClass
    {
        public static long PingTest(string host, int buffer_size = 64, int timeout = 1000)
        {
            try
            {
                Ping myPing = new();
                byte[] buffer = new byte[buffer_size];
                PingOptions pingOptions = new();    
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);
                if (reply.Status != IPStatus.Success)
                    return -1;
                return reply.RoundtripTime;
            }
            catch
            {
                return -1;
            }
        }
    }
}
