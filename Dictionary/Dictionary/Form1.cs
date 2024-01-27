using Dictionary.BLL.BaseResponse;
using Dictionary.OtherForms;
using Dictionary.Services.Interfaces;
using DictionaryTatarcha.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary
{
    public partial class Form1 : Form
    {
        //Сервисы для взаимодействия с сущностями бд.
        private readonly IWordServise<Noun> _nounServise;
        private readonly IWordServise<Verb> _verbServise;
        private readonly IWordServise<Adjective> _adjectiveServise;
        
        //Листы для записи слов.
        private List<string> russianWords = new List<string>();
        private List<string> tatarchaWords = new List<string>();

        //Вспомогательные переменные.
        private string categorySelected = string.Empty;
        private bool clickCheck;
        private bool clearIsUsed;
        //Проверка выбора категории.
        private bool categoryIsSelected;
        //Проверка получения отсортированного листа.
        private bool listIsSorted;
        //Переменная для вывода порядкового номера слова.
        int wordNumber = 0;

        public Form1()
        {
            InitializeComponent();
        }

        //Конструктор со всеми сервисами.
        public Form1(IWordServise<Noun> nounServise, IWordServise<Verb> verbServise, IWordServise<Adjective> adjectiveServise)
        {
            _adjectiveServise = adjectiveServise;
            _verbServise = verbServise;
            _nounServise = nounServise;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            categoryComboBox.SelectedItem = categoryComboBox.Items[3];
            russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];
        }

        //Метод для удаления объекта в комбобоксе.
        private void categoryComboBox_Click(object sender, EventArgs e)
        {
            if(!clickCheck)
            {
                //Параметры для доступа к очистке.
                categoryComboBox.Items.RemoveAt(3);
                clickCheck = true;
            }
        }

        //Метод для получения слов по категориям.
        private async void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Опеределени для очистки, что она не применена.
            clearIsUsed = false;
            //Обновление категории.
            categorySelected = string.Empty;
            //Очистка листов, чтобы не было наслоения.
            russianWords.Clear();
            tatarchaWords.Clear();
            //Очистка текстбоксов.
            tatarchaRichTextBox.Clear();
            russianRichTextBox.Clear();
            wordNumber = 0;
            //Убираю фильтры.
            russianFiltersComboBox.SelectedIndex = 0;
            tatarchaFiltersComboBox.SelectedIndex = 0;

            switch(categoryComboBox.SelectedIndex)
            {        
                case 0:
                    {
                        categoryIsSelected = true;
                        categorySelected = "n";
                        var responseRussian = await _nounServise.GetRussianWords();
                        var responseTatarcha = await _nounServise.GetTatarchaWords();
                        //Проверка получения данных.
                        if(responseRussian.Status == BLL.DataBaseResponseStatus.OK && responseTatarcha.Status == BLL.DataBaseResponseStatus.OK)
                        {
                            tatarchaWords = responseTatarcha.Data;
                            russianWords = responseRussian.Data;

                            //Перебор и заполнение рич текстбоксов.
                            for(int wordIndex = 0; wordIndex < responseRussian.Data.Count; wordIndex++)
                            {
                                wordNumber++;
                                tatarchaRichTextBox.Text += $"{wordNumber}) " + tatarchaWords[wordIndex] + "\n";
                                russianRichTextBox.Text += $"{wordNumber}) " + russianWords[wordIndex] + "\n";
                            }
                        }
                        //Вывод ошибок.
                        else
                        {
                            MessageBox.Show(responseRussian.Description + "\n" + "\n" + responseTatarcha.Description, "Предупреждение");
                        }
                    }
                    break;
                case 1:
                    {
                        categoryIsSelected = true;
                        categorySelected = "v";
                        var responseRussian = await _verbServise.GetRussianWords();
                        var responseTatarcha = await _verbServise.GetTatarchaWords();
                        //Проверка получения данных.
                        if(responseRussian.Status == BLL.DataBaseResponseStatus.OK && responseTatarcha.Status == BLL.DataBaseResponseStatus.OK)
                        {
                            tatarchaWords = responseTatarcha.Data;
                            russianWords = responseRussian.Data;

                            //Перебор и заполнение рич текстбоксов.
                            for (int wordIndex = 0; wordIndex < responseRussian.Data.Count; wordIndex++)
                            {
                                wordNumber++;
                                tatarchaRichTextBox.Text += $"{wordNumber}) " + tatarchaWords[wordIndex] + "\n";
                                russianRichTextBox.Text += $"{wordNumber}) " + russianWords[wordIndex] + "\n";
                            }
                        }
                        //Вывод ошибок.
                        else
                        {
                            MessageBox.Show(responseRussian.Description + "\n" + "\n" + responseTatarcha.Description, "Предупреждение");
                        }
                    }
                    break;
                case 2:
                    {
                        categoryIsSelected = true;
                        categorySelected = "a";
                        var responseRussian = await _adjectiveServise.GetRussianWords();
                        var responseTatarcha = await _adjectiveServise.GetTatarchaWords();
                        //Проверка получения данных.
                        if(responseRussian.Status == BLL.DataBaseResponseStatus.OK && responseTatarcha.Status == BLL.DataBaseResponseStatus.OK)
                        {
                            tatarchaWords = responseTatarcha.Data;
                            russianWords = responseRussian.Data;

                            //Перебор и заполнение рич текстбоксов.
                            for (int wordIndex = 0; wordIndex < responseRussian.Data.Count; wordIndex++)
                            {
                                wordNumber++;
                                tatarchaRichTextBox.Text += $"{wordNumber}) " + tatarchaWords[wordIndex] + "\n";
                                russianRichTextBox.Text += $"{wordNumber}) " + russianWords[wordIndex] + "\n";
                            }
                        }
                        //Вывод ошибок.
                        else
                        {
                            MessageBox.Show(responseRussian.Description + "\n" + "\n" + responseTatarcha.Description, "Предупреждение");
                        }
                    }
                    break;
            }
        }

        //Фильтры для русских слов.
        private async void russianFiltersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Опеределени для очистки, что она не применена.
            clearIsUsed = false;
            //Определение языка для сортировки.
            string selectedLanguage = "russian";
            //Лист для сортировки.
            List<string> wordsList = russianWords;
            //Очистка текстбоксов, чтобы не было наслоениния.
            russianRichTextBox.Clear();
            tatarchaRichTextBox.Clear();
            //Убираю фильтр из другого комбобокса.
            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];
            wordNumber = 0;

            try
            {
                switch(russianFiltersComboBox.SelectedIndex)
                {
                    #region Первый фильтр по алфавиту.

                    case 1:
                        {
                            if (categoryIsSelected)
                            {
                                //Проверка выбранной категории.
                                switch (categorySelected)
                                {
                                    case "n":
                                        {
                                            var sortedListWithWords = await _nounServise.SortByAlphabetWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //Перезапись листов.
                                                foreach (var noun in sortedListWithWords.Data)
                                                {
                                                    wordNumber++;
                                                    tatarchaRichTextBox.Text += $"{wordNumber}) " + noun.NounWord + "\n";
                                                    russianRichTextBox.Text += $"{wordNumber}) " + noun.NounTranslate + "\n";
                                                }
                                            }
                                        }
                                        break;
                                    case "a":
                                        {
                                            var sortedListWithWords = await _adjectiveServise.SortByAlphabetWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //Перезапись листов.
                                                foreach (var adjective in sortedListWithWords.Data)
                                                {
                                                    wordNumber++;
                                                    tatarchaRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveWord + "\n";
                                                    russianRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveTranslate + "\n";
                                                }
                                            }
                                        }
                                        break;
                                    case "v":
                                        {
                                            var sortedListWithWords = await _verbServise.SortByAlphabetWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //Перезапись листов.
                                                foreach (var verb in sortedListWithWords.Data)
                                                {
                                                    wordNumber++;
                                                    tatarchaRichTextBox.Text += $"{wordNumber}) " + verb.VerbWord + "\n";
                                                    russianRichTextBox.Text += $"{wordNumber}) " + verb.VerbTranslate + "\n";
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Выбирите категорию.", "Предупреждение");
                                russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
                            }
                        }
                        break;

                    #endregion

                    #region Второй фильтр по возрастанию.

                    case 2:
                        {
                            if (categoryIsSelected)
                            {
                                //Проверка выбранной категории.
                                switch (categorySelected)
                                {
                                    case "n":
                                        {
                                            var sortedListWithWords = await _nounServise.SortByWordFromShorterWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //Перезапись листов.
                                                foreach (var noun in sortedListWithWords.Data)
                                                {
                                                    wordNumber++;
                                                    tatarchaRichTextBox.Text += $"{wordNumber}) " + noun.NounWord + "\n";
                                                    russianRichTextBox.Text += $"{wordNumber}) " + noun.NounTranslate + "\n";
                                                }
                                            }
                                        }
                                        break;
                                    case "a":
                                        {
                                            var sortedListWithWords = await _adjectiveServise.SortByWordFromShorterWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //Перезапись листов.
                                                foreach (var adjective in sortedListWithWords.Data)
                                                {
                                                    wordNumber++;
                                                    tatarchaRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveWord + "\n";
                                                    russianRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveTranslate + "\n";
                                                }
                                            }
                                        }
                                        break;
                                    case "v":
                                        {
                                            var sortedListWithWords = await _verbServise.SortByWordFromShorterWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //Перезапись листов.
                                                foreach (var verb in sortedListWithWords.Data)
                                                {
                                                    wordNumber++;
                                                    tatarchaRichTextBox.Text += $"{wordNumber}) " + verb.VerbWord + "\n";
                                                    russianRichTextBox.Text += $"{wordNumber}) " + verb.VerbTranslate + "\n";
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Выбирите категорию.", "Предупреждение");
                                russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
                            }
                        }
                        break;

                    #endregion

                    #region Третий фильтр по убыванию.

                    case 3:
                        {
                            if (categoryIsSelected)
                            {
                                //Проверка выбранной категории.
                                switch (categorySelected)
                                {
                                    case "n":
                                        {
                                            var sortedListWithWords = await _nounServise.SortByWordFromLongerWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //Перезапись листов.
                                                foreach (var noun in sortedListWithWords.Data)
                                                {
                                                    wordNumber++;
                                                    tatarchaRichTextBox.Text += $"{wordNumber}) " + noun.NounWord + "\n";
                                                    russianRichTextBox.Text += $"{wordNumber}) " + noun.NounTranslate + "\n";
                                                }
                                            }
                                        }
                                        break;
                                    case "a":
                                        {
                                            var sortedListWithWords = await _adjectiveServise.SortByWordFromLongerWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //Перезапись листов.
                                                foreach (var adjective in sortedListWithWords.Data)
                                                {
                                                    wordNumber++;
                                                    tatarchaRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveWord + "\n";
                                                    russianRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveTranslate + "\n";
                                                }
                                            }
                                        }
                                        break;
                                    case "v":
                                        {
                                            var sortedListWithWords = await _verbServise.SortByWordFromLongerWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //Перезапись листов.
                                                foreach (var verb in sortedListWithWords.Data)
                                                {
                                                    wordNumber++;
                                                    tatarchaRichTextBox.Text += $"{wordNumber}) " + verb.VerbWord + "\n";
                                                    russianRichTextBox.Text += $"{wordNumber}) " + verb.VerbTranslate + "\n";
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Выбирите категорию.", "Предупреждение");
                                russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
                            }
                        }
                        break;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Фильтры для татарских слов.
        private async void tatarchaFiltersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Опеределени для очистки, что она не применена.
            clearIsUsed = false;
            //Определение языка для сортировки.
            string selectedLanguage = "tatarcha";
            List<string> wordsList = tatarchaWords;
            //Очистка текстбоксов, чтобы не было наслоениния.
            russianRichTextBox.Clear();
            tatarchaRichTextBox.Clear();
            //Убираю фильтр из другого комбобокса.
            russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
            wordNumber = 0;

            try
            {
                switch(tatarchaFiltersComboBox.SelectedIndex)
                {
                    #region Первый фильтр по алфавиту.

                    case 1:
                        if (categoryIsSelected)
                        {
                            //Проверка выбранной категории.
                            switch (categorySelected)
                            {
                                case "n":
                                    {
                                        var sortedListWithWords = await _nounServise.SortByAlphabetWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var noun in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + noun.NounWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " +noun.NounTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                                case "a":
                                    {
                                        var sortedListWithWords = await _adjectiveServise.SortByAlphabetWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var adjective in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                                case "v":
                                    {
                                        var sortedListWithWords = await _verbServise.SortByAlphabetWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var verb in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + verb.VerbWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " + verb.VerbTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выбирите категорию.", "Предупреждение");
                            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];
                        }
                        break;

                    #endregion

                    #region Второй фильтр по возрастанию длины слова.

                    case 2:
                        if (categoryIsSelected)
                        {
                            //Проверка выбранной категории.
                            switch (categorySelected)
                            {
                                case "n":
                                    {
                                        var sortedListWithWords = await _nounServise.SortByWordFromShorterWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var noun in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + noun.NounWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " + noun.NounTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                                case "a":
                                    {
                                        var sortedListWithWords = await _adjectiveServise.SortByWordFromShorterWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var adjective in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                                case "v":
                                    {
                                        var sortedListWithWords = await _verbServise.SortByWordFromShorterWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var verb in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + verb.VerbWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " + verb.VerbTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выбирите категорию.", "Предупреждение");
                            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];
                        }
                        break;

                    #endregion

                    #region Третий фильтр по убыванию.

                    case 3:
                        if (categoryIsSelected)
                        {
                            //Проверка выбранной категории.
                            switch (categorySelected)
                            {
                                case "n":
                                    {
                                        var sortedListWithWords = await _nounServise.SortByWordFromLongerWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var noun in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + noun.NounWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " + noun.NounTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                                case "a":
                                    {
                                        var sortedListWithWords = await _adjectiveServise.SortByWordFromLongerWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var adjective in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                                case "v":
                                    {
                                        var sortedListWithWords = await _verbServise.SortByWordFromLongerWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var verb in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + verb.VerbWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " + verb.VerbTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выбирите категорию.", "Предупреждение");
                            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];
                        }
                        break;

                    #endregion

                    #region Четвертый фильтр по количеству переводов.

                    case 4:
                        if (categoryIsSelected)
                        {
                            //Проверка выбранной категории.
                            switch (categorySelected)
                            {
                                case "n":
                                    {
                                        var sortedListWithWords = await _nounServise.SortByTranslateWords(wordsList);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var noun in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + noun.NounWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " + noun.NounTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                                case "a":
                                    {
                                        var sortedListWithWords = await _adjectiveServise.SortByTranslateWords(wordsList);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var adjective in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                                case "v":
                                    {
                                        var sortedListWithWords = await _verbServise.SortByTranslateWords(wordsList);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //Перезапись текстбоксов.
                                            foreach (var verb in sortedListWithWords.Data)
                                            {
                                                wordNumber++;
                                                tatarchaRichTextBox.Text += $"{wordNumber}) " + verb.VerbWord + "\n";
                                                russianRichTextBox.Text += $"{wordNumber}) " + verb.VerbTranslate + "\n";
                                            }
                                        }
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Выбирите категорию.", "Предупреждение");
                            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];
                        }
                        break;

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Очистка фильтров по кнопке.
        private void clearFiltersButton_Click(object sender, EventArgs e)
        {
            russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];

            //Вывод из базы таблицы.
            switch(categorySelected)
            {
                case "a":
                    categoryComboBox_SelectedIndexChanged(2, e);
                    break;
                case "n":
                    categoryComboBox_SelectedIndexChanged(0, e);
                    break;
                case "v":
                    categoryComboBox_SelectedIndexChanged(1, e);
                    break;
            }
        }

        //Метод для очистки текстбоксов.
        private void clearRTBButton_Click(object sender, EventArgs e)
        {
            //Очистка листов от слов.
            russianWords.Clear();
            tatarchaWords.Clear();
            //Обнуление фильтров.
            russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];

            //Избежение повтрного добавления объекта.
            if (!clearIsUsed)
            {
                //Добавление объекта в комбобокс с категориями и подставление его на место названия категории.
                categoryComboBox.Items.Add("Категория не выбрана");
                categoryComboBox.SelectedItem = categoryComboBox.Items[3];
                clearIsUsed = true;
                //Доступ к очистке по нажатию на комбобокс.
                clickCheck = false;
            }
            
            tatarchaRichTextBox.Text = string.Empty;
            russianRichTextBox.Text = string.Empty;
        }

        #region Подсказки для пользователя.

        //Подсказка поиска.
        private void searchButtonTip_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm("search");
            aboutForm.Show();
        }
        //Подсказка игры.
        private void gameAndAlfabetButtonTip_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm("gameAndAlfabet");
            aboutForm.Show();
        }
        //Подсказка для добавления слова в бд.
        private void wordAddButtonTip_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm("wordAdd");
            aboutForm.Show();
        }
        #endregion

        #region Другие кнопки.

        //Алфавит.
        private void alfabetButton_Click(object sender, EventArgs e)
        {
            AlfabetForm alfabetForm = new AlfabetForm();
            alfabetForm.Show();
        }

        //Игра.
        private void gameButton_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(_nounServise, _verbServise, _adjectiveServise);
            gameForm.Show();
        }

        //Добавление букв в поле для поиска слов.
        private void tatarchaLettersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxForSearchingWord.Text += tatarchaLettersComboBox.SelectedItem.ToString();
        }

        //Поиск слова.
        private async void searchButton_Click(object sender, EventArgs e)
        {
            try
            {
                wordNumber = 0;

                if (categoryIsSelected)
                {
                    switch (categorySelected)
                    {
                        case "n":
                            {
                                var sortedListWithWords = await _nounServise.GetSearchWord(textBoxForSearchingWord.Text);
                                if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                {
                                    tatarchaRichTextBox.Clear();
                                    russianRichTextBox.Clear();

                                    listIsSorted = true;
                                    //Перезапись текстбоксов.
                                    foreach (var noun in sortedListWithWords.Data)
                                    {
                                        wordNumber++;
                                        tatarchaRichTextBox.Text += $"{wordNumber}) " + noun.NounWord + "\n";
                                        russianRichTextBox.Text += $"{wordNumber}) " + noun.NounTranslate + "\n";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Слово не найдено", "Предупреждение");
                                }
                            }
                            break;
                        case "v":
                            {
                                var sortedListWithWords = await _verbServise.GetSearchWord(textBoxForSearchingWord.Text);
                                if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                {
                                    tatarchaRichTextBox.Clear();
                                    russianRichTextBox.Clear();

                                    listIsSorted = true;
                                    //Перезапись текстбоксов.
                                    foreach (var verb in sortedListWithWords.Data)
                                    {
                                        wordNumber++;
                                        tatarchaRichTextBox.Text += $"{wordNumber}) " + verb.VerbWord + "\n";
                                        russianRichTextBox.Text += $"{wordNumber}) " + verb.VerbTranslate + "\n";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Слово не найдено", "Предупреждение");
                                }
                            }
                            break;
                        case "a":
                            {
                                var sortedListWithWords = await _adjectiveServise.GetSearchWord(textBoxForSearchingWord.Text);
                                if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                {
                                    tatarchaRichTextBox.Clear();
                                    russianRichTextBox.Clear();

                                    listIsSorted = true;
                                    //Перезапись текстбоксов.
                                    foreach (var adjective in sortedListWithWords.Data)
                                    {
                                        wordNumber++;
                                        tatarchaRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveWord + "\n";
                                        russianRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveTranslate + "\n";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Слово не найдено", "Предупреждение");
                                }
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("Выбирите категорию.", "Предупреждение");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //Возвращение слов в ричтекстбоксы после поиска.
        private void wordBackButton_Click(object sender, EventArgs e)
        {
            textBoxForSearchingWord.Text = string.Empty;

            switch (categorySelected)
            {
                case "n":
                    categoryComboBox_SelectedIndexChanged(1, e);
                    break;
                case "v":
                    categoryComboBox_SelectedIndexChanged(2, e);
                    break;
                case "a":
                    categoryComboBox_SelectedIndexChanged(3, e);
                    break;
            }
        }

        //Добавление слова в базу.
        private void workWithDataBaseButton_Click(object sender, EventArgs e)
        {
            WorkWithDataBaseForm wordAddForm = new WorkWithDataBaseForm(_nounServise, _verbServise, _adjectiveServise);
            wordAddForm.Show();
        }

        #endregion

        #region Меню

        private void оПрограммеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram aboutProgram = new AboutProgram();
            aboutProgram.Show();
        }

        private void татарскийАлфавитToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlfabetForm alfabetForm = new AlfabetForm();
            alfabetForm.Show();
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}