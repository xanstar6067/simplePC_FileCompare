using FileMerger.Models;
using FileMerger.Services;
using FileMerger.UI;

namespace FileMerger
{
    public partial class FileComparisonForm : Form
    {
        private const string EmptyFolderText = "Папка не выбрана";

        private readonly IFileComparisonService comparisonService;
        private readonly FileComparisonListViewItemFactory resultItemFactory = new();
        private readonly List<FileComparisonResult> comparisonResults = new();
        private readonly Dictionary<ColumnHeader, string> columnHeaderTexts = new();

        private string? folderAPath;
        private string? folderBPath;
        private int sortColumnIndex = -1;
        private bool sortAscending = true;

        public FileComparisonForm()
            : this(new FileComparisonService())
        {
        }

        internal FileComparisonForm(IFileComparisonService comparisonService)
        {
            this.comparisonService = comparisonService;

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
            e.Effect = FolderDropData.ContainsFolder(e) ? DragDropEffects.Copy : DragDropEffects.None;
            SetDropZoneHighlight(isFolderA, e.Effect == DragDropEffects.Copy);
        }

        private void HandleDragDrop(DragEventArgs e, bool isFolderA)
        {
            SetDropZoneHighlight(isFolderA, false);

            string? directory = FolderDropData.GetDirectory(e);
            if (directory is null)
            {
                statusLabel.Text = "Перетащите именно папку, а не файл.";
                return;
            }

            SetFolder(isFolderA, directory);
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
                : DirectoryPathService.Normalize(path);

            string? oppositePath = isFolderA ? folderBPath : folderAPath;
            if (normalizedPath is not null && DirectoryPathService.AreSame(normalizedPath, oppositePath))
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

            if (DirectoryPathService.AreSame(folderAPath, folderBPath))
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
                IReadOnlyList<FileComparisonResult> results = await comparisonService.CompareAsync(pathA, pathB);

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

        private void RefreshResults()
        {
            ComparisonSummary summary = ComparisonSummaryService.Calculate(comparisonResults);

            totalCountLabel.Text = summary.TotalCount.ToString("N0");
            missingCountLabel.Text = summary.MissingCount.ToString("N0");
            differentCountLabel.Text = summary.DifferentCount.ToString("N0");
            sameCountLabel.Text = summary.SameCount.ToString("N0");

            ComparisonResultFilterOptions filterOptions = new(
                searchTextBox.Text,
                showMissingCheckBox.Checked,
                showDifferentCheckBox.Checked,
                showAllCheckBox.Checked);
            ComparisonSortOptions sortOptions = new(sortColumnIndex, sortAscending);

            List<FileComparisonResult> filteredResults = ComparisonResultSorter
                .Apply(ComparisonResultFilter.Apply(comparisonResults, filterOptions), sortOptions, FileComparisonDisplayText.GetStatusText)
                .ToList();

            resultsListView.BeginUpdate();
            resultsListView.Items.Clear();

            foreach (FileComparisonResult result in filteredResults)
            {
                resultsListView.Items.Add(resultItemFactory.Create(result));
            }

            resultsListView.EndUpdate();

            statusLabel.Text = comparisonResults.Count == 0
                ? "Результатов пока нет."
                : $"Показано {resultsListView.Items.Count:N0} из {comparisonResults.Count:N0}.";
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

        private enum PathKind
        {
            Relative,
            FolderA,
            FolderB
        }
    }
}
