using Microsoft.CognitiveServices.Speech;

var config = SpeechConfig.FromSubscription("your-ai-services-key", "eastus");

Console.WriteLine("Your text to speech input:");
string text = Console.ReadLine();

using (var synthesizer = new SpeechSynthesizer(config))
{
    using (var result = await synthesizer.SpeakTextAsync(text))
    {
        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
        {
            Console.WriteLine($"Speech synthesized to speaker for text [{text}]");
        }
        else if (result.Reason == ResultReason.Canceled)
        {
            var cancellation = SpeechSynthesisCancellationDetails.FromResult(result);
            Console.WriteLine($"CANCELED: Reason={cancellation.Reason}");
        }
    }
}