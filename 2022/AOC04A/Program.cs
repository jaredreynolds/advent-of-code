using AOC04A;
using Data;

var challengeData = Fetcher.GetChallengeData("04");
var input = challengeData.Input;

var assignmentPairs = InputParser.ParseInput(input);

var countFullyIncludedInOther = assignmentPairs.Count(ap => ap.IsOneAssignmentSubsetOfOther());
var countAnyOverlap = assignmentPairs.Count(ap => ap.AssignmentsOverlap());

Console.WriteLine($"{countFullyIncludedInOther}");
Console.WriteLine($"{countAnyOverlap}");
