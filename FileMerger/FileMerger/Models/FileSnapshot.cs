namespace FileMerger.Models
{
    internal sealed record FileSnapshot(
        string RelativePath,
        string FullPath,
        long Size,
        DateTime LastWriteTime);
}
