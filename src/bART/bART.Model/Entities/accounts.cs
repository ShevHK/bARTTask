using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART.Model.Entities
{
    public class accounts
    {
        [Key] public string name { get; set; }
        public string incidents_name{ get; set; }
    }
}
