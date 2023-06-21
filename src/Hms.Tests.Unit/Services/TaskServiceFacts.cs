using Hms.Core.Infrastructure.Services.Implementation;
using Hms.Core.Infrastructure.Services.Interface;
using Hms.Data.Models;
using Hms.Data.Repositories.Interfaces;
using Moq;

namespace Hms.Tests.Unit.Services
{
    [TestFixture]
    public class TaskServiceFacts
    {
        private ITaskService _taskService;
        private Mock<IHotelRecordsRepository> _recordsRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            _recordsRepository = new Mock<IHotelRecordsRepository>();
            _taskService = new TaskService(_recordsRepository.Object);
        }

        [Test]
        public async Task TaskTwo_ShouldGenerateAFile()
        {
            // Arrange
            _recordsRepository.Setup(x => x.GenerateReport()).ReturnsAsync(new Info()
            {
                Hotel = new Hotel()
                {
                    HotelID = It.IsAny<int>(),
                    Name = "demo"
                },
                HotelRates = new List<HotelRate>()
                {
                    new HotelRate(){Adults = 2, TargetDay = DateTime.Now, Price = new Price(){Currency = "EUR", NumericFloat = 2.2}, RateTags = new List<RateTag>()
                    {
                        new RateTag() {Name = "breakfast", Shape = true}
                    }}
                }
            });

            // Act
            var file = new FileInfo("task2.xlsx");
            if (file.Exists) file.Delete();
            await _taskService.TaskTwo();
            file = new FileInfo("task2.xlsx");
            // Assert
            Assert.IsTrue(file.Exists);
        }

        [Test]
        public void TaskThree_ShouldReturnNull_WhenHotelDoesNotExists()
        {
            // Arrange
            var hotelId = 5;
            var arrivalDate = DateTime.Now.ToString();
            _recordsRepository.Setup(x => x.GetHotelInfo(hotelId, arrivalDate)).Returns(new Info()
            {
                Hotel = new Hotel()
                {
                    HotelID = hotelId,
                    Name = "test hotel"
                },
                HotelRates = new List<HotelRate>()
                {
                    new HotelRate(){Adults = 2, TargetDay = DateTime.Parse(arrivalDate)}
                }
            });

            // Act
            var result = _taskService.TaskThree(5, DateTime.Parse(arrivalDate).AddDays(1).ToString());

            // Assert
            Assert.IsNull(result);
        }
    }
}