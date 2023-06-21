using FastMember;
using Hms.Core.Infrastructure.Extensions;
using Hms.Core.Infrastructure.Services.Interface;
using Hms.Data.Models;
using Hms.Data.Repositories.Interfaces;
using OfficeOpenXml;
using System.Data;

namespace Hms.Core.Infrastructure.Services.Implementation
{
    public class TaskService : ITaskService
    {
        private readonly IHotelRecordsRepository _recordsRepository;

        public TaskService(IHotelRecordsRepository recordsRepository)
        {
            _recordsRepository = recordsRepository;
        }

        public async Task TaskOne()
        {
            throw new NotImplementedException();
        }

        public async Task TaskTwo()
        {
            var table = new DataTable();
            var data = await _recordsRepository.GenerateReport();
            var excelData = new List<ExcelExportData>();
            foreach (var rate in data.HotelRates)
            {
                var toAdd = new ExcelExportData()
                {
                    ARRIVAL_DATE = rate.TargetDay.Date.Format(),
                    DEPARTURE_DATE = Helpers.GetDepartureDate(rate.Los, rate.TargetDay).Format(),
                    PRICE = rate.Price.NumericFloat,
                    CURRENCY = rate.Price.Currency,
                    RATENAME = rate.RateName,
                    ADULTS = rate.Adults
                };

                var breakFastTag = rate.RateTags?.FirstOrDefault(x => x.Name == "breakfast");
                if (breakFastTag != null)
                {
                    toAdd.BREAKFAST_INCLUDED = breakFastTag.Shape ? 1 : 0;
                }
                excelData.Add(toAdd);
            }

            using (var reader = ObjectReader.Create(excelData))
            {
                table.Load(reader);
            }

            var fileInfo = new FileInfo("task2.xlsx");
            //if a similarly named file is existing already, delete and create again.
            if (fileInfo.Exists)
            {
                fileInfo.Delete();
                fileInfo = new FileInfo("task2.xlsx");
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var pkg = new ExcelPackage(fileInfo))
            {
                var workSheet = pkg.Workbook.Worksheets.Add("Hotel report");
                workSheet.Cells["A1"].LoadFromDataTable(table, PrintHeaders: true);
                await pkg.SaveAsync();
            }
        }

        public Info? TaskThree(int hotelId, string arrivalDate)
        {
            return _recordsRepository.GetHotelInfo(hotelId, arrivalDate);
        }
    }
}
