using AOC05A;
using Data;

var challengeData = Fetcher.GetChallengeData("05");
var input = challengeData.Input;

var (dock, moves) = InputParser.ParseInput(input);

dock.Execute(moves, Dock.MoveStyle.Single);
dock.PrintTops();

(dock, moves) = InputParser.ParseInput(input);

dock.Execute(moves, Dock.MoveStyle.Multiple);
dock.PrintTops();
