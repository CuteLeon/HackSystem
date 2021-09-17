using HackSystem.WebAPI.DataAccess;
using HackSystem.WebAPI.MockServers.Configurations;
using HackSystem.WebAPI.MockServers.Extensions;
using HackSystem.WebAPI.Model.Mock;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HackSystem.WebAPI.MockServers.DataServices.Tests;

public class MockRouteDataServiceTests
{
    [Fact()]
    public void QueryMockRouteTest()
    {
        var mockRoutes = new List<MockRouteDetail>()
        {
            new MockRouteDetail() { Enabled = true, MockURI = "/Mock", ResponseBodyTemplate = "URI_Only" },
            new MockRouteDetail() { Enabled = true, MockURI = "/Mock", MockMethod = "Get", ResponseBodyTemplate = "URI_Get_Only" },
            new MockRouteDetail() { Enabled = true, MockURI = "/Mock", MockMethod = "Get", MockSourceHost = "localhost", ResponseBodyTemplate = "URI_Get_localhost_Only" },
            new MockRouteDetail() { Enabled = true, MockURI = "/Mock", MockMethod = "Get", MockSourceHost = "192.168.", ResponseBodyTemplate = "URI_Get_192_168_Only" },
            new MockRouteDetail() { Enabled = true, MockURI = "/Mock", MockMethod = "Get", MockSourceHost = "192.168.10.200", ResponseBodyTemplate = "URI_Get_192_168_10_200_Only" },
            new MockRouteDetail() { Enabled = true, MockURI = "/Mock", MockMethod = "Get", MockSourceHost = "192.168.20.", ResponseBodyTemplate = "URI_Get_192_168_20_Only" },
            new MockRouteDetail() { Enabled = true, MockURI = "/Mock", MockMethod = "Get", MockSourceHost = "192.168.20.255", ResponseBodyTemplate = "URI_Get_192_168_20_255_Only" },
            new MockRouteDetail() { Enabled = true, MockURI = "/Mock", MockMethod = "Post", ResponseBodyTemplate = "URI_Post_Only" },
            new MockRouteDetail() { Enabled = true, MockURI = "/Mock", MockMethod = "Post", MockSourceHost = "local", ResponseBodyTemplate = "URI_Post_local_Only" },
            new MockRouteDetail() { Enabled = true, MockURI = "/Mock", MockMethod = "Post", MockSourceHost = "loc", ResponseBodyTemplate = "URI_Post_loc_Only" },
            new MockRouteDetail() { Enabled = true, MockURI = "/Mock", MockMethod = "Post", MockSourceHost = "localho", ResponseBodyTemplate = "URI_Post_localho_Only" },
        };

        var serviceCollection = new ServiceCollection()
            .AddLogging()
            .AddMemoryCache()
            .AddDbContext<HackSystemDBContext>(options => options
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseLazyLoadingProxies())
            .AttachMockServer(new MockServerOptions());
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var dbContext = serviceProvider.GetRequiredService<HackSystemDBContext>();
        dbContext.AddRange(mockRoutes);
        dbContext.SaveChanges();

        var count = dbContext.Set<MockRouteDetail>().Count();
        Assert.Equal(mockRoutes.Count, count);
        var mockRouteDataService = serviceProvider.GetRequiredService<IMockRouteDataService>();

        Assert.ThrowsAsync<ArgumentNullException>(() => mockRouteDataService.QueryMockRoute(string.Empty, "Get", "localhost"));
        Assert.ThrowsAsync<ArgumentNullException>(() => mockRouteDataService.QueryMockRoute("/Mock", string.Empty, "localhost"));
        Assert.ThrowsAsync<ArgumentNullException>(() => mockRouteDataService.QueryMockRoute("/Mock", "Get", string.Empty));

        var mockRoute = mockRouteDataService.QueryMockRoute("Mock", "Get", "otherHost").Result;
        Assert.Equal("URI_Get_Only", mockRoute.ResponseBodyTemplate);
        mockRoute = mockRouteDataService.QueryMockRoute("/Mock", "Get", "otherHost").Result;
        Assert.Equal("URI_Get_Only", mockRoute.ResponseBodyTemplate);
        mockRoute = mockRouteDataService.QueryMockRoute("Mock/", "Get", "otherHost").Result;
        Assert.Equal("URI_Get_Only", mockRoute.ResponseBodyTemplate);
        mockRoute = mockRouteDataService.QueryMockRoute("/Mock/", "Get", "localhost").Result;
        Assert.Equal("URI_Get_localhost_Only", mockRoute.ResponseBodyTemplate);
        mockRoute = mockRouteDataService.QueryMockRoute("/Mock/", "Get", "192.168.10.100").Result;
        Assert.Equal("URI_Get_192_168_Only", mockRoute.ResponseBodyTemplate);
        mockRoute = mockRouteDataService.QueryMockRoute("/Mock/", "Get", "192.168.20.100").Result;
        Assert.Equal("URI_Get_192_168_20_Only", mockRoute.ResponseBodyTemplate);
        mockRoute = mockRouteDataService.QueryMockRoute("/Mock/", "Get", "192.168.20.255").Result;
        Assert.Equal("URI_Get_192_168_20_255_Only", mockRoute.ResponseBodyTemplate);

        mockRoute = mockRouteDataService.QueryMockRoute("/Mock/", "Post", "otherHost").Result;
        Assert.Equal("URI_Post_Only", mockRoute.ResponseBodyTemplate);
        mockRoute = mockRouteDataService.QueryMockRoute("/Mock/", "Post", "localhost").Result;
        Assert.Equal("URI_Post_localho_Only", mockRoute.ResponseBodyTemplate);
        mockRoute = mockRouteDataService.QueryMockRoute("/Mock/", "Post", "192.168.10.100").Result;
        Assert.Equal("URI_Post_Only", mockRoute.ResponseBodyTemplate);

        mockRoute = mockRouteDataService.QueryMockRoute("/Mock/", "Put", "otherHost").Result;
        Assert.Equal("URI_Only", mockRoute.ResponseBodyTemplate);
        mockRoute = mockRouteDataService.QueryMockRoute("/Mock/", "Put", "localhost").Result;
        Assert.Equal("URI_Only", mockRoute.ResponseBodyTemplate);
        mockRoute = mockRouteDataService.QueryMockRoute("/Mock/", "Put", "192.168.10.100").Result;
        Assert.Equal("URI_Only", mockRoute.ResponseBodyTemplate);

        mockRoute = mockRouteDataService.QueryMockRoute("/NotFound", "Get", "localhost").Result;
        Assert.Null(mockRoute);
        mockRoute = mockRouteDataService.QueryMockRoute("/NotFound", "Put", "localhost").Result;
        Assert.Null(mockRoute);
        mockRoute = mockRouteDataService.QueryMockRoute("/NotFound", "Post", "localhost").Result;
        Assert.Null(mockRoute);
    }
}
