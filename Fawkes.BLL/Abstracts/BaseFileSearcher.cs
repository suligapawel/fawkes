using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fawkes.BLL.Abstracts
{
    public abstract class BaseFileSearcher
    {
        protected readonly IEnumerable<string> _filesToSearch;

        public List<string> FilePaths { get; protected set; }

        public BaseFileSearcher(IEnumerable<string> filesToSearch)
        {
            FilePaths = new List<string>();
            _filesToSearch = filesToSearch;
        }

        protected async Task SearchInChildrenDirectories(ICollection<string> directoriesPaths)
        {
           if (directoriesPaths.Count > 0)
            {
                await Task.Run(async() =>
                {
                    foreach (var path in directoriesPaths)
                    {
                        try
                        {
                            await LookForFiles(path);
                        }
                        catch (UnauthorizedAccessException)
                        {
                            continue;
                        }
                    }
                });
            }
        }

        public abstract Task LookForFiles(string path);
    }
}
