using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HandNote.Api.Responses
{
    public class ErrorResponse
    {
        public bool Success { get; set; } = false;
        public string Message { get; set; }
        public List<string>? Errors { get; set; }

        public ErrorResponse(string message, List<string>? errors = null)
        {
            this.Success = false;
            Message = message;
            Errors = errors;
        }
    }

}