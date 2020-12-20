using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class FileData
    {
        public FileData()
        {
            Lines = new List<Line>();
            Readings = new List<Reading>();
        }
        public int Id { set; get; }
        public DateTime UploadingDate { set; get; }
        public int LineCount { set; get; }
        public List<Line> Lines { set; get; }
        public List<Reading> Readings { set; get; }

    }
}
