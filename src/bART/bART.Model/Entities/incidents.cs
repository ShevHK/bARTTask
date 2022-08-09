using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART.Model.Entities
{
    public class incidents
    {
        [Key] public string name { get; set; }
        public string? description { get; set; }
    }
}
