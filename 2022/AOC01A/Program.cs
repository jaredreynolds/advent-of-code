using AOC01A;
using Data;

var challengeData = Fetcher.GetChallengeData("01");
var input = challengeData.Input;

var elves = InputParser.ParseInput(input);

var mostCalorieElf = elves.MaxBy(elf => elf.TotalCalories)!;
Console.WriteLine($"Elf: {mostCalorieElf.Number}; Calories: {mostCalorieElf.TotalCalories}");
