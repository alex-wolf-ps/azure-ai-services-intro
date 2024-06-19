using Azure;
using Azure.AI.OpenAI;

string endpoint = "https://demoaiserviceswolf.openai.azure.com/";
string key = "9d72a9f5a65b47a9946500d4dcfee302";

OpenAIClient client = new(new Uri(endpoint), new AzureKeyCredential(key));

Console.WriteLine("Your prompt:");
var userPrompt = Console.ReadLine();
var chatCompletionsOptions = new ChatCompletionsOptions()
{
    DeploymentName = "gpt-4",
    Messages =
    {
        new ChatRequestUserMessage(userPrompt),
    }
};

Console.WriteLine("AI Response:");
await foreach(var item in client.GetChatCompletionsStreaming(chatCompletionsOptions))
{
    Console.Write(item.ContentUpdate);
}
