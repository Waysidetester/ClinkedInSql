using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ClinkedIN.Models
{
    public class Interest
    {
        public Interest(string name)
        {
            Name = name;
        }

        public string Name { get; set; }
        public int Id { get; set; }
        public int UserId { get; set; }

    }

}
