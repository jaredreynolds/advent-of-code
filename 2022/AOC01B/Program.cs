using AOC01A;
using Data;

var challengeData = Fetcher.GetChallengeData("01");
var input = challengeData.Input;

var elves = InputParser.ParseInput(input);

var top3TotalCalories = elves
    .OrderByDescending(elf => elf.TotalCalories)
    .Take(3)
    .Sum(elf => elf.TotalCalories);

Console.WriteLine($"Top 3 elves' total calories: {top3TotalCalories}");
