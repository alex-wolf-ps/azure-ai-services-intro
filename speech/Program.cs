using Microsoft.CognitiveServices.Speech;

var config = SpeechConfig.FromSubscription("your-key", "your-region");

// Creates a speech synthesizer using the default speaker as audio output.
using (var synthesizer = new SpeechSynthesizer(config))
{
    // Receive a text from console input and synthesize it to speaker.
    Console.WriteLine("Type some text that you want to speak...");
    string text = Console.ReadLine();
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

            if (cancellation.Reason == CancellationReason.Error)
            {
                Console.WriteLine($"CANCELED: ErrorDetails=[{cancellation.ErrorDetails}]");
            }
        }
    }
}