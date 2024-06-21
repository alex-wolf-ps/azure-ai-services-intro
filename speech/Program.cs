using Microsoft.CognitiveServices.Speech;

var config = SpeechConfig.FromSubscription("691ee6d6242f48d2b05301fb54e2d9b0", "eastus");

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