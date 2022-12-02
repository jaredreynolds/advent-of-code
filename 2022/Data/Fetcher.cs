using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using System.Reflection;

namespace Data;

public static class Fetcher
{
    public static ChallengeData GetChallengeData(string day)
    {
        var embeddedFileProvider = new ManifestEmbeddedFileProvider(Assembly.GetExecutingAssembly());

        return new ChallengeData
        {
            Example = GetFile(embeddedFileProvider, $@"Input\{day}\example.txt").ToArray(),
            Input = GetFile(embeddedFileProvider, $@"Input\{day}\input.txt").ToArray()
        };
    }

    private static IEnumerable<string> GetFile(IFileProvider fileProvider, string filename)
    {
        var fileInfo = fileProvider.GetFileInfo(filename);
        Debug.Assert(fileInfo.Exists);

        using var stream = fileInfo.CreateReadStream();
        using var reader = new StreamReader(stream);

        string? line;
        while ((line = reader.ReadLine()) is not null)
        {
            yield return line;
        }
    }
}
