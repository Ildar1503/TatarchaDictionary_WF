using Dictionary.BLL.BaseResponse;
using Dictionary.OtherForms;
using Dictionary.Services.Interfaces;
using DictionaryTatarcha.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace Dictionary
{
    public partial class Form1 : Form
    {
        //������� ��� �������������� � ���������� ��.
        private readonly IWordServise<Noun> _nounServise;
        private readonly IWordServise<Verb> _verbServise;
        private readonly IWordServise<Adjective> _adjectiveServise;
        
        //����� ��� ������ ����.
        private List<string> russianWords = new List<string>();
        private List<string> tatarchaWords = new List<string>();

        //��������������� ����������.
        private string categorySelected = string.Empty;
        private bool clickCheck;
        private bool clearIsUsed;
        //�������� ������ ���������.
        private bool categoryIsSelected;
        //�������� ��������� ���������������� �����.
        private bool listIsSorted;
        //���������� ��� ������ ����������� ������ �����.
        int wordNumber = 0;

        public Form1()
        {
            InitializeComponent();
        }

        //����������� �� ����� ���������.
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

        //����� ��� �������� ������� � ����������.
        private void categoryComboBox_Click(object sender, EventArgs e)
        {
            if(!clickCheck)
            {
                //��������� ��� ������� � �������.
                categoryComboBox.Items.RemoveAt(3);
                clickCheck = true;
            }
        }

        //����� ��� ��������� ���� �� ����������.
        private async void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //����������� ��� �������, ��� ��� �� ���������.
            clearIsUsed = false;
            //���������� ���������.
            categorySelected = string.Empty;
            //������� ������, ����� �� ���� ���������.
            russianWords.Clear();
            tatarchaWords.Clear();
            //������� �����������.
            tatarchaRichTextBox.Clear();
            russianRichTextBox.Clear();
            wordNumber = 0;
            //������ �������.
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
                        //�������� ��������� ������.
                        if(responseRussian.Status == BLL.DataBaseResponseStatus.OK && responseTatarcha.Status == BLL.DataBaseResponseStatus.OK)
                        {
                            tatarchaWords = responseTatarcha.Data;
                            russianWords = responseRussian.Data;

                            //������� � ���������� ��� �����������.
                            for(int wordIndex = 0; wordIndex < responseRussian.Data.Count; wordIndex++)
                            {
                                wordNumber++;
                                tatarchaRichTextBox.Text += $"{wordNumber}) " + tatarchaWords[wordIndex] + "\n";
                                russianRichTextBox.Text += $"{wordNumber}) " + russianWords[wordIndex] + "\n";
                            }
                        }
                        //����� ������.
                        else
                        {
                            MessageBox.Show(responseRussian.Description + "\n" + "\n" + responseTatarcha.Description, "��������������");
                        }
                    }
                    break;
                case 1:
                    {
                        categoryIsSelected = true;
                        categorySelected = "v";
                        var responseRussian = await _verbServise.GetRussianWords();
                        var responseTatarcha = await _verbServise.GetTatarchaWords();
                        //�������� ��������� ������.
                        if(responseRussian.Status == BLL.DataBaseResponseStatus.OK && responseTatarcha.Status == BLL.DataBaseResponseStatus.OK)
                        {
                            tatarchaWords = responseTatarcha.Data;
                            russianWords = responseRussian.Data;

                            //������� � ���������� ��� �����������.
                            for (int wordIndex = 0; wordIndex < responseRussian.Data.Count; wordIndex++)
                            {
                                wordNumber++;
                                tatarchaRichTextBox.Text += $"{wordNumber}) " + tatarchaWords[wordIndex] + "\n";
                                russianRichTextBox.Text += $"{wordNumber}) " + russianWords[wordIndex] + "\n";
                            }
                        }
                        //����� ������.
                        else
                        {
                            MessageBox.Show(responseRussian.Description + "\n" + "\n" + responseTatarcha.Description, "��������������");
                        }
                    }
                    break;
                case 2:
                    {
                        categoryIsSelected = true;
                        categorySelected = "a";
                        var responseRussian = await _adjectiveServise.GetRussianWords();
                        var responseTatarcha = await _adjectiveServise.GetTatarchaWords();
                        //�������� ��������� ������.
                        if(responseRussian.Status == BLL.DataBaseResponseStatus.OK && responseTatarcha.Status == BLL.DataBaseResponseStatus.OK)
                        {
                            tatarchaWords = responseTatarcha.Data;
                            russianWords = responseRussian.Data;

                            //������� � ���������� ��� �����������.
                            for (int wordIndex = 0; wordIndex < responseRussian.Data.Count; wordIndex++)
                            {
                                wordNumber++;
                                tatarchaRichTextBox.Text += $"{wordNumber}) " + tatarchaWords[wordIndex] + "\n";
                                russianRichTextBox.Text += $"{wordNumber}) " + russianWords[wordIndex] + "\n";
                            }
                        }
                        //����� ������.
                        else
                        {
                            MessageBox.Show(responseRussian.Description + "\n" + "\n" + responseTatarcha.Description, "��������������");
                        }
                    }
                    break;
            }
        }

        //������� ��� ������� ����.
        private async void russianFiltersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //����������� ��� �������, ��� ��� �� ���������.
            clearIsUsed = false;
            //����������� ����� ��� ����������.
            string selectedLanguage = "russian";
            //���� ��� ����������.
            List<string> wordsList = russianWords;
            //������� �����������, ����� �� ���� �����������.
            russianRichTextBox.Clear();
            tatarchaRichTextBox.Clear();
            //������ ������ �� ������� ����������.
            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];
            wordNumber = 0;

            try
            {
                switch(russianFiltersComboBox.SelectedIndex)
                {
                    #region ������ ������ �� ��������.

                    case 1:
                        {
                            if (categoryIsSelected)
                            {
                                //�������� ��������� ���������.
                                switch (categorySelected)
                                {
                                    case "n":
                                        {
                                            var sortedListWithWords = await _nounServise.SortByAlphabetWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //���������� ������.
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
                                                //���������� ������.
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
                                                //���������� ������.
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
                                MessageBox.Show("�������� ���������.", "��������������");
                                russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
                            }
                        }
                        break;

                    #endregion

                    #region ������ ������ �� �����������.

                    case 2:
                        {
                            if (categoryIsSelected)
                            {
                                //�������� ��������� ���������.
                                switch (categorySelected)
                                {
                                    case "n":
                                        {
                                            var sortedListWithWords = await _nounServise.SortByWordFromShorterWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //���������� ������.
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
                                                //���������� ������.
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
                                                //���������� ������.
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
                                MessageBox.Show("�������� ���������.", "��������������");
                                russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
                            }
                        }
                        break;

                    #endregion

                    #region ������ ������ �� ��������.

                    case 3:
                        {
                            if (categoryIsSelected)
                            {
                                //�������� ��������� ���������.
                                switch (categorySelected)
                                {
                                    case "n":
                                        {
                                            var sortedListWithWords = await _nounServise.SortByWordFromLongerWords(wordsList, selectedLanguage);
                                            if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                            {
                                                listIsSorted = true;
                                                //���������� ������.
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
                                                //���������� ������.
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
                                                //���������� ������.
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
                                MessageBox.Show("�������� ���������.", "��������������");
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

        //������� ��� ��������� ����.
        private async void tatarchaFiltersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            //����������� ��� �������, ��� ��� �� ���������.
            clearIsUsed = false;
            //����������� ����� ��� ����������.
            string selectedLanguage = "tatarcha";
            List<string> wordsList = tatarchaWords;
            //������� �����������, ����� �� ���� �����������.
            russianRichTextBox.Clear();
            tatarchaRichTextBox.Clear();
            //������ ������ �� ������� ����������.
            russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
            wordNumber = 0;

            try
            {
                switch(tatarchaFiltersComboBox.SelectedIndex)
                {
                    #region ������ ������ �� ��������.

                    case 1:
                        if (categoryIsSelected)
                        {
                            //�������� ��������� ���������.
                            switch (categorySelected)
                            {
                                case "n":
                                    {
                                        var sortedListWithWords = await _nounServise.SortByAlphabetWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //���������� �����������.
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
                                            //���������� �����������.
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
                                            //���������� �����������.
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
                            MessageBox.Show("�������� ���������.", "��������������");
                            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];
                        }
                        break;

                    #endregion

                    #region ������ ������ �� ����������� ����� �����.

                    case 2:
                        if (categoryIsSelected)
                        {
                            //�������� ��������� ���������.
                            switch (categorySelected)
                            {
                                case "n":
                                    {
                                        var sortedListWithWords = await _nounServise.SortByWordFromShorterWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //���������� �����������.
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
                                            //���������� �����������.
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
                                            //���������� �����������.
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
                            MessageBox.Show("�������� ���������.", "��������������");
                            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];
                        }
                        break;

                    #endregion

                    #region ������ ������ �� ��������.

                    case 3:
                        if (categoryIsSelected)
                        {
                            //�������� ��������� ���������.
                            switch (categorySelected)
                            {
                                case "n":
                                    {
                                        var sortedListWithWords = await _nounServise.SortByWordFromLongerWords(wordsList, selectedLanguage);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //���������� �����������.
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
                                            //���������� �����������.
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
                                            //���������� �����������.
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
                            MessageBox.Show("�������� ���������.", "��������������");
                            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];
                        }
                        break;

                    #endregion

                    #region ��������� ������ �� ���������� ���������.

                    case 4:
                        if (categoryIsSelected)
                        {
                            //�������� ��������� ���������.
                            switch (categorySelected)
                            {
                                case "n":
                                    {
                                        var sortedListWithWords = await _nounServise.SortByTranslateWords(wordsList);
                                        if (sortedListWithWords.Status == BLL.DataBaseResponseStatus.OK)
                                        {
                                            listIsSorted = true;
                                            //���������� �����������.
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
                                            //���������� �����������.
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
                                            //���������� �����������.
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
                            MessageBox.Show("�������� ���������.", "��������������");
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

        //������� �������� �� ������.
        private void clearFiltersButton_Click(object sender, EventArgs e)
        {
            russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];

            //����� �� ���� �������.
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

        //����� ��� ������� �����������.
        private void clearRTBButton_Click(object sender, EventArgs e)
        {
            //������� ������ �� ����.
            russianWords.Clear();
            tatarchaWords.Clear();
            //��������� ��������.
            russianFiltersComboBox.SelectedItem = russianFiltersComboBox.Items[0];
            tatarchaFiltersComboBox.SelectedItem = tatarchaFiltersComboBox.Items[0];

            //��������� ��������� ���������� �������.
            if (!clearIsUsed)
            {
                //���������� ������� � ��������� � ����������� � ������������ ��� �� ����� �������� ���������.
                categoryComboBox.Items.Add("��������� �� �������");
                categoryComboBox.SelectedItem = categoryComboBox.Items[3];
                clearIsUsed = true;
                //������ � ������� �� ������� �� ���������.
                clickCheck = false;
            }
            
            tatarchaRichTextBox.Text = string.Empty;
            russianRichTextBox.Text = string.Empty;
        }

        #region ��������� ��� ������������.

        //��������� ������.
        private void searchButtonTip_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm("search");
            aboutForm.Show();
        }
        //��������� ����.
        private void gameAndAlfabetButtonTip_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm("gameAndAlfabet");
            aboutForm.Show();
        }
        //��������� ��� ���������� ����� � ��.
        private void wordAddButtonTip_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new AboutForm("wordAdd");
            aboutForm.Show();
        }
        #endregion

        #region ������ ������.

        //�������.
        private void alfabetButton_Click(object sender, EventArgs e)
        {
            AlfabetForm alfabetForm = new AlfabetForm();
            alfabetForm.Show();
        }

        //����.
        private void gameButton_Click(object sender, EventArgs e)
        {
            GameForm gameForm = new GameForm(_nounServise, _verbServise, _adjectiveServise);
            gameForm.Show();
        }

        //���������� ���� � ���� ��� ������ ����.
        private void tatarchaLettersComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBoxForSearchingWord.Text += tatarchaLettersComboBox.SelectedItem.ToString();
        }

        //����� �����.
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
                                    //���������� �����������.
                                    foreach (var noun in sortedListWithWords.Data)
                                    {
                                        wordNumber++;
                                        tatarchaRichTextBox.Text += $"{wordNumber}) " + noun.NounWord + "\n";
                                        russianRichTextBox.Text += $"{wordNumber}) " + noun.NounTranslate + "\n";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("����� �� �������", "��������������");
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
                                    //���������� �����������.
                                    foreach (var verb in sortedListWithWords.Data)
                                    {
                                        wordNumber++;
                                        tatarchaRichTextBox.Text += $"{wordNumber}) " + verb.VerbWord + "\n";
                                        russianRichTextBox.Text += $"{wordNumber}) " + verb.VerbTranslate + "\n";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("����� �� �������", "��������������");
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
                                    //���������� �����������.
                                    foreach (var adjective in sortedListWithWords.Data)
                                    {
                                        wordNumber++;
                                        tatarchaRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveWord + "\n";
                                        russianRichTextBox.Text += $"{wordNumber}) " + adjective.AdjectiveTranslate + "\n";
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("����� �� �������", "��������������");
                                }
                            }
                            break;
                    }
                }
                else
                {
                    MessageBox.Show("�������� ���������.", "��������������");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //����������� ���� � ������������� ����� ������.
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

        //���������� ����� � ����.
        private void workWithDataBaseButton_Click(object sender, EventArgs e)
        {
            WorkWithDataBaseForm wordAddForm = new WorkWithDataBaseForm(_nounServise, _verbServise, _adjectiveServise);
            wordAddForm.Show();
        }

        #endregion

        #region ����

        private void ����������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutProgram aboutProgram = new AboutProgram();
            aboutProgram.Show();
        }

        private void ����������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AlfabetForm alfabetForm = new AlfabetForm();
            alfabetForm.Show();
        }

        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}