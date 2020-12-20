using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Reading
    {
        public int ConsumerId { set; get; }
        public int FileDataId { set; get; }
        public DateTime ReadingDateTime { set; get; }
        public FileData FileData { set; get; }
        public Consumer Consumer { set; get; }
    }
}
