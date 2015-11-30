using System;
using System.Data.Entity;
using System.Linq;

namespace EF6Test
{
    public class Program
    {
        public static void Main()
        {
            SportActivity act;
            using (var context = new EfTestContext())
            {
                act = context.SportActivities.First();
            }

            act.Free = false;

            using (var context = new EfTestContext())
            {
                var state = context.Entry(act).State;
               // context.SportActivities.Attach(act);
                state = context.Entry(act).State;
                context.Entry(act).State = EntityState.Modified;
                context.SaveChanges();
            }

            Console.ReadKey();
        }
    }
}

