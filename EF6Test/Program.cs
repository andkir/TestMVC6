using System;
using System.Data.Entity;
using EF6Test;
using EF6Test.Migrations;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("dfgdfg");
        Console.ReadLine();

        Database.SetInitializer(new MigrateDatabaseToLatestVersion<EfTestContext, Configuration>());

        var ctx = new EF6Test.EfTestContext();
        ctx.Database.Initialize(true);
       // var t = ctx.GetFreeSportActivities();

        //
        Console.ReadLine();
    }
}
