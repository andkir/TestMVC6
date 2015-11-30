using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("dfgdfg");
        Console.ReadLine();

        var ctx = new EF6Test.EfTestContext();
        ctx.Database.Initialize(true);
        Console.ReadLine();
    }
}
