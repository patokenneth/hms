using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Data.Models
{
    public class HotelRate
    {
        public int Adults { get; set; }
        public int Los { get; set; }
        public string RateDescription { get; set; }
        public int RateID { get; set; }
        public string RateName { get; set; }
        public DateTime TargetDay { get; set; }
        public Price Price { get; set; }
        public List<RateTag> RateTags { get; set; }
    }
}
