using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using UserNotifications;

namespace IsItMyTurn.iOS
{
    public class UserNotificationCenterDelegate : UNUserNotificationCenterDelegate
    {
        public UserNotificationCenterDelegate()
        {
            UNUserNotificationCenter.Current.Delegate = this;
        }
        public override void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            // Do something with the notification
            Console.WriteLine("Active Notification: {0}", notification);
            // Tell system to display the notification anyway or use
            // `None` to say we have handled the display locally.
            completionHandler.DynamicInvoke(UNNotificationPresentationOptions.Alert);
        }
    }
}