using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFRDal.Models
{
    public class ActorHolder
    {
        public int page { get; set; }

        public List<Person> results { get; set; }
    }
}
