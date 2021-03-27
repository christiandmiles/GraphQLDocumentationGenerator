using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;

namespace GraphQLDocumentationGenerator.Types
{
    public class IntrospectionType
    {

        [JsonPropertyName("kind")]
        public IntrospectionTypeKind Kind { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("interfaces")]
        public IEnumerable<IntrospectionType> Interfaces { get; set; }
        [JsonPropertyName("possibleTypes")]
        public IEnumerable<IntrospectionType> PossibleTypes { get; set; }
        [JsonPropertyName("fields")]
        public IEnumerable<IntrospectionField> Fields { get; set; }
        [JsonPropertyName("inputFields")]
        public IEnumerable<IntrospectionField> InputFields { get; set; }
        [JsonPropertyName("enumValues")]
        public IEnumerable<IntrospectionField> EnumValues { get; set; }
        [JsonPropertyName("ofType")]
        public IntrospectionType OfType { get; set; }

        public string ToMarkdown()
        {
            var sb = new StringBuilder($"---{Environment.NewLine}{Environment.NewLine}");

            if (OfType != null)
                sb.Append(OfType.ToMarkdown());

            sb.Append(Kind switch
            {
                IntrospectionTypeKind.NON_NULL => "!",
                _ => $"## {Name}{Environment.NewLine}{Environment.NewLine}{Description?.Trim()}".Trim()
            });

            if (PossibleTypes != null && PossibleTypes.Any())
            {
                sb.Append($"{Environment.NewLine}{Environment.NewLine}### Possible Types:{Environment.NewLine}{Environment.NewLine}");
                foreach (var item in PossibleTypes)
                {
                    sb.AppendLine($"{item.ToMarkdown()}");
                }
            }

            if (Interfaces != null && Interfaces.Any())
            {
                sb.Append($"{Environment.NewLine}{Environment.NewLine}### Interfaces:{Environment.NewLine}{Environment.NewLine}");
                foreach (var item in Interfaces)
                {
                    sb.AppendLine($"{item.ToMarkdownLink()}");
                }
            }

            if (Fields != null && Fields.Any())
            {
                sb.Append($"{Environment.NewLine}{Environment.NewLine}### Fields:{Environment.NewLine}{Environment.NewLine}");
                foreach (var field in Fields)
                {
                    sb.AppendLine($"{field.ToMarkdown()}");
                }
            }

            if (InputFields != null && InputFields.Any())
            {
                sb.Append($"{Environment.NewLine}{Environment.NewLine}### Input Fields:{Environment.NewLine}{Environment.NewLine}");
                foreach (var field in InputFields)
                {
                    sb.AppendLine($"{field.ToMarkdown()}");
                }
            }

            if (EnumValues != null && EnumValues.Any())
            {
                sb.Append($"{Environment.NewLine}{Environment.NewLine}### Enum Values:{Environment.NewLine}{Environment.NewLine}");
                foreach (var field in EnumValues)
                {
                    sb.AppendLine($"{field.ToMarkdown()}");
                }
            }

            return $"{sb.ToString().Trim()}{Environment.NewLine}";
        }

        public string ToMarkdownLink()
        {
            var sb = new StringBuilder();

            if (Kind == IntrospectionTypeKind.LIST)
                sb.Append("[");

            if (OfType != null)
                sb.Append(OfType.ToMarkdownLink());

            sb.Append(Kind switch
            {
                IntrospectionTypeKind.NON_NULL => "!",
                IntrospectionTypeKind.LIST => "]",
                _ => string.IsNullOrWhiteSpace(Name) ? "" : $"[{Name}](#{Name})"
            });

            return sb.ToString().Trim();
        }
    }
}
