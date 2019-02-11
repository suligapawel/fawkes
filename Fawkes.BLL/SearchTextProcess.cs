using Fawkes.BLL.Abstracts;
using Fawkes.BLL.FileSearchers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fawkes.BLL
{
    public class SearchTextProcess
    {
        private const string SEARCHER_IS_NOT_DEFINED = "Nie zdefiniowano szukacza.";
        private const string SEARCHING_FILES_IN_DIRECTORIES = "Przeszukuje katalogi w poszukiwaniu zdefiniowanych plików...";
        private const string SEARCHING_TEXT_IN_FILES = "Przeszukuje pliki w poszukiwaniu zdefiniowanych wyrazów...";
        private const string NO_FILES_WITH_PARAMETERS = "Nie znaleziono plików o podanych parametrach.";
        private const string NO_FILES_WITH_TEXT = "Nie znaleziono plików ze zdefiniowanym tekstem.";

        private readonly ISearchInitializer _initializer;
        private readonly IDisplayerAdapter _displayer;
        private readonly BaseFileSearcher _fileSearcher;
        private readonly TextSearcher _textSearcher;

        public SearchTextProcess(ISearchInitializer initializer, IDisplayerAdapter displayer)
        {
            _initializer = initializer;
            _displayer = displayer;
            _fileSearcher = ChoiceSearcher(initializer);
            _textSearcher = new TextSearcher(initializer.SearchedText);
        }

        public async Task GetSearchedFilePaths()
        {
            List<string> filesPaths = await GetFilesLocation();
            await FindFilesWithText(filesPaths);
            ShowFoundedPaths();
        }

        private BaseFileSearcher ChoiceSearcher(ISearchInitializer initializer)
        {
            switch (initializer.SearcherType)
            {
                case FileSearcherTypes.Names:
                    return new SearcherByNames(initializer.FilesToSearch);
                case FileSearcherTypes.Extensions:
                    return new SearcherByExtension(initializer.FilesToSearch);
                default:
                    throw new Exception(SEARCHER_IS_NOT_DEFINED);
            }
        }

        private async Task<List<string>> GetFilesLocation()
        {
            _displayer.Show(SEARCHING_FILES_IN_DIRECTORIES);
            await _fileSearcher.LookForFiles(_initializer.FromPath);

            if (!_fileSearcher.FilePaths.Any())
                throw new Exception(NO_FILES_WITH_PARAMETERS);

            return _fileSearcher.FilePaths;
        }

        private async Task FindFilesWithText(List<string> filesPaths)
        {
            _displayer.Show(SEARCHING_TEXT_IN_FILES);
            await _textSearcher.FindFilesWithSearchedText(filesPaths);
        }

        private void ShowFoundedPaths()
        {
            List<string> filePaths = _textSearcher.FilesPathWithText;

            if (!filePaths.Any())
                throw new Exception(NO_FILES_WITH_TEXT);

            foreach (var file in _textSearcher.FilesPathWithText)
            {
                _displayer.Show(file);
            }
        }
    }
}
