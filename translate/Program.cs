using Azure;
using Azure.AI.Translation.Text;

string key = "691ee6d6242f48d2b05301fb54e2d9b0";
string region = "eastus";

var client = new TextTranslationClient(new AzureKeyCredential(key), region);

try
{
    Console.WriteLine("Text to translate:");
    string sourceText = Console.ReadLine();
    string targetLanguage = "es"; // language code for German

    var response = await client.TranslateAsync(targetLanguage, sourceText);
    TranslatedTextItem translation = response.Value.FirstOrDefault();

    Console.WriteLine($"Source language: {translation?.DetectedLanguage?.Language}");
    Console.WriteLine($"Translated text: '{translation?.Translations?.FirstOrDefault()?.Text}'.");
}
catch (RequestFailedException exception)
{
    Console.WriteLine($"Error: {exception.ErrorCode} - {exception.Message}");
}