using Azure.AI.OpenAI;
using Azure.AI.OpenAI.Chat;
using OpenAI.Chat;

string aiEndpoint = "your-ai-services-endpoint";
string aiKey = "your-ai-services-key";

var azureClient = new AzureOpenAIClient(new Uri(aiEndpoint), aiKey);
var chatClient = azureClient.GetChatClient("gpt-4");

Console.WriteLine("Your prompt:");
var prompt = Console.ReadLine();

// Required for preview features
#pragma warning disable AOAI001
var chatOptions = new ChatCompletionOptions();
chatOptions.AddDataSource(new AzureSearchChatDataSource()
{
    Endpoint = new Uri("your-search-endpoint"),
    IndexName = "your-search-index-name",
    Authentication = DataSourceAuthentication.FromApiKey("your-search-key"),
});

var completion = chatClient.CompleteChatStreamingAsync(
    [
        new SystemChatMessage("You are a helpful AI assistant."),
        new UserChatMessage(prompt),
    ], chatOptions);

Console.WriteLine("AI Response:");
await foreach (var item in completion)
{
    foreach (var contentPart in item.ContentUpdate)
    {
        Console.Write(contentPart.Text);
    }
}
