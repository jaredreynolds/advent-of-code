using System.Collections.ObjectModel;

namespace AOC01A;

public class Elf
{
    public int Number { get; set; }
    public ObservableCollection<Food> FoodItems { get; } = new ObservableCollection<Food>();
    public int TotalCalories { get; private set; }

    public Elf(int number)
    {
        Number = number;
        FoodItems.CollectionChanged += FoodItems_CollectionChanged;
    }

    private void FoodItems_CollectionChanged(object? sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
    {
        foreach (Food food in e.NewItems ?? new List<Food>())
        {
            TotalCalories += food.Calories;
        }
    }
}
