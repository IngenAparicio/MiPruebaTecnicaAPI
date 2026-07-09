using Microsoft.AspNetCore.Http;
using System.Runtime.Serialization;


namespace MiPruebaTecnicaAccess.responses
{
    [Serializable]
    [DataContract]
    public class ResponseEntity<T>
    {
        [DataMember]
        public bool SuccessProcess { get; set; }

        [DataMember]
        public bool SuccessData { get; set; }
        
        [DataMember]
        public T Data { get; set; }
        
        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public int StatusCode { get; set; }


        public static ResponseEntity<T> Ok(T data, string message) => new()
        {
            SuccessProcess = true,
            SuccessData = true,
            StatusCode = StatusCodes.Status200OK,
            Data = data,
            Message = message
        };

        public static ResponseEntity<T> NotFound(T data, string message) => new()
        {
            SuccessProcess = true,
            SuccessData = false,
            StatusCode = StatusCodes.Status404NotFound,
            Data = data,
            Message = message
        };

        public static ResponseEntity<T> Error(T data, string message) => new()
        {
            SuccessProcess = false,
            SuccessData = false,
            StatusCode = StatusCodes.Status500InternalServerError,
            Data = data,
            Message = message

        };
    }
}
