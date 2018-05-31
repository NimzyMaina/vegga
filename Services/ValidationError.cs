using Newtonsoft.Json;

namespace vegga.Services
{
    public class ValidationError
    {
        [JsonProperty(NullValueHandling=NullValueHandling.Ignore)]
        public string Field { get; }

        public string Message { get; }

        public ValidationError(string field, string message)
        {
            //Field = field != string.Empty ? field : null;
            field = message;

        }
    }
}