using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Wrappers
{
    public class GenericResponse<T>
    {
        public T Data { get; set; }
        public string Message { get; set; }
        public bool Succes { get; set; }
        public List<string> Errors { get; set; }

        public GenericResponse()
        {
            
        }
        public GenericResponse(T data, string message = null) { 
            Data = data;
            Message = message;
            Succes = true;
        }

        public GenericResponse(string message)
        {
            Message = message;
            Succes = true;
        }
    }
}
