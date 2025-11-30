namespace construplan_examen.Models
{
    public enum ErrorType
    {
        None,
        NotFound,
        Validation,
        Conflict,
        ActionError,
        Unexpected,
    }

    public class ServiceResult<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public ErrorType ErrorType { get; set; } = ErrorType.None;
        public T? Data { get; set; }

        public static ServiceResult<T> Ok(T data, string message = "")
        {
            return new ServiceResult<T> { Success = true, Message = message, Data = data };
        }

        public static ServiceResult<T> OkAction(T data, string action, string fieldName)
        {
            return new ServiceResult<T> { Success = true, Message = $"Exito al {action} la {fieldName} ", Data = data };
        }

        public static ServiceResult<T> OkAction(string action, string fieldName)
        {
            return new ServiceResult<T> { Success = true, Message = $"Exito al {action} la {fieldName} " };
        }

        public static ServiceResult<T> OkFinded(T data, string fieldName)
        {
            return new ServiceResult<T> { Success = true, Message = $"{fieldName} obtenida con exito", Data = data };
        }

        public static ServiceResult<T> Fail(string message, ErrorType error)
        {
            return new ServiceResult<T> { Success = false, Message = message, ErrorType = error };
        }

        public static ServiceResult<T> FailWithData(T data, string message, ErrorType error)
        {
            return new ServiceResult<T> { Success = false, Message = message, ErrorType = error, Data = data };
        }

        public static ServiceResult<T> FailIdNotFound(string fieldName, int id)
        {
            return new ServiceResult<T> { Success = false, Message = $"La ID: {id} no corresponde a alguna {fieldName}", ErrorType = ErrorType.NotFound };
        }

    }
}
