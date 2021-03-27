using GraphQL.Types;
using Markdig;
using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using GraphQL;
using System.IO;
using GraphQLDocumentationGenerator.Types;

namespace GraphQLDocumentationGenerator.Web
{
    public class GraphQLDocumentationMiddleware
    {
        private readonly ISchema _schema;
        private readonly IDocumentExecuter _documentExecuter;
        private readonly IDocumentWriter _documentWriter;
        private string _mdCache;
        private string _htmlCache;

        public GraphQLDocumentationMiddleware(ISchema schema, IDocumentExecuter documentExecuter, IDocumentWriter documentWriter)
        {
            _schema = schema;
            _documentExecuter = documentExecuter;
            _documentWriter = documentWriter;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            _ = httpContext ?? throw new ArgumentNullException(nameof(httpContext));

            var isMarkdownRequest = httpContext.Request.Path.HasValue && httpContext.Request.Path.Value.EndsWith("md");
            httpContext.Response.ContentType = isMarkdownRequest ? "text/markdown" : "text/html";
            httpContext.Response.StatusCode = 200;

            if(_mdCache == null)
            {
                var result = await GetSchemaAsync(_schema);
                _mdCache = result?.ToMarkdown();
                _htmlCache = Markdown.ToHtml(_mdCache);
            }

            byte[] data = Encoding.UTF8.GetBytes(isMarkdownRequest ? _mdCache : _htmlCache);
            await httpContext.Response.Body.WriteAsync(data, 0, data.Length);
        }

        private async Task<IntrospectionQueryData> GetSchemaAsync(ISchema schema)
        {
            var options = new ExecutionOptions
            {
                Schema = schema,
                Query = IntrospectionQuery.GetQuery()
            };
            var result = await _documentExecuter.ExecuteAsync(options);
            using var stream = new MemoryStream();
            await _documentWriter.WriteAsync(stream, result);
            stream.Position = 0;
            var response = await IntrospectionQuery.DeserializeAsync(stream);
            return response?.Data;
        }
    }
}
