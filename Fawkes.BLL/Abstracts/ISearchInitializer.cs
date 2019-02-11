using Fawkes.BLL.FileSearchers;
using System.Collections.Generic;

namespace Fawkes.BLL.Abstracts
{
    public interface ISearchInitializer
    {
        string FromPath { get; }
        string SearchedText { get; }
        IEnumerable<string> FilesToSearch { get; }
        FileSearcherTypes SearcherType { get; }
    }
}
