using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFRDal.Models
{
    public class MovieHolder
    {
        public int page { get; set; }

        public List<SearchResultMovie> results { get; set; }
    }
}
