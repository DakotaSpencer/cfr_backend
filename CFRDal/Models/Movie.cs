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

        [DisplayName("Rated Adult")]
        public bool adult { get; set; }
        
        public string overview { get; set; }

        [DisplayName("Release Date")]
        public string release_date { get; set; }

        public Object[] genres { get; set; }

        public string title { get; set; }

        [DisplayName("Original Language")]
        public string original_language { get; set; }
        
        public string backdrop_path { get; set; }
    }
}
