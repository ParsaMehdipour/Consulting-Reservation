using CR.Core.Services.Interfaces.Notification;
using Microsoft.AspNetCore.Mvc;

namespace CR.Presentation.Areas.AdminPanel.Controllers.Api
{
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationServices _notificationServices;

        public NotificationController(INotificationServices notificationServices)
        {
            _notificationServices = notificationServices;
        }

        [Route("/api/Notification/GetNotification")]
        [HttpPost]
        public IActionResult GetNotification()
        {
            var result = _notificationServices.GetAllNotificationForAdminPanel();

            return new JsonResult(result);
        }
    }
}