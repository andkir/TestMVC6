using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF6Test
{
    public class SportActivity
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public bool Free { get; set; }
        public MyEnum MyPropertyEnum { get; set; }

    }

    [Flags]
    public enum MyEnum
    {
        qwe,
        asd,
        zxc
    }
}
