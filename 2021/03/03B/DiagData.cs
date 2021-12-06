namespace PowerRates;

public class DiagData
{
    public DiagData(string raw)
    {
        Raw = raw;
        Value = Convert.ToInt32(raw, 2);
    }

    public string Raw { get; init; }
    public int Value { get; init; }
}
