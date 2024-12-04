using AOC08A;
using Data;

var challengeData = Fetcher.GetChallengeData("08");
var input = challengeData.Example;

var map = InputParser.ParseInput(input);

Console.WriteLine(map.TreesVisibleFromOutside);
