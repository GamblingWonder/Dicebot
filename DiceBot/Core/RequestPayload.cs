using System.Text.Json.Serialization;

namespace DiceBot.Core.Request
{

    public class RequestPayload
    {

        [JsonPropertyName("operationName")]
        public string operationName { get; set; }

        [JsonPropertyName("query")]
        public string query { get; set; }

        [JsonPropertyName("variables")]
        public object variables { get; set; }

        [JsonPropertyName("identifier")]
        public string identifier { get; set; }

        public RequestPayload()
        {

        }

    }

}