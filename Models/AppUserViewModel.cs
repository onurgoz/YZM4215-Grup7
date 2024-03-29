using System.Collections.Generic;

namespace YZM4215_Grup7.Models
{
    public class AppUserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public List<string> Roles { get; set; }
    }
}