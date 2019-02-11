using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Fawkes.BLL
{
    public class TextSearcher
    {
        private readonly string _searchedText;
        public List<string> FilesPathWithText { get; }

        public TextSearcher(string searchedText)
        {
            _searchedText = searchedText;
            FilesPathWithText = new List<string>();
        }

        public async Task FindFilesWithSearchedText(List<string> filesPathList)
        {
            FilesPathWithText.Clear();

            if (filesPathList == null || !filesPathList.Any())
                return;

            await AddIfTextExistst(filesPathList);
        }

        private async Task AddIfTextExistst(List<string> filesList)
        {
            await Task.Run(() =>
            {
                foreach (var file in filesList)
                {
                    bool isTextExistsInFile = IsTextExistsInFile(file);
                    if (isTextExistsInFile)
                    {
                        FilesPathWithText.Add(file);
                    }
                }
            });
        }

        private bool IsTextExistsInFile(string file)
        {
            return File
                .ReadLines(file)
                .Any(line => line.ToLower().Contains(_searchedText));
        }
    }
}
