namespace FileMerger
{
    public partial class Form1 : Form
    {
        private const string EmptyFolderText = "Папка не выбрана";

        private readonly List<FileComparisonResult> comparisonResults = new();
        private readonly Dictionary<ColumnHeader, string> columnHeaderTexts = new();

        private string? folderAPath;
        private string? folderBPath;
        private int sortColumnIndex = -1;
        private bool sortAscending = true;

        public Form1()
        {
            InitializeComponent();
            InitializeRuntimeUi();
        }

        private void InitializeRuntimeUi()
        {
            RegisterDropZone(folderAPanel, true);
            RegisterDropZone(folderBPanel, false);

            browseAButton.Click += (_, _) => SelectFolder(true);
            browseBButton.Click += (_, _) => SelectFolder(false);
            clearAButton.Click += (_, _) => SetFolder(true, null);
            clearBButton.Click += (_, _) => SetFolder(false, null);

            compareButton.Click += async (_, _) => await CompareFoldersAsync();
            showMissingCheckBox.CheckedChanged += (_, _) => RefreshResults();
            showDifferentCheckBox.CheckedChanged += (_, _) => RefreshResults();
            showAllCheckBox.CheckedChanged += (_, _) => RefreshResults();
            searchTextBox.TextChanged += (_, _) => RefreshResults();
            resultsListView.Resize += (_, _) => ResizeResultColumns();
            resultsListView.ColumnClick += ResultsListView_ColumnClick;
            resultsListView.KeyDown += ResultsListView_KeyDown;
            resultsListView.MouseDown += ResultsListView_MouseDown;
            resultsListView.MultiSelect = true;
            resultsListView.HideSelection = false;

            toolTip.SetToolTip(folderAPanel, "Перетащите папку A");
            toolTip.SetToolTip(folderBPanel, "Перетащите папку B");
            toolTip.SetToolTip(compareButton, "Запустить сравнение выбранных папок");
            toolTip.SetToolTip(searchTextBox, "Фильтр по имени файла или любому пути");

            CaptureColumnHeaderTexts();
            ConfigureResultsContextMenu();
            ResetSummary();
            ResizeResultColumns();
        }

        private void RegisterDropZone(Control control, bool isFolderA)
        {
            control.AllowDrop = true;
            control.DragEnter += (_, e) => HandleDragEnter(e, isFolderA);
            control.DragDrop += (_, e) => HandleDragDrop(e, isFolderA);
            control.DragLeave += (_, _) => SetDropZoneHighlight(isFolderA, false);

            foreach (Control child in control.Controls)
            {
                if (child is not Button)
                {
                    RegisterDropZone(child, isFolderA);
                }
            }
        }

        private void HandleDragEnter(DragEventArgs e, bool isFolderA)
        {
            e.Effect = ContainsFolderDrop(e) ? DragDropEffects.Copy : DragDropEffects.None;
            SetDropZoneHighlight(isFolderA, e.Effect == DragDropEffects.Copy);
        }

        private void HandleDragDrop(DragEventArgs e, bool isFolderA)
        {
            SetDropZoneHighlight(isFolderA, false);

            string? directory = GetDroppedDirectory(e);
            if (directory is null)
            {
                statusLabel.Text = "Перетащите именно папку, а не файл.";
                return;
            }

            SetFolder(isFolderA, directory);
        }

        private static bool ContainsFolderDrop(DragEventArgs e)
        {
            return GetDroppedDirectory(e) is not null;
        }

        private static string? GetDroppedDirectory(DragEventArgs e)
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

        private void SetDropZoneHighlight(bool isFolderA, bool highlighted)
        {
            Panel panel = isFolderA ? folderAPanel : folderBPanel;
            panel.BackColor = highlighted ? Color.FromArgb(239, 246, 255) : Color.White;
        }

        private void SelectFolder(bool isFolderA)
        {
            string initialDirectory = (isFolderA ? folderAPath : folderBPath)
                ?? Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            using FolderBrowserDialog dialog = new()
            {
                Description = isFolderA ? "Выберите папку A" : "Выберите папку B",
                InitialDirectory = initialDirectory,
                ShowNewFolderButton = false,
                UseDescriptionForTitle = true
            };

            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                SetFolder(isFolderA, dialog.SelectedPath);
            }
        }

        private void SetFolder(bool isFolderA, string? path)
        {
            string? normalizedPath = string.IsNullOrWhiteSpace(path)
                ? null
                : NormalizeDirectoryPath(path);

            string? oppositePath = isFolderA ? folderBPath : folderAPath;
            if (normalizedPath is not null && IsSameDirectory(normalizedPath, oppositePath))
            {
                statusLabel.Text = isFolderA
                    ? "Эта папка уже выбрана как папка B. Выберите другую папку A."
                    : "Эта папка уже выбрана как папка A. Выберите другую папку B.";
                return;
            }

            if (isFolderA)
            {
                folderAPath = normalizedPath;
                folderAPathLabel.Text = normalizedPath ?? EmptyFolderText;
                toolTip.SetToolTip(folderAPathLabel, normalizedPath ?? "Папка A не выбрана");
            }
            else
            {
                folderBPath = normalizedPath;
                folderBPathLabel.Text = normalizedPath ?? EmptyFolderText;
                toolTip.SetToolTip(folderBPathLabel, normalizedPath ?? "Папка B не выбрана");
            }

            comparisonResults.Clear();
            resultsListView.Items.Clear();
            ResetSummary();
            statusLabel.Text = folderAPath is not null && folderBPath is not null
                ? "Папки выбраны. Нажмите \"Сравнить\"."
                : "Выберите две папки для старта.";
        }

        private async Task CompareFoldersAsync()
        {
            if (folderAPath is null || folderBPath is null)
            {
                statusLabel.Text = "Для сравнения нужны обе папки.";
                return;
            }

            if (IsSameDirectory(folderAPath, folderBPath))
            {
                statusLabel.Text = "Для сравнения выберите две разные папки.";
                return;
            }

            if (!Directory.Exists(folderAPath) || !Directory.Exists(folderBPath))
            {
                statusLabel.Text = "Одна из выбранных папок больше не существует.";
                return;
            }

            compareButton.Enabled = false;
            compareButton.Text = "Сравниваю...";
            statusLabel.Text = "Идет сравнение файлов.";
            Cursor = Cursors.WaitCursor;

            try
            {
                string pathA = folderAPath;
                string pathB = folderBPath;
                List<FileComparisonResult> results = await Task.Run(() => CompareFolders(pathA, pathB));

                comparisonResults.Clear();
                comparisonResults.AddRange(results);
                RefreshResults();
            }
            catch (Exception ex)
            {
                comparisonResults.Clear();
                resultsListView.Items.Clear();
                ResetSummary();
                statusLabel.Text = $"Не удалось выполнить сравнение: {ex.Message}";
            }
            finally
            {
                compareButton.Enabled = true;
                compareButton.Text = "Сравнить";
                Cursor = Cursors.Default;
            }
        }

        private static List<FileComparisonResult> CompareFolders(string pathA, string pathB)
        {
            Dictionary<string, FileSnapshot> filesA = IndexFiles(pathA);
            Dictionary<string, FileSnapshot> filesB = IndexFiles(pathB);
            SortedSet<string> allRelativePaths = new(StringComparer.OrdinalIgnoreCase);

            foreach (string relativePath in filesA.Keys)
            {
                allRelativePaths.Add(relativePath);
            }

            foreach (string relativePath in filesB.Keys)
            {
                allRelativePaths.Add(relativePath);
            }

            List<FileComparisonResult> results = new(allRelativePaths.Count);

            foreach (string relativePath in allRelativePaths)
            {
                filesA.TryGetValue(relativePath, out FileSnapshot? fileA);
                filesB.TryGetValue(relativePath, out FileSnapshot? fileB);

                FileComparisonStatus status;
                string? errorMessage = null;

                if (fileA is null)
                {
                    status = FileComparisonStatus.MissingInA;
                }
                else if (fileB is null)
                {
                    status = FileComparisonStatus.MissingInB;
                }
                else
                {
                    status = CompareFiles(fileA, fileB, out errorMessage);
                }

                results.Add(new FileComparisonResult(status, relativePath, fileA, fileB, errorMessage));
            }

            return results;
        }

        private static Dictionary<string, FileSnapshot> IndexFiles(string rootPath)
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

        private static FileComparisonStatus CompareFiles(
            FileSnapshot fileA,
            FileSnapshot fileB,
            out string? errorMessage)
        {
            errorMessage = null;

            if (fileA.Size != fileB.Size)
            {
                return FileComparisonStatus.Different;
            }

            byte[] bufferA = new byte[81920];
            byte[] bufferB = new byte[81920];

            try
            {
                using FileStream streamA = File.OpenRead(fileA.FullPath);
                using FileStream streamB = File.OpenRead(fileB.FullPath);

                while (true)
                {
                    int readA = streamA.Read(bufferA, 0, bufferA.Length);
                    int readB = streamB.Read(bufferB, 0, bufferB.Length);

                    if (readA != readB)
                    {
                        return FileComparisonStatus.Different;
                    }

                    if (readA == 0)
                    {
                        return FileComparisonStatus.Same;
                    }

                    if (!bufferA.AsSpan(0, readA).SequenceEqual(bufferB.AsSpan(0, readB)))
                    {
                        return FileComparisonStatus.Different;
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return FileComparisonStatus.ReadError;
            }
        }

        private void RefreshResults()
        {
            int totalCount = comparisonResults.Count;
            int missingCount = comparisonResults.Count(IsMissing);
            int differentCount = comparisonResults.Count(result =>
                result.Status is FileComparisonStatus.Different or FileComparisonStatus.ReadError);
            int sameCount = comparisonResults.Count(result => result.Status == FileComparisonStatus.Same);

            totalCountLabel.Text = totalCount.ToString("N0");
            missingCountLabel.Text = missingCount.ToString("N0");
            differentCountLabel.Text = differentCount.ToString("N0");
            sameCountLabel.Text = sameCount.ToString("N0");

            List<FileComparisonResult> filteredResults = ApplySort(comparisonResults.Where(MatchesFilter)).ToList();

            resultsListView.BeginUpdate();
            resultsListView.Items.Clear();

            foreach (FileComparisonResult result in filteredResults)
            {
                resultsListView.Items.Add(CreateListViewItem(result));
            }

            resultsListView.EndUpdate();

            statusLabel.Text = comparisonResults.Count == 0
                ? "Результатов пока нет."
                : $"Показано {resultsListView.Items.Count:N0} из {comparisonResults.Count:N0}.";
        }

        private bool MatchesFilter(FileComparisonResult result)
        {
            string searchText = searchTextBox.Text.Trim();
            if (searchText.Length > 0 && !MatchesSearch(result, searchText))
            {
                return false;
            }

            if (showAllCheckBox.Checked)
            {
                return true;
            }

            if (showMissingCheckBox.Checked && IsMissing(result))
            {
                return true;
            }

            if (showDifferentCheckBox.Checked &&
                result.Status is FileComparisonStatus.Different or FileComparisonStatus.ReadError)
            {
                return true;
            }

            return false;
        }

        private static bool MatchesSearch(FileComparisonResult result, string searchText)
        {
            return Contains(result.RelativePath, searchText)
                || Contains(result.FileA?.FullPath, searchText)
                || Contains(result.FileB?.FullPath, searchText);
        }

        private static bool Contains(string? value, string searchText)
        {
            return value?.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        private static bool IsMissing(FileComparisonResult result)
        {
            return result.Status is FileComparisonStatus.MissingInA or FileComparisonStatus.MissingInB;
        }

        private ListViewItem CreateListViewItem(FileComparisonResult result)
        {
            string statusText = GetStatusText(result);
            ListViewItem item = new(statusText);
            item.SubItems.Add(result.RelativePath);
            item.SubItems.Add(result.FileA?.FullPath ?? "-");
            item.SubItems.Add(result.FileB?.FullPath ?? "-");
            item.SubItems.Add(FormatSize(result.FileA?.Size));
            item.SubItems.Add(FormatSize(result.FileB?.Size));
            item.SubItems.Add(FormatDate(result.FileA?.LastWriteTime));
            item.SubItems.Add(FormatDate(result.FileB?.LastWriteTime));
            item.Tag = result;

            ApplyStatusStyle(item, result.Status);

            if (!string.IsNullOrWhiteSpace(result.ErrorMessage))
            {
                item.ToolTipText = result.ErrorMessage;
            }

            return item;
        }

        private void ResultsListView_ColumnClick(object? sender, ColumnClickEventArgs e)
        {
            if (sortColumnIndex == e.Column)
            {
                sortAscending = !sortAscending;
            }
            else
            {
                sortColumnIndex = e.Column;
                sortAscending = true;
            }

            UpdateSortIndicators();
            RefreshResults();
        }

        private void ResultsListView_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                CopySelectedRows();
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void ResultsListView_MouseDown(object? sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right)
            {
                return;
            }

            ListViewItem? clickedItem = resultsListView.GetItemAt(e.X, e.Y);
            if (clickedItem is not null && !clickedItem.Selected)
            {
                resultsListView.SelectedItems.Clear();
                clickedItem.Selected = true;
                clickedItem.Focused = true;
            }
        }

        private IEnumerable<FileComparisonResult> ApplySort(IEnumerable<FileComparisonResult> results)
        {
            if (sortColumnIndex < 0)
            {
                return results;
            }

            IOrderedEnumerable<FileComparisonResult> orderedResults = sortColumnIndex switch
            {
                0 => OrderByValue(results, GetStatusText, StringComparer.OrdinalIgnoreCase),
                1 => OrderByValue(results, result => result.RelativePath, StringComparer.OrdinalIgnoreCase),
                2 => OrderByValue(results, result => result.FileA?.FullPath ?? string.Empty, StringComparer.OrdinalIgnoreCase),
                3 => OrderByValue(results, result => result.FileB?.FullPath ?? string.Empty, StringComparer.OrdinalIgnoreCase),
                4 => OrderByValue(results, result => result.FileA?.Size),
                5 => OrderByValue(results, result => result.FileB?.Size),
                6 => OrderByValue(results, result => result.FileA?.LastWriteTime),
                7 => OrderByValue(results, result => result.FileB?.LastWriteTime),
                _ => OrderByValue(results, result => result.RelativePath, StringComparer.OrdinalIgnoreCase)
            };

            return orderedResults.ThenBy(result => result.RelativePath, StringComparer.OrdinalIgnoreCase);
        }

        private IOrderedEnumerable<FileComparisonResult> OrderByValue<TKey>(
            IEnumerable<FileComparisonResult> results,
            Func<FileComparisonResult, TKey> selector,
            IComparer<TKey>? comparer = null)
        {
            if (comparer is not null)
            {
                return sortAscending
                    ? results.OrderBy(selector, comparer)
                    : results.OrderByDescending(selector, comparer);
            }

            return sortAscending
                ? results.OrderBy(selector)
                : results.OrderByDescending(selector);
        }

        private void CaptureColumnHeaderTexts()
        {
            columnHeaderTexts.Clear();

            foreach (ColumnHeader columnHeader in resultsListView.Columns)
            {
                columnHeaderTexts[columnHeader] = columnHeader.Text;
            }
        }

        private void UpdateSortIndicators()
        {
            foreach (ColumnHeader columnHeader in resultsListView.Columns)
            {
                string headerText = columnHeaderTexts.TryGetValue(columnHeader, out string? text)
                    ? text
                    : columnHeader.Text;

                columnHeader.Text = columnHeader.Index == sortColumnIndex
                    ? $"{headerText} {(sortAscending ? "↑" : "↓")}"
                    : headerText;
            }
        }

        private static string GetStatusText(FileComparisonResult result)
        {
            return result.Status switch
            {
                FileComparisonStatus.MissingInA => "Нет в A",
                FileComparisonStatus.MissingInB => "Нет в B",
                FileComparisonStatus.Different => "Различается",
                FileComparisonStatus.Same => "Совпадает",
                FileComparisonStatus.ReadError => "Ошибка чтения",
                _ => "Неизвестно"
            };
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

        private static string FormatSize(long? size)
        {
            if (size is null)
            {
                return "-";
            }

            string[] units = { "Б", "КБ", "МБ", "ГБ", "ТБ" };
            double value = size.Value;
            int unitIndex = 0;

            while (value >= 1024 && unitIndex < units.Length - 1)
            {
                value /= 1024;
                unitIndex++;
            }

            return unitIndex == 0
                ? $"{value:N0} {units[unitIndex]}"
                : $"{value:N1} {units[unitIndex]}";
        }

        private static string FormatDate(DateTime? date)
        {
            return date?.ToString("dd.MM.yyyy HH:mm") ?? "-";
        }

        private void ResetSummary()
        {
            totalCountLabel.Text = "0";
            missingCountLabel.Text = "0";
            differentCountLabel.Text = "0";
            sameCountLabel.Text = "0";
        }

        private void ResizeResultColumns()
        {
            int clientWidth = resultsListView.ClientSize.Width;
            if (clientWidth <= 0)
            {
                return;
            }

            statusColumnHeader.Width = 140;
            sizeAColumnHeader.Width = 95;
            sizeBColumnHeader.Width = 95;
            modifiedAColumnHeader.Width = 145;
            modifiedBColumnHeader.Width = 145;

            int fixedWidth = statusColumnHeader.Width
                + sizeAColumnHeader.Width
                + sizeBColumnHeader.Width
                + modifiedAColumnHeader.Width
                + modifiedBColumnHeader.Width
                + 24;

            int flexibleWidth = Math.Max(780, clientWidth - fixedWidth);
            relativePathColumnHeader.Width = Math.Max(240, flexibleWidth / 3);
            folderAColumnHeader.Width = Math.Max(280, (flexibleWidth - relativePathColumnHeader.Width) / 2);
            folderBColumnHeader.Width = Math.Max(280, flexibleWidth - relativePathColumnHeader.Width - folderAColumnHeader.Width);
        }

        private void ConfigureResultsContextMenu()
        {
            ContextMenuStrip menu = new(components);
            ToolStripMenuItem copySelectedRowsItem = new("Копировать выбранные ячейки");
            ToolStripMenuItem copyRelativePathItem = new("Копировать относительный путь");
            ToolStripMenuItem copyFolderAPathItem = new("Копировать путь A");
            ToolStripMenuItem copyFolderBPathItem = new("Копировать путь B");

            copySelectedRowsItem.Click += (_, _) => CopySelectedRows();
            copyRelativePathItem.Click += (_, _) => CopySelectedPath(PathKind.Relative);
            copyFolderAPathItem.Click += (_, _) => CopySelectedPath(PathKind.FolderA);
            copyFolderBPathItem.Click += (_, _) => CopySelectedPath(PathKind.FolderB);

            menu.Items.Add(copySelectedRowsItem);
            menu.Items.Add(new ToolStripSeparator());
            menu.Items.Add(copyRelativePathItem);
            menu.Items.Add(copyFolderAPathItem);
            menu.Items.Add(copyFolderBPathItem);
            menu.Opening += (_, e) =>
            {
                FileComparisonResult? result = GetSelectedResult();
                e.Cancel = resultsListView.SelectedItems.Count == 0;

                if (result is not null)
                {
                    copyFolderAPathItem.Enabled = result.FileA is not null;
                    copyFolderBPathItem.Enabled = result.FileB is not null;
                }
            };

            resultsListView.ContextMenuStrip = menu;
        }

        private FileComparisonResult? GetSelectedResult()
        {
            return resultsListView.SelectedItems.Count == 0
                ? null
                : resultsListView.SelectedItems[0].Tag as FileComparisonResult;
        }

        private void CopySelectedPath(PathKind pathKind)
        {
            FileComparisonResult? result = GetSelectedResult();
            if (result is null)
            {
                return;
            }

            string? path = pathKind switch
            {
                PathKind.Relative => result.RelativePath,
                PathKind.FolderA => result.FileA?.FullPath,
                PathKind.FolderB => result.FileB?.FullPath,
                _ => null
            };

            if (!string.IsNullOrWhiteSpace(path))
            {
                Clipboard.SetText(path);
                statusLabel.Text = "Путь скопирован в буфер обмена.";
            }
        }

        private void CopySelectedRows()
        {
            if (resultsListView.SelectedItems.Count == 0)
            {
                return;
            }

            List<ListViewItem> selectedItems = resultsListView.SelectedItems
                .Cast<ListViewItem>()
                .OrderBy(item => item.Index)
                .ToList();

            List<string> lines = new(selectedItems.Count + 1)
            {
                string.Join('\t', resultsListView.Columns
                    .Cast<ColumnHeader>()
                    .Select(columnHeader => FormatClipboardCell(GetBaseColumnHeaderText(columnHeader))))
            };

            foreach (ListViewItem item in selectedItems)
            {
                IEnumerable<string> values = item.SubItems
                    .Cast<ListViewItem.ListViewSubItem>()
                    .Select(subItem => FormatClipboardCell(subItem.Text));

                lines.Add(string.Join('\t', values));
            }

            Clipboard.SetText(string.Join(Environment.NewLine, lines));
            statusLabel.Text = $"Скопировано строк: {selectedItems.Count:N0}.";
        }

        private string GetBaseColumnHeaderText(ColumnHeader columnHeader)
        {
            return columnHeaderTexts.TryGetValue(columnHeader, out string? text)
                ? text
                : columnHeader.Text;
        }

        private static string FormatClipboardCell(string value)
        {
            return value
                .Replace('\t', ' ')
                .Replace('\r', ' ')
                .Replace('\n', ' ');
        }

        private static string NormalizeDirectoryPath(string path)
        {
            return Path.TrimEndingDirectorySeparator(Path.GetFullPath(path.Trim()));
        }

        private static bool IsSameDirectory(string? firstPath, string? secondPath)
        {
            if (firstPath is null || secondPath is null)
            {
                return false;
            }

            return string.Equals(
                NormalizeDirectoryPath(firstPath),
                NormalizeDirectoryPath(secondPath),
                StringComparison.OrdinalIgnoreCase);
        }

        private enum PathKind
        {
            Relative,
            FolderA,
            FolderB
        }
    }
}
