using Hms.Data.Models;
using Hms.Data.Repositories.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Hms.Data.Repositories.Implementation
{
    public class HotelRecordsRepository : IHotelRecordsRepository
    {
        private Dictionary<int, Info> DataStore;
        private Info? DataForExport;
        public HotelRecordsRepository()
        {
            DataStore = new Dictionary<int, Info>();
            LoadData();
        }

        public Info? GetHotelInfo(int hotelId, string arrivalDate)
        {
            if (!DateTime.TryParse(arrivalDate, out var date))
            {
                Console.WriteLine("Date passed is invalid");
                return null;
            }
            var hotelInfo = new Info();
            try
            {
                var hotel = DataStore[hotelId];
                hotelInfo.Hotel = hotel.Hotel;
                hotelInfo.HotelRates = new List<HotelRate>();
                foreach (var rate in hotel.HotelRates)
                {
                    if (rate.TargetDay.Date == date.Date)
                    {
                        hotelInfo.HotelRates.Add(rate);
                    }
                }
            }
            catch (KeyNotFoundException e)
            {
                Console.WriteLine($"{hotelId} does not exist in our database. {e.Message}.");
                return null;
            }
            return hotelInfo;
        }

        public async Task<Info> GenerateReport()
        {
            DataForExport = new Info();
            try
            {
                var directory = Environment.CurrentDirectory;
                var path = Path.Combine(directory, @"Task2.json");
                StreamReader reader = new StreamReader(path);
                var data = await reader.ReadToEndAsync();
                var res = new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver()
                    {
                        NamingStrategy = new DefaultNamingStrategy()
                    }
                };
                DataForExport = JsonConvert.DeserializeObject<Info>(data, res);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return DataForExport;
        }

        private void LoadData()
        {
            var records = new List<Info>();
            try
            {
                string directory = Environment.CurrentDirectory;
                string path = Path.Combine(directory, @"Task3.json");
                StreamReader reader = new StreamReader(path);
                string data = reader.ReadToEnd();
                
                records = JsonConvert.DeserializeObject<List<Info>>(data);
                foreach (var record in records)
                {
                    DataStore.Add(record.Hotel.HotelID, record);
                }
            }
            catch (Exception e)
            {
                //log any unforeseen exception
                Console.WriteLine(e.Message ?? e.InnerException?.Message);
            }
        }
    }
}
