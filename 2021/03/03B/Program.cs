﻿using System.Text;
using PowerRates;
using static PowerRates.Utils;

var data = (new[] {
    "00100",
    "11110",
    "10110",
    "10111",
    "10101",
    "01111",
    "00111",
    "11100",
    "10000",
    "11001",
    "00010",
    "01010"
});

var filename = args.Length > 0 ? args[0] : "../input.txt";
if (File.Exists(filename)) {
    data = File.ReadAllLines(filename).ToArray();
} else {
    WriteLine(ConsoleColor.Yellow, "Input file not specified or found; using short sample data.");
}

var parsedData = data.Select(d => new DiagData(d)).ToArray();

var digitCount = data[0].Length;
// var columnData = new (int onesCount, int zerosCount)[digitCount];

var oxygenRating = GetRating((zero, one) => one >= zero);
var scrubberRating = GetRating((zero, one) => one < zero);

System.Console.WriteLine($"oxygen: {oxygenRating.value} ({oxygenRating.str})");
System.Console.WriteLine($"scrubber: {scrubberRating.value} ({scrubberRating.str})");
System.Console.WriteLine($"life support: {oxygenRating.value} * {scrubberRating.value} = {oxygenRating.value * scrubberRating.value}");

(string str, int value) GetRating(Func<int, int, bool> filterFn)
{
    System.Console.WriteLine("*************");
    var tempParsedData = parsedData.Copy();
    var index = 0;
    var sb = new StringBuilder();

    while (index < digitCount)
    {
        int zerosCount = 0;
        int onesCount = 0;

        foreach (var diagData in tempParsedData)
        {
            var digitString = diagData.Raw[index];
            if (digitString == '0')
            {
                ++zerosCount;
            }
            else
            {
                ++onesCount;
            }
        }

        var filter = filterFn(zerosCount, onesCount) ? '1' : '0';
        sb.Append(filter);

        tempParsedData = tempParsedData.Where(d => d.Raw[index] == filter).ToArray();

        foreach (var tpd in tempParsedData)
        {
            System.Console.WriteLine(tpd.Raw);
        }
        System.Console.WriteLine();

        if (tempParsedData.Length == 1)
        {
            return (tempParsedData[0].Raw, tempParsedData[0].Value);
        }

        ++index;
    }

    // var str = sb.ToString();
    // return (str, Convert.ToInt32(str, 2));
    return ("derp", -1);
}

// foreach (var diagData in parsedData)
// {
//     for (int i = 0; i < digitCount; i++)
//     {
//         var digitString = diagData.Raw[i];
//         if (digitString == '0')
//         {
//             columnData[i].zerosCount++;
//         }
//         else
//         {
//             columnData[i].onesCount++;
//         }
//     }
// }

// var sb = new StringBuilder();

// foreach (var colData in columnData)
// {
//     var gammaDigit = colData.onesCount > colData.zerosCount ? 1 : 0;
//     sb.Append(gammaDigit);
// }

// var gammaRateString = sb.ToString();
// var gammaRate = Convert.ToInt32(gammaRateString, 2);
// var epsilonRate = gammaRate ^ ((int)Math.Pow(2, digitCount) - 1);

// System.Console.WriteLine($"gamma: {gammaRate} ({gammaRateString})");
// System.Console.WriteLine($"epsilon: {epsilonRate} ({Convert.ToString(epsilonRate, 2)})");
// System.Console.WriteLine($"power rate = {gammaRate * epsilonRate}");
