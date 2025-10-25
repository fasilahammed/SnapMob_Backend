using System.Text.Json.Serialization;

namespace SnapMob_Backend.Common
{
    public class ApiResponse<T>
    {
       
            public int StatusCode { get; set; }
            public string? Message { get; set; }
            [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
            public T? Data { get; set; }


            public ApiResponse() { }

            public ApiResponse(int statusCode, string? message = null, T data = default)
            {
                Data = data;
                Message = message;
                StatusCode = statusCode;
            }
        }
}
