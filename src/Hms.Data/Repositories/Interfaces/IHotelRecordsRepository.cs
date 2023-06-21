using Hms.Data.Models;

namespace Hms.Data.Repositories.Interfaces
{
    public interface IHotelRecordsRepository
    {
        Info? GetHotelInfo(int hotelId, string arrivalDate);

        Task<Info> GenerateReport();
    }
}
