using System.Text.Json;
using System.Text.Json.Serialization;

namespace GraphQLDocumentationGenerator.Types
{
    public class IntrospectionQueryResponse
    {
        [JsonPropertyName("data")]
        public IntrospectionQueryData Data { get; set; }

        public static IntrospectionQueryResponse FromJson(string json) => JsonSerializer.Deserialize<IntrospectionQueryResponse>(json);
    }
}
