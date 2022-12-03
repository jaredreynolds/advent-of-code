namespace AOC03A;

public class Rucksack
{
    public List<List<Item>> Items { get; set; } = new();
    public List<Item> AllItems { get; set; } = new();
    public int GroupId { get; set; }

    public Rucksack(string itemIds, int rucksackId)
    {
        DecodeItems(itemIds);
        GroupId = rucksackId / 3;
    }

    private void DecodeItems(string itemIds)
    {
        int mid = itemIds.Length / 2;
        Items.Add(itemIds.ToCharArray()[..mid].Select(itemId => new Item(itemId)).ToList());
        Items.Add(itemIds.ToCharArray()[mid..].Select(itemId => new Item(itemId)).ToList());
        AllItems = Items[0].Concat(Items[1]).ToList();
    }

    public Item GetCommonItem()
    {
        var intersection = Items[0].IntersectBy(Items[1].Select(item => item.Id).ToArray(), item => item.Id).ToArray();
        return intersection.Single();
    }

    public static Item GetCommonItem(Rucksack[] group)
    {
        IEnumerable<Item> intersection = group[0].AllItems;

        for (int i = 1; i < group.Length; i++)
        {
            intersection = intersection.IntersectBy(group[i].AllItems.Select(item => item.Id), item => item.Id);
        }

        return intersection.Single();
    }
}
