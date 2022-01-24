using System.Collections.Generic;

namespace HRM.Models.Error
{
    public class BaseResponse<T>
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int Status { get; set; }
        
        public List<string> Errors;
        public T Data { get; set; }

        public BaseResponse()
        {
        }

        public BaseResponse(T data, string message = null, int status = 200)
        {
            Succeeded = true;
            Message = message;
            Data = data;
            Status = status;
        }

        public BaseResponse(string message)
        {
            Message = message;
        }
    }
}