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
var scores = new List<ulong>();

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
            if (previousChar != expectedOpeningChar) {
                WriteLine(ConsoleColor.DarkRed, escapeText: true, $"Broken: {ch}");
                isBroken = true;
                break;
            }
        }
        else
        {
            stack.Push(ch);
        }
    }

    if (!isBroken && stack.Count > 0) {
        Write(ConsoleColor.DarkYellow, "incomplete - repairing...  ");
        ulong score = 0;
        while(stack.Count > 0) {
            var openingChar = stack.Pop();
            var (closingChar, points) = GetClosingCharAndPoints(openingChar);
            Write(ConsoleColor.Green, escapeText: true, closingChar.ToString());
            score = (score * 5) + points;
        }
        WriteLine(ConsoleColor.DarkMagenta, $" = {score}");
        scores.Add(score);
    }
}

scores = scores.OrderBy(s => s).ToList();
WriteLine(ConsoleColor.DarkGray, string.Join(", ", scores));
WriteLine(ConsoleColor.Magenta, $"Middle score: {scores[scores.Count / 2]}!");

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

(char, ulong) GetClosingCharAndPoints(char ch)
{
    switch (ch)
    {
        case '(': return (')', 1);
        case '[': return (']', 2);
        case '{': return ('}', 3);
        case '<': return ('>', 4);
        default: return ('X', 0);
    }
}
