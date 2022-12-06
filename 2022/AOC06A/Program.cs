using AOC06A;
using Data;

var challengeData = Fetcher.GetChallengeData("06");
var input = challengeData.Input;

var signalStreams = InputParser.ParseInput(input);

foreach (var signalStream in signalStreams)
{
    Console.WriteLine($"({signalStream.PacketMarkerEndIndex},{signalStream.MessageMarkerEndIndex})");
}
