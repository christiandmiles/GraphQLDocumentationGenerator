using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace GraphQLDocumentationGenerator.Types
{
    public class IntrospectionField
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("IsDeprecated")]
        public bool IsDeprecated { get; set; }
        [JsonPropertyName("deprecationReason")]
        public string DeprecationReason { get; set; }
        [JsonPropertyName("type")]
        public IntrospectionType Type { get; set; }
        [JsonPropertyName("args")]
        public IEnumerable<IntrospectionInputValue> Args { get; set; }

        public string ToMarkdown()
        {
            var sb = new StringBuilder($"#### {Name}{(Type != null ? " (" + Type.ToMarkdownLink() + ")" : "")}");
            if (!string.IsNullOrWhiteSpace(Description))
                sb.Append($"{Environment.NewLine}{Environment.NewLine}{Description.Trim()}{Environment.NewLine}");

            if(Args != null && Args.Any())
            {
                sb.Append($"{Environment.NewLine}{Environment.NewLine}##### Arguments:{Environment.NewLine}{Environment.NewLine}");
                foreach (var field in Args)
                {
                    if(!string.IsNullOrWhiteSpace(field?.ToMarkdown()))
                        sb.AppendLine($"{field.ToMarkdown()}");
                }
            }

            return sb.ToString();
        }
    }
}
