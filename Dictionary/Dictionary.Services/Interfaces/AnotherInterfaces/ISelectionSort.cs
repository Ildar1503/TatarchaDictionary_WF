namespace Dictionary.Services.Interfaces.SortInterfaces
{
    public interface ISelectionSort
    {
        //Для сортировки с помощью метода выбора.
        public List<string> SelectionSortList(List<string> listForSort);
        //Для сортировки словаря с помощью метода выбора.
        public Dictionary<int, int> SelectionSortDictionary(Dictionary<int, int> dictForSort);
    }
}
