namespace Dictionary.Services.Interfaces.HelppersInterfaces
{
    public interface IWorkWithTextElements
    {
        //Удаление лишних запятых.
        public List<string> RemoveComa(List<string> listForRemoveTextElements);

        //Удаление лишних точек запятых.
        public List<string> RemoveDottedComa(List<string> listForRemoveTextElements);

        //Удаление стартового пробела и после слова.
        public List<string> RemoveSpacesFromList(List<string> listForRemoveSpaces);

        //Удаление стартового пробела и после слова.
        public string RemoveSpacesFromString(string text);

        //Получение первого слова из строки.
        public List<string> GetFirstWordFromList(List<string> listForRemoveTextElements);

        //Получение первого слова из строки.
        public string GetFirstWordFromString(string text);

        //Подсчет количества слов в листе после символов.
        public Dictionary<int, int> WordCounterFromList(List<string> listForWordCount);

        //Подсчет количества слов в строке после символов.
        public (int, int) WordCounterFromString(string text, int dictionaryKey);

        //Получение всех слов из строки.
        public List<string> GetWordsFromString(string text);
    }
}
