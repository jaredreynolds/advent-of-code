using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using SyntaxErrors;
using static SyntaxErrors.Utils;

var input = (new string[] {
    "[({(<(())[]>[[{[]{<()<>>",
    "[(()[<>])]({[<{<<[]>>(",
    "{([(<{}[<>[]}>{[]{[(<()>",
    "(((({<>}<{<{<>}{[]{[]{}",
    "[[<[([]))<([[{}[[()]]]",
    "[{[{({}]{}}([{[{{{}}([]",
    "{<[[]]>}<{[{[{[]{()[[[]",
    "[<(<(<(<{}))><([]([]()",
    "<{([([[(<>()){}]>(<<{{",
    "<{([{{}}[<[[[<>{}]]]>[]]",
});

var filename = args.Length > 0 ? args[0] : "../input.txt";
if (File.Exists(filename)) {
    input = File.ReadAllLines(filename);
} else {
    WriteLine(ConsoleColor.Yellow, "Input file not specified or found; using short sample data.");
}

var stack = new Stack<char>();
var brokenCharCounts = new ConcurrentDictionary<char, int>();

var data = input
    .Select(s => s.ToCharArray())
    .ToArray();

foreach (var chars in data)
{
    stack.Clear();
    var isBroken = false;

    foreach (var ch in chars)
    {
        if (IsClosingChar(ch, out var expectedOpeningChar))
        {
            var previousChar = stack.Pop();
            // WriteLine(ConsoleColor.DarkYellow, $"{ch}".Replace("{", "{{"));
            if (previousChar != expectedOpeningChar) {
                WriteLine(ConsoleColor.Red, escapeText: true, $"Broken: {ch}");
                isBroken = true;
                brokenCharCounts.AddOrUpdate(ch, 1, (_, count) => ++count);
                break;
            }
        }
        else
        {
            stack.Push(ch);
            // WriteLine(ConsoleColor.Green, new string(stack.ToArray()).Replace("{", "{{"));
        }
    }

    if (!isBroken && stack.Count > 0) {
        WriteLine(ConsoleColor.DarkYellow, "incomplete");
    }
}

var score = brokenCharCounts.Sum(kvp =>
{
    var (ch, count) = kvp;
    switch (ch)
    {
        case ')': return count * 3;
        case ']': return count * 57;
        case '}': return count * 1197;
        case '>': return count * 25137;
        default: return 0;
    }
});

WriteLine(ConsoleColor.Magenta, $"Final score: {score}!");

bool IsClosingChar(char ch, out char expectedOpeningChar)
{
    expectedOpeningChar = default;

    switch (ch)
    {
        case ')':
            expectedOpeningChar = '(';
            break;
        case ']':
            expectedOpeningChar = '[';
            break;
        case '}':
            expectedOpeningChar = '{';
            break;
        case '>':
            expectedOpeningChar = '<';
            break;
        default:
            return false;
    }

    return true;
}
