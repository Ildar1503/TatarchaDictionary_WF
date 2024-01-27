using Azure;
using Dictionary.Services.Interfaces;
using DictionaryTatarcha.Entities;
using NAudio.CoreAudioApi;
using System;
using System.Windows.Forms;

namespace Dictionary.OtherForms
{
    public partial class WorkWithDataBaseForm : Form
    {
        //Сервисы для взаимодействия с сущностями бд.
        private readonly IWordServise<Noun> _nounServise;
        private readonly IWordServise<Verb> _verbServise;
        private readonly IWordServise<Adjective> _adjectiveServise;

        //Объекты управления, которые будут добавлены динамично.
        Label label = new Label();
        TextBox textBox = new TextBox();
       
        //Вспомогательные переменные.
        private string categorySelected = string.Empty;
        //Для проверки нажатия на комбобокс.
        private bool clickCheck;
        //Для проверки нажатия на кнопки обновления и удаления объектов.
        private bool buttonClickCheck;
        //Проверка выбора категории.
        private bool categoryIsSelected;

        private string[] invalidSymbols = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0", "+", "=", "<", ">", ".", "/", "?", "_", "!", "@", "[", "]", "{", "}"};
        private string[] validIdSymbols = { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" };

        public WorkWithDataBaseForm(IWordServise<Noun> nounServise, IWordServise<Verb> verbServise, IWordServise<Adjective> adjectiveServise)
        {
            _adjectiveServise = adjectiveServise;
            _verbServise = verbServise;
            _nounServise = nounServise;
            InitializeComponent();
        }

        private void WordAddForm_Load(object sender, EventArgs e)
        {
            //Настройка объектов управления.
            label.Text = "Поле для id";
            label.Location = new Point(3, 271);
            textBox.Size = new Size(201, 27);
            textBox.Location = new Point(3, 294);
            this.textBox.TextChanged += new EventHandler(DinamicTextBoxValidationCheker);
        }

        //Выбор категории.
        private async void wordCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            wordsRichTextBox.Clear();
            RemoveControls();

            switch (wordCategoryComboBox.SelectedIndex)
            {
                case 0:
                    {
                        categoryIsSelected = true;
                        categorySelected = "n";
                        var response = await _nounServise.GetAllWords();

                        //Проверка получения данных.
                        if (response.Status == BLL.DataBaseResponseStatus.OK)
                        {
                            //Перебор и заполнение рич текстбокса.
                            foreach (var word in response.Data)
                            {
                                wordsRichTextBox.Text += $" Id - {word.Id} | {word.NounTranslate} | {word.NounWord}" + "\n";
                            }
                        }
                        //Вывод ошибок.
                        else
                        {
                            MessageBox.Show(response.Description, "Предупреждение");
                        }
                    }
                    break;
                case 1:
                    {
                        categoryIsSelected = true;
                        categorySelected = "v";
                        var response = await _verbServise.GetAllWords();

                        //Проверка получения данных.
                        if (response.Status == BLL.DataBaseResponseStatus.OK)
                        {
                            //Перебор и заполнение рич текстбокса.
                            foreach (var word in response.Data)
                            {
                                wordsRichTextBox.Text += $" Id - {word.Id} | {word.VerbTranslate} | {word.VerbWord}" + "\n";
                            }
                        }
                        //Вывод ошибок.
                        else
                        {
                            MessageBox.Show(response.Description, "Предупреждение");
                        }
                    }
                    break;
                case 2:
                    {
                        categoryIsSelected = true;
                        categorySelected = "a";
                        var response = await _adjectiveServise.GetAllWords();

                        //Проверка получения данных.
                        if (response.Status == BLL.DataBaseResponseStatus.OK)
                        {
                            //Перебор и заполнение рич текстбокса.
                            foreach (var word in response.Data)
                            {
                                wordsRichTextBox.Text += $" Id - {word.Id} | {word.AdjectiveTranslate} | {word.AdjectiveWord}" + "\n";
                            }
                        }
                        //Вывод ошибок.
                        else
                        {
                            MessageBox.Show(response.Description, "Предупреждение");
                        }
                    }
                    break;
            }
        }

        //Выбор буквы.
        private void tatarchaLettersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            tatarchaTextBox.Text += tatarchaLettersComboBox.SelectedItem.ToString();
        }

        //Метод, убирающий пустое поле в комбобоксе.
        private void wordCategoryComboBox_Click(object sender, EventArgs e)
        {
            if (!clickCheck)
            {
                //Параметры для доступа к очистке.
                wordCategoryComboBox.Items.RemoveAt(0);
                clickCheck = true;
            }
        }

        #region Кнокпки для измений данных в бд.

        //Добавление слова.
        private async void wordAddButton_Click(object sender, EventArgs e)
        {
            try
            {
                //Очистка текстбокса, чтобы не было наложения.
                wordsRichTextBox.Clear();
                RemoveControls();

                if (categoryIsSelected)
                {
                    switch (categorySelected)
                    {
                        case "n":
                            {
                                var response = await _nounServise.AddNewWord(russianTextBox.Text, tatarchaTextBox.Text);
                                if (response != null)
                                {
                                    //Перебор и заполнение рич текстбокса.
                                    foreach (var word in response.Data)
                                    {
                                        wordsRichTextBox.Text += $" Id - {word.Id} | {word.NounTranslate} | {word.NounWord}" + "\n";
                                    }
                                }
                            }
                            break;
                        case "v":
                            {
                                var response = await _verbServise.AddNewWord(russianTextBox.Text, tatarchaTextBox.Text);
                                if (response != null)
                                {
                                    //Перебор и заполнение рич текстбокса.
                                    foreach (var word in response.Data)
                                    {
                                        wordsRichTextBox.Text += $" Id - {word.Id} | {word.VerbTranslate} | {word.VerbWord}" + "\n";
                                    }
                                }
                            }
                            break;
                        case "a":
                            {
                                var response = await _adjectiveServise.AddNewWord(russianTextBox.Text, tatarchaTextBox.Text);
                                if (response != null)
                                {
                                    //Перебор и заполнение рич текстбокса.
                                    foreach (var word in response.Data)
                                    {
                                        wordsRichTextBox.Text += $" Id - {word.Id} | {word.AdjectiveTranslate} | {word.AdjectiveWord}" + "\n";
                                    }
                                }
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Для продолжения выберите категорию слова", "Предупреждение");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Операция не может быть проведена, введенные данные некорректы");
            }
        }

        //Обновление слова.
        private async void updateButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (categoryIsSelected)
                {
                    if (buttonClickCheck)
                    {
                        if (russianTextBox.Text.Length > 1 && tatarchaTextBox.Text.Length > 1 && textBox.Text.Length > 0)
                        {
                            //Очистка текстбокса, чтобы не было наложения.
                            wordsRichTextBox.Clear();

                            switch (categorySelected)
                            {
                                case "n":
                                    {
                                        Noun noun = new Noun { NounTranslate = russianTextBox.Text, NounWord = tatarchaTextBox.Text };
                                        var response = await _nounServise.UpdateWord(noun, Convert.ToInt32(textBox.Text));
                                        if (response != null)
                                        {
                                            //Перебор и заполнение рич текстбокса.
                                            foreach (var word in response.Data)
                                            {
                                                wordsRichTextBox.Text += $" Id - {word.Id} | {word.NounTranslate} | {word.NounWord}" + "\n";
                                            }
                                        }
                                    }
                                    break;
                                case "v":
                                    {
                                        Verb verb = new Verb { VerbTranslate = russianTextBox.Text, VerbWord = tatarchaTextBox.Text };
                                        var response = await _verbServise.UpdateWord(verb, Convert.ToInt32(textBox.Text));
                                        if (response != null)
                                        {
                                            //Перебор и заполнение рич текстбокса.
                                            foreach (var word in response.Data)
                                            {
                                                wordsRichTextBox.Text += $" Id - {word.Id} | {word.VerbTranslate} | {word.VerbWord}" + "\n";
                                            }
                                        }
                                    }
                                    break;
                                case "a":
                                    {
                                        Adjective adjective = new Adjective { AdjectiveTranslate = russianTextBox.Text, AdjectiveWord = tatarchaTextBox.Text };
                                        var response = await _adjectiveServise.UpdateWord(adjective, Convert.ToInt32(textBox.Text));
                                        if (response != null)
                                        {
                                            //Перебор и заполнение рич текстбокса.
                                            foreach (var word in response.Data)
                                            {
                                                wordsRichTextBox.Text += $" Id - {word.Id} | {word.AdjectiveTranslate} | {word.AdjectiveWord}" + "\n";
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Поля для изменений заполнены некорректно", "Предупреждение");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Чтобы обновить слово - впишите его идентификатор (id) в текстовое поле.", "Предупреждение");
                        //Динамичное добавление текстового поля и заголовка.
                        panel1.Controls.Add(textBox);
                        panel1.Controls.Add(label);
                        buttonClickCheck = true;
                    }
                }
                else
                {
                    MessageBox.Show("Для продолжения выберите категорию слова", "Предупреждение");
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Операция не может быть проведена, введенные данные некорректы");
            }
        }

        //Удаление слов.
        private async void deleteButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (categoryIsSelected)
                {
                    if (buttonClickCheck)
                    {
                        //Очистка текстбокса, чтобы не было наложения.
                        wordsRichTextBox.Clear();

                        switch (categorySelected)
                        {
                            case "n":
                                {
                                    int id = Convert.ToInt32(textBox.Text);
                                    var response = await _nounServise.DeleteWord(id);
                                    if (response != null)
                                    {
                                        //Перебор и заполнение рич текстбокса.
                                        foreach (var word in response.Data)
                                        {
                                            wordsRichTextBox.Text += $" Id - {word.Id} | {word.NounTranslate} | {word.NounWord}" + "\n";
                                        }
                                    }
                                }
                                break;
                            case "v":
                                {
                                    var response = await _verbServise.DeleteWord(Convert.ToInt32(textBox.Text));
                                    if (response != null)
                                    {
                                        //Перебор и заполнение рич текстбокса.
                                        foreach (var word in response.Data)
                                        {
                                            wordsRichTextBox.Text += $" Id - {word.Id} | {word.VerbTranslate} | {word.VerbWord}" + "\n";
                                        }
                                    }
                                }
                                break;
                            case "a":
                                {
                                    var response = await _adjectiveServise.DeleteWord(Convert.ToInt32(textBox.Text));
                                    if (response != null)
                                    {
                                        //Перебор и заполнение рич текстбокса.
                                        foreach (var word in response.Data)
                                        {
                                            wordsRichTextBox.Text += $" Id - {word.Id} | {word.AdjectiveTranslate} | {word.AdjectiveWord}" + "\n";
                                        }
                                    }
                                }
                                break;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Чтобы удалить слово - впишите его идентификатор (id) в текстовое поле.", "Предупреждение");
                        //Динамичное добавление текстового поля и заголовка.
                        panel1.Controls.Add(textBox);
                        panel1.Controls.Add(label);
                        buttonClickCheck = true;
                    }
                }
                else
                {
                    MessageBox.Show("Для продолжения выберите категорию слова", "Предупреждение");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Операция не может быть проведена, введенные данные некорректы");
            }
        }

        #endregion

        #region Проверка валидности введенных данных.

        private void DinamicTextBoxValidationCheker(object sender, EventArgs e)
        {
            if (!validIdSymbols.Contains(textBox.Text.ToString()) && textBox.Text.Length == 1)
            {
                MessageBox.Show("Недопустимый символ! Для данного поля доступны только числа", "Предупреждение");
                textBox.Text = "";
            }
            if (textBox.Text.Length >= 2 && !validIdSymbols.Contains(textBox.Text[textBox.Text.Length - 1].ToString()))
            {
                MessageBox.Show("Недопустимый символ! Для данного поля доступны только числа", "Предупреждение");
                textBox.Text = textBox.Text.Remove(textBox.Text.Length - 1);
            }

        }

        private void russianTextBox_TextChanged(object sender, EventArgs e)
        {
            if(russianTextBox.Text.Length > 0)
            {
                //Проверка последнего веденного символа.
                foreach (var symbol in invalidSymbols)
                {
                    if (russianTextBox.Text.ToString() == symbol && russianTextBox.Text.Length == 1)
                    {
                        MessageBox.Show("Недопустимый символ!", "Предупреждение");
                        russianTextBox.Text = "";
                    }
                    if (russianTextBox.Text[russianTextBox.Text.Length - 1].ToString() == symbol && russianTextBox.Text.Length >= 2)
                    {
                        MessageBox.Show("Недопустимый символ!", "Предупреждение");
                        russianTextBox.Text = russianTextBox.Text.Remove(russianTextBox.Text.Length - 1);
                    }
                }
            }
        }

        private void tatarchaTextBox_TextChanged(object sender, EventArgs e)
        {
            if(tatarchaTextBox.Text.Length > 0)
            {
                //Проверка последнего веденного символа.
                foreach (var symbol in invalidSymbols)
                {
                    if (tatarchaTextBox.Text.ToString() == symbol && tatarchaTextBox.Text.Length == 1)
                    {
                        MessageBox.Show("Недопустимый символ!", "Предупреждение");
                        tatarchaTextBox.Text = "";
                    }
                    if (tatarchaTextBox.Text[tatarchaTextBox.Text.Length - 1].ToString() == symbol && tatarchaTextBox.Text.Length >= 2)
                    {
                        MessageBox.Show("Недопустимый символ!", "Предупреждение");
                        tatarchaTextBox.Text = tatarchaTextBox.Text.Remove(tatarchaTextBox.Text.Length - 1);
                    }
                }
            }
        }

        #endregion

        //Метод для очистки неспользуемых элементов управления.
        private void RemoveControls()
        {
            if (buttonClickCheck)
            {
                panel1.Controls.Remove(textBox);
                panel1.Controls.Remove(label);
                buttonClickCheck = false;
            }
        }
    }
}
