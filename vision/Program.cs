using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

var imageUrl = "https://aiservicesintro.blob.core.windows.net/receipts/receipt.jpg";

var client = new ComputerVisionClient(
    new ApiKeyServiceClientCredentials("691ee6d6242f48d2b05301fb54e2d9b0"))
    { Endpoint = "https://demoaiservices222.openai.azure.com" };

var headers = await client.ReadAsync(imageUrl);

int idLength = 36;
string operationId = headers.OperationLocation.Substring(headers.OperationLocation.Length - idLength);

ReadOperationResult results = new();
while ((results.Status == OperationStatusCodes.Running || results.Status == OperationStatusCodes.NotStarted)) 
{
    results = await client.GetReadResultAsync(Guid.Parse(operationId));
}

foreach (ReadResult page in results.AnalyzeResult.ReadResults)
{
    foreach (Line line in page.Lines)
    {
        Console.WriteLine(line.Text);
    }
}
