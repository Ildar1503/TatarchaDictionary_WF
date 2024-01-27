namespace Dictionary
{
    partial class Form1
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.tatarchaFiltersComboBox = new System.Windows.Forms.ComboBox();
            this.clearFiltersButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.russianFiltersComboBox = new System.Windows.Forms.ComboBox();
            this.clearRTBButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.categoryComboBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tatarchaRichTextBox = new System.Windows.Forms.RichTextBox();
            this.russianRichTextBox = new System.Windows.Forms.RichTextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panelAddWord = new System.Windows.Forms.Panel();
            this.workWithDataBaseButton = new System.Windows.Forms.Button();
            this.wordAddButtonTip = new System.Windows.Forms.Button();
            this.searchPanel = new System.Windows.Forms.Panel();
            this.wordBackButton = new System.Windows.Forms.Button();
            this.tatarchaLettersComboBox = new System.Windows.Forms.ComboBox();
            this.textBoxForSearchingWord = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchButtonTip = new System.Windows.Forms.Button();
            this.gameAndAlfabetPanel = new System.Windows.Forms.Panel();
            this.alfabetButton = new System.Windows.Forms.Button();
            this.gameAndAlfabetButtonTip = new System.Windows.Forms.Button();
            this.gameButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.менюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.татарскийАлфавитToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panelAddWord.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.gameAndAlfabetPanel.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.tatarchaFiltersComboBox);
            this.panel1.Controls.Add(this.clearFiltersButton);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.russianFiltersComboBox);
            this.panel1.Controls.Add(this.clearRTBButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.categoryComboBox);
            this.panel1.Location = new System.Drawing.Point(15, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(362, 245);
            this.panel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 20);
            this.label3.TabIndex = 7;
            this.label3.Text = "Фильтры для татарских слов";
            // 
            // tatarchaFiltersComboBox
            // 
            this.tatarchaFiltersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tatarchaFiltersComboBox.FormattingEnabled = true;
            this.tatarchaFiltersComboBox.Items.AddRange(new object[] {
            "Без фильтров",
            "В алфавитном порядке",
            "По возрастанию длины слова",
            "По убыванию длины слова",
            "По количеству переводов"});
            this.tatarchaFiltersComboBox.Location = new System.Drawing.Point(20, 131);
            this.tatarchaFiltersComboBox.Name = "tatarchaFiltersComboBox";
            this.tatarchaFiltersComboBox.Size = new System.Drawing.Size(320, 28);
            this.tatarchaFiltersComboBox.TabIndex = 6;
            this.tatarchaFiltersComboBox.SelectedIndexChanged += new System.EventHandler(this.tatarchaFiltersComboBox_SelectedIndexChanged);
            // 
            // clearFiltersButton
            // 
            this.clearFiltersButton.Location = new System.Drawing.Point(20, 165);
            this.clearFiltersButton.Name = "clearFiltersButton";
            this.clearFiltersButton.Size = new System.Drawing.Size(320, 29);
            this.clearFiltersButton.TabIndex = 5;
            this.clearFiltersButton.Text = "Очистка фильтров";
            this.clearFiltersButton.UseVisualStyleBackColor = true;
            this.clearFiltersButton.Click += new System.EventHandler(this.clearFiltersButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(192, 20);
            this.label2.TabIndex = 4;
            this.label2.Text = "Фильтры для русских слов";
            // 
            // russianFiltersComboBox
            // 
            this.russianFiltersComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.russianFiltersComboBox.FormattingEnabled = true;
            this.russianFiltersComboBox.Items.AddRange(new object[] {
            "Без фильтров",
            "В алфавитном порядке",
            "По возрастанию длины слова",
            "По убыванию длины слова"});
            this.russianFiltersComboBox.Location = new System.Drawing.Point(20, 77);
            this.russianFiltersComboBox.Name = "russianFiltersComboBox";
            this.russianFiltersComboBox.Size = new System.Drawing.Size(320, 28);
            this.russianFiltersComboBox.TabIndex = 3;
            this.russianFiltersComboBox.SelectedIndexChanged += new System.EventHandler(this.russianFiltersComboBox_SelectedIndexChanged);
            // 
            // clearRTBButton
            // 
            this.clearRTBButton.Location = new System.Drawing.Point(20, 209);
            this.clearRTBButton.Name = "clearRTBButton";
            this.clearRTBButton.Size = new System.Drawing.Size(320, 29);
            this.clearRTBButton.TabIndex = 2;
            this.clearRTBButton.Text = "Очистка";
            this.clearRTBButton.UseVisualStyleBackColor = true;
            this.clearRTBButton.Click += new System.EventHandler(this.clearRTBButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Категория слов";
            // 
            // categoryComboBox
            // 
            this.categoryComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryComboBox.FormattingEnabled = true;
            this.categoryComboBox.Items.AddRange(new object[] {
            "Существительные",
            "Глаголы ",
            "Прилагательные",
            "Категория не выбрана"});
            this.categoryComboBox.Location = new System.Drawing.Point(20, 23);
            this.categoryComboBox.Name = "categoryComboBox";
            this.categoryComboBox.Size = new System.Drawing.Size(320, 28);
            this.categoryComboBox.TabIndex = 0;
            this.categoryComboBox.SelectedIndexChanged += new System.EventHandler(this.categoryComboBox_SelectedIndexChanged);
            this.categoryComboBox.Click += new System.EventHandler(this.categoryComboBox_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.tatarchaRichTextBox);
            this.panel2.Controls.Add(this.russianRichTextBox);
            this.panel2.Location = new System.Drawing.Point(383, 27);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(568, 411);
            this.panel2.TabIndex = 1;
            // 
            // tatarchaRichTextBox
            // 
            this.tatarchaRichTextBox.Location = new System.Drawing.Point(199, 3);
            this.tatarchaRichTextBox.Name = "tatarchaRichTextBox";
            this.tatarchaRichTextBox.ReadOnly = true;
            this.tatarchaRichTextBox.Size = new System.Drawing.Size(362, 418);
            this.tatarchaRichTextBox.TabIndex = 1;
            this.tatarchaRichTextBox.Text = "";
            // 
            // russianRichTextBox
            // 
            this.russianRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.russianRichTextBox.Name = "russianRichTextBox";
            this.russianRichTextBox.ReadOnly = true;
            this.russianRichTextBox.Size = new System.Drawing.Size(190, 418);
            this.russianRichTextBox.TabIndex = 0;
            this.russianRichTextBox.Text = "";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.panelAddWord);
            this.panel3.Controls.Add(this.searchPanel);
            this.panel3.Controls.Add(this.gameAndAlfabetPanel);
            this.panel3.Location = new System.Drawing.Point(15, 281);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(362, 157);
            this.panel3.TabIndex = 1;
            // 
            // panelAddWord
            // 
            this.panelAddWord.BackColor = System.Drawing.Color.MintCream;
            this.panelAddWord.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelAddWord.Controls.Add(this.workWithDataBaseButton);
            this.panelAddWord.Controls.Add(this.wordAddButtonTip);
            this.panelAddWord.Location = new System.Drawing.Point(242, 3);
            this.panelAddWord.Name = "panelAddWord";
            this.panelAddWord.Size = new System.Drawing.Size(113, 147);
            this.panelAddWord.TabIndex = 4;
            // 
            // workWithDataBaseButton
            // 
            this.workWithDataBaseButton.Location = new System.Drawing.Point(3, 40);
            this.workWithDataBaseButton.Name = "workWithDataBaseButton";
            this.workWithDataBaseButton.Size = new System.Drawing.Size(103, 100);
            this.workWithDataBaseButton.TabIndex = 3;
            this.workWithDataBaseButton.Text = "Добавление, удаление, обновление слов в базе";
            this.workWithDataBaseButton.UseVisualStyleBackColor = true;
            this.workWithDataBaseButton.Click += new System.EventHandler(this.workWithDataBaseButton_Click);
            // 
            // wordAddButtonTip
            // 
            this.wordAddButtonTip.Location = new System.Drawing.Point(3, 3);
            this.wordAddButtonTip.Name = "wordAddButtonTip";
            this.wordAddButtonTip.Size = new System.Drawing.Size(30, 30);
            this.wordAddButtonTip.TabIndex = 0;
            this.wordAddButtonTip.Text = "💡";
            this.wordAddButtonTip.UseVisualStyleBackColor = true;
            this.wordAddButtonTip.Click += new System.EventHandler(this.wordAddButtonTip_Click);
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.MintCream;
            this.searchPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.searchPanel.Controls.Add(this.wordBackButton);
            this.searchPanel.Controls.Add(this.tatarchaLettersComboBox);
            this.searchPanel.Controls.Add(this.textBoxForSearchingWord);
            this.searchPanel.Controls.Add(this.searchButton);
            this.searchPanel.Controls.Add(this.searchButtonTip);
            this.searchPanel.Location = new System.Drawing.Point(123, 3);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(113, 147);
            this.searchPanel.TabIndex = 3;
            // 
            // wordBackButton
            // 
            this.wordBackButton.Location = new System.Drawing.Point(76, 3);
            this.wordBackButton.Name = "wordBackButton";
            this.wordBackButton.Size = new System.Drawing.Size(30, 30);
            this.wordBackButton.TabIndex = 3;
            this.wordBackButton.Text = "🔙";
            this.wordBackButton.UseVisualStyleBackColor = true;
            this.wordBackButton.Click += new System.EventHandler(this.wordBackButton_Click);
            // 
            // tatarchaLettersComboBox
            // 
            this.tatarchaLettersComboBox.FormattingEnabled = true;
            this.tatarchaLettersComboBox.Items.AddRange(new object[] {
            "",
            "Ә",
            "ә",
            "Җ",
            "җ",
            "Ң",
            "ң",
            "Ө",
            "ө",
            "Ү",
            "ү",
            "Һ",
            "һ"});
            this.tatarchaLettersComboBox.Location = new System.Drawing.Point(3, 38);
            this.tatarchaLettersComboBox.Name = "tatarchaLettersComboBox";
            this.tatarchaLettersComboBox.Size = new System.Drawing.Size(103, 28);
            this.tatarchaLettersComboBox.TabIndex = 2;
            this.tatarchaLettersComboBox.SelectedIndexChanged += new System.EventHandler(this.tatarchaLettersComboBox_SelectedIndexChanged);
            // 
            // textBoxForSearchingWord
            // 
            this.textBoxForSearchingWord.Location = new System.Drawing.Point(3, 72);
            this.textBoxForSearchingWord.Name = "textBoxForSearchingWord";
            this.textBoxForSearchingWord.Size = new System.Drawing.Size(103, 27);
            this.textBoxForSearchingWord.TabIndex = 2;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(3, 105);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(103, 35);
            this.searchButton.TabIndex = 1;
            this.searchButton.Text = "Найти";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchButtonTip
            // 
            this.searchButtonTip.Location = new System.Drawing.Point(3, 3);
            this.searchButtonTip.Name = "searchButtonTip";
            this.searchButtonTip.Size = new System.Drawing.Size(30, 30);
            this.searchButtonTip.TabIndex = 0;
            this.searchButtonTip.Text = "💡";
            this.searchButtonTip.UseVisualStyleBackColor = true;
            this.searchButtonTip.Click += new System.EventHandler(this.searchButtonTip_Click);
            // 
            // gameAndAlfabetPanel
            // 
            this.gameAndAlfabetPanel.BackColor = System.Drawing.Color.MintCream;
            this.gameAndAlfabetPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gameAndAlfabetPanel.Controls.Add(this.alfabetButton);
            this.gameAndAlfabetPanel.Controls.Add(this.gameAndAlfabetButtonTip);
            this.gameAndAlfabetPanel.Controls.Add(this.gameButton);
            this.gameAndAlfabetPanel.Location = new System.Drawing.Point(3, 3);
            this.gameAndAlfabetPanel.Name = "gameAndAlfabetPanel";
            this.gameAndAlfabetPanel.Size = new System.Drawing.Size(113, 147);
            this.gameAndAlfabetPanel.TabIndex = 2;
            // 
            // alfabetButton
            // 
            this.alfabetButton.Location = new System.Drawing.Point(3, 68);
            this.alfabetButton.Name = "alfabetButton";
            this.alfabetButton.Size = new System.Drawing.Size(103, 35);
            this.alfabetButton.TabIndex = 3;
            this.alfabetButton.Text = "Алфавит";
            this.alfabetButton.UseVisualStyleBackColor = true;
            this.alfabetButton.Click += new System.EventHandler(this.alfabetButton_Click);
            // 
            // gameAndAlfabetButtonTip
            // 
            this.gameAndAlfabetButtonTip.Location = new System.Drawing.Point(3, 3);
            this.gameAndAlfabetButtonTip.Name = "gameAndAlfabetButtonTip";
            this.gameAndAlfabetButtonTip.Size = new System.Drawing.Size(30, 30);
            this.gameAndAlfabetButtonTip.TabIndex = 1;
            this.gameAndAlfabetButtonTip.Text = "💡";
            this.gameAndAlfabetButtonTip.UseVisualStyleBackColor = true;
            this.gameAndAlfabetButtonTip.Click += new System.EventHandler(this.gameAndAlfabetButtonTip_Click);
            // 
            // gameButton
            // 
            this.gameButton.Location = new System.Drawing.Point(3, 105);
            this.gameButton.Name = "gameButton";
            this.gameButton.Size = new System.Drawing.Size(103, 35);
            this.gameButton.TabIndex = 2;
            this.gameButton.Text = "Играть";
            this.gameButton.UseVisualStyleBackColor = true;
            this.gameButton.Click += new System.EventHandler(this.gameButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.менюToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(963, 28);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // менюToolStripMenuItem
            // 
            this.менюToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.татарскийАлфавитToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.менюToolStripMenuItem.Name = "менюToolStripMenuItem";
            this.менюToolStripMenuItem.Size = new System.Drawing.Size(65, 24);
            this.менюToolStripMenuItem.Text = "Меню";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // татарскийАлфавитToolStripMenuItem
            // 
            this.татарскийАлфавитToolStripMenuItem.Name = "татарскийАлфавитToolStripMenuItem";
            this.татарскийАлфавитToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.татарскийАлфавитToolStripMenuItem.Text = "Татарский алфавит";
            this.татарскийАлфавитToolStripMenuItem.Click += new System.EventHandler(this.татарскийАлфавитToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(963, 448);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Словарь татарского языка";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panelAddWord.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.gameAndAlfabetPanel.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panel1;
        private Panel panel2;
        private RichTextBox tatarchaRichTextBox;
        private RichTextBox russianRichTextBox;
        private Button clearRTBButton;
        private Label label1;
        private ComboBox categoryComboBox;
        private Panel panel3;
        private Label label2;
        private ComboBox russianFiltersComboBox;
        private Button clearFiltersButton;
        private Label label3;
        private ComboBox tatarchaFiltersComboBox;
        private Panel searchPanel;
        private Panel gameAndAlfabetPanel;
        private Button gameAndAlfabetButtonTip;
        private Button searchButtonTip;
        private Panel panelAddWord;
        private Button wordAddButtonTip;
        private Button searchButton;
        private Button workWithDataBaseButton;
        private Button gameButton;
        private TextBox textBoxForSearchingWord;
        private Button alfabetButton;
        private ComboBox tatarchaLettersComboBox;
        private Button wordBackButton;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem менюToolStripMenuItem;
        private ToolStripMenuItem оПрограммеToolStripMenuItem;
        private ToolStripMenuItem татарскийАлфавитToolStripMenuItem;
        private ToolStripMenuItem выходToolStripMenuItem;
    }
}