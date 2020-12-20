using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication6.Models
{
    public class UserVersions
    {
        public int UserId { set; get; }
        public string UserName { set; get; }
        public int VersionsCount { set; get; }
        public bool ReadLastVersion { set; get; }
    }
}
