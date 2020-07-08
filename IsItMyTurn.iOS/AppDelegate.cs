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
using IsItMyTurn.Models;
using Security;

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
            string uniqueId = GetUniqueId();
            var FCMToken = Xamarin.Forms.Application.Current.Properties.Keys.Contains("Fcmtoken");
            if (FCMToken)
            {
                string checkToken = Xamarin.Forms.Application.Current.Properties["Fcmtoken"].ToString();
                if (checkToken != "")
                {
                    if (Xamarin.Forms.Application.Current.Properties["Fcmtoken"].ToString() == fcmToken)
                    {
                        System.Diagnostics.Debug.WriteLine($"######Token######  :  {fcmToken}");
                    }
                    else
                    {
                        string oldToken = Xamarin.Forms.Application.Current.Properties["Fcmtoken"].ToString();
                        var successResponse = await DeviceInfoToDatabase(uniqueId, oldToken, fcmToken);
                        if (successResponse)
                        {
                            Xamarin.Forms.Application.Current.Properties["Fcmtoken"] = Messaging.SharedInstance.FcmToken ?? "";
                            await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                            System.Diagnostics.Debug.WriteLine($"######Token######  :  {fcmToken}");
                        }
                    }
                }
                else
                {
                    string oldToken = "";
                    var successResponse = await DeviceInfoToDatabase(uniqueId, oldToken, fcmToken);
                    if (successResponse)
                    {
                        Xamarin.Forms.Application.Current.Properties["Fcmtoken"] = Messaging.SharedInstance.FcmToken;
                        await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                        System.Diagnostics.Debug.WriteLine($"######Token######  :  {fcmToken}");
                    }
                }
            }
            else
            {
                string oldToken = "";
                var successResponse = await DeviceInfoToDatabase(uniqueId, oldToken, fcmToken);
                if (successResponse)
                {
                    Xamarin.Forms.Application.Current.Properties["Fcmtoken"] = Messaging.SharedInstance.FcmToken;
                    await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                    System.Diagnostics.Debug.WriteLine($"######Token######  :  {fcmToken}");
                }
            }
        }

        public async Task<bool> DeviceInfoToDatabase(string uniqueId, string oldToken, string newToken)
        {
            Identifier fcmToken = new Identifier()
            {
                DeviceId = uniqueId,
                OldToken = oldToken,
                NewToken = newToken
            };

            string json = JsonConvert.SerializeObject(fcmToken);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("https://isitmyturnapi.azurewebsites.net/api/fcmtoken", content);
            return response.IsSuccessStatusCode;
        }

        public string GetUniqueId()
        {
            var query = new SecRecord(SecKind.GenericPassword);
            query.Service = NSBundle.MainBundle.BundleIdentifier;
            query.Account = "UniqueID";

            NSData uniqueId = SecKeyChain.QueryAsData(query);

            if (uniqueId != null)
            {
                return uniqueId.ToString();
            }
            else
            {
                query.ValueData = NSData.FromString(System.Guid.NewGuid().ToString());
                var err = SecKeyChain.Add(query);
                if (err != SecStatusCode.Success && err != SecStatusCode.DuplicateItem)
                    throw new Exception("Cannot store Unique ID");

                return query.ValueData.ToString();
            }
        }
    }
}
