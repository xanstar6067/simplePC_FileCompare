using FileMerger.Models;

namespace FileMerger.Services
{
    internal readonly record struct ComparisonSortOptions(int ColumnIndex, bool Ascending);

    internal static class ComparisonResultSorter
    {
        public static IEnumerable<FileComparisonResult> Apply(
            IEnumerable<FileComparisonResult> results,
            ComparisonSortOptions options,
            Func<FileComparisonResult, string> statusTextSelector)
        {
            if (options.ColumnIndex < 0)
            {
                return results;
            }

            IOrderedEnumerable<FileComparisonResult> orderedResults = options.ColumnIndex switch
            {
                0 => OrderByValue(results, statusTextSelector, options.Ascending, StringComparer.OrdinalIgnoreCase),
                1 => OrderByValue(results, result => result.RelativePath, options.Ascending, StringComparer.OrdinalIgnoreCase),
                2 => OrderByValue(results, result => result.FileA?.FullPath ?? string.Empty, options.Ascending, StringComparer.OrdinalIgnoreCase),
                3 => OrderByValue(results, result => result.FileB?.FullPath ?? string.Empty, options.Ascending, StringComparer.OrdinalIgnoreCase),
                4 => OrderByValue(results, result => result.FileA?.Size, options.Ascending),
                5 => OrderByValue(results, result => result.FileB?.Size, options.Ascending),
                6 => OrderByValue(results, result => result.FileA?.LastWriteTime, options.Ascending),
                7 => OrderByValue(results, result => result.FileB?.LastWriteTime, options.Ascending),
                _ => OrderByValue(results, result => result.RelativePath, options.Ascending, StringComparer.OrdinalIgnoreCase)
            };

            return orderedResults.ThenBy(result => result.RelativePath, StringComparer.OrdinalIgnoreCase);
        }

        private static IOrderedEnumerable<FileComparisonResult> OrderByValue<TKey>(
            IEnumerable<FileComparisonResult> results,
            Func<FileComparisonResult, TKey> selector,
            bool ascending,
            IComparer<TKey>? comparer = null)
        {
            if (comparer is not null)
            {
                return ascending
                    ? results.OrderBy(selector, comparer)
                    : results.OrderByDescending(selector, comparer);
            }

            return ascending
                ? results.OrderBy(selector)
                : results.OrderByDescending(selector);
        }
    }
}
