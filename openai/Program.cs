using Azure.AI.OpenAI;
using Azure.AI.OpenAI.Chat;
using OpenAI.Chat;

string aiEndpoint = "https://demoaiservices222.openai.azure.com";
string aiKey = "691ee6d6242f48d2b05301fb54e2d9b0";
 
var azureAIClient = new AzureOpenAIClient(new Uri(aiEndpoint), aiKey);
var chatClient = azureAIClient.GetChatClient("gpt-4");

Console.WriteLine("Your prompt:");
var prompt = Console.ReadLine();

// Required for preview features
#pragma warning disable AOAI001
var chatOptions = new ChatCompletionOptions();
chatOptions.AddDataSource(new AzureSearchChatDataSource()
{
    Endpoint = new Uri("https://aiservicesintrosearch.search.windows.net"),
    IndexName = "azureblob-index",
    Authentication = DataSourceAuthentication.FromApiKey("SHBI7T6jM1rR7o5baz19Evq56iHIXv8FHVQyEnZesfAzSeDwX5gH"),
});

var completionUpdates = chatClient.CompleteChatStreamingAsync([
    new SystemChatMessage("You are a helpful AI assistant."),
    new UserChatMessage(prompt)], chatOptions);

Console.WriteLine("AI Response:");
await foreach (var update in completionUpdates)
{
    foreach (var contentPart in update.ContentUpdate)
    {
        Console.Write(contentPart.Text);
    }
}
