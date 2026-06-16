using FileMerger.Models;

namespace FileMerger.UI
{
    internal sealed class FileComparisonListViewItemFactory
    {
        public ListViewItem Create(FileComparisonResult result)
        {
            ListViewItem item = new(FileComparisonDisplayText.GetStatusText(result));
            item.SubItems.Add(result.RelativePath);
            item.SubItems.Add(result.FileA?.FullPath ?? "-");
            item.SubItems.Add(result.FileB?.FullPath ?? "-");
            item.SubItems.Add(FileComparisonDisplayText.FormatSize(result.FileA?.Size));
            item.SubItems.Add(FileComparisonDisplayText.FormatSize(result.FileB?.Size));
            item.SubItems.Add(FileComparisonDisplayText.FormatDate(result.FileA?.LastWriteTime));
            item.SubItems.Add(FileComparisonDisplayText.FormatDate(result.FileB?.LastWriteTime));
            item.Tag = result;

            ApplyStatusStyle(item, result.Status);

            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                item.ToolTipText = result.ErrorMessage;
            }

            return item;
        }

        private static void ApplyStatusStyle(ListViewItem item, FileComparisonStatus status)
        {
            switch (status)
            {
                case FileComparisonStatus.MissingInA:
                case FileComparisonStatus.MissingInB:
                    item.BackColor = Color.FromArgb(255, 247, 237);
                    item.ForeColor = Color.FromArgb(124, 45, 18);
                    break;
                case FileComparisonStatus.Different:
                    item.BackColor = Color.FromArgb(254, 252, 232);
                    item.ForeColor = Color.FromArgb(113, 63, 18);
                    break;
                case FileComparisonStatus.ReadError:
                    item.BackColor = Color.FromArgb(254, 242, 242);
                    item.ForeColor = Color.FromArgb(127, 29, 29);
                    break;
                case FileComparisonStatus.Same:
                    item.BackColor = Color.FromArgb(240, 253, 244);
                    item.ForeColor = Color.FromArgb(20, 83, 45);
                    break;
            }
        }
    }
}
