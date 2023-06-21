using Hms.Data.Models;

namespace Hms.Core.Infrastructure.Services.Interface
{
    public interface ITaskService
    {
        Task TaskOne();
        Task TaskTwo();
        Info TaskThree(int hotelId, string arrivalDate);
    }
}
