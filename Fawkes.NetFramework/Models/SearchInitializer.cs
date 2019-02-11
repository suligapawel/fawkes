using Fawkes.BLL.Abstracts;
using Fawkes.BLL.FileSearchers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace Fawkes.NetFramework.Models
{
    public class SearchInitializer : ISearchInitializer
    {
        private readonly string _filedIsEmpty = $"Sprawdź czy w appsettings.json istnieje pole {nameof(SearchedText)}";

        public string FromPath { get; }
        public string SearchedText { get; }
        public IEnumerable<string> FilesToSearch { get; }
        public FileSearcherTypes SearcherType { get; }

        public SearchInitializer(NameValueCollection configValues)
        {
            SearcherType = (FileSearcherTypes)Enum.Parse(typeof(FileSearcherTypes), configValues["searchBy"]);
            FromPath = configValues["fromDirectory"];
            SearchedText = configValues["searchedText"];
            FilesToSearch = configValues["fileInfo"].Split(';');
        }
    }
}
