using Dictionary.Services.Implementations;
using Dictionary.Services.Interfaces;
using DictionaryTatarcha.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dictionary.OtherForms
{
    public partial class GameForm : Form
    {
        //Сервисы для взаимодействия с сущностями бд.
        private readonly IWordServise<Noun> _nounServise;
        private readonly IWordServise<Verb> _verbServise;
        private readonly IWordServise<Adjective> _adjectiveServise;

        private Random random = new Random();
        //Массив для кнокпок.
        private Button[] tatarchaButtons = new Button[5];
        private Button[] russianButtons = new Button[5];
        //Лист для использованных ключей.
        private List<string> usedKeys = new List<string>();
        //Лист для русских слов.
        private List<string> russianButtonsWords = new List<string>();
        //Лист для татарских слов.
        private List<string> tatarchaButtonsWords = new List<string>();
        //Словарь, в который будут записаны русские слова в качестве ключа, а татарские в качестве значения.
        private Dictionary<string, string> russianAndTatarchaDictionary = new Dictionary<string, string>();
        //Проверка нажатия на кнопку.
        private bool buttonIsClicked = false;
        //Переменные для записи ключа, значения объекта словаря.
        private string keyWord;
        private string valueWord;
        //Переменная для получения кнопки.
        private Button currentButton = new Button();
        //Подсчет угаданных слов.
        private int currectWordSelectCount = 0;
        //Получение выбранной категории.
        private char categorySelectedChar;
        //Проверка выбора категории.
        private bool isCategotySelected = false;

        public GameForm()
        {
            InitializeComponent();
        }

        public GameForm(IWordServise<Noun> nounServise, IWordServise<Verb> verbServise, IWordServise<Adjective> adjectiveServise)
        {
            _adjectiveServise = adjectiveServise;
            _verbServise = verbServise;
            _nounServise = nounServise;
            InitializeComponent();
        }

        private void GameForm_Load(object sender, EventArgs e)
        {
            wordCategoryComboBox.SelectedIndex = 0;

            #region Добавление кнопок.

            tatarchaButtons[0] = buttonTatarcha1;
            tatarchaButtons[1] = buttonTatarcha2;
            tatarchaButtons[2] = buttonTatarcha3;
            tatarchaButtons[3] = buttonTatarcha4;
            tatarchaButtons[4] = buttonTatarcha5;

            russianButtons[0] = buttonRussian1;
            russianButtons[1] = buttonRussian2;
            russianButtons[2] = buttonRussian3;
            russianButtons[3] = buttonRussian4;
            russianButtons[4] = buttonRussian5;

            for(int buttonIndex = 0; buttonIndex < 5; buttonIndex++)
            {
                russianButtons[buttonIndex].Text = "";
                tatarchaButtons[buttonIndex].Text = "";
            }

            #endregion
        }

        //Получение категории слова.
        private async void wordCategoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = 0;

            switch (wordCategoryComboBox.SelectedIndex)
            {
                //Существительные.
                case 1:
                    {
                        tatarchaButtonsWords.Clear();
                        russianButtonsWords.Clear();
                        isCategotySelected = true;
                        categorySelectedChar = 'n';
                        RemoveButtonsSeting();

                        var response = await _nounServise.GetWords();
                        //Проверка получения данных.
                        if (response.Status == BLL.DataBaseResponseStatus.OK)
                        {
                            russianAndTatarchaDictionary.Clear();
                            //Заполнение листов словами.
                            foreach (var words in response.Data)
                            {
                                if (!russianAndTatarchaDictionary.ContainsKey(words.NounTranslate))
                                {
                                    russianAndTatarchaDictionary.Add(words.NounTranslate, words.NounWord);
                                }
                            }

                            //Перебор словаря.
                            foreach(var key in russianAndTatarchaDictionary.Keys)
                            {
                                int randomNumber1 = random.Next(5);
                                int randomNamuber2 = random.Next(5);
                                //Проверка, чтобы не допустить повторной записи одного и того же значения, также чтобы индекс не превышал длинну массива.
                                if (!usedKeys.Contains(key) && index < 5 && randomNamuber2 == randomNumber1)
                                {
                                    russianButtonsWords.Add(key);
                                    tatarchaButtonsWords.Add(russianAndTatarchaDictionary[key]);
                                    index++;
                                }
                            }

                            ButtonsWordAdd(russianButtonsWords, tatarchaButtonsWords);
                        }
                        //Вывод ошибок.
                        else
                        {
                            MessageBox.Show(response.Description, "Предупреждение");
                        }
                    }
                    break;
                //Глаголы.
                case 2:
                    {
                        {
                            tatarchaButtonsWords.Clear();
                            russianButtonsWords.Clear();
                            isCategotySelected = true;
                            categorySelectedChar = 'v';
                            RemoveButtonsSeting();

                            var response = await _verbServise.GetWords();
                            //Проверка получения данных.
                            if (response.Status == BLL.DataBaseResponseStatus.OK)
                            {
                                russianAndTatarchaDictionary.Clear();
                                //Заполнение листов словами.
                                foreach (var words in response.Data)
                                {
                                    if (!russianAndTatarchaDictionary.ContainsKey(words.VerbTranslate))
                                    {
                                        russianAndTatarchaDictionary.Add(words.VerbTranslate, words.VerbWord);
                                    }
                                }

                                //Перебор словаря.
                                foreach (var key in russianAndTatarchaDictionary.Keys)
                                {
                                    int randomNumber1 = random.Next(5);
                                    int randomNamuber2 = random.Next(5);
                                    //Проверка, чтобы не допустить повторной записи одного и того же значения, также чтобы индекс не превышал длинну массива.
                                    if (!usedKeys.Contains(key) && index < 5 && randomNamuber2 == randomNumber1)
                                    {
                                        russianButtonsWords.Add(key);
                                        tatarchaButtonsWords.Add(russianAndTatarchaDictionary[key]);
                                        index++;
                                    }
                                }

                                ButtonsWordAdd(russianButtonsWords, tatarchaButtonsWords);
                            }
                            //Вывод ошибок.
                            else
                            {
                                MessageBox.Show(response.Description, "Предупреждение");
                            }
                        }
                    }
                    break;
                //Прилагательные.
                case 3:
                    {
                        {
                            tatarchaButtonsWords.Clear();
                            russianButtonsWords.Clear();
                            isCategotySelected = true;
                            categorySelectedChar = 'a';
                            RemoveButtonsSeting();

                            var response = await _adjectiveServise.GetWords();
                            //Проверка получения данных.
                            if (response.Status == BLL.DataBaseResponseStatus.OK)
                            {
                                russianAndTatarchaDictionary.Clear();
                                //Заполнение листов словами.
                                foreach (var words in response.Data)
                                {
                                    if (!russianAndTatarchaDictionary.ContainsKey(words.AdjectiveTranslate))
                                    {
                                        russianAndTatarchaDictionary.Add(words.AdjectiveTranslate, words.AdjectiveWord);
                                    }
                                }

                                //Перебор словаря.
                                foreach (var key in russianAndTatarchaDictionary.Keys)
                                {
                                    int randomNumber1 = random.Next(5);
                                    int randomNamuber2 = random.Next(5);
                                    //Проверка, чтобы не допустить повторной записи одного и того же значения, также чтобы индекс не превышал длинну массива.
                                    if (!usedKeys.Contains(key) && index < 5 && randomNamuber2 == randomNumber1)
                                    {
                                        russianButtonsWords.Add(key);
                                        tatarchaButtonsWords.Add(russianAndTatarchaDictionary[key]);
                                        index++;
                                    }
                                }

                                ButtonsWordAdd(russianButtonsWords, tatarchaButtonsWords);
                            }
                            //Вывод ошибок.
                            else
                            {
                                MessageBox.Show(response.Description, "Предупреждение");
                            }
                        }
                    }
                    break;
            }
        }

        //Метод для заполнения кнопок словами.
        private void ButtonsWordAdd(List<string> russianWordsArray, List<string> tatarchaWordsArray)
        {
            russianWordsArray = russianWordsArray.OrderBy(rand => random.Next()).ToList();
            tatarchaWordsArray = tatarchaWordsArray.OrderBy(rand => random.Next()).ToList();

            //Заполнение кнопок текстом.
            try
            {
                for (int wordIndex = 0; wordIndex < 5; wordIndex++)
                {
                    tatarchaButtons[wordIndex].Text = tatarchaWordsArray[wordIndex];
                    russianButtons[wordIndex].Text = russianWordsArray[wordIndex];
                }
            }
            catch(Exception)
            {
                switch(categorySelectedChar)
                {
                    case 'n':
                        wordCategoryComboBox_SelectedIndexChanged(1, new EventArgs());
                        break;
                    case 'v':
                        wordCategoryComboBox_SelectedIndexChanged(2, new EventArgs());
                        break;
                    case 'a':
                        wordCategoryComboBox_SelectedIndexChanged(3, new EventArgs());
                        break;
                }
            }
        }

        #region Кнокпки.

        private void buttonRussian1_Click(object sender, EventArgs e)
        {
            if (isCategotySelected)
            {
                if (buttonIsClicked)
                {
                    if (valueWord == russianAndTatarchaDictionary[buttonRussian1.Text])
                    {
                        buttonRussian1.Enabled = false;
                        currentButton.Enabled = false;
                        currentButton.BackColor = Color.Green;
                        buttonRussian1.BackColor = Color.Green;
                        buttonIsClicked = false;
                        currectWordSelectCount++;

                        if (currectWordSelectCount == 5)
                        {
                            DialogResult = MessageBox.Show("Вы угадали все слова хотите продолжить?", "Поздравление", MessageBoxButtons.OKCancel);
                            if (DialogResult == DialogResult.OK)
                            {
                                switch (categorySelectedChar)
                                {
                                    case 'n':
                                        wordCategoryComboBox_SelectedIndexChanged(1, e);
                                        break;
                                    case 'v':
                                        wordCategoryComboBox_SelectedIndexChanged(2, e);
                                        break;
                                    case 'a':
                                        wordCategoryComboBox_SelectedIndexChanged(3, e);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не верно, выбирете другое слово");
                    }
                }
                else
                {
                    currentButton = buttonRussian1;
                    keyWord = buttonRussian1.Text;
                    buttonIsClicked = true;
                }
            }
            else
            {
                MessageBox.Show("Для продолжения выбирете категорию слов");
            }
        }

        private void buttonRussian2_Click(object sender, EventArgs e)
        {
            if (isCategotySelected)
            {
                if (buttonIsClicked)
                {
                    if (valueWord == russianAndTatarchaDictionary[buttonRussian2.Text])
                    {
                        buttonRussian2.Enabled = false;
                        currentButton.Enabled = false;
                        currentButton.BackColor = Color.Green;
                        buttonRussian2.BackColor = Color.Green;
                        buttonIsClicked = false;
                        currectWordSelectCount++;

                        if (currectWordSelectCount == 5)
                        {
                            DialogResult = MessageBox.Show("Вы угадали все слова хотите продолжить?", "Поздравление", MessageBoxButtons.OKCancel);
                            if (DialogResult == DialogResult.OK)
                            {
                                switch (categorySelectedChar)
                                {
                                    case 'n':
                                        wordCategoryComboBox_SelectedIndexChanged(1, e);
                                        break;
                                    case 'v':
                                        wordCategoryComboBox_SelectedIndexChanged(2, e);
                                        break;
                                    case 'a':
                                        wordCategoryComboBox_SelectedIndexChanged(3, e);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не верно, выбирете другое слово");
                    }
                }
                else
                {
                    currentButton = buttonRussian2;
                    keyWord = buttonRussian2.Text;
                    buttonIsClicked = true;
                }
            }
            else
            {
                MessageBox.Show("Для продолжения выбирете категорию слов");
            }
        }

        private void buttonRussian3_Click(object sender, EventArgs e)
        {
            if (isCategotySelected)
            {
                if (buttonIsClicked)
                {
                    if (valueWord == russianAndTatarchaDictionary[buttonRussian3.Text])
                    {
                        buttonRussian3.Enabled = false;
                        currentButton.Enabled = false;
                        currentButton.BackColor = Color.Green;
                        buttonRussian3.BackColor = Color.Green;
                        buttonIsClicked = false;
                        currectWordSelectCount++;

                        if (currectWordSelectCount == 5)
                        {
                            DialogResult = MessageBox.Show("Вы угадали все слова хотите продолжить?", "Поздравление", MessageBoxButtons.OKCancel);
                            if (DialogResult == DialogResult.OK)
                            {
                                switch (categorySelectedChar)
                                {
                                    case 'n':
                                        wordCategoryComboBox_SelectedIndexChanged(1, e);
                                        break;
                                    case 'v':
                                        wordCategoryComboBox_SelectedIndexChanged(2, e);
                                        break;
                                    case 'a':
                                        wordCategoryComboBox_SelectedIndexChanged(3, e);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не верно, выбирете другое слово");
                    }
                }
                else
                {
                    currentButton = buttonRussian3;
                    keyWord = buttonRussian3.Text;
                    buttonIsClicked = true;
                }
            }
            else
            {
                MessageBox.Show("Для продолжения выбирете категорию слов");
            }
        }

        private void buttonRussian4_Click(object sender, EventArgs e)
        {
            if (isCategotySelected)
            {
                if (buttonIsClicked)
                {
                    if (valueWord == russianAndTatarchaDictionary[buttonRussian4.Text])
                    {
                        buttonRussian4.Enabled = false;
                        currentButton.Enabled = false;
                        currentButton.BackColor = Color.Green;
                        buttonRussian4.BackColor = Color.Green;
                        buttonIsClicked = false;
                        currectWordSelectCount++;

                        if (currectWordSelectCount == 5)
                        {
                            DialogResult = MessageBox.Show("Вы угадали все слова хотите продолжить?", "Поздравление", MessageBoxButtons.OKCancel);
                            if (DialogResult == DialogResult.OK)
                            {
                                switch (categorySelectedChar)
                                {
                                    case 'n':
                                        wordCategoryComboBox_SelectedIndexChanged(1, e);
                                        break;
                                    case 'v':
                                        wordCategoryComboBox_SelectedIndexChanged(2, e);
                                        break;
                                    case 'a':
                                        wordCategoryComboBox_SelectedIndexChanged(3, e);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не верно, выбирете другое слово");
                    }
                }
                else
                {
                    currentButton = buttonRussian4;
                    keyWord = buttonRussian4.Text;
                    buttonIsClicked = true;
                }
            }
            else
            {
                MessageBox.Show("Для продолжения выбирете категорию слов");
            }
        }

        private void buttonRussian5_Click(object sender, EventArgs e)
        {
            if (isCategotySelected)
            {
                if (buttonIsClicked)
                {
                    if (valueWord == russianAndTatarchaDictionary[buttonRussian5.Text])
                    {
                        buttonRussian5.Enabled = false;
                        currentButton.Enabled = false;
                        currentButton.BackColor = Color.Green;
                        buttonRussian5.BackColor = Color.Green;
                        buttonIsClicked = false;
                        currectWordSelectCount++;

                        if (currectWordSelectCount == 5)
                        {
                            DialogResult = MessageBox.Show("Вы угадали все слова хотите продолжить?", "Поздравление", MessageBoxButtons.OKCancel);
                            if (DialogResult == DialogResult.OK)
                            {
                                switch (categorySelectedChar)
                                {
                                    case 'n':
                                        wordCategoryComboBox_SelectedIndexChanged(1, e);
                                        break;
                                    case 'v':
                                        wordCategoryComboBox_SelectedIndexChanged(2, e);
                                        break;
                                    case 'a':
                                        wordCategoryComboBox_SelectedIndexChanged(3, e);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не верно, выбирете другое слово");
                    }
                }
                else
                {
                    currentButton = buttonRussian5;
                    keyWord = buttonRussian5.Text;
                    buttonIsClicked = true;
                }
            }
            else
            {
                MessageBox.Show("Для продолжения выбирете категорию слов");
            }
        }

        private void buttonTatarcha1_Click(object sender, EventArgs e)
        {
            if (isCategotySelected)
            {
                if (buttonIsClicked)
                {
                    if (buttonTatarcha1.Text == russianAndTatarchaDictionary[keyWord])
                    {
                        buttonTatarcha1.Enabled = false;
                        currentButton.Enabled = false;
                        currentButton.BackColor = Color.Green;
                        buttonTatarcha1.BackColor = Color.Green;
                        buttonIsClicked = false;
                        currectWordSelectCount++;

                        if (currectWordSelectCount == 5)
                        {
                            DialogResult = MessageBox.Show("Вы угадали все слова хотите продолжить?", "Поздравление", MessageBoxButtons.OKCancel);
                            if (DialogResult == DialogResult.OK)
                            {
                                switch (categorySelectedChar)
                                {
                                    case 'n':
                                        wordCategoryComboBox_SelectedIndexChanged(1, e);
                                        break;
                                    case 'v':
                                        wordCategoryComboBox_SelectedIndexChanged(2, e);
                                        break;
                                    case 'a':
                                        wordCategoryComboBox_SelectedIndexChanged(3, e);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не верно, выбирете другое слово");
                    }
                }
                else
                {
                    currentButton = buttonTatarcha1;
                    valueWord = buttonTatarcha1.Text;
                    buttonIsClicked = true;
                }
            }
            else
            {
                MessageBox.Show("Для продолжения выбирете категорию слов");
            }
        }

        private void buttonTatarcha2_Click(object sender, EventArgs e)
        {
            if (isCategotySelected)
            {
                if (buttonIsClicked)
                {
                    if (buttonTatarcha2.Text == russianAndTatarchaDictionary[keyWord])
                    {
                        buttonTatarcha2.Enabled = false;
                        currentButton.Enabled = false;
                        currentButton.BackColor = Color.Green;
                        buttonTatarcha2.BackColor = Color.Green;
                        buttonIsClicked = false;
                        currectWordSelectCount++;

                        if (currectWordSelectCount == 5)
                        {
                            DialogResult = MessageBox.Show("Вы угадали все слова хотите продолжить?", "Поздравление", MessageBoxButtons.OKCancel);
                            if (DialogResult == DialogResult.OK)
                            {
                                switch (categorySelectedChar)
                                {
                                    case 'n':
                                        wordCategoryComboBox_SelectedIndexChanged(1, e);
                                        break;
                                    case 'v':
                                        wordCategoryComboBox_SelectedIndexChanged(2, e);
                                        break;
                                    case 'a':
                                        wordCategoryComboBox_SelectedIndexChanged(3, e);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не верно, выбирете другое слово");
                    }
                }
                else
                {
                    currentButton = buttonTatarcha2;
                    valueWord = buttonTatarcha2.Text;
                    buttonIsClicked = true;
                }
            }
            else
            {
                MessageBox.Show("Для продолжения выбирете категорию слов");
            }
        }

        private void buttonTatarcha3_Click(object sender, EventArgs e)
        {
            if (isCategotySelected)
            {
                if (buttonIsClicked)
                {
                    if (buttonTatarcha3.Text == russianAndTatarchaDictionary[keyWord])
                    {
                        buttonTatarcha3.Enabled = false;
                        currentButton.Enabled = false;
                        currentButton.BackColor = Color.Green;
                        buttonTatarcha3.BackColor = Color.Green;
                        buttonIsClicked = false;
                        currectWordSelectCount++;

                        if (currectWordSelectCount == 5)
                        {
                            DialogResult = MessageBox.Show("Вы угадали все слова хотите продолжить?", "Поздравление", MessageBoxButtons.OKCancel);
                            if (DialogResult == DialogResult.OK)
                            {
                                switch (categorySelectedChar)
                                {
                                    case 'n':
                                        wordCategoryComboBox_SelectedIndexChanged(1, e);
                                        break;
                                    case 'v':
                                        wordCategoryComboBox_SelectedIndexChanged(2, e);
                                        break;
                                    case 'a':
                                        wordCategoryComboBox_SelectedIndexChanged(3, e);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не верно, выбирете другое слово");
                    }
                }
                else
                {
                    currentButton = buttonTatarcha3;
                    valueWord = buttonTatarcha3.Text;
                    buttonIsClicked = true;
                }
            }
            else
            {
                MessageBox.Show("Для продолжения выбирете категорию слов");
            }
        }

        private void buttonTatarcha4_Click(object sender, EventArgs e)
        {
            if (isCategotySelected)
            {
                if (buttonIsClicked)
                {
                    if (buttonTatarcha4.Text == russianAndTatarchaDictionary[keyWord])
                    {
                        buttonTatarcha4.Enabled = false;
                        currentButton.Enabled = false;
                        currentButton.BackColor = Color.Green;
                        buttonTatarcha4.BackColor = Color.Green;
                        buttonIsClicked = false;
                        currectWordSelectCount++;

                        if (currectWordSelectCount == 5)
                        {
                            DialogResult = MessageBox.Show("Вы угадали все слова хотите продолжить?", "Поздравление", MessageBoxButtons.OKCancel);
                            if (DialogResult == DialogResult.OK)
                            {
                                switch (categorySelectedChar)
                                {
                                    case 'n':
                                        wordCategoryComboBox_SelectedIndexChanged(1, e);
                                        break;
                                    case 'v':
                                        wordCategoryComboBox_SelectedIndexChanged(2, e);
                                        break;
                                    case 'a':
                                        wordCategoryComboBox_SelectedIndexChanged(3, e);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не верно, выбирете другое слово");
                    }
                }
                else
                {
                    currentButton = buttonTatarcha4;
                    valueWord = buttonTatarcha4.Text;
                    buttonIsClicked = true;
                }
            }
            else
            {
                MessageBox.Show("Для продолжения выбирете категорию слов");
            }
        }

        private void buttonTatarcha5_Click(object sender, EventArgs e)
        {
            if (isCategotySelected)
            {
                if (buttonIsClicked)
                {
                    if (buttonTatarcha5.Text == russianAndTatarchaDictionary[keyWord])
                    {
                        buttonTatarcha5.Enabled = false;
                        currentButton.Enabled = false;
                        currentButton.BackColor = Color.Green;
                        buttonTatarcha5.BackColor = Color.Green;
                        buttonIsClicked = false;
                        currectWordSelectCount++;

                        if (currectWordSelectCount == 5)
                        {
                            DialogResult = MessageBox.Show("Вы угадали все слова хотите продолжить?", "Поздравление", MessageBoxButtons.OKCancel);
                            if (DialogResult == DialogResult.OK)
                            {
                                switch (categorySelectedChar)
                                {
                                    case 'n':
                                        wordCategoryComboBox_SelectedIndexChanged(1, e);
                                        break;
                                    case 'v':
                                        wordCategoryComboBox_SelectedIndexChanged(2, e);
                                        break;
                                    case 'a':
                                        wordCategoryComboBox_SelectedIndexChanged(3, e);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не верно, выбирете другое слово");
                    }
                }
                else
                {
                    currentButton = buttonTatarcha5;
                    valueWord = buttonTatarcha5.Text;
                    buttonIsClicked = true;
                }
            }
            else
            {
                MessageBox.Show("Для продолжения выбирете категорию слов");
            }
        }

        private void RemoveButtonsSeting()
        {
            currectWordSelectCount = 0;

            foreach(var button in tatarchaButtons)
            {
                button.Enabled = true;
                button.BackColor = Color.DarkSeaGreen;
            }

            foreach (var button in russianButtons)
            {
                button.Enabled = true;
                button.BackColor = Color.DarkSeaGreen;
            }
        }

        #endregion

        //Метод очистки игрового поля.
        private void removeButton_Click(object sender, EventArgs e)
        {
            RemoveButtonsSeting();

            switch (categorySelectedChar)
            {
                case 'n':
                    wordCategoryComboBox_SelectedIndexChanged(1, e);
                    break;
                case 'v':
                    wordCategoryComboBox_SelectedIndexChanged(2, e);
                    break;
                case 'a':
                    wordCategoryComboBox_SelectedIndexChanged(3, e);
                    break;
            }
        }
    }
}
