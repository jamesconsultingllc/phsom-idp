using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phsom.Idp.Models
{
    public class Profile
    {
        public string sid { get; set; }
        public string sub { get; set; }
        public int auth_time { get; set; }
        public string idp { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string preferred_username { get; set; }
        public Address address { get; set; }
        public string name { get; set; }
        public List<string> amr { get; set; }
    }
}
