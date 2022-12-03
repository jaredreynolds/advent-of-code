using AOC03A;
using Data;

var challengeData = Fetcher.GetChallengeData("03");
var input = challengeData.Input;

var rucksacks = InputParser.ParseInput(input);

var sum = rucksacks.Select(r => r.GetCommonItem().Priority).Sum();
Console.WriteLine($"sum: {sum}");
