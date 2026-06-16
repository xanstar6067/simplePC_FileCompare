using FileMerger.Models;

namespace FileMerger.Services
{
    internal sealed class FileIndexService
    {
        public Dictionary<string, FileSnapshot> Index(string rootPath)
        {
            EnumerationOptions options = new()
            {
                AttributesToSkip = FileAttributes.ReparsePoint,
                IgnoreInaccessible = true,
                RecurseSubdirectories = true,
                ReturnSpecialDirectories = false
            };

            Dictionary<string, FileSnapshot> files = new(StringComparer.OrdinalIgnoreCase);

            foreach (string fullPath in Directory.EnumerateFiles(rootPath, "*", options))
            {
                FileInfo fileInfo;
                try
                {
                    fileInfo = new FileInfo(fullPath);
                }
                catch
                {
                    continue;
                }

                string relativePath = Path.GetRelativePath(rootPath, fullPath);
                files[relativePath] = new FileSnapshot(
                    relativePath,
                    fileInfo.FullName,
                    fileInfo.Length,
                    fileInfo.LastWriteTime);
            }

            return files;
        }
    }
}
