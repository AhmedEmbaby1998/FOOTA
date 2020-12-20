using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class Line
    {
        public int FileDataId { set; get; }
        public int LineNum { set; get; }
        public string Data { set; get; }
        public FileData FileData { set; get; }
        public int Type { set; get; }        
    }
}
