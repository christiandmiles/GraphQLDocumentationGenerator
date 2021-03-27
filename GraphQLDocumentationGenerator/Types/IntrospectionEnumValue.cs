using System.Text.Json.Serialization;

namespace GraphQLDocumentationGenerator.Types
{
    public class IntrospectionEnumValue
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("IsDeprecated")]
        public bool IsDeprecated { get; set; }
        [JsonPropertyName("deprecationReason")]
        public string DeprecationReason { get; set; }
    }
}
