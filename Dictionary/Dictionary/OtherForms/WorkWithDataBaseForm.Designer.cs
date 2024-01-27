namespace Dictionary.OtherForms
{
    partial class WorkWithDataBaseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.tatarchaLettersComboBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tatarchaTextBox = new System.Windows.Forms.TextBox();
            this.deleteButton = new System.Windows.Forms.Button();
            this.updateButton = new System.Windows.Forms.Button();
            this.russianTextBox = new System.Windows.Forms.TextBox();
            this.wordAddButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.wordCategoryComboBox = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.wordsRichTextBox = new System.Windows.Forms.RichTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.tatarchaLettersComboBox);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.tatarchaTextBox);
            this.panel1.Controls.Add(this.deleteButton);
            this.panel1.Controls.Add(this.updateButton);
            this.panel1.Controls.Add(this.russianTextBox);
            this.panel1.Controls.Add(this.wordAddButton);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.wordCategoryComboBox);
            this.panel1.Location = new System.Drawing.Point(5, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(211, 429);
            this.panel1.TabIndex = 4;
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
            this.tatarchaLettersComboBox.Location = new System.Drawing.Point(3, 142);
            this.tatarchaLettersComboBox.Name = "tatarchaLettersComboBox";
            this.tatarchaLettersComboBox.Size = new System.Drawing.Size(201, 28);
            this.tatarchaLettersComboBox.TabIndex = 21;
            this.tatarchaLettersComboBox.SelectedIndexChanged += new System.EventHandler(this.tatarchaLettersComboBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 173);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 20);
            this.label3.TabIndex = 20;
            this.label3.Text = "Поле для татрского слова";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(182, 20);
            this.label2.TabIndex = 19;
            this.label2.Text = "Поле для русского слова";
            // 
            // tatarchaTextBox
            // 
            this.tatarchaTextBox.Location = new System.Drawing.Point(3, 196);
            this.tatarchaTextBox.Name = "tatarchaTextBox";
            this.tatarchaTextBox.Size = new System.Drawing.Size(201, 27);
            this.tatarchaTextBox.TabIndex = 18;
            this.tatarchaTextBox.TextChanged += new System.EventHandler(this.tatarchaTextBox_TextChanged);
            // 
            // deleteButton
            // 
            this.deleteButton.Location = new System.Drawing.Point(3, 397);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(201, 29);
            this.deleteButton.TabIndex = 17;
            this.deleteButton.Text = "Удалить ";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.deleteButton_Click);
            // 
            // updateButton
            // 
            this.updateButton.Location = new System.Drawing.Point(3, 362);
            this.updateButton.Name = "updateButton";
            this.updateButton.Size = new System.Drawing.Size(201, 29);
            this.updateButton.TabIndex = 16;
            this.updateButton.Text = "Обновить ";
            this.updateButton.UseVisualStyleBackColor = true;
            this.updateButton.Click += new System.EventHandler(this.updateButton_Click);
            // 
            // russianTextBox
            // 
            this.russianTextBox.Location = new System.Drawing.Point(3, 109);
            this.russianTextBox.Name = "russianTextBox";
            this.russianTextBox.Size = new System.Drawing.Size(201, 27);
            this.russianTextBox.TabIndex = 15;
            this.russianTextBox.TextChanged += new System.EventHandler(this.russianTextBox_TextChanged);
            // 
            // wordAddButton
            // 
            this.wordAddButton.Location = new System.Drawing.Point(3, 327);
            this.wordAddButton.Name = "wordAddButton";
            this.wordAddButton.Size = new System.Drawing.Size(201, 29);
            this.wordAddButton.TabIndex = 14;
            this.wordAddButton.Text = "Добавить";
            this.wordAddButton.UseVisualStyleBackColor = true;
            this.wordAddButton.Click += new System.EventHandler(this.wordAddButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(128, 20);
            this.label1.TabIndex = 13;
            this.label1.Text = "Категория слова.";
            // 
            // wordCategoryComboBox
            // 
            this.wordCategoryComboBox.FormattingEnabled = true;
            this.wordCategoryComboBox.Items.AddRange(new object[] {
            "",
            "Существительные",
            "Глаголы ",
            "Прилагательные"});
            this.wordCategoryComboBox.Location = new System.Drawing.Point(4, 30);
            this.wordCategoryComboBox.Name = "wordCategoryComboBox";
            this.wordCategoryComboBox.Size = new System.Drawing.Size(200, 28);
            this.wordCategoryComboBox.TabIndex = 12;
            this.wordCategoryComboBox.SelectedIndexChanged += new System.EventHandler(this.wordCategoryComboBox_SelectedIndexChanged);
            this.wordCategoryComboBox.Click += new System.EventHandler(this.wordCategoryComboBox_Click);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.wordsRichTextBox);
            this.panel2.Location = new System.Drawing.Point(222, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(561, 429);
            this.panel2.TabIndex = 5;
            // 
            // wordsRichTextBox
            // 
            this.wordsRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.wordsRichTextBox.Name = "wordsRichTextBox";
            this.wordsRichTextBox.Size = new System.Drawing.Size(550, 419);
            this.wordsRichTextBox.TabIndex = 4;
            this.wordsRichTextBox.Text = "";
            // 
            // WordAddForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSeaGreen;
            this.ClientSize = new System.Drawing.Size(799, 453);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "WordAddForm";
            this.Text = "Работа с базой данных";
            this.Load += new System.EventHandler(this.WordAddForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private Panel panel1;
        private ComboBox tatarchaLettersComboBox;
        private Label label3;
        private Label label2;
        private TextBox tatarchaTextBox;
        private Button deleteButton;
        private Button updateButton;
        private TextBox russianTextBox;
        private Button wordAddButton;
        private Label label1;
        private ComboBox wordCategoryComboBox;
        private Panel panel2;
        private RichTextBox wordsRichTextBox;
    }
}