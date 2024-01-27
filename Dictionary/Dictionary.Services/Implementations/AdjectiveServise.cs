using Azure;
using Dictionary.BLL.BaseResponse;
using Dictionary.Dal.Interfaces;
using Dictionary.Services.Interfaces;
using Dictionary.Services.Interfaces.HelppersInterfaces;
using Dictionary.Services.Interfaces.SortInterfaces;
using DictionaryTatarcha.Entities;

namespace Dictionary.Services.Implementations
{
    public class AdjectiveServise : IWordServise<Adjective>
    {
        //Зависимости.
        private readonly IDictionaryDal<Adjective> _dictionary;
        private readonly IWorkWithTextElements _workTextElements;
        private readonly ISelectionSort _sort;

        //Лист для записи id слов от отсортированного словаря.
        private List<int> idsList = new List<int>();
        //Лист, который будет служить в качестве передачи всех отсортированных данных.
        private List<Adjective> sortedAdjectiviesList = new List<Adjective>();
        //Лист для получения первых слов из строки с словами.
        private List<string> firstWordsListFromSortedList = new List<string>();
        //Словарь для подсчета количества слов.
        private Dictionary<int, int> wordsCountedDictionary = new Dictionary<int, int>();
        //Лист для записи слов в алфавитном порядке.
        private List<string> alphabetSortList = new List<string>();
        //Лист для получения листа с удленными пробелами.
        private List<string> spacesRemoveList = new List<string>();

        public AdjectiveServise(IDictionaryDal<Adjective> dictionary, IWorkWithTextElements removeTextElements, ISelectionSort sort)
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
            sortedAdjectiviesList.Clear();
            firstWordsListFromSortedList.Clear();
            wordsCountedDictionary.Clear();

            DataBaseResponse<List<string>> baseResponse = new DataBaseResponse<List<string>>();
            try
            {
                var response = await _dictionary.GetAll();
                if(response != null)
                {
                    //Лист для русских слов.
                    List<string> russianAdjectives = new List<string>();
                    //Добавление слов.
                    foreach(var word in response)
                    {
                        russianAdjectives.Add(word.AdjectiveTranslate);
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = _workTextElements.RemoveSpacesFromList(russianAdjectives);
                }
                return baseResponse;
            }
            catch(Exception ex)
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
            sortedAdjectiviesList.Clear();
            firstWordsListFromSortedList.Clear();
            wordsCountedDictionary.Clear();

            DataBaseResponse<List<string>> baseResponse = new DataBaseResponse<List<string>>();
            try
            {
                var response = await _dictionary.GetAll();
                if(response != null)
                {
                    //Лист для татарских слов.
                    List<string> tatarchaAdjectives = new List<string>();
                    //Добавление слов.
                    foreach(var word in response)
                    {
                        tatarchaAdjectives.Add(word.AdjectiveWord);
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = _workTextElements.RemoveSpacesFromList(tatarchaAdjectives);
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
        public async Task<DataBaseResponse<List<Adjective>>> SortByAlphabetWords(List<string> listWords, string language)
        {
            //Очистка листов.
            idsList.Clear();
            sortedAdjectiviesList.Clear();

            DataBaseResponse<List<Adjective>> baseResponse = new DataBaseResponse<List<Adjective>>();
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
                                foreach (Adjective adjective in response)
                                {
                                    adjective.AdjectiveTranslate = _workTextElements.RemoveSpacesFromString(adjective.AdjectiveTranslate);
                                    if (alphabetSortList[word] == adjective.AdjectiveTranslate)
                                    {
                                        idsList.Add(adjective.Id);
                                        break;
                                    }
                                }
                            }
                            break;
                        case "tatarcha":
                            for (int word = 0; word < alphabetSortList.Count; word++)
                            {
                                foreach (Adjective adjective in response)
                                {
                                    adjective.AdjectiveWord = _workTextElements.RemoveSpacesFromString(adjective.AdjectiveWord);
                                    if (alphabetSortList[word] == adjective.AdjectiveWord)
                                    {
                                        idsList.Add(adjective.Id);
                                        break;
                                    }
                                }
                            }
                            break;
                    }

                    //Перезапись листа.
                    foreach (var id in idsList)
                    {
                        foreach (var adjective in response)
                        {
                            if (adjective.Id == id)
                            {
                                sortedAdjectiviesList.Add(
                                    new Adjective
                                    {
                                        AdjectiveTranslate = _workTextElements.RemoveSpacesFromString(adjective.AdjectiveTranslate),
                                        AdjectiveWord = _workTextElements.RemoveSpacesFromString(adjective.AdjectiveWord)
                                    }
                                );
                                break;
                            }
                        }
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = sortedAdjectiviesList;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Adjective>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Сортировка по количеству переводов.
        public async Task<DataBaseResponse<List<Adjective>>> SortByTranslateWords(List<string> listWords)
        {
            //Очистка листов.
            idsList.Clear();
            sortedAdjectiviesList.Clear();

            DataBaseResponse<List<Adjective>> baseResponse = new DataBaseResponse<List<Adjective>>();
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
                        foreach (var adjective in response)
                        {
                            adjective.AdjectiveWord = _workTextElements.RemoveSpacesFromString(adjective.AdjectiveWord);

                            //Из строки сначала будет получено колисчество слов в подстроке.
                            if ((word.Key, word.Value) == _workTextElements.WordCounterFromString(adjective.AdjectiveWord, dictionaryKey))
                            {
                                //Добавление уникального id, чтобы не допустить повторения слов.
                                if (!idsList.Contains(adjective.Id))
                                {
                                    idsList.Add(adjective.Id);
                                    break;
                                }
                            }
                        }
                        dictionaryKey++;
                    }

                    //Перезапись листа.
                    for (int id = 0; id < idsList.Count; id++)
                    {
                        foreach (var adjective in response)
                        {
                            if (adjective.Id == idsList[id])
                            {
                                sortedAdjectiviesList.Add(
                                    new Adjective
                                    {
                                        AdjectiveTranslate = _workTextElements.RemoveSpacesFromString(adjective.AdjectiveTranslate),
                                        AdjectiveWord = _workTextElements.RemoveSpacesFromString(adjective.AdjectiveWord)
                                    }
                                );
                                break;
                            }
                        }
                    }

                    baseResponse.Data = sortedAdjectiviesList;
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Adjective>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }
        
        //Сортировка от большего слова к меньшему.
        public async Task<DataBaseResponse<List<Adjective>>> SortByWordFromLongerWords(List<string> listWords, string language)
        {
            //Очистка листов.
            idsList.Clear();
            sortedAdjectiviesList.Clear();

            DataBaseResponse<List<Adjective>> baseResponse = new DataBaseResponse<List<Adjective>>();
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
                                foreach (var adjective in response)
                                {
                                    //Получение первого слова из строки.
                                    string currentAdjective = _workTextElements.GetFirstWordFromString(adjective.AdjectiveTranslate);
                                    //Удаление пробелов, если они есть.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentAdjective))
                                    {
                                        if (!idsList.Contains(adjective.Id))
                                        {
                                            idsList.Add(adjective.Id);
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
                                foreach (var adjective in response)
                                {
                                    //Получение первого слова из строки.
                                    string currentAdjective = _workTextElements.GetFirstWordFromString(adjective.AdjectiveWord);
                                    //Удаление пробелов, если они есть.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentAdjective))
                                    {
                                        if (!idsList.Contains(adjective.Id))
                                        {
                                            idsList.Add(adjective.Id);
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
                        foreach (var adjective in response)
                        {
                            if (adjective.Id == idsList[id] && language == "russian")
                            {
                                sortedAdjectiviesList.Add(
                                    new Adjective
                                    {
                                        AdjectiveTranslate = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id]),
                                        AdjectiveWord = _workTextElements.RemoveSpacesFromString(adjective.AdjectiveWord)
                                    }
                                );
                                break;
                            }

                            if (adjective.Id == idsList[id] && language == "tatarcha")
                            {
                                sortedAdjectiviesList.Add(
                                    new Adjective
                                    {
                                        AdjectiveTranslate = _workTextElements.RemoveSpacesFromString(adjective.AdjectiveTranslate),
                                        AdjectiveWord = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id])
                                    }
                                );
                                break;
                            }
                        }
                    }

                    sortedAdjectiviesList.Reverse();
                    baseResponse.Data = sortedAdjectiviesList;
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Adjective>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Сортировка от меньшего слова к большему.
        public async Task<DataBaseResponse<List<Adjective>>> SortByWordFromShorterWords(List<string> listWords, string language)
        {
            //Очистка листов.
            idsList.Clear();
            sortedAdjectiviesList.Clear();

            DataBaseResponse<List<Adjective>> baseResponse = new DataBaseResponse<List<Adjective>>();
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
                                foreach (var adjective in response)
                                {
                                    //Получение первого слова из строки.
                                    string currentAdjective = _workTextElements.GetFirstWordFromString(adjective.AdjectiveTranslate);
                                    //Удаление пробелов, если они есть.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentAdjective))
                                    {
                                        if (!idsList.Contains(adjective.Id))
                                        {
                                            idsList.Add(adjective.Id);
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
                                foreach (var adjective in response)
                                {
                                    //Получение первого слова из строки.
                                    string currentAdjective = _workTextElements.GetFirstWordFromString(adjective.AdjectiveWord);
                                    //Удаление пробелов, если они есть.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentAdjective))
                                    {
                                        if (!idsList.Contains(adjective.Id))
                                        {
                                            idsList.Add(adjective.Id);
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
                        foreach (var adjective in response)
                        {
                            if (adjective.Id == idsList[id] && language == "russian")
                            {
                                sortedAdjectiviesList.Add(
                                    new Adjective
                                    {
                                        AdjectiveTranslate = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id]),
                                        AdjectiveWord = _workTextElements.RemoveSpacesFromString(adjective.AdjectiveWord)
                                    }
                                );
                                break;
                            }

                            if (adjective.Id == idsList[id] && language == "tatarcha")
                            {
                                sortedAdjectiviesList.Add(
                                    new Adjective
                                    {
                                        AdjectiveTranslate = _workTextElements.RemoveSpacesFromString(adjective.AdjectiveTranslate),
                                        AdjectiveWord = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id])
                                    }
                                );
                                break;
                            }
                        }
                    }

                    baseResponse.Data = sortedAdjectiviesList;
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Adjective>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Получение всех слов в виде коллекции.
        public async Task<DataBaseResponse<List<Adjective>>> GetWords()
        {
            //Очистка листов.
            idsList.Clear();
            sortedAdjectiviesList.Clear();
            firstWordsListFromSortedList.Clear();
            wordsCountedDictionary.Clear();

            DataBaseResponse<List<Adjective>> baseResponse = new DataBaseResponse<List<Adjective>>();
            try
            {
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    //Лист для русских слов.
                    List<string> russianAdjectives = new List<string>();
                    //Лист для татарских слов.
                    List<string> tatarcharAdjectivies = new List<string>();
                    //Добавление слов.
                    foreach (var word in response)
                    {
                        russianAdjectives.Add(_workTextElements.RemoveSpacesFromString(word.AdjectiveTranslate));
                        tatarcharAdjectivies.Add(_workTextElements.RemoveSpacesFromString(word.AdjectiveWord));
                    }
                    //Заполнение коллкции на возвращение из метода.
                    if (tatarcharAdjectivies.Count == russianAdjectives.Count)
                    {
                        for (int index = 0; index < russianAdjectives.Count; index++)
                        {
                            sortedAdjectiviesList.Add(
                                new Adjective
                                {
                                    AdjectiveWord = _workTextElements.GetFirstWordFromString(tatarcharAdjectivies[index]),
                                    AdjectiveTranslate = _workTextElements.GetFirstWordFromString(russianAdjectives[index])
                                });
                        }
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = sortedAdjectiviesList;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Adjective>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Получение искомого слова/слов.
        public async Task<DataBaseResponse<List<Adjective>>> GetSearchWord(string searchWord)
        {
            //Очистка листа.
            sortedAdjectiviesList.Clear();

            DataBaseResponse<List<Adjective>> baseResponse = new DataBaseResponse<List<Adjective>>();
            try
            {
                List<string> wordsFromSting = new List<string>();

                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    foreach (var word in response)
                    {
                        word.AdjectiveWord = _workTextElements.RemoveSpacesFromString(word.AdjectiveWord);
                        word.AdjectiveTranslate = _workTextElements.RemoveSpacesFromString(word.AdjectiveTranslate);

                        if (word.AdjectiveTranslate == searchWord)
                        {
                            sortedAdjectiviesList.Add(
                                new Adjective
                                {
                                    AdjectiveWord = word.AdjectiveWord,
                                    AdjectiveTranslate = word.AdjectiveTranslate
                                });
                        }

                        //Очистка и перезапись листа новыми словами.
                        wordsFromSting.Clear();
                        wordsFromSting = _workTextElements.GetWordsFromString(word.AdjectiveWord);

                        //Перебор листа с татарскими словами.
                        foreach (var curStrWord in wordsFromSting)
                        {
                            //Удаление пробелов, если они есть.
                            string tempWord = _workTextElements.RemoveSpacesFromString(curStrWord);

                            if (tempWord == searchWord)
                            {
                                sortedAdjectiviesList.Add(
                                new Adjective
                                {
                                    AdjectiveWord = tempWord,
                                    AdjectiveTranslate = word.AdjectiveTranslate
                                });
                            }
                        }
                    }

                    //Проверка нахождения слова.
                    if (sortedAdjectiviesList.Count >= 1)
                    {
                        baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                        baseResponse.Data = sortedAdjectiviesList;
                    }
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Adjective>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Добавление нового слова.
        public async Task<DataBaseResponse<List<Adjective>>> AddNewWord(string russianWord, string tatarchaWord)
        {
            DataBaseResponse<List<Adjective>> baseResponse = new DataBaseResponse<List<Adjective>>();
            try
            {
                Adjective wordForAdd = new Adjective { AdjectiveTranslate = russianWord, AdjectiveWord = tatarchaWord };

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
                return new DataBaseResponse<List<Adjective>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Обновление слова.
        public async Task<DataBaseResponse<List<Adjective>>> UpdateWord(Adjective wordForUpdate, int id)
        {
            DataBaseResponse<List<Adjective>> baseResponse = new DataBaseResponse<List<Adjective>>();
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
                return new DataBaseResponse<List<Adjective>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Удаление слова.
        public async Task<DataBaseResponse<List<Adjective>>> DeleteWord(int wordId)
        {
            DataBaseResponse<List<Adjective>> baseResponse = new DataBaseResponse<List<Adjective>>();
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
                return new DataBaseResponse<List<Adjective>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Получение слов.
        public async Task<DataBaseResponse<List<Adjective>>> GetAllWords()
        {
            DataBaseResponse<List<Adjective>> baseResponse = new DataBaseResponse<List<Adjective>>();
            try
            {
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    //Красиво оформляю слова в коллекции.
                    foreach (var word in response)
                    {
                        word.AdjectiveWord = _workTextElements.RemoveSpacesFromString(word.AdjectiveWord);
                        word.AdjectiveTranslate = _workTextElements.RemoveSpacesFromString(word.AdjectiveTranslate);
                    }

                    baseResponse.Data = response;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Adjective>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }
    }
}
