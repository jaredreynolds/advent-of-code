namespace AOC01A;

public class Food
{
    public int Calories { get; internal set; }

    public Food(int calories)
    {
        Calories = calories;
    }
}