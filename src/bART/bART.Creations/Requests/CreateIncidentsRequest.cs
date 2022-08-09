using bART.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART.Creations.Requests
{
    public class CreateIncidentsRequest
    {
        public List<Inc> incidents { get; set; }
    }

    public class Inc
    { 
        public incidents incident { get; set; }
        public List<Acc> accounts { get; set; }
    }

    public class Acc
    {
        public string name { get; set; }
        public List<Cont> contacts { get; set; }
    }

    public class Cont
    {
        public string email { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public long? phone { get; set; }
        public int account_id { get; set; }
    }




}
