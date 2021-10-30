using HackSystem.WebAPI.MockServer.Domain.Entity;
using HackSystem.DataTransferObjects.MockServer;

namespace HackSystem.WebAPI.Mappers.TaskServer;

public class MockServerMapperProfile : Profile
{
    public MockServerMapperProfile()
    {
        this.CreateMap<MockRouteDetail, MockRouteResponse>();
    }
}
