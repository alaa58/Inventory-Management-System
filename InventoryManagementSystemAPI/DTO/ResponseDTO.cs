namespace InventoryManagementSystemAPI.DTO
{
    public class ResponseDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T? Data { get; set; }
        public ErrorCode? ErrorCode { get; set; }

        public static ResponseDTO<T> Succeded(T data, string message = "")
        {
            return new ResponseDTO<T>
            {
                Success = true,
                Data = data,
                Message = message,
            };
        }

        public static ResponseDTO<T> Error(ErrorCode code, string message)
        {
            return new ResponseDTO<T>
            {
                Success = false,
                ErrorCode = code,
                Message = message
            };
        }
    }

    public enum ErrorCode
    {
        None = 0,
        UnExcepectedError = 2,
        NotFound = 100,
        AlreadyExist = 101,
    }
}
