
namespace BuildingBlocks.BaseEntities
{
    public class ResponseBase<T> : ResponseBase
    {
        public T Data { get; set; }

        public ResponseBase() : base()
        {
        }

        public ResponseBase(T data)
        {
            Success = true;
            Data = data;
        }
    }

    public class ResponseBase
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }

        public ResponseBase()
        {
            Errors = new List<string>();
        }

        public void AddError(string error)
        {
            Success = false;
            Errors.Add(error);
        }

        public void AddErrors(IEnumerable<string> errors)
        {
            Success = false;
            Errors.AddRange(errors);
        }

        public void SetSuccess(string message = "Operation completed successfully.")
        {
            Success = true;
            Message = message;
        }

        public void SetFailure(string message = "Operation failed.")
        {
            Success = false;
            Message = message;
        }
    }

}
