using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GraphQLDocumentationGenerator.Types
{
    public class IntrospectionSchema
    {
        [JsonPropertyName("queryType")]
        public IntrospectionType QueryType { get; set; }
        [JsonPropertyName("mutationType")]
        public IntrospectionType MutationType { get; set; }
        [JsonPropertyName("types")]
        public IEnumerable<IntrospectionType> Types { get; set; }

        public string ToMarkdown()
        {
            var types = Types.Where(x => !x.Name.StartsWith("__"));
            var orderedTypes = types.OrderByDescending(x => x.Name == MutationType?.Name || x.Name == QueryType?.Name ? 1 : 0)
                                    .ThenByDescending(x => x.Kind == IntrospectionTypeKind.OBJECT ? 1 : 0)
                                    .ThenBy(x => x.Name);
            return string.Join(Environment.NewLine, orderedTypes.Select(x => x?.ToMarkdown() ?? ""));
        }
    }
}
