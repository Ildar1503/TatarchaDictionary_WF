using Dictionary.Services.Interfaces.HelppersInterfaces;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Dictionary.Services.Implementations.AnotherImplementations
{
    public class WorkWithTextElements : IWorkWithTextElements
    {
        //Удаление лишних запятых.
        public List<string> RemoveComa(List<string> listForRemoveTextElements)
        {
            List<string> listForReturn = new List<string>();

            for (int word = 0; word < listForRemoveTextElements.Count; word++)
            {
                //Переменная для проверки, чтобы не было повторной записи индекса.
                bool comaIsChecked = false;
                //Индекс с которого начинается удаление.
                int startRemoveIndex = 0;
                //Получение текущего слова.
                string currentWord = listForRemoveTextElements[word];

                for (int letter = 0; letter < listForRemoveTextElements[word].Length; letter++)
                {
                    if (currentWord[letter] == ',' && !comaIsChecked)
                    {
                        comaIsChecked = true;
                        startRemoveIndex = letter;
                        break;
                    }
                }

                //Проверка на получение индекса, чтобы удалить запятую, если она есть.
                if (startRemoveIndex > 0)
                {
                    int endRemoveIndex = currentWord.Length - startRemoveIndex;
                    currentWord = currentWord.Remove(startRemoveIndex, endRemoveIndex);
                }

                listForReturn.Add(currentWord);
            }

            return listForReturn;
        }

        //Удаление лишних точек запятых.
        public List<string> RemoveDottedComa(List<string> listForRemoveTextElements)
        {
            List<string> listForReturn = new List<string>();

            for (int word = 0; word < listForRemoveTextElements.Count; word++)
            {
                //Переменная для проверки, чтобы не было повторной записи индекса.
                bool comaIsChecked = false;
                //Индекс с которого начинается удаление.
                int startRemoveIndex = 0;
                //Получение текущего слова.
                string currentWord = listForRemoveTextElements[word];

                for (int letter = 0; letter < listForRemoveTextElements[word].Length; letter++)
                {
                    if (currentWord[letter] == ';' && !comaIsChecked)
                    {
                        comaIsChecked = true;
                        startRemoveIndex = letter;
                        break;
                    }
                }

                //Проверка на получение индекса, чтобы удалить запятую, если она есть.
                if (startRemoveIndex > 0)
                {
                    int endRemoveIndex = currentWord.Length - startRemoveIndex;
                    currentWord = currentWord.Remove(startRemoveIndex, endRemoveIndex);
                }

                listForReturn.Add(currentWord);
            }

            return listForReturn;
        }

        //Удаление первых и последних пробелов из листа.
        public List<string> RemoveSpacesFromList(List<string> listForRemoveTextElements)
        {
            List<string> listForReturn = new List<string>();

            for (int word = 0; word < listForRemoveTextElements.Count; word++)
            {
                string curWord = listForRemoveTextElements[word];
                int finishIndex = 0;

                //Подсчет всех пробелов до слова.
                while (curWord[finishIndex] == ' ' && finishIndex < curWord.Length)
                {
                    finishIndex++;
                }

                //Удаление всех оставшихся пробелов после слова, если есть таковые.
                if (curWord[curWord.Length - 1] == ' ')
                {
                    curWord = curWord.Remove(curWord.Length - 1);
                }

                curWord = curWord.Remove(0, finishIndex);

                listForReturn.Add(curWord);
            }

            return listForReturn;
        }

        //Получение первого слова из строки в лист.
        public List<string> GetFirstWordFromList(List<string> listForRemoveTextElements)
        {
            List<string> listForReturn = new List<string>();

            for (int word = 0; word < listForRemoveTextElements.Count; word++)
            {
                string wordForList = string.Empty;
                //Получение слова.
                string currentWord = listForRemoveTextElements[word];
                //Проверка найденного слова.
                bool wordIsChecked = false;

                if (!wordIsChecked)
                {
                    for (int letter = 0; letter < currentWord.Length; letter++)
                    {
                        if (currentWord[letter] == ',' || currentWord[letter] == ';')
                        {
                            //Проверка на наличие пробела до символов.
                            if (currentWord[letter - 1] == ' ')
                            {
                                wordForList = wordForList.Remove(letter - 1);
                            }
                            else
                            {
                                wordForList = wordForList.Remove(letter);
                            }
                            wordIsChecked = true;
                            break;
                        }
                        else
                        {
                            wordForList += currentWord[letter];
                        }
                    }
                }

                listForReturn.Add(wordForList);
            }

            return listForReturn;
        }

        //Получение первого слова из строки.
        public string GetFirstWordFromString(string text)
        {
            string wordForReturn = string.Empty;

            for (int letter = 0; letter < text.Length; letter++)
            {
                if (text[letter] == ',' || text[letter] == ';')
                {
                    //Проверка на наличие пробела до символов.
                    if (text[letter - 1] == ' ')
                    {
                        wordForReturn = wordForReturn.Remove(letter - 1);
                    }
                    else
                    {
                        wordForReturn = wordForReturn.Remove(letter);
                    }
                    break;
                }
                else
                {
                    wordForReturn += text[letter];
                }
            }

            return wordForReturn;
        }

        //Удаление первых и последних пробелов из строки.
        public string RemoveSpacesFromString(string text)
        {
            int finishIndex = 0;

            for (int letter = 0; letter < text.Length; letter++)
            {
                //Подсчет всех пробелов до слова.
                while (text[finishIndex] == ' ' && finishIndex < text.Length)
                {
                    finishIndex++;
                }

                //Удаление всех оставшихся пробелов после слова, если есть таковые.
                if (text[text.Length - 1] == ' ')
                {
                    text = text.Remove(text.Length - 1);
                }
            }

            return text.Remove(0, finishIndex);
        }

        //Подсчет слов в подстроке листа.
        public Dictionary<int, int> WordCounterFromList(List<string> listForWordCount)
        {
            //Словарь для подсчета количества слов.
            Dictionary<int, int> wordsCounterDictionary = new Dictionary<int, int>();

            for(int word = 0; word < listForWordCount.Count; word++)
            {
                int wordCounter = 1;
                string curWord = listForWordCount[word];

                for (int letter = 0; letter < curWord.Length; letter++)
                {
                    if (curWord[letter] == ',' || curWord[letter] == ';')
                    {
                        wordCounter++;
                    }
                }

                wordsCounterDictionary.Add(word, wordCounter);
            }

            return wordsCounterDictionary;
        }

        //Подсчет слов в строке.
        public (int, int) WordCounterFromString(string text, int keyDictionary)
        {
            int wordCounter = 1;

            for (int letter = 0; letter < text.Length; letter++)
            {
                if (text[letter] == ',' || text[letter] == ';')
                {
                    wordCounter++;   
                }
            }

            return (keyDictionary, wordCounter);
        }

        //Получение слов из строки.
        public List<string> GetWordsFromString(string text)
        {
            //Переменная для временной записи слова.
            string wordTemp = string.Empty;
            List<string> listForReturn = new List<string>();

            for(int textIndex = 0; textIndex < text.Length; textIndex++)
            {
                if(text[textIndex] == ';' || text[textIndex] == ',')
                {
                    //Запись слова в лист и перезапись переменной.
                    listForReturn.Add(wordTemp);
                    wordTemp = string.Empty;
                }
                else if (textIndex == text.Length - 1 && text[textIndex] != ' ')
                {
                    wordTemp += text[textIndex];
                    //Запись слова в лист и перезапись переменной.
                    listForReturn.Add(wordTemp);
                    wordTemp = string.Empty;
                }
                else
                {
                    wordTemp += text[textIndex];
                }
            }

            return listForReturn;
        }
    }
}
