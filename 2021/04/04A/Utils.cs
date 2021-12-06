namespace Bingo;

public static class Utils {
    public static T[] Randomize<T>(this T[] input, int? numPasses = null)
    {
        var output = input.Copy();

        if (output.Length == 0)
        {
            return new T[0];
        }

        numPasses ??= input.Length;
        var random = new Random();

        for (int i = 0; i < numPasses.Value; i++)
        {
            swap(output, random.Next(input.Length), random.Next(input.Length));
        }

        return output;

        void swap(T[] array, int index1, int index2)
        {
            T temp = array[index1];
            array[index1] = array[index2];
            array[index2] = temp;
        }
    }

    public static T[] Copy<T>(this T[] input)
    {
        var output = new T[input.Length];
        Array.Copy(input, output, output.Length);
        return output;
    }

    public static void WriteLine(ConsoleColor foregroundColor, string text, params object[] parameters)
    {
        WriteLine(foregroundColor, Console.BackgroundColor, text, parameters);
    }

    public static void WriteLine(ConsoleColor foregroundColor, ConsoleColor backgroundColor, string text, params object[] parameters)
    {
        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.WriteLine(text, parameters);
        Console.ResetColor();
    }
}