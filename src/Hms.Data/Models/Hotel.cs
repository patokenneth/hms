using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Data.Models
{
    public class Hotel
    {
        public int Classification { get; set; }
        public int HotelID { get; set; }
        public string Name { get; set; }
        public double ReviewScore { get; set; }
    }
}
