using Fawkes.BLL.Abstracts;
using Fawkes.BLL.FileSearchers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fawkes.WPF.Model
{
    public class SearchInitializer : ISearchInitializer
    {
        public string FromPath { get; private set; }
        public string SearchedText { get; private set; }
        public IEnumerable<string> FilesToSearch { get; private set; }
        public FileSearcherTypes SearcherType { get; private set; }

        public SearchInitializer SetValues(string searchBy, string fileInfo, string searchedText, string fromPath)
        {
            FilesToSearch = SetFilesToSearch(fileInfo);
            SearcherType = SetSearcherType(searchBy);
            SearchedText = searchedText;
            FromPath = fromPath;

            return this;
        }

        private FileSearcherTypes SetSearcherType(string searchBy)
        {
            bool parseResult = Enum.TryParse(searchBy, out FileSearcherTypes enumResult);

            return parseResult
                ? enumResult
                : FileSearcherTypes.ParseError;
        }

        private IEnumerable<string> SetFilesToSearch(string fileNames)
        {
            return fileNames.Split(';');
        }

        public bool Valid()
        {
            return IsValidICollection(FilesToSearch)
                && IsValidText(SearchedText)
                && IsValidText(FromPath)
                && SearcherType != FileSearcherTypes.ParseError;
        }

        private bool IsValidText(string text) => !string.IsNullOrWhiteSpace(text);
        private bool IsValidICollection<T>(IEnumerable<T> collection) => collection != null && collection.Any();


    }
}
