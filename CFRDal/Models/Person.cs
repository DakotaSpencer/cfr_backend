using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFRDal.Models
{
    public class Person
    {
        public string Name { get; set; }

        public int Id { get; set; }
        public string[]? also_known_as { get; set; }
        public string? biography { get; set; }
        public string? birthday { get; set; }
        public string? deathday { get; set; }
        public int? gender { get; set; }
        public string? homepage { get; set; }
        public string? known_for_department { get; set; }
        public string? place_of_birth { get; set; }
        public double? popularity { get; set; }
        public string? profile_path { get; set; }
    }
}
