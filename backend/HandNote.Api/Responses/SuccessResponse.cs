using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Api.Responses
{
    public class SuccessResponse<T>
    {
        public bool Success { get; set; } = true;
        public T Data { get; set; }
        public string Message { get; set; }

        public SuccessResponse(T data, string message = "")
        {
            Data = data;
            Message = message;
        }
    }
}