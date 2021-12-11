namespace SyntaxErrors;

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
        Write(foregroundColor, Console.BackgroundColor, escapeText: false, linebreak: true, text, parameters);
    }

    public static void WriteLine(ConsoleColor foregroundColor, bool escapeText, string text, params object[] parameters)
    {
        Write(foregroundColor, Console.BackgroundColor, escapeText, linebreak: true, text, parameters);
    }

    public static void Write(ConsoleColor foregroundColor, string text, params object[] parameters)
    {
        Write(foregroundColor, Console.BackgroundColor, escapeText: false, linebreak: false, text, parameters);
    }

    public static void Write(ConsoleColor foregroundColor, bool escapeText, string text, params object[] parameters)
    {
        Write(foregroundColor, Console.BackgroundColor, escapeText, linebreak: false, text, parameters);
    }

    public static void Write(ConsoleColor foregroundColor, ConsoleColor backgroundColor, bool escapeText, bool linebreak, string text, params object[] parameters)
    {
        if (escapeText)
        {
            text = text.Replace("{", "{{").Replace("}", "}}");
        }

        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
        Console.Write(text, parameters);
        Console.ResetColor();

        if (linebreak)
        {
            Console.WriteLine();
        }
    }

    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        if (source == null || action == null) {
            return;
        }

        foreach (var item in source)
        {
            action(item);
        }
    }

    public static void Increment<T>(this IDictionary<T, int> source, T[] indices)
    {
        if (source == null || indices.Length == 0) {
            return;
        }

        foreach (var index in indices)
        {
            if (source.TryGetValue(index, out var count))
            {
                ++source[index];
            }
            else
            {
                source[index] = 1;
            }
        }
    }

    public static void AddRange<T>(this ICollection<T> target, IEnumerable<T> source)
    {
        if (target == null || source == null)
        {
            return;
        }

        foreach (var item in source)
        {
            target.Add(item);
        }
    }

    public static T[] InitializeWithNewObjects<T>(this T[] array) where T : new()
    {
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = new T();
        }

        return array;
    }

    public static T[] Initialize<T>(this T[] array, Func<int, T> initializer)
    {
        if (initializer == null)
        {
            return array;
        }

        for (int i = 0; i < array.Length; i++)
        {
            array[i] = initializer(i);
        }

        return array;
    }

    public static string AsSortedString(this HashSet<char> set)
    {
        if (set == null)
        {
            return String.Empty;
        }

        return String.Concat(set.OrderBy(c => c));
    }
}
