namespace VCardOnAbp.ApiServices.Vmcardio.Dtos
{
    public class ResponseModel<T>
    {
        public int code { get; set; }
        public string message { get; set; }
        public T data { get; set; }
    }
}
