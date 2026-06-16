namespace FileMerger.Models
{
    internal enum FileComparisonStatus
    {
        Same,
        Different,
        MissingInA,
        MissingInB,
        ReadError
    }
}
