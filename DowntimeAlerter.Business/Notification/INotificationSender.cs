using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DowntimeAlerter.Business.Notification
{
    public interface INotificationSender
    {
        Task SendNotificationAsync(string to, string subject, string html);
    }
}
