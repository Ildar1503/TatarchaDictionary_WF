using Dictionary.BLL.BaseResponse;
using DictionaryTatarcha.Entities;

namespace Dictionary.Services.Interfaces
{
    public interface IWordServise<T>
    {
        //Получение списка русских слов.
        public Task<DataBaseResponse<List<string>>> GetRussianWords();
        //Получение списка татарских слов.
        public Task<DataBaseResponse<List<string>>> GetTatarchaWords();
        //Сортировка слов в алфавитном порядке.
        public Task<DataBaseResponse<List<T>>> SortByAlphabetWords(List<string> listWords, string language);
        //Во возрастанию длины слов.
        public Task<DataBaseResponse<List<T>>> SortByWordFromLongerWords(List<string> listWords, string language);
        //По убыванию длины слова.
        public Task<DataBaseResponse<List<T>>> SortByWordFromShorterWords(List<string> listWords, string language);
        //По количесву переводов, только не для русских слов.
        public Task<DataBaseResponse<List<T>>> SortByTranslateWords(List<string> listWords);
        //Получение слов из бд в виде коллекции.
        public Task<DataBaseResponse<List<T>>> GetWords();
        //Получение искомого слова/слов.
        public Task<DataBaseResponse<List<T>>> GetSearchWord(string searchWord);
        //Добавление нового слова.
        public Task<DataBaseResponse<List<T>>> AddNewWord(string russianWord, string tatarchaWord); 
        //Обновление слова.
        public Task<DataBaseResponse<List<T>>> UpdateWord(T wordForUpdate, int id);
        //Удаление слова.
        public Task<DataBaseResponse<List<T>>> DeleteWord(int wordId);
        //Получение всех слов из бд.
        public Task<DataBaseResponse<List<T>>> GetAllWords();
    }
}
