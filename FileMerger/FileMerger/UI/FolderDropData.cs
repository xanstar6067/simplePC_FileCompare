namespace FileMerger.UI
{
    internal static class FolderDropData
    {
        public static bool ContainsFolder(DragEventArgs e)
        {
            return GetDirectory(e) is not null;
        }

        public static string? GetDirectory(DragEventArgs e)
        {
            if (e.Data is null || !e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return null;
            }

            string[]? paths = e.Data.GetData(DataFormats.FileDrop) as string[];
            string? firstPath = paths?.FirstOrDefault();

            return firstPath is not null && Directory.Exists(firstPath)
                ? firstPath
                : null;
        }
    }
}
