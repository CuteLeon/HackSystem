using HackSystem.WebAPI.Infrastructure.DBContexts;
using HackSystem.WebAPI.MockServer.Application.Repository;
using HackSystem.WebAPI.MockServer.Domain.Configurations;
using HackSystem.WebAPI.MockServer.Domain.Entity;
using HackSystem.WebAPI.MockServer.Extensions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace HackSystem.WebAPI.MockServer.DataServices.Tests;

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
        mockRoutes.ForEach(route =>
        {
            route.RouteName = "Test Route";
            route.ForwardRequestBodyTemplate = String.Empty;
            route.ForwardMethod = String.Empty;
            route.ForwardAddress = String.Empty;
        });
        var serviceCollection = new ServiceCollection()
            .AddLogging()
            .AddMemoryCache()
            .AddDbContext<HackSystemDbContext>(options => options
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseLazyLoadingProxies())
            .AttachMockServer(new MockServerOptions());
        var serviceProvider = serviceCollection.BuildServiceProvider();

        var dbContext = serviceProvider.GetRequiredService<HackSystemDbContext>();
        dbContext.AddRange(mockRoutes);
        dbContext.SaveChanges();

        var count = dbContext.Set<MockRouteDetail>().Count();
        Assert.Equal(mockRoutes.Count, count);
        var mockRouteDataService = serviceProvider.GetRequiredService<IMockRouteRepository>();

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
