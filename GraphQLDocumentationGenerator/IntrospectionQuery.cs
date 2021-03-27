using GraphQLDocumentationGenerator.Types;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GraphQLDocumentationGenerator
{
    public static class IntrospectionQuery
    {
        public static string GetQuery() => Encoding.UTF8.GetString(Properties.Resources.IntrospectionQuery);

        public static IntrospectionQueryResponse Deserialize(string json) => JsonSerializer.Deserialize<IntrospectionQueryResponse>(json);

        public static async Task<IntrospectionQueryResponse> DeserializeAsync(Stream json) => await JsonSerializer.DeserializeAsync<IntrospectionQueryResponse>(json);
    }
}
