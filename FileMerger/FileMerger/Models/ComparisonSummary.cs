namespace FileMerger.Models
{
    internal readonly record struct ComparisonSummary(
        int TotalCount,
        int MissingCount,
        int DifferentCount,
        int SameCount);
}
