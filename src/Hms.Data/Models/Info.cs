using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Data.Models
{
    public class Info
    {
        public Hotel Hotel { get; set; }
        public List<HotelRate> HotelRates { get; set; }
    }
}
