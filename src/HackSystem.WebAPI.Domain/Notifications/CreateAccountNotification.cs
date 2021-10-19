using HackSystem.Intermediary.Domain;
using HackSystem.WebAPI.Domain.Entity.Identity;

namespace HackSystem.WebAPI.Domain.Notifications
{
    public class CreateAccountNotification : IIntermediaryNotification
    {
        public HackSystemUser User { get; set; }
    }
}
