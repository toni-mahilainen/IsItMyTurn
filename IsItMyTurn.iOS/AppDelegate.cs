using System;
using System.Collections.Generic;
using System.Linq;
using Firebase.CloudMessaging;
using UserNotifications;
using Foundation;
using UIKit;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;

namespace IsItMyTurn.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        public void DidRefreshRegistrationToken(Messaging messaging, string fcmToken)
        {
            System.Diagnostics.Debug.WriteLine($"FCM Token: {fcmToken}");
        }

        //This method is invoked when the application has loaded and is ready to run.In this
        //method you should instantiate the window, load the UI into it and then make the window
        //visible.
        //You have 17 seconds to return from this method, or iOS will terminate your application.
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Xamarin.Forms.Forms.Init();
            Firebase.Core.App.Configure();
            RegisterForRemoteNotifications();
            LoadApplication(new App());
            Messaging.SharedInstance.Delegate = this;
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                var pushSettings = UIUserNotificationSettings.GetSettingsForTypes(
                                   UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound,
                                   new NSSet());

                UIApplication.SharedApplication.RegisterUserNotificationSettings(pushSettings);
                UIApplication.SharedApplication.RegisterForRemoteNotifications();
            }
            else
            {
                UIRemoteNotificationType notificationTypes = UIRemoteNotificationType.Alert | UIRemoteNotificationType.Badge | UIRemoteNotificationType.Sound;
                UIApplication.SharedApplication.RegisterForRemoteNotificationTypes(notificationTypes);
            }
            
            if (UNUserNotificationCenter.Current != null)
            {
                UNUserNotificationCenter.Current.Delegate = new UserNotificationCenterDelegate();
            }
            return base.FinishedLaunching(app, options);
        }

        private void RegisterForRemoteNotifications()
        {
            // Register your app for remote notifications.
            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                // For iOS 10 display notification (sent via APNS)
                UNUserNotificationCenter.Current.Delegate = this;
                var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
                UNUserNotificationCenter.Current.RequestAuthorization(authOptions, async (granted, error) =>
                {
                    Console.WriteLine(granted);
                    await System.Threading.Tasks.Task.Delay(500);
                });
            }
            else
            {
                // iOS 9 or before
                var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
                var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
                UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            }
            UIApplication.SharedApplication.RegisterForRemoteNotifications();
        }

        // Override Methods:
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            Messaging.SharedInstance.ApnsToken = deviceToken;
        }

        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            base.FailedToRegisterForRemoteNotifications(application, error);
        }

        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            completionHandler(UIBackgroundFetchResult.NewData);
        }

        [Export("messaging:didReceiveRegistrationToken:")]
        public async void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        {
            //var FCMToken = Xamarin.Forms.Application.Current.Properties.Keys.Contains("Fcmtocken");
            bool notificationSettings = GetApplicationNotificationSettings();
            if (notificationSettings)
            {
                //var FCMTockenValue = Xamarin.Forms.Application.Current.Properties["Fcmtocken"].ToString();
                //if (FCMTockenValue == fcmToken)
                //{
                //    // Do nothing
                //}
                //else
                //{
                //    Xamarin.Forms.Application.Current.Properties["Fcmtocken"] = fcmToken;
                //    Xamarin.Forms.Application.Current.SavePropertiesAsync();
                //}
                var FCMToken = Xamarin.Forms.Application.Current.Properties.Keys.Contains("Fcmtoken");
                // If token exists, it is without enabled notifications. It will be removed.
                if (FCMToken)
                {
                    Xamarin.Forms.Application.Current.Properties.Remove("Fcmtoken");
                }
                Xamarin.Forms.Application.Current.Properties["Fcmtoken"] = Messaging.SharedInstance.FcmToken ?? "";
                var successResponse = await TokenToDatabase(fcmToken);
                if (successResponse)
                {
                    await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                    System.Diagnostics.Debug.WriteLine($"######Token######  :  {fcmToken}");
                }
            }
            else
            {
                // FCM Token got here
                Xamarin.Forms.Application.Current.Properties["Fcmtoken"] = Messaging.SharedInstance.FcmToken ?? "";
                await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                System.Diagnostics.Debug.WriteLine($"######Token######  :  {fcmToken}");
                //Console.WriteLine(fcmToken);
            }
        }

        public async Task<bool> TokenToDatabase(string token)
        {
            StringContent content = new StringContent(token, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("https://isitmyturnapi.azurewebsites.net/api/completedshift", content);
            return response.IsSuccessStatusCode;
        }

        // Check if notification is enabled
        public bool GetApplicationNotificationSettings()
        {
            var settings = UIApplication.SharedApplication.CurrentUserNotificationSettings.Types;
            return settings != UIUserNotificationType.None;
        }
    }
}
