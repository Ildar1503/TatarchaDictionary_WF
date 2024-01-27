using Dictionary.BLL.BaseResponse;
using Dictionary.Dal.Interfaces;
using Dictionary.Services.Interfaces;
using Dictionary.Services.Interfaces.HelppersInterfaces;
using Dictionary.Services.Interfaces.SortInterfaces;
using DictionaryTatarcha.Entities;

namespace Dictionary.Services.Implementations
{
    public class NounServise : IWordServise<Noun>
    {
        //Зависимости.
        private readonly IDictionaryDal<Noun> _dictionary;
        private readonly IWorkWithTextElements _workTextElements;
        private readonly ISelectionSort _sort;

        //Лист для записи id слов от отсортированного листа.
        private List<int> idsList = new List<int>();
        //Лист, который будет служить в качестве передачи всех отсортированных данных.
        private List<Noun> sortedNounsList = new List<Noun>();
        //Лист для получения первых слов из строки с словами.
        private List<string> firstWordsListFromSortedList = new List<string>();
        //Словарь для подсчета количества слов.
        private Dictionary<int, int> wordsCountedDictionary = new Dictionary<int, int>();
        //Лист для записи слов в алфавитном порядке.
        private List<string> alphabetSortList = new List<string>();
        //Лист для получения листа с удленными пробелами.
        private List<string> spacesRemoveList = new List<string>();

        public NounServise(IDictionaryDal<Noun> dictionary, IWorkWithTextElements removeTextElements, ISelectionSort sort)
        {
            _workTextElements = removeTextElements;
            _sort = sort;
            _dictionary = dictionary;
        }

        //Получение русских слов.
        public async Task<DataBaseResponse<List<string>>> GetRussianWords()
        {
            //Очистка листов.
            idsList.Clear();
            sortedNounsList.Clear();
            firstWordsListFromSortedList.Clear();
            wordsCountedDictionary.Clear();

            DataBaseResponse<List<string>> baseResponse = new DataBaseResponse<List<string>>();
            try
            {
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    //Лист для русских слов.
                    List<string> russianNouns = new List<string>();
                    //Добавление слов.
                    foreach(var word in response)
                    {
                        russianNouns.Add(word.NounTranslate);
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = _workTextElements.RemoveSpacesFromList(russianNouns);
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<string>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Получение татарских слов.
        public async Task<DataBaseResponse<List<string>>> GetTatarchaWords()
        {
            //Очистка листов.
            idsList.Clear();
            sortedNounsList.Clear();
            firstWordsListFromSortedList.Clear();
            wordsCountedDictionary.Clear();

            DataBaseResponse<List<string>> baseResponse = new DataBaseResponse<List<string>>();
            try
            {
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    //Лист для татарских слов.
                    List<string> tatarchaNouns = new List<string>();
                    //Добавление слов.
                    foreach (var word in response)
                    {
                        tatarchaNouns.Add(word.NounWord);
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = _workTextElements.RemoveSpacesFromList(tatarchaNouns);
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<string>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Сортировка в алфавитном порядке.
        public async Task<DataBaseResponse<List<Noun>>> SortByAlphabetWords(List<string> listWords, string language)
        {
            //Очистка листов.
            idsList.Clear();
            sortedNounsList.Clear();

            DataBaseResponse<List<Noun>> baseResponse = new DataBaseResponse<List<Noun>>();
            try
            {
                //Заполнение второго листа.
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    //Удаление пробелов.
                    alphabetSortList = _workTextElements.RemoveSpacesFromList(listWords);
                    //Сортировка листа.
                    alphabetSortList.Sort();
                    //Проверка выбранного языка.
                    switch (language.ToLower())
                    {
                        case "russian":
                            for (int word = 0; word < alphabetSortList.Count; word++)
                            {
                                foreach (Noun noun in response)
                                {
                                    noun.NounTranslate = _workTextElements.RemoveSpacesFromString(noun.NounTranslate);
                                    if (alphabetSortList[word] == noun.NounTranslate)
                                    {
                                        idsList.Add(noun.Id);
                                        break;
                                    }
                                }
                            }
                            break;
                        case "tatarcha":
                            for (int word = 0; word < alphabetSortList.Count; word++)
                            {
                                foreach (Noun noun in response)
                                {
                                    noun.NounWord = _workTextElements.RemoveSpacesFromString(noun.NounWord);
                                    if (alphabetSortList[word] == noun.NounWord)
                                    {
                                        idsList.Add(noun.Id);
                                        break;
                                    }
                                }
                            }
                            break;
                    }

                    //Перезапись листа.
                    foreach (var id in idsList)
                    {
                        foreach (var noun in response)
                        {
                            if (noun.Id == id)
                            {
                                sortedNounsList.Add(
                                    new Noun
                                    {
                                        NounTranslate = _workTextElements.RemoveSpacesFromString(noun.NounTranslate),
                                        NounWord = _workTextElements.RemoveSpacesFromString(noun.NounWord)
                                    }
                                );
                                break;
                            }
                        }
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = sortedNounsList;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Noun>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Сортировка по количеству переводов.
        public async Task<DataBaseResponse<List<Noun>>> SortByTranslateWords(List<string> listWords)
        {
            //Очистка листов.
            idsList.Clear();
            sortedNounsList.Clear();

            DataBaseResponse<List<Noun>> baseResponse = new DataBaseResponse<List<Noun>>();
            try
            {
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    spacesRemoveList = _workTextElements.RemoveSpacesFromList(listWords);
                    //Подсчет количества слов.
                    wordsCountedDictionary = _workTextElements.WordCounterFromList(spacesRemoveList);
                    //Сортировка словаря.
                    wordsCountedDictionary = _sort.SelectionSortDictionary(wordsCountedDictionary);

                    //Переменная для получения ключа.
                    int dictionaryKey = 0;

                    //Заполнение id слов.
                    foreach (var word in wordsCountedDictionary)
                    {
                        foreach (var noun in response)
                        {
                            noun.NounWord = _workTextElements.RemoveSpacesFromString(noun.NounWord);

                            //Из строки сначала будет получено колисчество слов в подстроке.
                            if ((word.Key, word.Value) == _workTextElements.WordCounterFromString(noun.NounWord, dictionaryKey))
                            {
                                //Добавление уникального id, чтобы не допустить повторения слов.
                                if (!idsList.Contains(noun.Id))
                                {
                                    idsList.Add(noun.Id);
                                    break;
                                }
                            }
                        }
                        dictionaryKey++;
                    }

                    //Перезапись листа.
                    for (int id = 0; id < idsList.Count; id++)
                    {
                        foreach (var noun in response)
                        {
                            if (noun.Id == idsList[id])
                            {
                                sortedNounsList.Add(
                                    new Noun
                                    {
                                        NounTranslate = _workTextElements.RemoveSpacesFromString(noun.NounTranslate),
                                        NounWord = _workTextElements.RemoveSpacesFromString(noun.NounWord)
                                    }
                                );
                                break;
                            }
                        }
                    }

                    baseResponse.Data = sortedNounsList;
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Noun>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Сортировка от большего слова к меньшему.
        public async Task<DataBaseResponse<List<Noun>>> SortByWordFromLongerWords(List<string> listWords, string language)
        {
            //Очистка листов.
            idsList.Clear();
            sortedNounsList.Clear();

            DataBaseResponse<List<Noun>> baseResponse = new DataBaseResponse<List<Noun>>();
            try
            {
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    //Очистка лишних пробелов.
                    firstWordsListFromSortedList = _workTextElements.RemoveSpacesFromList(listWords);
                    //Получение на основе отсортированного листа первые слова из строки слов.
                    firstWordsListFromSortedList = _workTextElements.GetFirstWordFromList(firstWordsListFromSortedList);
                    //Сортировка на основе первых слов.
                    firstWordsListFromSortedList = _sort.SelectionSortList(firstWordsListFromSortedList);

                    //Проверка выбранного языка.
                    switch (language)
                    {
                        case "russian":
                            //Заполнение id слов.
                            foreach (var word in firstWordsListFromSortedList)
                            {
                                foreach (var noun in response)
                                {
                                    //Получение первого слова из строки.
                                    string currentNoun = _workTextElements.GetFirstWordFromString(noun.NounTranslate);
                                    //Удаление пробелов, если они есть.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentNoun))
                                    {
                                        if (!idsList.Contains(noun.Id))
                                        {
                                            idsList.Add(noun.Id);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        case "tatarcha":
                            //Заполнение id слов.
                            foreach (var word in firstWordsListFromSortedList)
                            {
                                foreach (var noun in response)
                                {
                                    //Получение первого слова из строки.
                                    string currentNoun = _workTextElements.GetFirstWordFromString(noun.NounWord);
                                    //Удаление пробелов, если они есть.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentNoun))
                                    {
                                        if (!idsList.Contains(noun.Id))
                                        {
                                            idsList.Add(noun.Id);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                    }

                    //Перезапись листа.
                    for (int id = 0; id < idsList.Count; id++)
                    {
                        foreach (var noun in response)
                        {
                            if (noun.Id == idsList[id] && language == "russian")
                            {
                                sortedNounsList.Add(
                                    new Noun
                                    {
                                        NounTranslate = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id]),
                                        NounWord = _workTextElements.RemoveSpacesFromString(noun.NounWord)
                                    }
                                );
                                break;
                            }

                            if (noun.Id == idsList[id] && language == "tatarcha")
                            {
                                sortedNounsList.Add(
                                    new Noun
                                    {
                                        NounTranslate = _workTextElements.RemoveSpacesFromString(noun.NounTranslate),
                                        NounWord = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id])
                                    }
                                );
                                break;
                            }
                        }
                    }

                    sortedNounsList.Reverse();
                    baseResponse.Data = sortedNounsList;
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Noun>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Сортировка от меньшего слова к большему.
        public async Task<DataBaseResponse<List<Noun>>> SortByWordFromShorterWords(List<string> listWords, string language)
        {
            //Очистка листов.
            idsList.Clear();
            sortedNounsList.Clear();

            DataBaseResponse<List<Noun>> baseResponse = new DataBaseResponse<List<Noun>>();
            try
            {
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    //Очистка лишних пробелов.
                    firstWordsListFromSortedList = _workTextElements.RemoveSpacesFromList(listWords);
                    //Получение на основе отсортированного листа первые слова из строки слов.
                    firstWordsListFromSortedList = _workTextElements.GetFirstWordFromList(firstWordsListFromSortedList);
                    //Сортировка на основе первых слов.
                    firstWordsListFromSortedList = _sort.SelectionSortList(firstWordsListFromSortedList);

                    //Проверка выбранного языка.
                    switch (language)
                    {
                        case "russian":
                            //Заполнение id слов.
                            foreach (var word in firstWordsListFromSortedList)
                            {
                                foreach (var noun in response)
                                {
                                    //Получение первого слова из строки.
                                    string currentNoun = _workTextElements.GetFirstWordFromString(noun.NounTranslate);
                                    //Удаление пробелов, если они есть.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentNoun))
                                    {
                                        if (!idsList.Contains(noun.Id))
                                        {
                                            idsList.Add(noun.Id);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                        case "tatarcha":
                            //Заполнение id слов.
                            foreach (var word in firstWordsListFromSortedList)
                            {
                                foreach (var noun in response)
                                {
                                    //Получение первого слова из строки.
                                    string currentNoun = _workTextElements.GetFirstWordFromString(noun.NounWord);
                                    //Удаление пробелов, если они есть.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentNoun))
                                    {
                                        if (!idsList.Contains(noun.Id))
                                        {
                                            idsList.Add(noun.Id);
                                            break;
                                        }
                                    }
                                }
                            }
                            break;
                    }

                    //Перезапись листа.
                    for (int id = 0; id < idsList.Count; id++)
                    {
                        foreach (var noun in response)
                        {
                            if (noun.Id == idsList[id] && language == "russian")
                            {
                                sortedNounsList.Add(
                                    new Noun
                                    {
                                        NounTranslate = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id]),
                                        NounWord = _workTextElements.RemoveSpacesFromString(noun.NounWord)
                                    }
                                );
                                break;
                            }

                            if (noun.Id == idsList[id] && language == "tatarcha")
                            {
                                sortedNounsList.Add(
                                    new Noun
                                    {
                                        NounTranslate = _workTextElements.RemoveSpacesFromString(noun.NounTranslate),
                                        NounWord = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id])
                                    }
                                );
                                break;
                            }
                        }
                    }

                    baseResponse.Data = sortedNounsList;
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                }
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Noun>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Получение слов в виде коллекиции.
        public async Task<DataBaseResponse<List<Noun>>> GetWords()
        {
            //Очистка листов.
            idsList.Clear();
            sortedNounsList.Clear();
            firstWordsListFromSortedList.Clear();
            wordsCountedDictionary.Clear();

            DataBaseResponse<List<Noun>> baseResponse = new DataBaseResponse<List<Noun>>();
            try
            {
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    //Лист для русских слов.
                    List<string> russianNouns = new List<string>();
                    //Лист для татарских слов.
                    List<string> tatarcharNouns = new List<string>();
                    //Добавление слов.
                    foreach (var word in response)
                    {
                        russianNouns.Add(_workTextElements.RemoveSpacesFromString(word.NounTranslate));
                        tatarcharNouns.Add(_workTextElements.RemoveSpacesFromString(word.NounWord));
                    }
                    //Заполнение коллкции на возвращение из метода.
                    if (tatarcharNouns.Count == russianNouns.Count)
                    {
                        for (int index = 0; index < russianNouns.Count; index++)
                        {
                            sortedNounsList.Add(
                                new Noun
                                {
                                    NounWord = _workTextElements.GetFirstWordFromString(tatarcharNouns[index]),
                                    NounTranslate = _workTextElements.GetFirstWordFromString(russianNouns[index])
                                });
                        }
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = sortedNounsList;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Noun>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Получение искомого слова/слов.
        public async Task<DataBaseResponse<List<Noun>>> GetSearchWord(string searchWord)
        {
            //Очистка листа.
            sortedNounsList.Clear();

            DataBaseResponse<List<Noun>> baseResponse = new DataBaseResponse<List<Noun>>();
            try
            {
                List<string> wordsFromSting = new List<string>();

                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    foreach (var word in response)
                    {
                        word.NounWord = _workTextElements.RemoveSpacesFromString(word.NounWord);
                        word.NounTranslate = _workTextElements.RemoveSpacesFromString(word.NounTranslate);

                        if (word.NounTranslate == searchWord)
                        {
                            sortedNounsList.Add(
                                new Noun
                                {
                                    NounWord = word.NounWord,
                                    NounTranslate = word.NounTranslate
                                });
                        }

                        //Очистка и перезапись листа новыми словами.
                        wordsFromSting.Clear();
                        wordsFromSting = _workTextElements.GetWordsFromString(word.NounWord);

                        //Перебор листа с татарскими словами.
                        foreach (var curStrWord in wordsFromSting)
                        {
                            //Удаление пробелов, если они есть.
                            string tempWord = _workTextElements.RemoveSpacesFromString(curStrWord);

                            if (tempWord == searchWord)
                            {
                                sortedNounsList.Add(
                                new Noun
                                {
                                    NounWord = tempWord,
                                    NounTranslate = word.NounTranslate
                                });
                            }
                        }
                    }

                    //Проверка нахождения слова.
                    if (sortedNounsList.Count >= 1)
                    {
                        baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                        baseResponse.Data = sortedNounsList;
                    }
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Noun>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Добавление нового слова.
        public async Task<DataBaseResponse<List<Noun>>> AddNewWord(string russianWord, string tatarchaWord)
        {
            DataBaseResponse<List<Noun>> baseResponse = new DataBaseResponse<List<Noun>>();
            try
            {
                Noun wordForAdd = new Noun { NounTranslate = russianWord, NounWord = tatarchaWord };

                var response = await _dictionary.Add(wordForAdd);
                if (response)
                {
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    //После добаления слова вернет обновленную таблицу со словами.
                    baseResponse.Data = await _dictionary.GetAll();
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Noun>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Обновление слова.
        public async Task<DataBaseResponse<List<Noun>>> UpdateWord(Noun wordForUpdate, int id)
        {
            DataBaseResponse<List<Noun>> baseResponse = new DataBaseResponse<List<Noun>>();
            try
            {
                var response = await _dictionary.Update(wordForUpdate, id);
                if (response)
                {
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    //После обновления слова вернет обновленную таблицу со словами.
                    baseResponse.Data = await _dictionary.GetAll();
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Noun>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Удаление слова.
        public async Task<DataBaseResponse<List<Noun>>> DeleteWord(int wordId)
        {
            DataBaseResponse<List<Noun>> baseResponse = new DataBaseResponse<List<Noun>>();
            try
            {
                var response = await _dictionary.Delete(wordId);
                if (response)
                {
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    //После удаления слова вернет обновленную таблицу со словами.
                    baseResponse.Data = await _dictionary.GetAll();
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Noun>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Получение слов.
        public async Task<DataBaseResponse<List<Noun>>> GetAllWords()
        {
            DataBaseResponse<List<Noun>> baseResponse = new DataBaseResponse<List<Noun>>();
            try
            {
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    //Красиво оформляю слова в коллекции.
                    foreach (var word in response)
                    {
                        word.NounWord = _workTextElements.RemoveSpacesFromString(word.NounWord);
                        word.NounTranslate = _workTextElements.RemoveSpacesFromString(word.NounTranslate);
                    }

                    baseResponse.Data = response;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Noun>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }
    }
}
