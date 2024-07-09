using Azure.AI.OpenAI;
using Azure.AI.OpenAI.Chat;
using OpenAI.Chat;

string aiEndpoint = "your-ai-services-endpoint";
string aiKey = "your-ai-services-key";
 
var azureAIClient = new AzureOpenAIClient(new Uri(aiEndpoint), aiKey);
var chatClient = azureAIClient.GetChatClient("gpt-4"); // your-model-name

Console.WriteLine("Your prompt:");
var prompt = Console.ReadLine();

var completionUpdates = chatClient.CompleteChatStreamingAsync([
    new SystemChatMessage("You are a helpful AI assistant."),
    new UserChatMessage(prompt)]);

Console.WriteLine("AI Response:");
await foreach (var update in completionUpdates)
{
    foreach (var contentPart in update.ContentUpdate)
    {
        Console.Write(contentPart.Text);
    }
}
