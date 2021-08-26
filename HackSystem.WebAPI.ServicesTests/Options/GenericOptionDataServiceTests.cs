using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.Model.Option;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace HackSystem.WebAPI.Services.Options.Tests
{
    public class GenericOptionDataServiceTests
    {
        private HackSystemDBContext GetDBContext()
        {
            var genericOptions = new List<GenericOption>()
            {
                new GenericOption() { OptionName = "OptionName", OptionValue = "OptionValue_0" },
                new GenericOption() { OptionName = "OptionName", OptionValue = "OptionValue_1", OwnerLevel = "HackSystem",},
                new GenericOption() { OptionName = "OptionName", OptionValue = "OptionValue_2", OwnerLevel = "HackSystem", Category = "GenericCategory" },
                new GenericOption() { OptionName = "OptionName", OptionValue = "OptionValue_3", Category = "GenericCategory" },
            };
            var dbContext = new HackSystemDBContext(
                new DbContextOptionsBuilder<HackSystemDBContext>()
                .UseLazyLoadingProxies()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options);

            dbContext.AddRange(genericOptions);
            dbContext.SaveChanges();

            Assert.Equal(genericOptions.Count, dbContext.Set<GenericOption>().Count());
            return dbContext;
        }

        [Fact()]
        public void QueryGenericOptionsTest()
        {
            var logger = new Mock<ILogger<GenericOptionDataService>>();
            logger.Setup(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception?>(), It.IsAny<Func<object, Exception?, string>>())).Verifiable();
            IGenericOptionDataService genericOptionDataService = new GenericOptionDataService(logger.Object, this.GetDBContext());

            var optionValue = genericOptionDataService.QueryGenericOption("OtherOptionName").Result;
            Assert.Null(optionValue);

            optionValue = genericOptionDataService.QueryGenericOption("OptionName").Result;
            Assert.Equal("OptionValue_0", optionValue.OptionValue);

            optionValue = genericOptionDataService.QueryGenericOption("OptionName", "HackSystem").Result;
            Assert.Equal("OptionValue_1", optionValue.OptionValue);

            optionValue = genericOptionDataService.QueryGenericOption("OptionName", "OtherSystem").Result;
            Assert.Equal("OptionValue_0", optionValue.OptionValue);

            optionValue = genericOptionDataService.QueryGenericOption("OptionName", category: "GenericCategory").Result;
            Assert.Equal("OptionValue_3", optionValue.OptionValue);

            optionValue = genericOptionDataService.QueryGenericOption("OptionName", category: "OtherCategory").Result;
            Assert.Equal("OptionValue_0", optionValue.OptionValue);

            optionValue = genericOptionDataService.QueryGenericOption("OptionName", "HackSystem", "GenericCategory").Result;
            Assert.Equal("OptionValue_2", optionValue.OptionValue);

            optionValue = genericOptionDataService.QueryGenericOption("OptionName", "OtherSystem", "GenericCategory").Result;
            Assert.Equal("OptionValue_3", optionValue.OptionValue);

            optionValue = genericOptionDataService.QueryGenericOption("OptionName", "HackSystem", "OtherCategory").Result;
            Assert.Equal("OptionValue_1", optionValue.OptionValue);

            optionValue = genericOptionDataService.QueryGenericOption("OptionName", "OtherSystem", "OtherCategory").Result;
            Assert.Equal("OptionValue_0", optionValue.OptionValue);
        }
    }
}