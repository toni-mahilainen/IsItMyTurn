using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using IsItMyTurn.Models;
using Newtonsoft.Json;

namespace IsItMyTurn.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class FirebaseInstanceIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";

        // [START refresh_token]
        public async override void OnTokenRefresh()
        {

            // Get updated InstanceID token.
            var fcmToken = FirebaseInstanceId.Instance.Token;
            if (fcmToken != null)
            {
                string oldToken = "";
                var successResponse = await TokenToDatabase(oldToken, fcmToken);
                if (successResponse)
                {
                    Android.Util.Log.Debug(TAG, "Refreshed token: " + fcmToken);
                    System.Diagnostics.Debug.WriteLine($"######Token######  :  {fcmToken}");
                    Xamarin.Forms.Application.Current.Properties["Fcmtoken"] = fcmToken;
                    await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                    System.Diagnostics.Debug.WriteLine($"######Token######  :  {fcmToken}");
                }
            }
        }
        // [END refresh_token] 

        public async Task<bool> TokenToDatabase(string oldToken, string newToken)
        {
            FcmToken fcmToken = new FcmToken()
            {
                OldToken = oldToken,
                NewToken = newToken
            };

            string json = JsonConvert.SerializeObject(fcmToken);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("https://isitmyturnapi.azurewebsites.net/api/fcmtoken", content);
            return response.IsSuccessStatusCode;
        }
    }
}