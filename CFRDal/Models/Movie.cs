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
        public int Id { get; set; }

        [DisplayName("Rated Adult")]
        public bool RatedAdult { get; set; }
        
        public string Overview { get; set; }

        [DisplayName("Release Date")]
        public string ReleaseDate { get; set; }

        public List<string> Genres { get; set; }

        public string Title { get; set; }

        [DisplayName("Original Language")]
        public string OriginalLanguage { get; set; }
    }
}
