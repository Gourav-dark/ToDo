
namespace ToDo.Shared.Wrapper;

public class ResponseWrapper<T>
{
    public T Data { get; set; }
    public bool Succeeded { get; set; }
    //public string[] Errors { get; set; }
    public string Message { get; set; }=string.Empty;
#pragma warning disable
    public ResponseWrapper() { }
#pragma warning restore
    public ResponseWrapper(T data)
    {
        Succeeded = true;
        Message = string.Empty;
        //Errors = null;
        Data = data;
    }
}
