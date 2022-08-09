using bART.Model.Entities;
using System.Collections.Generic;

namespace bART.Creations.Response
{
    public class GetAllResponse
    {
        public List<incidents> incidents { get; set; }
        public List<accounts> accounts { get; set; }
        public List<contacts> contacts { get; set; }
    }
}
