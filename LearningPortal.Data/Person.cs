public class Person
{
    public string Na me { get; set; }
    public int Age { get; set; }

    public void Greet() => Console.WriteLine($"Hi, I'm {Name}.");
}
