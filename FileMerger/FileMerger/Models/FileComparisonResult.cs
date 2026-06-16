namespace FileMerger.Models
{
    internal sealed record FileComparisonResult(
        FileComparisonStatus Status,
        string RelativePath,
        FileSnapshot? FileA,
        FileSnapshot? FileB,
        string? ErrorMessage);
}
