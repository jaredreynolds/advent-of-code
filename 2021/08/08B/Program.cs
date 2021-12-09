using System.Diagnostics.CodeAnalysis;
using System.Text;
using Displays;
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

var data = input.Select(line =>
{
    var parts = line.Split('|');
    var codes = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(digit => new HashSet<char>(digit.ToCharArray())).ToArray();
    var output = parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(digit => new HashSet<char>(digit.ToCharArray())).ToArray();
    return (codes, output);
});

int outputSum = 0;

foreach (var display in data)
{
    var (codes, output) = display;
    var codesByLength = codes.ToLookup(c => c.Count);
    var digits = new HashSet<char>[10].InitializeWithNewObjects();

    // foreach (var grouping in codesByLength)
    // {
    //     var codesString = string.Join(' ', grouping.Select(c => c.AsSortedString()));
    //     System.Console.WriteLine($"{grouping.Key}: {codesString}");
    // }

    // handle digits of known length (1, 4, 7, 8)
    digits[1] = codesByLength[2].First();
    digits[4] = codesByLength[4].First();
    digits[7] = codesByLength[3].First();
    digits[8] = codesByLength[7].First();

    // handle codes of length 6 (6, 9, 0)
    foreach (var code in codesByLength[6])
    {
        if (!code.IsSupersetOf(digits[1]))
        {
            digits[6] = code;
        }
        else if (code.IsSupersetOf(digits[4]))
        {
            digits[9] = code;
        }
        else
        {
            digits[0] = code;
        }
    }

    // handle codes of length 5 (2, 3, 5)
    foreach (var code in codesByLength[5])
    {
        if (code.IsSupersetOf(digits[1]))
        {
            digits[3] = code;
        }
        else if (code.IsSubsetOf(digits[6]))
        {
            digits[5] = code;
        }
        else
        {
            digits[2] = code;
        }
    }

    // System.Console.WriteLine();
    // for (int i = 0; i < digits.Length; i++)
    // {
    //     System.Console.WriteLine($"{i}: {digits[i].AsSortedString()}");
    // }
    // System.Console.WriteLine();

    var digitsByCode = new Dictionary<string, int>();
    for (int i = 0; i < digits.Length; i++)
    {
        digitsByCode.Add(digits[i].AsSortedString(), i);
    }

    var sb = new StringBuilder();
    foreach (var digitCode in output)
    {
        sb.Append(digitsByCode[digitCode.AsSortedString()]);
    }
    var outputNum = Int32.Parse(sb.ToString());
    outputSum += outputNum;
    System.Console.WriteLine(outputNum);
}

System.Console.WriteLine($"sum: {outputSum}");
