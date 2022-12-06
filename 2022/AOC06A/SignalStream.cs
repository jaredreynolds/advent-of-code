namespace AOC06A;

public class SignalStream
{
    public int PacketMarkerEndIndex { get; }
    public int MessageMarkerEndIndex { get; set; }

    public SignalStream(string signalStream)
    {
        PacketMarkerEndIndex = FindMarkerIndex(signalStream, MarkerType.Packet);
        MessageMarkerEndIndex = FindMarkerIndex(signalStream, MarkerType.Message);
    }

    private static int FindMarkerIndex(string signalStream, MarkerType markerType)
    {
        Queue<char> patternBuffer = new();
        HashSet<char> markerFinder;
        int index;
        var markerLength = (int)markerType;

        for (index = 0; index < signalStream.Length; index++)
        {
            patternBuffer.Enqueue(signalStream[index]);

            if (index < markerLength - 1) continue;

            markerFinder = patternBuffer.ToHashSet();

            if (markerFinder.Count == markerLength) break;

            patternBuffer.Dequeue();
        }

        return index + 1;
    }
}
