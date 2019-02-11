using Fawkes.BLL.Abstracts;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Fawkes.BLL.FileSearchers
{
    public class SearcherByExtension : BaseFileSearcher
    {
        public SearcherByExtension(IEnumerable<string> filesExtensions) : base(filesExtensions) { }

        public override async Task LookForFiles(string path)
        {
            string[] directoriesPaths = Directory.GetDirectories(path);
            List<string> searchedFilesPaths = await SetSearchedFilesPaths(path);

            await TryAddFilePathToList(searchedFilesPaths);
            await SearchInChildrenDirectories(directoriesPaths);
        }

        private async Task<List<string>> SetSearchedFilesPaths(string path)
        {
            List<string> searchedFilesPaths = new List<string>();

            await Task.Run(() =>
             {
                 foreach (var file in _filesToSearch)
                 {
                     foreach (var filePath in Directory.GetFiles(path, $"*.{file}"))
                     {
                         searchedFilesPaths.Add(filePath);
                     }
                 }
             });

            return searchedFilesPaths;
        }

        private async Task TryAddFilePathToList(IEnumerable<string> filePaths)
        {
            await Task.Run(() =>
            {
                foreach (var path in filePaths)
                {
                    if (!string.IsNullOrWhiteSpace(path))
                        FilePaths.Add(path);
                }
            });
        }
    }
}
