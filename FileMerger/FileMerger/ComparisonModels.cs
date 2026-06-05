namespace FileMerger
{
    internal enum FileComparisonStatus
    {
        Same,
        Different,
        MissingInA,
        MissingInB,
        ReadError
    }

    internal sealed class FileSnapshot
    {
        public FileSnapshot(string relativePath, string fullPath, long size, DateTime lastWriteTime)
        {
            RelativePath = relativePath;
            FullPath = fullPath;
            Size = size;
            LastWriteTime = lastWriteTime;
        }

        public string RelativePath { get; }

        public string FullPath { get; }

        public long Size { get; }

        public DateTime LastWriteTime { get; }
    }

    internal sealed class FileComparisonResult
    {
        public FileComparisonResult(
            FileComparisonStatus status,
            string relativePath,
            FileSnapshot? fileA,
            FileSnapshot? fileB,
            string? errorMessage)
        {
            Status = status;
            RelativePath = relativePath;
            FileA = fileA;
            FileB = fileB;
            ErrorMessage = errorMessage;
        }

        public FileComparisonStatus Status { get; }

        public string RelativePath { get; }

        public FileSnapshot? FileA { get; }

        public FileSnapshot? FileB { get; }

        public string? ErrorMessage { get; }
    }
}
