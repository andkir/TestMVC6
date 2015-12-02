using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6Test
{
    public class EfTestContext : DbContext
    {
        public DbSet<SportComplex> SportComplexes { get; set; }
        public DbSet<SportActivity> SportActivities { get; set; }

        public IEnumerable<SportActivity> GetFreeSportActivities()
        {
            return this.Database.SqlQuery<SportActivity>("exec [dbo].[spGetFreeSportActivities]").ToList();
        }
    }
}
