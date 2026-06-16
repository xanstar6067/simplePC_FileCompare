namespace FileMerger.Models
{
    internal readonly record struct FileComparisonOptions(bool VerifyMd5Checksum)
    {
        public static FileComparisonOptions Default { get; } = new(false);
    }
}
