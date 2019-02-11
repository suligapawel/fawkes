using Fawkes.BLL.Abstracts;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Fawkes.BLL.FileSearchers
{
    public class SearcherByNames : BaseFileSearcher
    {

        public SearcherByNames(IEnumerable<string> fileNames) : base(fileNames) { }

        public override async Task LookForFiles(string path)
        {
            string[] directoriesPaths = Directory.GetDirectories(path);

            string filePath = GetFileNameIfExist(path);
            TryAddFilePathToList(filePath);

            await SearchInChildrenDirectories(directoriesPaths);
        }

        private string GetFileNameIfExist(string path)
        {
            string fileFullPath;

            foreach (var fileName in _filesToSearch)
            {
                fileFullPath = $@"{path}\{fileName}";

                if (File.Exists(fileFullPath))
                {
                    return fileFullPath;
                }
            }

            return string.Empty;
        }

        private void TryAddFilePathToList(string filePath)
        {
            if (!string.IsNullOrWhiteSpace(filePath))
                FilePaths.Add(filePath);
        }
    }
}
