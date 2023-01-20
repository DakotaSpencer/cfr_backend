using CFRDal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CFRDal
{
    public interface IManager
    {
        public string GetMovie(int id);
    }
}
