using Dictionary.Services.Interfaces.SortInterfaces;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace Dictionary.Services.Implementations.AnotherImplementations
{
    public class SelectionSort : ISelectionSort
    {
        //Для сортировки с помощью метода выбора.
        public List<string> SelectionSortList(List<string> listForSort)
        {
            for (int i = 0; i < listForSort.Count; i++)
            {
                int min = i;
                for (int j = i + 1; j < listForSort.Count; j++)
                {
                    if (listForSort[j].Length < listForSort[min].Length)
                    {
                        min = j;
                    }
                }

                string temp = listForSort[min];
                listForSort[min] = listForSort[i];
                listForSort[i] = temp;
            }
            return listForSort;
        }

        //Для сортировки словаря с помощью метода выбора.
        public Dictionary<int, int> SelectionSortDictionary(Dictionary<int, int> dictionaryForSort)
        {
            for (int word = 0; word < dictionaryForSort.Count; word++)
            {
                int min = word;
                for(int compareWord = word + 1; compareWord < dictionaryForSort.Count; compareWord++)
                {
                    if (dictionaryForSort[compareWord] < dictionaryForSort[min])
                    {
                        min = compareWord;
                    }
                }

                int temp = dictionaryForSort[min];
                dictionaryForSort[min] = dictionaryForSort[word];
                dictionaryForSort[word] = temp;
            }

            return dictionaryForSort;
        }
    }
}
