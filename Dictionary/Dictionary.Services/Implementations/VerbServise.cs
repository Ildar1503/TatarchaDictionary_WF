using Dictionary.BLL.BaseResponse;
using Dictionary.Dal.Interfaces;
using Dictionary.Services.Interfaces;
using Dictionary.Services.Interfaces.HelppersInterfaces;
using Dictionary.Services.Interfaces.SortInterfaces;
using DictionaryTatarcha.Entities;
using System.Collections.Generic;

namespace Dictionary.Services.Implementations
{
    public class VerbServise : IWordServise<Verb>
    {
        //Зависимости.
        private readonly IDictionaryDal<Verb> _dictionary;
        private readonly IWorkWithTextElements _workTextElements;
        private readonly ISelectionSort _sort;

        //Лист для записи id слов от отсортированного листа.
        private List<int> idsList = new List<int>();
        //Лист, который будет служить в качестве передачи всех отсортированных данных.
        private List<Verb> sortedVerbsList = new List<Verb>();
        //Лист для получения первых слов из строки с словами.
        private List<string> firstWordsListFromSortedList = new List<string>();
        //Словарь для подсчета количества слов.
        private Dictionary<int, int> wordsCountedDictionary = new Dictionary<int, int>();
        //Лист для записи слов в алфавитном порядке.
        private List<string> alphabetSortList = new List<string>();
        //Лист для получения листа с удленными пробелами.
        private List<string> spacesRemoveList = new List<string>();

        public VerbServise(IDictionaryDal<Verb> dictionary, IWorkWithTextElements removeTextElements, ISelectionSort sort)
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
            sortedVerbsList.Clear();
            firstWordsListFromSortedList.Clear();
            wordsCountedDictionary.Clear();

            DataBaseResponse<List<string>> baseResponse = new DataBaseResponse<List<string>>();
            try
            {
                var response = await _dictionary.GetAll();
                if(response != null)
                {
                    //Лист для русских слов.
                    List<string> russianVerbs = new List<string>();
                    //Добавление слов.
                    foreach(var word in response)
                    {
                        russianVerbs.Add(word.VerbTranslate);
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = _workTextElements.RemoveSpacesFromList(russianVerbs);
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
            sortedVerbsList.Clear();

            DataBaseResponse<List<string>> baseResponse = new DataBaseResponse<List<string>>();
            try
            {
                var response = await _dictionary.GetAll();
                if(response != null)
                {
                    //Лист для татарских слов.
                    List<string> tatarchaVerbs = new List<string>();
                    //Добавление слов.
                    foreach (var word in response)
                    {
                        tatarchaVerbs.Add(word.VerbWord);
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = _workTextElements.RemoveSpacesFromList(tatarchaVerbs);
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
        public async Task<DataBaseResponse<List<Verb>>> SortByAlphabetWords(List<string> listWords, string language)
        {
            //Очистка листов.
            idsList.Clear();
            sortedVerbsList.Clear();

            DataBaseResponse<List<Verb>> baseResponse = new DataBaseResponse<List<Verb>>();
            try
            {
                //Заполнение второго листа.
                var response = await _dictionary.GetAll();
                if(response != null)
                {
                    //Удаление пробелов.
                    alphabetSortList = _workTextElements.RemoveSpacesFromList(listWords);
                    //Сортировка листа.
                    alphabetSortList.Sort();
                    //Проверка выбранного языка.
                    switch (language.ToLower())
                    {
                        case "russian":
                            for(int word = 0; word < alphabetSortList.Count; word++)
                            {
                                foreach(Verb verb in response)
                                {
                                    verb.VerbTranslate = _workTextElements.RemoveSpacesFromString(verb.VerbTranslate);
                                    if (alphabetSortList[word] == verb.VerbTranslate)
                                    {
                                        idsList.Add(verb.Id);
                                        break;
                                    }
                                }
                            }
                            break;
                        case "tatarcha":
                            for (int word = 0; word < alphabetSortList.Count; word++)
                            {
                                foreach (Verb verb in response)
                                {
                                    verb.VerbWord = _workTextElements.RemoveSpacesFromString(verb.VerbWord);
                                    if (alphabetSortList[word] == verb.VerbWord)
                                    {
                                        idsList.Add(verb.Id);
                                        break;
                                    }
                                }
                            }
                            break;
                    }

                    //Перезапись листа.
                    foreach(var id in idsList)
                    {
                        foreach(var verb in response)
                        {
                            if(verb.Id == id)
                            {
                                sortedVerbsList.Add(
                                    new Verb
                                    {
                                        VerbTranslate = _workTextElements.RemoveSpacesFromString(verb.VerbTranslate),
                                        VerbWord = _workTextElements.RemoveSpacesFromString(verb.VerbWord)
                                    }
                                );
                                break;
                            }
                        }
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = sortedVerbsList;
                }
                return baseResponse;
            }
            catch(Exception ex)
            {
                return new DataBaseResponse<List<Verb>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Сортировка по количеству переводов, только для нерусского языка, данный метод только для татрских слов.
        public async Task<DataBaseResponse<List<Verb>>> SortByTranslateWords(List<string> listWords)
        {
            //Очистка листов.
            idsList.Clear();
            sortedVerbsList.Clear();

            DataBaseResponse<List<Verb>> baseResponse = new DataBaseResponse<List<Verb>>();
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
                    foreach(var word in wordsCountedDictionary)
                    {
                        foreach (var verb in response)
                        {
                            verb.VerbWord = _workTextElements.RemoveSpacesFromString(verb.VerbWord);

                            //Из строки сначала будет получено колисчество слов в подстроке.
                            if ((word.Key, word.Value) == _workTextElements.WordCounterFromString(verb.VerbWord, dictionaryKey))
                            {
                                //Добавление уникального id, чтобы не допустить повторения слов.
                                if (!idsList.Contains(verb.Id))
                                {
                                    idsList.Add(verb.Id);
                                    break;
                                }
                            }
                        }
                        dictionaryKey++;
                    }

                    //Перезапись листа.
                    for (int id = 0; id < idsList.Count; id++)
                    {
                        foreach (var verb in response)
                        {
                            if (verb.Id == idsList[id])
                            {
                                sortedVerbsList.Add(
                                    new Verb
                                    {
                                        VerbTranslate = _workTextElements.RemoveSpacesFromString(verb.VerbTranslate),
                                        VerbWord = _workTextElements.RemoveSpacesFromString(verb.VerbWord)
                                    }
                                );
                                break;
                            }
                        }
                    }

                    baseResponse.Data = sortedVerbsList;
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                }
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Verb>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Сортировка от большего слова к меньшему.
        public async Task<DataBaseResponse<List<Verb>>> SortByWordFromLongerWords(List<string> listWords, string language)
        {
            //Очистка листов.
            idsList.Clear();
            sortedVerbsList.Clear();

            DataBaseResponse<List<Verb>> baseResponse = new DataBaseResponse<List<Verb>>();
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
                                foreach (var verb in response)
                                {
                                    //Получение первого слова из строки.
                                    string currentVerb = _workTextElements.GetFirstWordFromString(verb.VerbTranslate);
                                    //Удаление пробелов, если они есть.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentVerb))
                                    {
                                        if (!idsList.Contains(verb.Id))
                                        {
                                            idsList.Add(verb.Id);
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
                                foreach (var verb in response)
                                {
                                    //Получение первого слова из строки.
                                    string currentVerb = _workTextElements.GetFirstWordFromString(verb.VerbWord);
                                    //Удаление пробелов, если они есть.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentVerb))
                                    {
                                        if (!idsList.Contains(verb.Id))
                                        {
                                            idsList.Add(verb.Id);
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
                        foreach (var verb in response)
                        {
                            if (verb.Id == idsList[id] && language == "russian")
                            {
                                sortedVerbsList.Add(
                                    new Verb
                                    {
                                        VerbTranslate = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id]),
                                        VerbWord = _workTextElements.RemoveSpacesFromString(verb.VerbWord)
                                    }
                                );
                                break;
                            }

                            if (verb.Id == idsList[id] && language == "tatarcha")
                            {
                                sortedVerbsList.Add(
                                    new Verb
                                    {
                                        VerbTranslate = _workTextElements.RemoveSpacesFromString(verb.VerbTranslate),
                                        VerbWord = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id])
                                    }
                                );
                                break;
                            }
                        }
                    }

                    sortedVerbsList.Reverse();
                    baseResponse.Data = sortedVerbsList;
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                }
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Verb>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Сортировка от меньшего слова к большему.
        public async Task<DataBaseResponse<List<Verb>>> SortByWordFromShorterWords(List<string> listWords, string language)
        {
            //Очистка листов.
            idsList.Clear();
            sortedVerbsList.Clear();

            DataBaseResponse<List<Verb>> baseResponse = new DataBaseResponse<List<Verb>>();
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
                            foreach(var word in firstWordsListFromSortedList)
                            {
                                foreach (var verb in response)
                                {
                                    //Получение первого слова в строке.
                                    string currentVerb = _workTextElements.GetFirstWordFromString(verb.VerbTranslate);
                                    //Удаление пробелов, если они есть и добавление слов по id, если они не найдены в списке.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentVerb))
                                    {
                                        if (!idsList.Contains(verb.Id))
                                        {
                                            idsList.Add(verb.Id);
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
                                foreach (var verb in response)
                                {
                                    //Получение первого слова в строке.
                                    string currentVerb = _workTextElements.GetFirstWordFromString(verb.VerbWord);
                                    //Удаление пробелов, если они есть и добавление слов по id, если они не найдены в списке.
                                    if (word == _workTextElements.RemoveSpacesFromString(currentVerb))
                                    {
                                        if (!idsList.Contains(verb.Id))
                                        {
                                            idsList.Add(verb.Id);
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
                        foreach (var verb in response)
                        {
                            if (verb.Id == idsList[id] && language == "russian")
                            {
                                sortedVerbsList.Add(
                                    new Verb
                                    {
                                        VerbTranslate = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id]),
                                        VerbWord = _workTextElements.RemoveSpacesFromString(verb.VerbWord)
                                    }
                                );
                                break;
                            }

                            if (verb.Id == idsList[id] && language == "tatarcha")
                            {
                                sortedVerbsList.Add(
                                    new Verb
                                    {
                                        VerbTranslate = _workTextElements.RemoveSpacesFromString(verb.VerbTranslate),
                                        VerbWord = _workTextElements.RemoveSpacesFromString(firstWordsListFromSortedList[id])
                                    }
                                );
                                break;
                            }
                        }
                    }

                    baseResponse.Data = sortedVerbsList;
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                }
                return baseResponse;

            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Verb>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Получение слов в виде коллекции.
        public async Task<DataBaseResponse<List<Verb>>> GetWords()
        {
            //Очистка листов.
            idsList.Clear();
            sortedVerbsList.Clear();
            firstWordsListFromSortedList.Clear();
            wordsCountedDictionary.Clear();

            DataBaseResponse<List<Verb>> baseResponse = new DataBaseResponse<List<Verb>>();
            try
            {
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    //Лист для русских слов.
                    List<string> russianVerbs = new List<string>();
                    //Лист для татарских слов.
                    List<string> tatarcharVerbs = new List<string>();
                    //Добавление слов.
                    foreach (var word in response)
                    {
                        russianVerbs.Add(_workTextElements.RemoveSpacesFromString(word.VerbTranslate));
                        tatarcharVerbs.Add(_workTextElements.RemoveSpacesFromString(word.VerbWord));
                    }
                    //Заполнение коллкции на возвращение из метода.
                    if (tatarcharVerbs.Count == russianVerbs.Count)
                    {
                        for (int index = 0; index < russianVerbs.Count; index++)
                        {
                            sortedVerbsList.Add(
                                new Verb
                                {
                                    VerbWord = _workTextElements.GetFirstWordFromString(tatarcharVerbs[index]),
                                    VerbTranslate = _workTextElements.GetFirstWordFromString(russianVerbs[index])
                                });
                        }
                    }

                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    baseResponse.Data = sortedVerbsList;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Verb>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Получение искомого слова/слов.
        public async Task<DataBaseResponse<List<Verb>>> GetSearchWord(string searchWord)
        {
            //Очистка листа.
            sortedVerbsList.Clear();

            DataBaseResponse<List<Verb>> baseResponse = new DataBaseResponse<List<Verb>>();
            try
            {
                List<string> wordsFromSting = new List<string>();   

                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    foreach(var word in response)
                    {
                        word.VerbWord = _workTextElements.RemoveSpacesFromString(word.VerbWord);
                        word.VerbTranslate = _workTextElements.RemoveSpacesFromString(word.VerbTranslate);

                        if (word.VerbTranslate == searchWord)
                        {
                            sortedVerbsList.Add( 
                                new Verb 
                                {
                                    VerbWord = word.VerbWord, 
                                    VerbTranslate = word.VerbTranslate
                                });
                        }

                        //Очистка и перезапись листа новыми словами.
                        wordsFromSting.Clear();
                        wordsFromSting = _workTextElements.GetWordsFromString(word.VerbWord);

                        //Перебор листа с татарскими словами.
                        foreach(var curStrWord in wordsFromSting)
                        {
                            //Удаление пробелов, если они есть.
                            string tempWord = _workTextElements.RemoveSpacesFromString(curStrWord);

                            if(tempWord == searchWord)
                            {
                                sortedVerbsList.Add(
                                new Verb
                                {
                                    VerbWord = tempWord,
                                    VerbTranslate = word.VerbTranslate
                                });
                            }
                        }
                    }
                    
                    //Проверка нахождения слова.
                    if(sortedVerbsList.Count >= 1)
                    {
                        baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                        baseResponse.Data = sortedVerbsList;
                    }
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Verb>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Добавление нового слова.
        public async Task<DataBaseResponse<List<Verb>>> AddNewWord(string russianWord, string tatarchaWord)
        {
            DataBaseResponse<List<Verb>> baseResponse = new DataBaseResponse<List<Verb>>();
            try
            {
                Verb wordForAdd = new Verb { VerbTranslate = russianWord, VerbWord = tatarchaWord };

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
                return new DataBaseResponse<List<Verb>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Обновление слова. 
        public async Task<DataBaseResponse<List<Verb>>> UpdateWord(Verb wordForUpdate, int id)
        {
            DataBaseResponse<List<Verb>> baseResponse = new DataBaseResponse<List<Verb>>();
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
                return new DataBaseResponse<List<Verb>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Удаление слова.
        public async Task<DataBaseResponse<List<Verb>>> DeleteWord(int wordId)
        {
            DataBaseResponse<List<Verb>> baseResponse = new DataBaseResponse<List<Verb>>();
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
                return new DataBaseResponse<List<Verb>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }

        //Получение слов.
        public async Task<DataBaseResponse<List<Verb>>> GetAllWords()
        {
            DataBaseResponse<List<Verb>> baseResponse = new DataBaseResponse<List<Verb>>();
            try
            {
                var response = await _dictionary.GetAll();
                if (response != null)
                {
                    baseResponse.Status = BLL.DataBaseResponseStatus.OK;
                    //Красиво оформляю слова в коллекции.
                    foreach(var word in response)
                    {
                        word.VerbWord = _workTextElements.RemoveSpacesFromString(word.VerbWord);
                        word.VerbTranslate = _workTextElements.RemoveSpacesFromString(word.VerbTranslate);
                    }

                    baseResponse.Data = response;
                }
                return baseResponse;
            }
            catch (Exception ex)
            {
                return new DataBaseResponse<List<Verb>>()
                {
                    Description = ex.Message,
                    Status = BLL.DataBaseResponseStatus.DataNotFounded
                };
            }
        }
    }
}