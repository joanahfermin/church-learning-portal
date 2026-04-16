public class Person
{
    public string Name { get; set; }
    public int Age { get; set; }

    public void Greet() => Console.WriteLine($"Hi, I'm {Name}.");
}
