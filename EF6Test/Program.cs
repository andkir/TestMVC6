using System;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("dfgdfg");
        Console.ReadLine();

        var ctx = new EF6Test.EfTestContext();
        var t = ctx.GetFreeSportActivities();


        Console.ReadLine();
    }
}
