using AOC02A;
using Data;

var challengeData = Fetcher.GetChallengeData("02");
var input = challengeData.Input;

var rounds = InputParser.ParseInput(input);

var score = rounds
    .Sum(round => round.MyPlay + round.Outcome);

Console.WriteLine($"Score: {score}");