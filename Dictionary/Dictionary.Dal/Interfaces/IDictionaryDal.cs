namespace Dictionary.Dal.Interfaces
{
    //Интерфейс для построения взаимодействия с данными из бд.
    public interface IDictionaryDal<T>
    {
        public Task<List<T>> GetAll();
        public Task<bool> Update(T entity, int id);
        public Task<bool> Add(T entity);
        public Task<bool> Delete(int id);
    }
}
