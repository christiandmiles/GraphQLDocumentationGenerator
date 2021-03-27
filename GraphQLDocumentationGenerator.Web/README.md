# .Net Web Integration

Helper library to use GraphQLDocumentationGenerator in graphql-dotnet projects.

## Example Usage

Simply add the following call in your `Startup.Configuration` to serve a HTML version at `/docs` and the markdown file at `/docs.md`.

```csharp
app.UseGraphQLDocumentation("/docs");
```