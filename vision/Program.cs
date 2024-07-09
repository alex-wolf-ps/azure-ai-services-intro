using Microsoft.Azure.CognitiveServices.Vision.ComputerVision;
using Microsoft.Azure.CognitiveServices.Vision.ComputerVision.Models;

var imageUrl = "https://aiservicesintro.blob.core.windows.net/receipts/receipt.jpg";

var client = new ComputerVisionClient(
    new ApiKeyServiceClientCredentials("your-ai-services-key"))
    { Endpoint = "your-ai-services-endpoint" };

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
