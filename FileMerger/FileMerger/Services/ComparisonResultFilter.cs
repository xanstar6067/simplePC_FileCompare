using FileMerger.Models;

namespace FileMerger.Services
{
    internal readonly record struct ComparisonResultFilterOptions(
        string SearchText,
        bool ShowMissing,
        bool ShowDifferent,
        bool ShowAll);

    internal static class ComparisonResultFilter
    {
        public static IEnumerable<FileComparisonResult> Apply(
            IEnumerable<FileComparisonResult> results,
            ComparisonResultFilterOptions options)
        {
            string searchText = options.SearchText.Trim();

            foreach (FileComparisonResult result in results)
            {
                if (searchText.Length > 0 && !MatchesSearch(result, searchText))
                {
                    continue;
                }

                if (options.ShowAll ||
                    (options.ShowMissing && IsMissing(result)) ||
                    (options.ShowDifferent && IsDifferentOrUnreadable(result)))
                {
                    yield return result;
                }
            }
        }

        public static bool IsMissing(FileComparisonResult result)
        {
            return result.Status is FileComparisonStatus.MissingInA or FileComparisonStatus.MissingInB;
        }

        private static bool IsDifferentOrUnreadable(FileComparisonResult result)
        {
            return result.Status is FileComparisonStatus.Different or FileComparisonStatus.ReadError;
        }

        private static bool MatchesSearch(FileComparisonResult result, string searchText)
        {
            return Contains(result.RelativePath, searchText)
                || Contains(result.FileA?.FullPath, searchText)
                || Contains(result.FileB?.FullPath, searchText);
        }

        private static bool Contains(string? value, string searchText)
        {
            return value?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
