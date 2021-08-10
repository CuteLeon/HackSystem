using System;
using System.Collections.Generic;
using System.Linq;
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
                new GenericOption() { OptionName = "OptionName", OptionValue = "OptionValue" },
                new GenericOption() { OwnerLevel = "HackSystem", OptionName = "OptionName", OptionValue = "HackSystemOptionValue" },
                new GenericOption() { OwnerLevel = "HackSystem", Category = "Generic", OptionName = "OptionName", OptionValue = "HackSystemGenericOptionValue" },
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
        public void QueryGenericOptionsByNameTest()
        {
            var logger = new Mock<ILogger<GenericOptionDataService>>();
            logger.Setup(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception?>(), It.IsAny<Func<object, Exception?, string>>())).Verifiable();
            IGenericOptionDataService genericOptionDataService = new GenericOptionDataService(logger.Object, GetDBContext());

            var optionValue = genericOptionDataService.QueryGenericOptions("OptionName").Result;
            Assert.Equal("OptionValue", optionValue.OptionValue);
        }

        [Fact()]
        public void QueryGenericOptionsByOwnerAndCategoryAndNameTest()
        {
            var logger = new Mock<ILogger<GenericOptionDataService>>();
            logger.Setup(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception?>(), It.IsAny<Func<object, Exception?, string>>())).Verifiable();
            IGenericOptionDataService genericOptionDataService = new GenericOptionDataService(logger.Object, GetDBContext());

            var optionValue = genericOptionDataService.QueryGenericOptions("HackSystem", "Generic", "OptionName").Result;
            Assert.Equal("HackSystemGenericOptionValue", optionValue.OptionValue);
        }

        [Fact()]
        public void QueryGenericOptionsByOwnerAndNameTest()
        {
            var logger = new Mock<ILogger<GenericOptionDataService>>();
            logger.Setup(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception?>(), It.IsAny<Func<object, Exception?, string>>())).Verifiable();
            IGenericOptionDataService genericOptionDataService = new GenericOptionDataService(logger.Object, GetDBContext());

            var optionValue = genericOptionDataService.QueryGenericOptions("HackSystem", "OptionName").Result;
            Assert.Equal("HackSystemOptionValue", optionValue.OptionValue);
        }
    }
}