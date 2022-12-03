using AOC03A;
using Data;

var challengeData = Fetcher.GetChallengeData("03");
var input = challengeData.Input;

var rucksacks = InputParser.ParseInput(input);

var sum = rucksacks
    .GroupBy(r => r.GroupId)
    .Sum(g => Rucksack.GetCommonItem(g.ToArray()).Priority);
Console.WriteLine($"sum: {sum}");
