namespace FileMerger
{
    partial class FileComparisonForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FileComparisonForm));
            toolTip = new ToolTip(components);
            mainLayout = new TableLayoutPanel();
            headerPanel = new Panel();
            subtitleLabel = new Label();
            titleLabel = new Label();
            folderLayout = new TableLayoutPanel();
            folderAPanel = new Panel();
            clearAButton = new Button();
            browseAButton = new Button();
            folderAPathLabel = new Label();
            folderAHintLabel = new Label();
            folderATitleLabel = new Label();
            folderBPanel = new Panel();
            clearBButton = new Button();
            browseBButton = new Button();
            folderBPathLabel = new Label();
            folderBHintLabel = new Label();
            folderBTitleLabel = new Label();
            commandPanel = new Panel();
            statusLabel = new Label();
            compareButton = new Button();
            verifyMd5CheckBox = new CheckBox();
            filterPanel = new FlowLayoutPanel();
            showMissingCheckBox = new CheckBox();
            showDifferentCheckBox = new CheckBox();
            showAllCheckBox = new CheckBox();
            searchPanel = new Panel();
            searchTextBox = new TextBox();
            searchLabel = new Label();
            summaryLayout = new TableLayoutPanel();
            totalPanel = new Panel();
            totalCountLabel = new Label();
            totalTitleLabel = new Label();
            missingPanel = new Panel();
            missingCountLabel = new Label();
            missingTitleLabel = new Label();
            differentPanel = new Panel();
            differentCountLabel = new Label();
            differentTitleLabel = new Label();
            samePanel = new Panel();
            sameCountLabel = new Label();
            sameTitleLabel = new Label();
            resultsListView = new ListView();
            statusColumnHeader = new ColumnHeader();
            relativePathColumnHeader = new ColumnHeader();
            folderAColumnHeader = new ColumnHeader();
            folderBColumnHeader = new ColumnHeader();
            sizeAColumnHeader = new ColumnHeader();
            sizeBColumnHeader = new ColumnHeader();
            modifiedAColumnHeader = new ColumnHeader();
            modifiedBColumnHeader = new ColumnHeader();
            mainLayout.SuspendLayout();
            headerPanel.SuspendLayout();
            folderLayout.SuspendLayout();
            folderAPanel.SuspendLayout();
            folderBPanel.SuspendLayout();
            commandPanel.SuspendLayout();
            filterPanel.SuspendLayout();
            searchPanel.SuspendLayout();
            summaryLayout.SuspendLayout();
            totalPanel.SuspendLayout();
            missingPanel.SuspendLayout();
            differentPanel.SuspendLayout();
            samePanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainLayout
            // 
            mainLayout.BackColor = Color.FromArgb(246, 248, 251);
            mainLayout.ColumnCount = 1;
            mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            mainLayout.Controls.Add(headerPanel, 0, 0);
            mainLayout.Controls.Add(folderLayout, 0, 1);
            mainLayout.Controls.Add(commandPanel, 0, 2);
            mainLayout.Controls.Add(summaryLayout, 0, 3);
            mainLayout.Controls.Add(resultsListView, 0, 4);
            mainLayout.Dock = DockStyle.Fill;
            mainLayout.Location = new Point(0, 0);
            mainLayout.Name = "mainLayout";
            mainLayout.Padding = new Padding(22, 20, 22, 22);
            mainLayout.RowCount = 5;
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 72F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 156F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 74F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 84F));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            mainLayout.Size = new Size(1184, 761);
            mainLayout.TabIndex = 0;
            // 
            // headerPanel
            // 
            headerPanel.Controls.Add(subtitleLabel);
            headerPanel.Controls.Add(titleLabel);
            headerPanel.Dock = DockStyle.Fill;
            headerPanel.Location = new Point(25, 23);
            headerPanel.Name = "headerPanel";
            headerPanel.Size = new Size(1134, 66);
            headerPanel.TabIndex = 0;
            // 
            // subtitleLabel
            // 
            subtitleLabel.AutoSize = true;
            subtitleLabel.Font = new Font("Segoe UI", 10.5F);
            subtitleLabel.ForeColor = Color.FromArgb(89, 99, 116);
            subtitleLabel.Location = new Point(3, 39);
            subtitleLabel.Name = "subtitleLabel";
            subtitleLabel.Size = new Size(465, 19);
            subtitleLabel.TabIndex = 1;
            subtitleLabel.Text = "Перетащите две папки, запустите сравнение и отфильтруйте результат.";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI Semibold", 18F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(28, 35, 45);
            titleLabel.Location = new Point(0, 0);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(229, 32);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Сравнение файлов";
            // 
            // folderLayout
            // 
            folderLayout.ColumnCount = 2;
            folderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            folderLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            folderLayout.Controls.Add(folderAPanel, 0, 0);
            folderLayout.Controls.Add(folderBPanel, 1, 0);
            folderLayout.Dock = DockStyle.Fill;
            folderLayout.Location = new Point(22, 92);
            folderLayout.Margin = new Padding(0);
            folderLayout.Name = "folderLayout";
            folderLayout.RowCount = 1;
            folderLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            folderLayout.Size = new Size(1140, 156);
            folderLayout.TabIndex = 1;
            // 
            // folderAPanel
            // 
            folderAPanel.AllowDrop = true;
            folderAPanel.BackColor = Color.White;
            folderAPanel.BorderStyle = BorderStyle.FixedSingle;
            folderAPanel.Controls.Add(clearAButton);
            folderAPanel.Controls.Add(browseAButton);
            folderAPanel.Controls.Add(folderAPathLabel);
            folderAPanel.Controls.Add(folderAHintLabel);
            folderAPanel.Controls.Add(folderATitleLabel);
            folderAPanel.Dock = DockStyle.Fill;
            folderAPanel.Location = new Point(3, 3);
            folderAPanel.Name = "folderAPanel";
            folderAPanel.Padding = new Padding(18);
            folderAPanel.Size = new Size(564, 150);
            folderAPanel.TabIndex = 0;
            // 
            // clearAButton
            // 
            clearAButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            clearAButton.BackColor = Color.White;
            clearAButton.FlatAppearance.BorderColor = Color.FromArgb(210, 216, 226);
            clearAButton.FlatStyle = FlatStyle.Flat;
            clearAButton.Font = new Font("Segoe UI", 9F);
            clearAButton.ForeColor = Color.FromArgb(75, 85, 99);
            clearAButton.Location = new Point(476, 17);
            clearAButton.Name = "clearAButton";
            clearAButton.Size = new Size(68, 30);
            clearAButton.TabIndex = 4;
            clearAButton.Text = "Сброс";
            clearAButton.UseVisualStyleBackColor = false;
            // 
            // browseAButton
            // 
            browseAButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            browseAButton.BackColor = Color.FromArgb(238, 242, 248);
            browseAButton.FlatAppearance.BorderSize = 0;
            browseAButton.FlatStyle = FlatStyle.Flat;
            browseAButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            browseAButton.ForeColor = Color.FromArgb(35, 44, 58);
            browseAButton.Location = new Point(19, 99);
            browseAButton.Name = "browseAButton";
            browseAButton.Size = new Size(114, 32);
            browseAButton.TabIndex = 3;
            browseAButton.Text = "Выбрать";
            browseAButton.UseVisualStyleBackColor = false;
            // 
            // folderAPathLabel
            // 
            folderAPathLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            folderAPathLabel.AutoEllipsis = true;
            folderAPathLabel.Font = new Font("Segoe UI", 9.75F);
            folderAPathLabel.ForeColor = Color.FromArgb(49, 58, 73);
            folderAPathLabel.Location = new Point(19, 68);
            folderAPathLabel.Name = "folderAPathLabel";
            folderAPathLabel.Size = new Size(525, 22);
            folderAPathLabel.TabIndex = 2;
            folderAPathLabel.Text = "Папка не выбрана";
            // 
            // folderAHintLabel
            // 
            folderAHintLabel.AutoSize = true;
            folderAHintLabel.Font = new Font("Segoe UI", 9.75F);
            folderAHintLabel.ForeColor = Color.FromArgb(107, 119, 140);
            folderAHintLabel.Location = new Point(20, 42);
            folderAHintLabel.Name = "folderAHintLabel";
            folderAHintLabel.Size = new Size(296, 17);
            folderAHintLabel.TabIndex = 1;
            folderAHintLabel.Text = "Перетащите папку сюда или выберите вручную";
            // 
            // folderATitleLabel
            // 
            folderATitleLabel.AutoSize = true;
            folderATitleLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            folderATitleLabel.ForeColor = Color.FromArgb(28, 35, 45);
            folderATitleLabel.Location = new Point(18, 17);
            folderATitleLabel.Name = "folderATitleLabel";
            folderATitleLabel.Size = new Size(70, 21);
            folderATitleLabel.TabIndex = 0;
            folderATitleLabel.Text = "Папка A";
            // 
            // folderBPanel
            // 
            folderBPanel.AllowDrop = true;
            folderBPanel.BackColor = Color.White;
            folderBPanel.BorderStyle = BorderStyle.FixedSingle;
            folderBPanel.Controls.Add(clearBButton);
            folderBPanel.Controls.Add(browseBButton);
            folderBPanel.Controls.Add(folderBPathLabel);
            folderBPanel.Controls.Add(folderBHintLabel);
            folderBPanel.Controls.Add(folderBTitleLabel);
            folderBPanel.Dock = DockStyle.Fill;
            folderBPanel.Location = new Point(573, 3);
            folderBPanel.Name = "folderBPanel";
            folderBPanel.Padding = new Padding(18);
            folderBPanel.Size = new Size(564, 150);
            folderBPanel.TabIndex = 1;
            // 
            // clearBButton
            // 
            clearBButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            clearBButton.BackColor = Color.White;
            clearBButton.FlatAppearance.BorderColor = Color.FromArgb(210, 216, 226);
            clearBButton.FlatStyle = FlatStyle.Flat;
            clearBButton.Font = new Font("Segoe UI", 9F);
            clearBButton.ForeColor = Color.FromArgb(75, 85, 99);
            clearBButton.Location = new Point(476, 17);
            clearBButton.Name = "clearBButton";
            clearBButton.Size = new Size(68, 30);
            clearBButton.TabIndex = 4;
            clearBButton.Text = "Сброс";
            clearBButton.UseVisualStyleBackColor = false;
            // 
            // browseBButton
            // 
            browseBButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            browseBButton.BackColor = Color.FromArgb(238, 242, 248);
            browseBButton.FlatAppearance.BorderSize = 0;
            browseBButton.FlatStyle = FlatStyle.Flat;
            browseBButton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            browseBButton.ForeColor = Color.FromArgb(35, 44, 58);
            browseBButton.Location = new Point(19, 99);
            browseBButton.Name = "browseBButton";
            browseBButton.Size = new Size(114, 32);
            browseBButton.TabIndex = 3;
            browseBButton.Text = "Выбрать";
            browseBButton.UseVisualStyleBackColor = false;
            // 
            // folderBPathLabel
            // 
            folderBPathLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            folderBPathLabel.AutoEllipsis = true;
            folderBPathLabel.Font = new Font("Segoe UI", 9.75F);
            folderBPathLabel.ForeColor = Color.FromArgb(49, 58, 73);
            folderBPathLabel.Location = new Point(19, 68);
            folderBPathLabel.Name = "folderBPathLabel";
            folderBPathLabel.Size = new Size(525, 22);
            folderBPathLabel.TabIndex = 2;
            folderBPathLabel.Text = "Папка не выбрана";
            // 
            // folderBHintLabel
            // 
            folderBHintLabel.AutoSize = true;
            folderBHintLabel.Font = new Font("Segoe UI", 9.75F);
            folderBHintLabel.ForeColor = Color.FromArgb(107, 119, 140);
            folderBHintLabel.Location = new Point(20, 42);
            folderBHintLabel.Name = "folderBHintLabel";
            folderBHintLabel.Size = new Size(296, 17);
            folderBHintLabel.TabIndex = 1;
            folderBHintLabel.Text = "Перетащите папку сюда или выберите вручную";
            // 
            // folderBTitleLabel
            // 
            folderBTitleLabel.AutoSize = true;
            folderBTitleLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            folderBTitleLabel.ForeColor = Color.FromArgb(28, 35, 45);
            folderBTitleLabel.Location = new Point(18, 17);
            folderBTitleLabel.Name = "folderBTitleLabel";
            folderBTitleLabel.Size = new Size(69, 21);
            folderBTitleLabel.TabIndex = 0;
            folderBTitleLabel.Text = "Папка B";
            // 
            // commandPanel
            // 
            commandPanel.Controls.Add(statusLabel);
            commandPanel.Controls.Add(compareButton);
            commandPanel.Controls.Add(verifyMd5CheckBox);
            commandPanel.Controls.Add(filterPanel);
            commandPanel.Controls.Add(searchPanel);
            commandPanel.Dock = DockStyle.Fill;
            commandPanel.Location = new Point(25, 251);
            commandPanel.Name = "commandPanel";
            commandPanel.Size = new Size(1134, 68);
            commandPanel.TabIndex = 2;
            // 
            // statusLabel
            // 
            statusLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            statusLabel.AutoSize = true;
            statusLabel.Font = new Font("Segoe UI", 9.75F);
            statusLabel.ForeColor = Color.FromArgb(89, 99, 116);
            statusLabel.Location = new Point(135, 44);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(199, 17);
            statusLabel.TabIndex = 4;
            statusLabel.Text = "Выберите две папки для старта.";
            // 
            // compareButton
            // 
            compareButton.BackColor = Color.FromArgb(37, 99, 235);
            compareButton.FlatAppearance.BorderSize = 0;
            compareButton.FlatStyle = FlatStyle.Flat;
            compareButton.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            compareButton.ForeColor = Color.White;
            compareButton.Location = new Point(0, 3);
            compareButton.Name = "compareButton";
            compareButton.Size = new Size(174, 36);
            compareButton.TabIndex = 0;
            compareButton.Text = "Сравнить";
            compareButton.UseVisualStyleBackColor = false;
            //
            // verifyMd5CheckBox
            //
            verifyMd5CheckBox.AutoSize = true;
            verifyMd5CheckBox.Font = new Font("Segoe UI", 9.75F);
            verifyMd5CheckBox.ForeColor = Color.FromArgb(49, 58, 73);
            verifyMd5CheckBox.Location = new Point(0, 43);
            verifyMd5CheckBox.Name = "verifyMd5CheckBox";
            verifyMd5CheckBox.Size = new Size(116, 21);
            verifyMd5CheckBox.TabIndex = 3;
            verifyMd5CheckBox.Text = "Проверять MD5";
            verifyMd5CheckBox.UseVisualStyleBackColor = true;
            // 
            // filterPanel
            // 
            filterPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filterPanel.Controls.Add(showMissingCheckBox);
            filterPanel.Controls.Add(showDifferentCheckBox);
            filterPanel.Controls.Add(showAllCheckBox);
            filterPanel.Location = new Point(188, 3);
            filterPanel.Name = "filterPanel";
            filterPanel.Size = new Size(555, 38);
            filterPanel.TabIndex = 1;
            filterPanel.WrapContents = false;
            // 
            // showMissingCheckBox
            // 
            showMissingCheckBox.AutoSize = true;
            showMissingCheckBox.Checked = true;
            showMissingCheckBox.CheckState = CheckState.Checked;
            showMissingCheckBox.Font = new Font("Segoe UI", 9.75F);
            showMissingCheckBox.ForeColor = Color.FromArgb(49, 58, 73);
            showMissingCheckBox.Location = new Point(3, 8);
            showMissingCheckBox.Margin = new Padding(3, 8, 18, 3);
            showMissingCheckBox.Name = "showMissingCheckBox";
            showMissingCheckBox.Size = new Size(176, 21);
            showMissingCheckBox.TabIndex = 0;
            showMissingCheckBox.Text = "Показать отсутствующее";
            showMissingCheckBox.UseVisualStyleBackColor = true;
            // 
            // showDifferentCheckBox
            // 
            showDifferentCheckBox.AutoSize = true;
            showDifferentCheckBox.Checked = true;
            showDifferentCheckBox.CheckState = CheckState.Checked;
            showDifferentCheckBox.Font = new Font("Segoe UI", 9.75F);
            showDifferentCheckBox.ForeColor = Color.FromArgb(49, 58, 73);
            showDifferentCheckBox.Location = new Point(200, 8);
            showDifferentCheckBox.Margin = new Padding(3, 8, 18, 3);
            showDifferentCheckBox.Name = "showDifferentCheckBox";
            showDifferentCheckBox.Size = new Size(150, 21);
            showDifferentCheckBox.TabIndex = 1;
            showDifferentCheckBox.Text = "Показать различное";
            showDifferentCheckBox.UseVisualStyleBackColor = true;
            // 
            // showAllCheckBox
            // 
            showAllCheckBox.AutoSize = true;
            showAllCheckBox.Font = new Font("Segoe UI", 9.75F);
            showAllCheckBox.ForeColor = Color.FromArgb(49, 58, 73);
            showAllCheckBox.Location = new Point(371, 8);
            showAllCheckBox.Margin = new Padding(3, 8, 18, 3);
            showAllCheckBox.Name = "showAllCheckBox";
            showAllCheckBox.Size = new Size(106, 21);
            showAllCheckBox.TabIndex = 2;
            showAllCheckBox.Text = "Показать все";
            showAllCheckBox.UseVisualStyleBackColor = true;
            // 
            // searchPanel
            // 
            searchPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            searchPanel.Controls.Add(searchTextBox);
            searchPanel.Controls.Add(searchLabel);
            searchPanel.Location = new Point(786, 0);
            searchPanel.Name = "searchPanel";
            searchPanel.Size = new Size(348, 42);
            searchPanel.TabIndex = 2;
            // 
            // searchTextBox
            // 
            searchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            searchTextBox.BorderStyle = BorderStyle.FixedSingle;
            searchTextBox.Font = new Font("Segoe UI", 10F);
            searchTextBox.Location = new Point(58, 8);
            searchTextBox.Name = "searchTextBox";
            searchTextBox.PlaceholderText = "имя или путь";
            searchTextBox.Size = new Size(287, 25);
            searchTextBox.TabIndex = 1;
            // 
            // searchLabel
            // 
            searchLabel.AutoSize = true;
            searchLabel.Font = new Font("Segoe UI", 9.75F);
            searchLabel.ForeColor = Color.FromArgb(89, 99, 116);
            searchLabel.Location = new Point(3, 11);
            searchLabel.Name = "searchLabel";
            searchLabel.Size = new Size(44, 17);
            searchLabel.TabIndex = 0;
            searchLabel.Text = "Поиск";
            // 
            // summaryLayout
            // 
            summaryLayout.ColumnCount = 4;
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            summaryLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
            summaryLayout.Controls.Add(totalPanel, 0, 0);
            summaryLayout.Controls.Add(missingPanel, 1, 0);
            summaryLayout.Controls.Add(differentPanel, 2, 0);
            summaryLayout.Controls.Add(samePanel, 3, 0);
            summaryLayout.Dock = DockStyle.Fill;
            summaryLayout.Location = new Point(22, 322);
            summaryLayout.Margin = new Padding(0);
            summaryLayout.Name = "summaryLayout";
            summaryLayout.RowCount = 1;
            summaryLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            summaryLayout.Size = new Size(1140, 84);
            summaryLayout.TabIndex = 3;
            // 
            // totalPanel
            // 
            totalPanel.BackColor = Color.White;
            totalPanel.BorderStyle = BorderStyle.FixedSingle;
            totalPanel.Controls.Add(totalCountLabel);
            totalPanel.Controls.Add(totalTitleLabel);
            totalPanel.Dock = DockStyle.Fill;
            totalPanel.Location = new Point(3, 3);
            totalPanel.Name = "totalPanel";
            totalPanel.Padding = new Padding(16, 13, 16, 13);
            totalPanel.Size = new Size(279, 78);
            totalPanel.TabIndex = 0;
            // 
            // totalCountLabel
            // 
            totalCountLabel.AutoSize = true;
            totalCountLabel.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            totalCountLabel.ForeColor = Color.FromArgb(28, 35, 45);
            totalCountLabel.Location = new Point(14, 30);
            totalCountLabel.Name = "totalCountLabel";
            totalCountLabel.Size = new Size(32, 37);
            totalCountLabel.TabIndex = 1;
            totalCountLabel.Text = "0";
            // 
            // totalTitleLabel
            // 
            totalTitleLabel.AutoSize = true;
            totalTitleLabel.Font = new Font("Segoe UI", 9.75F);
            totalTitleLabel.ForeColor = Color.FromArgb(89, 99, 116);
            totalTitleLabel.Location = new Point(16, 12);
            totalTitleLabel.Name = "totalTitleLabel";
            totalTitleLabel.Size = new Size(77, 17);
            totalTitleLabel.TabIndex = 0;
            totalTitleLabel.Text = "Всего путей";
            // 
            // missingPanel
            // 
            missingPanel.BackColor = Color.White;
            missingPanel.BorderStyle = BorderStyle.FixedSingle;
            missingPanel.Controls.Add(missingCountLabel);
            missingPanel.Controls.Add(missingTitleLabel);
            missingPanel.Dock = DockStyle.Fill;
            missingPanel.Location = new Point(288, 3);
            missingPanel.Name = "missingPanel";
            missingPanel.Padding = new Padding(16, 13, 16, 13);
            missingPanel.Size = new Size(279, 78);
            missingPanel.TabIndex = 1;
            // 
            // missingCountLabel
            // 
            missingCountLabel.AutoSize = true;
            missingCountLabel.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            missingCountLabel.ForeColor = Color.FromArgb(190, 75, 29);
            missingCountLabel.Location = new Point(14, 30);
            missingCountLabel.Name = "missingCountLabel";
            missingCountLabel.Size = new Size(32, 37);
            missingCountLabel.TabIndex = 1;
            missingCountLabel.Text = "0";
            // 
            // missingTitleLabel
            // 
            missingTitleLabel.AutoSize = true;
            missingTitleLabel.Font = new Font("Segoe UI", 9.75F);
            missingTitleLabel.ForeColor = Color.FromArgb(89, 99, 116);
            missingTitleLabel.Location = new Point(16, 12);
            missingTitleLabel.Name = "missingTitleLabel";
            missingTitleLabel.Size = new Size(76, 17);
            missingTitleLabel.TabIndex = 0;
            missingTitleLabel.Text = "Отсутствует";
            // 
            // differentPanel
            // 
            differentPanel.BackColor = Color.White;
            differentPanel.BorderStyle = BorderStyle.FixedSingle;
            differentPanel.Controls.Add(differentCountLabel);
            differentPanel.Controls.Add(differentTitleLabel);
            differentPanel.Dock = DockStyle.Fill;
            differentPanel.Location = new Point(573, 3);
            differentPanel.Name = "differentPanel";
            differentPanel.Padding = new Padding(16, 13, 16, 13);
            differentPanel.Size = new Size(279, 78);
            differentPanel.TabIndex = 2;
            // 
            // differentCountLabel
            // 
            differentCountLabel.AutoSize = true;
            differentCountLabel.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            differentCountLabel.ForeColor = Color.FromArgb(180, 83, 9);
            differentCountLabel.Location = new Point(14, 30);
            differentCountLabel.Name = "differentCountLabel";
            differentCountLabel.Size = new Size(32, 37);
            differentCountLabel.TabIndex = 1;
            differentCountLabel.Text = "0";
            // 
            // differentTitleLabel
            // 
            differentTitleLabel.AutoSize = true;
            differentTitleLabel.Font = new Font("Segoe UI", 9.75F);
            differentTitleLabel.ForeColor = Color.FromArgb(89, 99, 116);
            differentTitleLabel.Location = new Point(16, 12);
            differentTitleLabel.Name = "differentTitleLabel";
            differentTitleLabel.Size = new Size(71, 17);
            differentTitleLabel.TabIndex = 0;
            differentTitleLabel.Text = "Различное";
            // 
            // samePanel
            // 
            samePanel.BackColor = Color.White;
            samePanel.BorderStyle = BorderStyle.FixedSingle;
            samePanel.Controls.Add(sameCountLabel);
            samePanel.Controls.Add(sameTitleLabel);
            samePanel.Dock = DockStyle.Fill;
            samePanel.Location = new Point(858, 3);
            samePanel.Name = "samePanel";
            samePanel.Padding = new Padding(16, 13, 16, 13);
            samePanel.Size = new Size(279, 78);
            samePanel.TabIndex = 3;
            // 
            // sameCountLabel
            // 
            sameCountLabel.AutoSize = true;
            sameCountLabel.Font = new Font("Segoe UI Semibold", 20F, FontStyle.Bold);
            sameCountLabel.ForeColor = Color.FromArgb(22, 128, 93);
            sameCountLabel.Location = new Point(14, 30);
            sameCountLabel.Name = "sameCountLabel";
            sameCountLabel.Size = new Size(32, 37);
            sameCountLabel.TabIndex = 1;
            sameCountLabel.Text = "0";
            // 
            // sameTitleLabel
            // 
            sameTitleLabel.AutoSize = true;
            sameTitleLabel.Font = new Font("Segoe UI", 9.75F);
            sameTitleLabel.ForeColor = Color.FromArgb(89, 99, 116);
            sameTitleLabel.Location = new Point(16, 12);
            sameTitleLabel.Name = "sameTitleLabel";
            sameTitleLabel.Size = new Size(71, 17);
            sameTitleLabel.TabIndex = 0;
            sameTitleLabel.Text = "Совпадает";
            // 
            // resultsListView
            // 
            resultsListView.BackColor = Color.White;
            resultsListView.BorderStyle = BorderStyle.FixedSingle;
            resultsListView.Columns.AddRange(new ColumnHeader[] { statusColumnHeader, relativePathColumnHeader, folderAColumnHeader, folderBColumnHeader, sizeAColumnHeader, sizeBColumnHeader, modifiedAColumnHeader, modifiedBColumnHeader });
            resultsListView.Dock = DockStyle.Fill;
            resultsListView.Font = new Font("Segoe UI", 9.75F);
            resultsListView.FullRowSelect = true;
            resultsListView.GridLines = true;
            resultsListView.Location = new Point(25, 409);
            resultsListView.MultiSelect = true;
            resultsListView.Name = "resultsListView";
            resultsListView.Size = new Size(1134, 327);
            resultsListView.TabIndex = 4;
            resultsListView.UseCompatibleStateImageBehavior = false;
            resultsListView.View = View.Details;
            // 
            // statusColumnHeader
            // 
            statusColumnHeader.Text = "Статус";
            statusColumnHeader.Width = 140;
            // 
            // relativePathColumnHeader
            // 
            relativePathColumnHeader.Text = "Относительный путь";
            relativePathColumnHeader.Width = 270;
            // 
            // folderAColumnHeader
            // 
            folderAColumnHeader.Text = "Полный путь в A";
            folderAColumnHeader.Width = 310;
            // 
            // folderBColumnHeader
            // 
            folderBColumnHeader.Text = "Полный путь в B";
            folderBColumnHeader.Width = 310;
            // 
            // sizeAColumnHeader
            // 
            sizeAColumnHeader.Text = "Размер A";
            sizeAColumnHeader.TextAlign = HorizontalAlignment.Right;
            sizeAColumnHeader.Width = 95;
            // 
            // sizeBColumnHeader
            // 
            sizeBColumnHeader.Text = "Размер B";
            sizeBColumnHeader.TextAlign = HorizontalAlignment.Right;
            sizeBColumnHeader.Width = 95;
            // 
            // modifiedAColumnHeader
            // 
            modifiedAColumnHeader.Text = "Изменен A";
            modifiedAColumnHeader.Width = 145;
            // 
            // modifiedBColumnHeader
            // 
            modifiedBColumnHeader.Text = "Изменен B";
            modifiedBColumnHeader.Width = 145;
            // 
            // FileComparisonForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(246, 248, 251);
            ClientSize = new Size(1184, 761);
            Controls.Add(mainLayout);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(1040, 680);
            Name = "FileComparisonForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FileMerger - сравнение файлов";
            mainLayout.ResumeLayout(false);
            headerPanel.ResumeLayout(false);
            headerPanel.PerformLayout();
            folderLayout.ResumeLayout(false);
            folderAPanel.ResumeLayout(false);
            folderAPanel.PerformLayout();
            folderBPanel.ResumeLayout(false);
            folderBPanel.PerformLayout();
            commandPanel.ResumeLayout(false);
            commandPanel.PerformLayout();
            filterPanel.ResumeLayout(false);
            filterPanel.PerformLayout();
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            summaryLayout.ResumeLayout(false);
            totalPanel.ResumeLayout(false);
            totalPanel.PerformLayout();
            missingPanel.ResumeLayout(false);
            missingPanel.PerformLayout();
            differentPanel.ResumeLayout(false);
            differentPanel.PerformLayout();
            samePanel.ResumeLayout(false);
            samePanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel mainLayout;
        private Panel headerPanel;
        private Label subtitleLabel;
        private Label titleLabel;
        private TableLayoutPanel folderLayout;
        private Panel folderAPanel;
        private Button clearAButton;
        private Button browseAButton;
        private Label folderAPathLabel;
        private Label folderAHintLabel;
        private Label folderATitleLabel;
        private Panel folderBPanel;
        private Button clearBButton;
        private Button browseBButton;
        private Label folderBPathLabel;
        private Label folderBHintLabel;
        private Label folderBTitleLabel;
        private Panel commandPanel;
        private Label statusLabel;
        private Button compareButton;
        private CheckBox verifyMd5CheckBox;
        private FlowLayoutPanel filterPanel;
        private CheckBox showMissingCheckBox;
        private CheckBox showDifferentCheckBox;
        private CheckBox showAllCheckBox;
        private Panel searchPanel;
        private TextBox searchTextBox;
        private Label searchLabel;
        private TableLayoutPanel summaryLayout;
        private Panel totalPanel;
        private Label totalCountLabel;
        private Label totalTitleLabel;
        private Panel missingPanel;
        private Label missingCountLabel;
        private Label missingTitleLabel;
        private Panel differentPanel;
        private Label differentCountLabel;
        private Label differentTitleLabel;
        private Panel samePanel;
        private Label sameCountLabel;
        private Label sameTitleLabel;
        private ListView resultsListView;
        private ColumnHeader statusColumnHeader;
        private ColumnHeader relativePathColumnHeader;
        private ColumnHeader folderAColumnHeader;
        private ColumnHeader folderBColumnHeader;
        private ColumnHeader sizeAColumnHeader;
        private ColumnHeader sizeBColumnHeader;
        private ColumnHeader modifiedAColumnHeader;
        private ColumnHeader modifiedBColumnHeader;
        private ToolTip toolTip;
    }
}
