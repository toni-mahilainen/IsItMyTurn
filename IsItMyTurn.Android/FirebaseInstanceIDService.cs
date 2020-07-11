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
using Android.Telephony;
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
        // [START refresh_token]
        public async override void OnTokenRefresh()
        {

            // Get device ID
            var uniqueId = Android.Provider.Settings.Secure.GetString(Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            // Get it hashed
            var hashedId = GetSha256HashForId(uniqueId);
            // Get updated registration token.
            var fcmToken = FirebaseInstanceId.Instance.Token;

            if (fcmToken != null)
            {
                // A token and a hashed device ID to database
                var successResponse = await DeviceInfoToDatabase(hashedId, fcmToken);
                if (successResponse)
                {
                    // If succeeded, a token will be saved to properties with key "Fcmtoken"
                    System.Diagnostics.Debug.WriteLine($"######Token######  :  {fcmToken}");
                    Xamarin.Forms.Application.Current.Properties["Fcmtoken"] = fcmToken;
                    await Xamarin.Forms.Application.Current.SavePropertiesAsync();
                }
            }
        }
        // [END refresh_token] 

        private async Task<bool> DeviceInfoToDatabase(string uniqueId, string token)
        {
            Identifier fcmToken = new Identifier()
            {
                UniqueIdentifier = uniqueId,
                Token = token
            };

            string json = JsonConvert.SerializeObject(fcmToken);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.PostAsync("https://isitmyturnapi.azurewebsites.net/api/fcmtoken", content);
            return response.IsSuccessStatusCode;
        }

        private static string GetSha256HashForId(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            var sha = new System.Security.Cryptography.SHA256Managed();
            byte[] textData = Encoding.UTF8.GetBytes(text);
            byte[] hash = sha.ComputeHash(textData);
            return BitConverter.ToString(hash).Replace("-", string.Empty);
        }
    }
}