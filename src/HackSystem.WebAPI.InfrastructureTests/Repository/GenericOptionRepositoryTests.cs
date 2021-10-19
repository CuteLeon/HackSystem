using HackSystem.WebAPI.Application.Repository;
using HackSystem.WebAPI.Domain.Entity;
using HackSystem.WebAPI.Infrastructure.DBContexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace HackSystem.WebAPI.Infrastructure.Repository.Tests;

public class GenericOptionRepositoryTests
{
    private HackSystemDbContext GetDBContext()
    {
        var genericOptions = new List<GenericOption>()
        {
            new GenericOption() { OptionName = "OptionName", OptionValue = "OptionValue_0", OwnerLevel = "", Category = ""  },
            new GenericOption() { OptionName = "OptionName", OptionValue = "OptionValue_1", OwnerLevel = "HackSystem", Category = "" },
            new GenericOption() { OptionName = "OptionName", OptionValue = "OptionValue_2", OwnerLevel = "HackSystem", Category = "GenericCategory" },
            new GenericOption() { OptionName = "OptionName", OptionValue = "OptionValue_3", OwnerLevel = "", Category = "GenericCategory" },
        };
        var dbContext = new HackSystemDbContext(
            new DbContextOptionsBuilder<HackSystemDbContext>()
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
        var logger = new Mock<ILogger<GenericOptionRepository>>();
        logger.Setup(l => l.Log(It.IsAny<LogLevel>(), It.IsAny<EventId>(), It.IsAny<object>(), It.IsAny<Exception?>(), It.IsAny<Func<object, Exception?, string>>())).Verifiable();
        IGenericOptionRepository genericOptionDataService = new GenericOptionRepository(logger.Object, this.GetDBContext());

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
