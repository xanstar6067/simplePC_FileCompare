using FileMerger.Models;

namespace FileMerger.Services
{
    internal sealed class FileComparisonService : IFileComparisonService
    {
        private readonly FileIndexService fileIndexService;
        private readonly FileContentComparisonService fileContentComparisonService;

        public FileComparisonService()
            : this(new FileIndexService(), new FileContentComparisonService())
        {
        }

        internal FileComparisonService(
            FileIndexService fileIndexService,
            FileContentComparisonService fileContentComparisonService)
        {
            this.fileIndexService = fileIndexService;
            this.fileContentComparisonService = fileContentComparisonService;
        }

        public Task<IReadOnlyList<FileComparisonResult>> CompareAsync(
            string pathA,
            string pathB,
            FileComparisonOptions options)
        {
            return Task.Run<IReadOnlyList<FileComparisonResult>>(() => Compare(pathA, pathB, options));
        }

        private List<FileComparisonResult> Compare(
            string pathA,
            string pathB,
            FileComparisonOptions options)
        {
            Dictionary<string, FileSnapshot> filesA = fileIndexService.Index(pathA);
            Dictionary<string, FileSnapshot> filesB = fileIndexService.Index(pathB);
            SortedSet<string> allRelativePaths = new(StringComparer.OrdinalIgnoreCase);

            foreach (string relativePath in filesA.Keys)
            {
                allRelativePaths.Add(relativePath);
            }

            foreach (string relativePath in filesB.Keys)
            {
                allRelativePaths.Add(relativePath);
            }

            List<FileComparisonResult> results = new(allRelativePaths.Count);

            foreach (string relativePath in allRelativePaths)
            {
                filesA.TryGetValue(relativePath, out FileSnapshot? fileA);
                filesB.TryGetValue(relativePath, out FileSnapshot? fileB);

                FileComparisonStatus status;
                string? errorMessage = null;

                if (fileA is null)
                {
                    status = FileComparisonStatus.MissingInA;
                }
                else if (fileB is null)
                {
                    status = FileComparisonStatus.MissingInB;
                }
                else
                {
                    status = fileContentComparisonService.Compare(fileA, fileB, options, out errorMessage);
                }

                results.Add(new FileComparisonResult(status, relativePath, fileA, fileB, errorMessage));
            }

            return results;
        }
    }
}
