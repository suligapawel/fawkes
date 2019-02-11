using Fawkes.BLL;
using Fawkes.NetFramework.Models;
using System;
using System.Collections.Specialized;
using System.Configuration;

namespace Fawkes.NetFramework
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                NameValueCollection appSettings = ConfigurationManager.AppSettings;
                var process = new SearchTextProcess(new SearchInitializer(appSettings), new Displayer());

                process.GetSearchedFilePaths().GetAwaiter().GetResult();

                Console.WriteLine("Zakończono z powodzeniem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
