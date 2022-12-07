using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCapture
{
    internal class FileOperations
    {
        private readonly static DateTimeOffset TODAY = DateTimeOffset.Now;
        public static string directory = "";

        public static void WriteToFile(string topic, string value)
        {
            string fileName = directory + "\\" + GetFileName(topic);

            string output = Convert.ToString(TODAY.ToUnixTimeMilliseconds()) + ";" + value;

            if (!File.Exists(fileName))
            {
                Console.WriteLine("Creating New File: " + fileName);
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine("Time;Value");
                    sw.WriteLine(output);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(fileName))
                {
                    sw.WriteLine(output);
                }
            }
        }

        public static void WriteToFile(string topic, int value)
        {
            string inValue = Convert.ToString(value);
            WriteToFile(topic, inValue);
        }

        public static void WriteToFile(string topic, long value)
        {
            string inValue = Convert.ToString(value);
            WriteToFile(topic, inValue);
        }

        public static void WriteToFile(string topic, double value)
        {
            string inValue = Convert.ToString(value);
            WriteToFile(topic, inValue);
        }

        private static string GetFileName(string topic)
        {
            string dayOfMonth = TODAY.Day.ToString();
            string month = TODAY.Month.ToString();
            string year = TODAY.Year.ToString();

            int weekDay = (int)TODAY.DayOfWeek;
            bool isWeekend = weekDay == 0 || weekDay == 6;

            return topic + "_" + dayOfMonth + month + year + "_" + (isWeekend ? "we" : "w") + ".csv";
        }
    }
}
