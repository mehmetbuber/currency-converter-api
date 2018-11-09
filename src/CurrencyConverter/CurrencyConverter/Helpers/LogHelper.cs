using System;
using System.IO;
using System.Linq;
using System.Text;

namespace CurrencyConverter.Helpers
{
    public static class LogHelper
    {
        public static void WriteToFile(string Message)
        {
            string LogFolder = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
            string FileName = String.Format("{0:dd-MM-yyyy}", DateTime.Now);

            if (Directory.Exists(LogFolder) == false)
                Directory.CreateDirectory(LogFolder);

            var sw = new StreamWriter(String.Format("{0}{1}.txt", LogFolder, FileName), true);
            sw.WriteLine(DateTime.Now.ToString());
            sw.WriteLine(Message);
            sw.Flush();
            sw.Close();
        }
        public static void WriteToFileNoDate(string Message)
        {
            Message = Message.Replace(System.Environment.NewLine, " ");
            string LogFolder = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
            string FileName = String.Format("{0:dd-MM-yyyy}", DateTime.Now);

            if (Directory.Exists(LogFolder) == false)
                Directory.CreateDirectory(LogFolder);

            var sw = new StreamWriter(String.Format("{0}{1}.txt", LogFolder, FileName), true);
            sw.WriteLine(Message);
            sw.Flush();
            sw.Close();
        }

        public static void SaveAs(string csvFileName)
        {
            string LogFolder = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
            string FileName = String.Format("{0:dd-MM-yyyy}", DateTime.Now);

            string[] allLines = File.ReadAllLines(String.Format("{0}{1}.txt", LogFolder, FileName));

            var csv = new StringBuilder();
            allLines.ToList().ForEach(line =>
            {
                csv.AppendLine(string.Join(",", line));
            });

            File.WriteAllText(String.Format("{0}{1}.csv", LogFolder, csvFileName), csv.ToString());


        }

        public static void WriteAllText(string relativePath, string text)
        {
            File.WriteAllText(System.Web.HttpContext.Current.Server.MapPath(relativePath), text);
        }

        public static string ReadAllText(string relativePath, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = Encoding.UTF8;
            return File.ReadAllText(System.Web.HttpContext.Current.Server.MapPath(relativePath), encoding);
        }

        public static string[] ReadAllLines(string relativePath)
        {
            return File.ReadAllLines(System.Web.HttpContext.Current.Server.MapPath(relativePath));
        }

        public static void DeleteText(string relativePath)
        {
            System.IO.File.Delete(System.Web.HttpContext.Current.Server.MapPath(relativePath));
        }
    }
}