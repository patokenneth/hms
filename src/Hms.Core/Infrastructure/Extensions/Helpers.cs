using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hms.Core.Infrastructure.Extensions
{
    public static class Helpers
    {
        public static DateTime GetDepartureDate(int los, DateTime ArrivalDate)
        {
            return ArrivalDate.AddDays(los);
        }

        public static string Format(this DateTime date)
        {
            return date.ToString("dd.MM.yy");
        }
    }
}
