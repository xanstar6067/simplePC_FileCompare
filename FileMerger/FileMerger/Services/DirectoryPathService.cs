namespace FileMerger.Services
{
    internal static class DirectoryPathService
    {
        public static string Normalize(string path)
        {
            return Path.TrimEndingDirectorySeparator(Path.GetFullPath(path.Trim()));
        }

        public static bool AreSame(string? firstPath, string? secondPath)
        {
            if (firstPath is null || secondPath is null)
            {
                return false;
            }

            return string.Equals(
                Normalize(firstPath),
                Normalize(secondPath),
                StringComparison.OrdinalIgnoreCase);
        }
    }
}
