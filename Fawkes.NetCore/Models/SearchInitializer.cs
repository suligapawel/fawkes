using Fawkes.BLL.Abstracts;
using Fawkes.BLL.FileSearchers;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Fawkes
{
    public class SearchInitializer : ISearchInitializer
    {
        private readonly string _filedIsEmpty = $"Sprawdź czy w appsettings.json istnieje pole {nameof(SearchedText)}";

        public string FromPath { get; }
        public string SearchedText { get; }
        public IEnumerable<string> FilesToSearch { get; }
        public FileSearcherTypes SearcherType { get; }

        public SearchInitializer(IConfigurationRoot configuration)
        {
            SearcherType = SetSearcherType(configuration.GetSection("searchBy").Value);
            SearchedText = configuration.GetSection("searchedText")?.Value ?? throw new ArgumentNullException(_filedIsEmpty);
            FromPath = SetPath(configuration);
            FilesToSearch = SetFileNames(configuration);
        }

        private FileSearcherTypes SetSearcherType(string value)
        {
            return Enum.TryParse(value, out FileSearcherTypes searcherType)
                ? searcherType
                : FileSearcherTypes.ParseError;
        }

        private string SetPath(IConfigurationRoot configuration)
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string path = configuration.GetSection("fromDirectory")?.Value ?? currentDirectory;

            return !string.IsNullOrWhiteSpace(path) ? path : currentDirectory;
        }

        private IEnumerable<string> SetFileNames(IConfigurationRoot configuration)
        {
            return configuration
                      .GetSection("fileInfo")
                      .AsEnumerable()
                      .Where(x => !string.IsNullOrWhiteSpace(x.Value))
                      .Select(x => x.Value);
        }
    }
}
