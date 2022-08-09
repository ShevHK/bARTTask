using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART.Model.Entities
{
    public class contacts
    {
        [Key] public string email { get; set; }
        public string first_name { get; set; }
        public string Last_name { get; set; }
        public long? phone { get; set; }
        public string account_name { get; set; }
    }
}
