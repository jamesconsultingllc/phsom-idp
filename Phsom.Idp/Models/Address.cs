using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Phsom.Idp.Models
{
    public class Address
    {
        public string street_address { get; set; }
        public string locality { get; set; }
        public string region { get; set; }
        public string postal_code { get; set; }
    }
}
