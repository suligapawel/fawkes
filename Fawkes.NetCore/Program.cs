using Fawkes.BLL;
using Fawkes.BLL.Abstracts;
using Fawkes.NetCore.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace Fawkes.NetCore
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IConfigurationRoot configuration = BuildConfigration();
                ISearchInitializer searchInfo = new SearchInitializer(configuration);

                var process = new SearchTextProcess(new SearchInitializer(configuration), new Displayer());
                process.GetSearchedFilePaths().GetAwaiter().GetResult();

                Console.WriteLine("Zakończono z powodzeniem!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }

        private static IConfigurationRoot BuildConfigration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            return builder.Build();
        }
    }
}
