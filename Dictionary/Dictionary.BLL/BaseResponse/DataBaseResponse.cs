namespace Dictionary.BLL.BaseResponse
{
    public class DataBaseResponse<T> : IDataBaseResponse<T>
    {
        //Получение сообщения, если что-то пойдет не так.
        public string Description { get; set; }
        public T Data { get; set; }
        //Статус полученных данны.
        public DataBaseResponseStatus Status { get; set; }
    }
}
