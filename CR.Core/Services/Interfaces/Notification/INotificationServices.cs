using CR.Common.DTOs;
using CR.Core.DTOs.Notification;

namespace CR.Core.Services.Interfaces.Notification
{
    public interface INotificationServices
    {
        ResultDto<NotificationDTO> GetAllNotificationForAdminPanel();

    }
}
