# GraphQL Documentation Generator

Basic GraphQL Markdown documentation generator in C#. Includes middleware for easy integration with graphql-dotnet solutions.

## Example Usage

Using GraphQL.Client you can query a public facing API as follows:

```csharp
public async Task<string> GetSchemaAsync(Uri uri, HttpClient httpClient)
{
    var client = new GraphQLHttpClient(new GraphQLHttpClientOptions
    {
        EndPoint = uri
    }, new SystemTextJsonSerializer(), httpClient);

    var request = new GraphQLRequest
    {
        Query = IntrospectionQuery.GetQuery()
    };

    var response = await client.SendQueryAsync<IntrospectionQueryData>(request);
    return response?.Data?.ToMarkdown();
}
```

## .Net Web Integration

See the [GraphQLDocumentationGenerator.Web](./GraphQLDocumentationGenerator.Web/README.md) project for integration with graphql-dotnet solutions.