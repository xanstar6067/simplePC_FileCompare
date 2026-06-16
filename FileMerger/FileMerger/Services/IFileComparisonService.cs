using FileMerger.Models;

namespace FileMerger.Services
{
    internal interface IFileComparisonService
    {
        Task<IReadOnlyList<FileComparisonResult>> CompareAsync(
            string pathA,
            string pathB,
            FileComparisonOptions options);
    }
}
