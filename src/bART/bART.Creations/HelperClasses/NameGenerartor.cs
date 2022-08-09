using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bART.Creations.HelperClasses
{
    public class NameGenerartor
    {
        public static string Name() 
        {
            var rand = new Random();
            var number = rand.Next(1,10000);
            return "incident №"+number;
        }
    }
}
