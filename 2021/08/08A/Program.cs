using System.Diagnostics.CodeAnalysis;
using System.Text;
using static Displays.Utils;

var input = (new string[] {
    "be cfbegad cbdgef fgaecd cgeb fdcge agebfd fecdb fabcd edb |fdgacbe cefdb cefbgd gcbe",
    "edbfga begcd cbg gc gcadebf fbgde acbgfd abcde gfcbed gfec |fcgedb cgb dgebacf gc",
    "fgaebd cg bdaec gdafb agbcfd gdcbef bgcad gfac gcb cdgabef |cg cg fdcagb cbg",
    "fbegcd cbd adcefb dageb afcb bc aefdc ecdab fgdeca fcdbega |efabcd cedba gadfec cb",
    "aecbfdg fbg gf bafeg dbefa fcge gcbea fcaegb dgceab fcbdga |gecf egdcabf bgf bfgea",
    "fgeab ca afcebg bdacfeg cfaedg gcfdb baec bfadeg bafgc acf |gebdcfa ecba ca fadegcb",
    "dbcfg fgd bdegcaf fgec aegbdf ecdfab fbedc dacgb gdcebf gf |cefg dcbef fcge gbcadfe",
    "bdfegc cbegaf gecbf dfcage bdacg ed bedf ced adcbefg gebcd |ed bcgafe cdgba cbgef",
    "egadfb cdbfeg cegd fecab cgb gbdefca cg fgcdab egfdb bfceg |gbdfcae bgc cg cgb",
    "gcafb gcf dcaebfg ecagb gf abcdeg gaef cafbge fdbac fegbdc |fgae cfgab fg bagce"
});

var filename = args.Length > 0 ? args[0] : "../input.txt";
if (File.Exists(filename)) {
    input = File.ReadAllLines(filename);
} else {
    WriteLine(ConsoleColor.Yellow, "Input file not specified or found; using short sample data.");
}

var uniquePatterns = new HashSet<int>(new[] { 2, 4, 3, 7 }); // number of segments for digits 1, 4, 7, 8

var outputs = input.Select(line =>
{
    var parts = line.Split('|');
    // var patterns = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var output = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
    // return (patterns, output);
    return output;
}).ToArray();

var uniquePatternCount = outputs.Sum(output => {
    return output.Count(digit => uniquePatterns.Contains(digit.Length));
});

System.Console.WriteLine(uniquePatternCount);
