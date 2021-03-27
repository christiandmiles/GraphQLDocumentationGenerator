using System;
using System.Text.Json.Serialization;

namespace GraphQLDocumentationGenerator.Types
{
    public class IntrospectionQueryData
    {
        [JsonPropertyName("__schema")]
        public IntrospectionSchema Schema { get; set; }

        public string ToMarkdown()
        {
            return $"# GraphQL Docs{Environment.NewLine}{Environment.NewLine}{Schema?.ToMarkdown()}";
        }
    }
}
