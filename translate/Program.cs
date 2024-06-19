using Azure;
using Azure.AI.Translation.Text;

string key = "9d72a9f5a65b47a9946500d4dcfee302";
string region = "eastus";

var client = new TextTranslationClient(new AzureKeyCredential(key), region);

try
{
    string targetLanguage = "de";
    string inputText = Console.ReadLine();

    var response = await client.TranslateAsync(targetLanguage, inputText);
    TranslatedTextItem translation = response.Value.FirstOrDefault();

    Console.WriteLine($"Detected language: {translation?.DetectedLanguage?.Language}");
    Console.WriteLine($"Translated text: '{translation?.Translations?.FirstOrDefault()?.Text}'.");
}
catch (RequestFailedException exception)
{
    Console.WriteLine($"Error Code: {exception.ErrorCode} with Message: {exception.Message}");
}