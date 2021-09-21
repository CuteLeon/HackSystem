using HackSystem.WebAPI.Domain.Entity.Identity;

namespace HackSystem.WebAPI.Application.Behaviors;

public interface IAccountCreatedNotificationHandler
{
    Task InitialUser(HackSystemUser user);
}
