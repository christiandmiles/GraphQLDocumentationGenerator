using Microsoft.AspNetCore.Builder;

namespace GraphQLDocumentationGenerator.Web
{
    public static class GraphQLDocumentationApplicationBuilderExtensions
    {

        public static IApplicationBuilder UseGraphQLDocumentation(this IApplicationBuilder app, string path = "/docs")
        {
            return app.UseWhen(
               context => context.Request.Path.StartsWithSegments(path, out var remaining) && (string.IsNullOrEmpty(remaining) || remaining == ".md"),
               b => b.UseMiddleware<GraphQLDocumentationMiddleware>());
        }
    }
}
