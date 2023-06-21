using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Data.Models
{
    public class ExcelExportData
    {
        public string ARRIVAL_DATE { get; set; }
        public string DEPARTURE_DATE { get; set; }
        public double PRICE { get; set; }
        public string CURRENCY { get; set; }
        public string RATENAME { get; set; }
        public int ADULTS { get; set; }
        public int BREAKFAST_INCLUDED { get; set; }
    }
}
