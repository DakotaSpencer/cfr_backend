using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFRDal.Models
{
    public class Movie
    {
        public int id { get; set; }

        public bool adult { get; set; }
        
        public string overview { get; set; }

        public string release_date { get; set; }

        public Object[] genres { get; set; }

        public string title { get; set; }

        public string original_language { get; set; }
        
        public string backdrop_path { get; set; }

        public string tagline { get; set; }

        public string poster_path { get; set; }

        public Object[] production_companies { get; set; }
    }
}
