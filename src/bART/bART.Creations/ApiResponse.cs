using System.Text.Json.Serialization;

namespace bART.Creations
{
    public class ApiResponse<T>
    {
        public string Result { get; set; } = s;
        public T Data { get; set; }
        public bool? Error { get; set; }
        public string? ErrorDescription { get; set; }
        [JsonIgnore] public static string s = "Success";   
        [JsonIgnore] public static string f = "Failure";   
    }
}
