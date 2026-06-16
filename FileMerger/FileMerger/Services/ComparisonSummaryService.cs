using FileMerger.Models;

namespace FileMerger.Services
{
    internal static class ComparisonSummaryService
    {
        public static ComparisonSummary Calculate(IReadOnlyCollection<FileComparisonResult> results)
        {
            int missingCount = results.Count(ComparisonResultFilter.IsMissing);
            int differentCount = results.Count(result =>
                result.Status is FileComparisonStatus.Different or FileComparisonStatus.ReadError);
            int sameCount = results.Count(result => result.Status == FileComparisonStatus.Same);

            return new ComparisonSummary(
                results.Count,
                missingCount,
                differentCount,
                sameCount);
        }
    }
}
