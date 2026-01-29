using System;
using System.Collections.Generic;
using System.Text;

namespace PortalEmpresas.Shared.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }
        public string TraceId { get; set; }

        public ApiResponse()
        {
            Success = true;
            Errors = new List<string>();
        }

        public ApiResponse(T data)
        {
            Success = true;
            Data = data;
            Errors = new List<string>();
        }

        public ApiResponse(List<string> errors)
        {
            Success = false;
            Errors = errors;
        }
    }
}
