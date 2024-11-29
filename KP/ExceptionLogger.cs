using System;
using System.IO;

namespace KP
{
    public static class ExceptionLogger
    {
        public static string LogFilePath { get; set; }

        static ExceptionLogger()
        {
            string appPath = AppDomain.CurrentDomain.BaseDirectory;
            LogFilePath = Path.Combine(appPath, "ExceptionLog.txt");
        }

        public static void LogException(Exception ex)
        {
            try
            {
                using (var sw = new StreamWriter(LogFilePath, true))
                {
                    sw.WriteLine("Дата: " + DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
                    sw.WriteLine("Исключение: " + ex.GetType().ToString());
                    sw.WriteLine("Сообщение: " + ex.Message);
                    sw.WriteLine("Метод: " + ex.TargetSite);
                    sw.WriteLine("Стек вызовов: " + ex.StackTrace);
                    sw.WriteLine(new string('-', 80));
                }
            }
            catch
            {
                
            }
        }
    }
}