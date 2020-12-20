using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Consumer
    {
        public int Id { set; get; }
        public string UserName { set; get; }
        public int UpdateCount { set; get; }
        public string Password { set; get; }
        public List<Reading> Readings { set; get; }
        public DateTime ActiveTill {set; get; }
    }
}
