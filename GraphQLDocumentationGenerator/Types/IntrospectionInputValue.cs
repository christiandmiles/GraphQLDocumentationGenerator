using System;
using System.Text;
using System.Text.Json.Serialization;

namespace GraphQLDocumentationGenerator.Types
{
    public class IntrospectionInputValue
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("defaultValue")]
        public string DefaultValue { get; set; }
        [JsonPropertyName("type")]
        public IntrospectionType Type { get; set; }

        public string ToMarkdown()
        {
            var sb = new StringBuilder($"###### {Name} ({Type.ToMarkdownLink()})");
            if (!string.IsNullOrWhiteSpace(DefaultValue))
                sb.Append($"{Environment.NewLine}{Environment.NewLine}Default Value: {DefaultValue}{Environment.NewLine}");

            if (!string.IsNullOrWhiteSpace(Description))
                sb.Append($"{Environment.NewLine}{Environment.NewLine}{Description}{Environment.NewLine}");

            return sb.ToString();
        }
    }
}
